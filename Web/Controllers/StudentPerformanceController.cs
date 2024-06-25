using Entities.EntityModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Services.DataService.Interfaces;
using Services.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers.Interfaces;

namespace Web.Controllers
{
    public class StudentPerformanceController : Controller
    {
        #region "Declared objects & Constructor"
        private readonly ProjectDbContext _db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICookieHelperService _cookieHelperService;
        private readonly ICredentialManagerService _credentialManager;
        private readonly IDisplayMessageHelper _displayMessageHelper;
        private readonly ICryptoHelperService _cryptoHelperService;
        private readonly IDateTimeHelperService _dateTimeHelperService;
        private readonly IFileUploadHelperService _fileUploadHelperService;

        public StudentPerformanceController(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, ICookieHelperService _cookieHelperService, ICredentialManagerService _credentialManager, IDisplayMessageHelper _displayMessageHelper, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService, IFileUploadHelperService _fileUploadHelperService)
        {
            this._db = _db;
            this.httpContextAccessor = httpContextAccessor;
            this._cookieHelperService = _cookieHelperService;
            this._credentialManager = _credentialManager;
            this._displayMessageHelper = _displayMessageHelper;
            this._cryptoHelperService = _cryptoHelperService;
            this._dateTimeHelperService = _dateTimeHelperService;
            this._fileUploadHelperService = _fileUploadHelperService;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var departments = Enumerable.Empty<VmSelectList>();
            try
            {
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
            }
            catch (Exception ex)
            {
                departments = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            await Task.Delay(0);
            return View();
        }

        public async Task<JsonResult> GetStudentAllCoursesPerformanceByID(int departmentId, int semesterId, int studentId)
        {
            var deptSemesterCoursesPerformance = new VmStudentsSemesterCoursesAndPerformance();
            try
            {
                if (departmentId > 0 && semesterId > 0 && studentId > 0)
                {
                    deptSemesterCoursesPerformance.StudentInfo = _db.Students
                        .Where(w => w.StudentId == studentId)
                        .Select(s => new VmStudentCustomData
                        {
                            StudentId = s.StudentId
                        }).FirstOrDefault();
                    deptSemesterCoursesPerformance.AllMarkingCriterias = await _db.MarkingCriterias
                       .Where(w => w.MarkingCriteriaId > 0)
                       .OrderBy(o => o.MarkingCriteriaId)
                       .Select(s => new VmMarkingCriteria
                       {
                           MarkingCriteriaId = s.MarkingCriteriaId,
                           MarkingCriteriaName = s.MarkingCriteriaName,
                           MinimumValue = s.MinimumValue,
                           MaximumValue = s.MaximumValue,
                           PassingValue = s.PassingValue,
                           MarkingCriteriaDetails = s.Details
                       }).ToListAsync();
                    deptSemesterCoursesPerformance.AllMarkingBadges = await _db.MarkingBadges
                        .Where(w => w.MarkingBadgeId > 0)
                        .Select(s => new VmMarkingBadge
                        {
                            MarkingBadgeId = s.MarkingBadgeId,
                            MarkingBadgeName = s.MarkingBadgeName,
                            MarkingBadgeColor = s.BadgeColorCode,
                            MarkingBadgeDetails = s.Details
                        }).ToListAsync();
                    deptSemesterCoursesPerformance.AllCoursesWithPerformance = _db.Courses
                        .Where(w => w.DepartmentId == departmentId && w.SemesterId == semesterId).ToList()
                        .Select(s => new VmDepartmentSemesterCourses
                        {
                            CourseId = s.CourseId,
                            CourseName = s.CourseName,
                            CourseCode = s.CourseCode,
                            CourseDetails = s.Details,
                            CoursePerformance = _db.StudentCourseMarks
                                             .Where(ww => ww.CourseId == s.CourseId && ww.StudentId == studentId)
                                             .OrderBy(ob => ob.MarkingCriteriaId)
                                             .Include("MarkingCriteriaInfo").ToList()
                                             .Select(ss => new VmStudentCoursePerformance
                                             {
                                                 CriteriaInfo = new VmMarkingCriteria
                                                 {
                                                     MarkingCriteriaId = ss.MarkingCriteriaInfo.MarkingCriteriaId,
                                                     MarkingCriteriaName = ss.MarkingCriteriaInfo.MarkingCriteriaName,
                                                     MinimumValue = ss.MarkingCriteriaInfo.MinimumValue,
                                                     MaximumValue = ss.MarkingCriteriaInfo.MaximumValue,
                                                     PassingValue = ss.MarkingCriteriaInfo.PassingValue,
                                                     MarkingCriteriaDetails = ss.MarkingCriteriaInfo.Details,
                                                     AchievedMarks = ss.CourseCriteriaMarks
                                                 },
                                                 BadgeInfo = GetMarkingBadge(ss.MarkingCriteriaId, ss.CourseCriteriaMarks)
                                             }).ToList()
                        }).ToList();
                }
            }
            catch (Exception ex)
            { deptSemesterCoursesPerformance = new VmStudentsSemesterCoursesAndPerformance(); }
            return Json(deptSemesterCoursesPerformance);
        }

        private VmMarkingBadge GetMarkingBadge(int markingCriteriaId, decimal courseCriteriaMarks)
        {
            var markingBadge = new VmMarkingBadge();
            try
            {
                if (markingCriteriaId > 0 && courseCriteriaMarks >= 0)
                {
                    var markingBadges = _db.MarkingCriteriasBadges.Where(w => w.MarkingCriteriaId == markingCriteriaId).Include("MarkingBadge").ToList();
                    if (markingBadges.Any())
                    {
                        foreach (var badgeInfo in markingBadges)
                        {
                            //return decimal.Round(rate, 2) == rate;
                            if (badgeInfo != null && courseCriteriaMarks >= badgeInfo.MinimumValue && courseCriteriaMarks <= badgeInfo.MaximumValue)
                            {
                                markingBadge = new VmMarkingBadge
                                {
                                    MarkingBadgeId = badgeInfo.MarkingBadge.MarkingBadgeId,
                                    MarkingBadgeName = badgeInfo.MarkingBadge.MarkingBadgeName,
                                    MarkingBadgeColor = badgeInfo.MarkingBadge.BadgeColorCode,
                                    MarkingBadgeDetails = badgeInfo.MarkingBadge.Details
                                };
                                return markingBadge;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { markingBadge = new VmMarkingBadge(); }
            return markingBadge;
        }

    }
}
