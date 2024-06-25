using Entities.EntityModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Repository.Context;
using Services.DataService.Interfaces;
using Services.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Constants;
using Web.Helpers.Interfaces;

namespace Web.Controllers
{
    public class StudentMarksController : Controller
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

        public StudentMarksController(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, ICookieHelperService _cookieHelperService, ICredentialManagerService _credentialManager, IDisplayMessageHelper _displayMessageHelper, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService, IFileUploadHelperService _fileUploadHelperService)
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

        #region "Single Student Marks Related Methods"
        public async Task<IActionResult> MarkEntry()
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

        public async Task<JsonResult> GetStudentAllCoursesMarksByID(int departmentId, int semesterId, int studentId)
        {
            var semesterCoursesMarks = new VmStudentsSemesterCoursesAndMarks();
            try
            {
                if (departmentId > 0 && semesterId > 0 && studentId > 0)
                {
                    semesterCoursesMarks.DepartmentId = departmentId;
                    semesterCoursesMarks.SemesterId = semesterId;
                    semesterCoursesMarks.StudentId = studentId;
                    semesterCoursesMarks.AllMarkingCriterias = await _db.MarkingCriterias
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
                    semesterCoursesMarks.AllCoursesWithMarks = _db.Courses
                        .Where(w => w.DepartmentId == departmentId && w.SemesterId == semesterId).ToList()
                        .Select(s => new VmSemesterCourses
                        {
                            CourseId = s.CourseId,
                            CourseName = s.CourseName,
                            CourseCode = s.CourseCode,
                            CourseDetails = s.Details,
                            CourseMarks = _db.StudentCourseMarks
                                             .Where(ww => ww.CourseId == s.CourseId && ww.StudentId == studentId)
                                             .OrderBy(ob => ob.MarkingCriteriaId)
                                             .Include("MarkingCriteriaInfo").ToList()
                                             .Select(ss => new VmStudentCourseMarks
                                             {
                                                 MarkingCriteriaId = ss.MarkingCriteriaId,
                                                 CriteriaInfo = new VmMarkingCriteria
                                                 {
                                                     MarkingCriteriaId = ss.MarkingCriteriaInfo.MarkingCriteriaId,
                                                     MarkingCriteriaName = ss.MarkingCriteriaInfo.MarkingCriteriaName,
                                                     MinimumValue = ss.MarkingCriteriaInfo.MinimumValue,
                                                     MaximumValue = ss.MarkingCriteriaInfo.MaximumValue,
                                                     PassingValue = ss.MarkingCriteriaInfo.PassingValue,
                                                     MarkingCriteriaDetails = ss.MarkingCriteriaInfo.Details,
                                                 },
                                                 CourseCriteriaMarks = Convert.ToDecimal(String.Format("{0:0.00}", ss.CourseCriteriaMarks))
                                             }).ToList()
                        }).ToList();
                }
            }
            catch (Exception ex)
            { semesterCoursesMarks = new VmStudentsSemesterCoursesAndMarks(); }
            return Json(semesterCoursesMarks);
        }

        [HttpPost]
        public async Task<IActionResult> StudentMarksAddOrUpdate(VmStudentMarksEntry model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var courseIDs = _db.Courses.Where(w => w.DepartmentId == model.DepartmentId && w.SemesterId == model.SemesterId)
                        .Select(s => s.CourseId).ToList();
                    var oldMarksData = _db.StudentCourseMarks.Where(w => w.StudentId == model.StudentId && courseIDs.Contains(w.CourseId))
                        .ToList();
                    if (oldMarksData.Any())
                    {
                        #region "Remove Unmatched Data"
                        var newMarkingCriteriaIDs = new List<int>();
                        var newCourseIDs = model.StudentCourses != null && model.StudentCourses.Any() ? model.StudentCourses.Select(s => s.CourseId).ToList() : new List<int>();
                        foreach (var course in model.StudentCourses)
                        {
                            if (course != null && course.CourseMarks.Any())
                            {
                                foreach (var mark in course.CourseMarks)
                                {
                                    newCourseIDs.Add(mark != null ? mark.MarkingCriteriaId : 0);
                                }
                            }
                        }
                        //Fetch Previous Unmatched Data
                        var oldUnmatchedMarksData = _db.StudentCourseMarks
                            .Where(w => w.StudentId == model.StudentId && !courseIDs.Contains(w.CourseId) && !newMarkingCriteriaIDs.Contains(w.MarkingCriteriaId))
                            .ToList();
                        if (oldUnmatchedMarksData.Any())
                        {
                            _db.StudentCourseMarks.RemoveRange(oldUnmatchedMarksData);
                            await _db.SaveChangesAsync();
                        }
                        #endregion
                        #region "Insert/Update New Provided Marks Data"
                        var studentNewMarks = new List<StudentCourseMarks>();
                        if (model.StudentCourses != null && model.StudentCourses.Any())
                        {
                            foreach (var course in model.StudentCourses)
                            {
                                if (course != null && course.CourseMarks.Any())
                                {
                                    foreach (var mark in course.CourseMarks)
                                    {
                                        if (mark != null)
                                        {
                                            var oldMarks = _db.StudentCourseMarks.FirstOrDefault(w => w.StudentId == model.StudentId && w.CourseId == course.CourseId && w.MarkingCriteriaId == mark.MarkingCriteriaId);
                                            if (oldMarks != null)
                                            {
                                                oldMarks.UpdatedBy = userId;
                                                oldMarks.UpdatedDate = currentDateTime;
                                                oldMarks.CourseCriteriaMarks = mark.CourseCriteriaMarks;
                                            }
                                            else
                                            {
                                                var newData = new StudentCourseMarks
                                                {
                                                    StudentId = model.StudentId,
                                                    CourseId = course.CourseId,
                                                    MarkingCriteriaId = mark.MarkingCriteriaId,
                                                    CourseCriteriaMarks = mark.CourseCriteriaMarks,
                                                    CreatedBy = userId,
                                                    CreatedDate = currentDateTime,
                                                    UpdatedBy = userId,
                                                    UpdatedDate = currentDateTime
                                                };
                                                studentNewMarks.Add(newData);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (studentNewMarks.Any())
                        { _db.StudentCourseMarks.AddRange(studentNewMarks); }
                        #endregion
                        sucMsg = ConstantUserMessages.STUDENT_MARKS_UPDATED;
                    }
                    else
                    {
                        var studentMarks = new List<StudentCourseMarks>();
                        if (model.StudentCourses != null && model.StudentCourses.Any())
                        {
                            foreach (var course in model.StudentCourses)
                            {
                                if (course != null && course.CourseMarks.Any())
                                {
                                    foreach (var mark in course.CourseMarks)
                                    {
                                        if (mark != null)
                                        {
                                            var newData = new StudentCourseMarks
                                            {
                                                StudentId = model.StudentId,
                                                CourseId = course.CourseId,
                                                MarkingCriteriaId = mark.MarkingCriteriaId,
                                                CourseCriteriaMarks = mark.CourseCriteriaMarks,
                                                CreatedBy = userId,
                                                CreatedDate = currentDateTime,
                                                UpdatedBy = userId,
                                                UpdatedDate = currentDateTime
                                            };
                                            studentMarks.Add(newData);
                                        }
                                    }
                                }
                            }
                        }
                        if (studentMarks.Any())
                        { _db.StudentCourseMarks.AddRange(studentMarks); }
                        sucMsg = ConstantUserMessages.STUDENT_MARKS_CREATED;
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Bulk Student Marks Entry From CSV/Excel Related Methods"
        public async Task<IActionResult> MarkUpload()
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

        [HttpPost]
        public async Task<IActionResult> MarksUploadWithAllCriteria(VmStudentMarksUpload model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var extension = Path.GetExtension(model.ExcelOrCsvFile != null ? model.ExcelOrCsvFile.FileName : "test.test");
                    if (model.ExcelOrCsvFile.Length > 0 && (extension == ".csv" || extension == ".xlsx" || extension == ".xls"))
                    {
                        var studentMarks = new List<StudentCourseMarks>();
                        var currentDateTime = _dateTimeHelperService.Now();
                        var userId = _cookieHelperService.GetUserIdFromCookie();
                        var markingCriterias = _db.MarkingCriterias.OrderBy(o => o.MarkingCriteriaId).Select(s => new
                        {
                            Id = s.MarkingCriteriaId,
                            Name = s.MarkingCriteriaName
                        }).ToList();

                        using (var stream = new MemoryStream())
                        {
                            await model.ExcelOrCsvFile.CopyToAsync(stream);
                            using (var package = new ExcelPackage(stream))
                            {
                                var workSheet = package.Workbook.Worksheets[0];
                                var rowCount = workSheet.Dimension.Rows;
                                var columnCount = workSheet.Dimension.Columns;
                                for (int row = 2; row < rowCount; row++)
                                {
                                    int studentId = 0;
                                    int courseId = 0;
                                    for (int column = 1; column < columnCount; column++)
                                    {
                                        var colName = workSheet.Cells[1, column].Value;
                                        var colValue = workSheet.Cells[row, column].Value;
                                        var columnName = colName != null ? colName.ToString() : "";
                                        var columnValue = colValue != null ? colValue.ToString() : "0";
                                        if (!string.IsNullOrEmpty(columnName))
                                        {
                                            columnName = columnName.Trim().ToLower();
                                            if (columnName == "studentid")
                                            {
                                                studentId = Convert.ToInt32(columnValue.Trim());
                                            }
                                            else if (columnName == "courseid")
                                            {
                                                courseId = Convert.ToInt32(columnValue.Trim());
                                            }
                                            else if (markingCriterias.Any(f => f.Name.ToLower() == columnName))
                                            {
                                                var criteriaId = markingCriterias.FirstOrDefault(f => f.Name.ToLower() == columnName).Id;
                                                if (studentId > 0 && courseId > 0 && criteriaId > 0)
                                                {
                                                    studentMarks.Add(new StudentCourseMarks
                                                    {
                                                        StudentId = studentId,
                                                        CourseId = courseId,
                                                        MarkingCriteriaId = criteriaId,
                                                        CourseCriteriaMarks = Convert.ToDecimal(columnValue.Trim()),
                                                        CreatedBy = userId,
                                                        CreatedDate = currentDateTime,
                                                        UpdatedBy = userId,
                                                        UpdatedDate = currentDateTime
                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (studentMarks.Any())
                        {
                            var newStudentMarks = new List<StudentCourseMarks>();
                            foreach (var mark in studentMarks)
                            {
                                var existing = _db.StudentCourseMarks.FirstOrDefault(f => f.StudentId == mark.StudentId && f.CourseId == mark.CourseId && f.MarkingCriteriaId == mark.MarkingCriteriaId);
                                if (existing != null)
                                {
                                    existing.CourseCriteriaMarks = mark.CourseCriteriaMarks;
                                    existing.UpdatedBy = mark.UpdatedBy;
                                    existing.UpdatedDate = mark.UpdatedDate;
                                }
                                else { newStudentMarks.Add(mark); }
                            }
                            if (newStudentMarks.Any())
                                _db.StudentCourseMarks.AddRange(newStudentMarks);
                            await _db.SaveChangesAsync();
                            sucMsg = "Student marks has been successfully update!";
                        }
                        else
                        {
                            errMsg = "Error occurred, no student marks found!";
                        }
                    }
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Student Results Showing Related Methods"
        public async Task<IActionResult> Result()
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
        #endregion

    }//End of The Controller
}//End of The Namespace
