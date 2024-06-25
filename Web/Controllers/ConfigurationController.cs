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
using System.Text.Json;
using System.Threading.Tasks;
using Web.Constants;
using Web.Helpers.Interfaces;

namespace Web.Controllers
{
    public class ConfigurationController : Controller
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

        public ConfigurationController(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, ICookieHelperService _cookieHelperService, ICredentialManagerService _credentialManager, IDisplayMessageHelper _displayMessageHelper, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService, IFileUploadHelperService _fileUploadHelperService)
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

        #region "Department Related Methods"
        public async Task<IActionResult> Department()
        {
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxDepartments(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.Departments.AsEnumerable();
                var allUsers = await _db.AppUsers.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.DepartmentName) && w.DepartmentName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Details) && w.Details.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.DepartmentId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }

                var displayedValues = allResults.OrderByDescending(o => o.DepartmentId).Skip(param.start).Take(displayLength).ToList()
                    .Select(s => new
                    {
                        departmentId = s.DepartmentId,
                        departmentName = s.DepartmentName,
                        departmentDetails = s.Details,
                        createdBy = allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy) != null ? allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy).UserName : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.departmentId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentAddEdit(VmDepartment model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.Departments.FirstOrDefault(f => f.DepartmentId == model.DepartmentId);
                    if (existingData != null)
                    {
                        existingData.DepartmentName = model.DepartmentName;
                        existingData.Details = model.DepartmentDetails;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        sucMsg = ConstantUserMessages.DEPARTMENT_UPDATED;
                    }
                    else
                    {
                        var newDepartment = new Department
                        {
                            DepartmentName = model.DepartmentName,
                            Details = model.DepartmentDetails,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime
                        };
                        _db.Departments.Add(newDepartment);
                        sucMsg = ConstantUserMessages.DEPARTMENT_CREATED;
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.Departments.Where(f => f.DepartmentId == id).Include("Courses").Include("Students").Include("Employees").ToList().FirstOrDefault();
                    if (existingData != null)
                    {
                        if (!existingData.Courses.Any() && !existingData.Students.Any() && !existingData.Employees.Any())
                        {
                            var data = _db.Departments.FirstOrDefault(f => f.DepartmentId == id);
                            _db.Departments.Remove(data);
                            await _db.SaveChangesAsync();
                            sucMsg = ConstantUserMessages.DEPARTMENT_DELETED;
                        }
                        else
                            errMsg = "Error occurred! Can't delete, related to other data!";
                    }
                    else
                        errMsg = "Error occurred, no data found with this id!";
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, department id is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Student Batch Related Methods"
        public async Task<IActionResult> Batch()
        {
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxBatches(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.StudentBatches.AsEnumerable();
                var allUsers = await _db.AppUsers.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.BatchName) && w.BatchName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Details) && w.Details.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.BatchId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }

                var displayedValues = allResults.OrderByDescending(o => o.BatchId).Skip(param.start).Take(displayLength).ToList()
                    .Select(s => new
                    {
                        batchId = s.BatchId,
                        batchName = s.BatchName,
                        batchDetails = s.Details,
                        createdBy = allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy) != null ? allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy).UserName : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.batchId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }

        [HttpPost]
        public async Task<IActionResult> BatchAddEdit(VmStudentBatch model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.StudentBatches.FirstOrDefault(f => f.BatchId == model.BatchId);
                    if (existingData != null)
                    {
                        existingData.BatchName = model.BatchName;
                        existingData.Details = model.BatchDetails;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        sucMsg = ConstantUserMessages.BATCH_UPDATED;
                    }
                    else
                    {
                        var newData = new StudentBatch
                        {
                            BatchName = model.BatchName,
                            Details = model.BatchDetails,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime
                        };
                        _db.StudentBatches.Add(newData);
                        sucMsg = ConstantUserMessages.BATCH_CREATED;
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }

        public async Task<IActionResult> DeleteBatch(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.StudentBatches.Where(f => f.BatchId == id).Include("Students").FirstOrDefault();
                    if (existingData != null)
                    {
                        if (!existingData.Students.Any())
                        {
                            var data = _db.StudentBatches.FirstOrDefault(f => f.BatchId == id);
                            _db.StudentBatches.Remove(data);
                            await _db.SaveChangesAsync();
                            sucMsg = ConstantUserMessages.BATCH_DELETED;
                        }
                        else
                            errMsg = "Error occurred! Can't delete, related to other data!";
                    }
                    else
                        errMsg = "Error occurred, no data found with this id!";
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required id is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Academic Semester Related Methods"
        public async Task<IActionResult> Semester()
        {
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxSemesters(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.Semesters.AsEnumerable();
                var allUsers = await _db.AppUsers.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.SemesterName) && w.SemesterName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Details) && w.Details.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.SemesterId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }

                var displayedValues = allResults.OrderByDescending(o => o.SemesterId).Skip(param.start).Take(displayLength).ToList()
                    .Select(s => new
                    {
                        semesterId = s.SemesterId,
                        semesterName = s.SemesterName,
                        semesterDetails = s.Details,
                        createdBy = allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy) != null ? allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy).UserName : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.semesterId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }

        [HttpPost]
        public async Task<IActionResult> SemesterAddEdit(VmAcademicSemester model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.Semesters.FirstOrDefault(f => f.SemesterId == model.SemesterId);
                    if (existingData != null)
                    {
                        existingData.SemesterName = model.SemesterName;
                        existingData.Details = model.SemesterDetails;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        sucMsg = ConstantUserMessages.SEMESTER_UPDATED;
                    }
                    else
                    {
                        var newData = new Semester
                        {
                            SemesterName = model.SemesterName,
                            Details = model.SemesterDetails,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime
                        };
                        _db.Semesters.Add(newData);
                        sucMsg = ConstantUserMessages.SEMESTER_CREATED;
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }

        public async Task<IActionResult> DeleteSemester(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.Semesters.Where(f => f.SemesterId == id).Include("Courses").Include("Students").ToList().FirstOrDefault();
                    if (existingData != null)
                    {
                        if (!existingData.Courses.Any() && !existingData.Students.Any())
                        {
                            var data = _db.Semesters.FirstOrDefault(f => f.SemesterId == id);
                            _db.Semesters.Remove(data);
                            await _db.SaveChangesAsync();
                            sucMsg = ConstantUserMessages.SEMESTER_DELETED;
                        }
                        else
                            errMsg = "Error occurred! Can't delete, related to other data!";
                    }
                    else
                        errMsg = "Error occurred, no data found with this id!";
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required id is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Academic Courses Related Methods"
        public async Task<IActionResult> Course()
        {
            IEnumerable<VmSelectList> departments = Enumerable.Empty<VmSelectList>();
            IEnumerable<VmSelectList> semesters = Enumerable.Empty<VmSelectList>();
            try
            {
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
                semesters = _db.Semesters.ToList().Select(s => new VmSelectList { Id = s.SemesterId, Name = s.SemesterName });
            }
            catch (Exception ex)
            {
                departments = Enumerable.Empty<VmSelectList>();
                semesters = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            ViewBag.Semesters = semesters;
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxCourses(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.Courses.Where(w => w.CourseId > 0).Include("DepartmentInfo").Include("SemesterInfo").ToList();
                var allUsers = await _db.AppUsers.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.CourseName) && w.CourseName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.CourseCode) && w.CourseCode.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Details) && w.Details.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.DepartmentInfo.DepartmentName) && w.DepartmentInfo.DepartmentName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.SemesterInfo.SemesterName) && w.SemesterInfo.SemesterName.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.CourseId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }


                var displayedValues = allResults.OrderByDescending(o => o.CourseId).Skip(param.start).Take(displayLength)
                    .Select(s => new
                    {
                        courseId = s.CourseId,
                        courseName = s.CourseName,
                        courseCode = s.CourseCode,
                        departmentId = s.DepartmentId,
                        departmentName = s.DepartmentInfo != null ? s.DepartmentInfo.DepartmentName : "",
                        semesterId = s.SemesterId,
                        semesterName = s.SemesterInfo != null ? s.SemesterInfo.SemesterName : "",
                        courseDetails = s.Details,
                        createdBy = allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy) != null ? allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy).UserName : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.courseId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }

        [HttpPost]
        public async Task<IActionResult> CourseAddEdit(VmAcademicCourse model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.Courses.FirstOrDefault(f => f.CourseId == model.CourseId);
                    if (existingData != null)
                    {
                        existingData.CourseName = model.CourseName;
                        existingData.CourseCode = model.CourseCode;
                        existingData.Details = model.CourseDetails;
                        existingData.DepartmentId = model.DepartmentId;
                        existingData.SemesterId = model.SemesterId;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        sucMsg = ConstantUserMessages.COURSE_UPDATED;
                    }
                    else
                    {
                        var newData = new Course
                        {
                            CourseName = model.CourseName,
                            CourseCode = model.CourseCode,
                            Details = model.CourseDetails,
                            DepartmentId = model.DepartmentId,
                            SemesterId = model.SemesterId,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime
                        };
                        _db.Courses.Add(newData);
                        sucMsg = ConstantUserMessages.COURSE_CREATED;
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.Courses.Where(f => f.CourseId == id).Include("Students").ToList().FirstOrDefault();
                    if (existingData != null)
                    {
                        if (!existingData.Students.Any())
                        {
                            var data = _db.Courses.FirstOrDefault(f => f.CourseId == id);
                            _db.Courses.Remove(data);
                            await _db.SaveChangesAsync();
                            sucMsg = ConstantUserMessages.COURSE_DELETED;
                        }
                        else
                            errMsg = "Error occurred! Can't delete, related to other data!";
                    }
                    else
                        errMsg = "Error occurred, no data found with this id!";
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required id is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Marking Criteris or Category Related Methods"
        public async Task<IActionResult> MarkingCriteria()
        {
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxMarkingCriterias(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.MarkingCriterias.AsEnumerable();
                var allUsers = await _db.AppUsers.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.MarkingCriteriaName) && w.MarkingCriteriaName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Details) && w.Details.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.MarkingCriteriaId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }

                var displayedValues = allResults.OrderByDescending(o => o.MarkingCriteriaId).Skip(param.start).Take(displayLength).ToList()
                    .Select(s => new
                    {
                        markingCriteriaId = s.MarkingCriteriaId,
                        markingCriteriaName = s.MarkingCriteriaName,
                        markingCriteriaMinimum = s.MinimumValue,
                        markingCriteriaMaximum = s.MaximumValue,
                        markingCriteriaPassing = s.PassingValue,
                        markingCriteriaDetails = s.Details,
                        createdBy = allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy) != null ? allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy).UserName : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.markingCriteriaId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }

        [HttpPost]
        public async Task<IActionResult> MarkingCriteriaAddEdit(VmMarkingCriteria model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.MarkingCriterias.FirstOrDefault(f => f.MarkingCriteriaId == model.MarkingCriteriaId);
                    if (existingData != null)
                    {
                        existingData.MarkingCriteriaName = model.MarkingCriteriaName;
                        existingData.MinimumValue = model.MinimumValue;
                        existingData.MaximumValue = model.MaximumValue;
                        existingData.PassingValue = model.PassingValue;
                        existingData.Details = model.MarkingCriteriaDetails;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        sucMsg = ConstantUserMessages.MARKING_CRITERIA_UPDATED;
                    }
                    else
                    {
                        var newData = new MarkingCriteria
                        {
                            MarkingCriteriaName = model.MarkingCriteriaName,
                            MinimumValue = model.MinimumValue,
                            MaximumValue = model.MaximumValue,
                            PassingValue = model.PassingValue,
                            Details = model.MarkingCriteriaDetails,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime
                        };
                        _db.MarkingCriterias.Add(newData);
                        sucMsg = ConstantUserMessages.MARKING_CRITERIA_CREATED;
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }

        public async Task<IActionResult> DeleteMarkingCriteria(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.MarkingCriterias.Where(f => f.MarkingCriteriaId == id).Include("MarkingCriteriasBadges")
                                       .ToList().FirstOrDefault();
                    if (existingData != null)
                    {
                        if (!existingData.MarkingCriteriasBadges.Any())
                        {
                            var data = _db.MarkingCriterias.FirstOrDefault(f => f.MarkingCriteriaId == id);
                            _db.MarkingCriterias.Remove(data);
                            await _db.SaveChangesAsync();
                            sucMsg = ConstantUserMessages.MARKING_CRITERIA_DELETED;
                        }
                        else
                            errMsg = "Error occurred! Can't delete, related to other data!";
                    }
                    else
                        errMsg = "Error occurred, no data found with this id!";
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required id is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Marking Badge Related Methods"
        public async Task<IActionResult> MarkingBadge()
        {
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxMarkingBadges(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.MarkingBadges.Where(w => w.MarkingBadgeId > 0).ToList();
                var allUsers = await _db.AppUsers.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.MarkingBadgeName) && w.MarkingBadgeName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Details) && w.Details.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.MarkingBadgeId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }

                var displayedValues = allResults.OrderByDescending(o => o.MarkingBadgeId).Skip(param.start).Take(displayLength)
                    .Select(s => new
                    {
                        markingBadgeId = s.MarkingBadgeId,
                        markingBadgeName = s.MarkingBadgeName,
                        markingBadgeColor = !string.IsNullOrEmpty(s.BadgeColorCode) ? s.BadgeColorCode : (s.MarkingBadgeName.ToLower().Contains("excellent") ? "#1cc88a" : (s.MarkingBadgeName.ToLower().Contains("very good") ? "#4e73df" : (s.MarkingBadgeName.ToLower().Contains("good") ? "#36b9cc" : (s.MarkingBadgeName.ToLower().Contains("average") ? "#f6c23e" : (s.MarkingBadgeName.ToLower().Contains("poor") ? "#e74a3b" : "#000000"))))),
                        markingBadgeDetails = s.Details,
                        createdBy = allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy) != null ? allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy).UserName : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.markingBadgeId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }

        [HttpPost]
        public async Task<IActionResult> MarkingBadgeAddEdit(VmMarkingBadge model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.MarkingBadges.FirstOrDefault(f => f.MarkingBadgeId == model.MarkingBadgeId);
                    if (existingData != null)
                    {
                        existingData.MarkingBadgeName = model.MarkingBadgeName;
                        if (!string.IsNullOrEmpty(model.MarkingBadgeColor))
                        { existingData.BadgeColorCode = model.MarkingBadgeColor; }
                        existingData.Details = model.MarkingBadgeDetails;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        sucMsg = ConstantUserMessages.MARKING_BADGE_UPDATED;
                    }
                    else
                    {
                        var newData = new MarkingBadge
                        {
                            MarkingBadgeName = model.MarkingBadgeName,
                            BadgeColorCode = !string.IsNullOrEmpty(model.MarkingBadgeColor) ? model.MarkingBadgeColor : "#000000",
                            Details = model.MarkingBadgeDetails,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime
                        };
                        _db.MarkingBadges.Add(newData);
                        sucMsg = ConstantUserMessages.MARKING_BADGE_CREATED;
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }

        public async Task<IActionResult> DeleteMarkingBadge(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.MarkingBadges.Where(f => f.MarkingBadgeId == id).Include("MarkingCriteriasBadges").ToList().FirstOrDefault();
                    if (existingData != null)
                    {
                        if (!existingData.MarkingCriteriasBadges.Any())
                        {
                            var data = _db.MarkingBadges.FirstOrDefault(f => f.MarkingBadgeId == id);
                            _db.MarkingBadges.Remove(data);
                            await _db.SaveChangesAsync();
                            sucMsg = ConstantUserMessages.MARKING_BADGE_DELETED;
                        }
                        else
                            errMsg = "Error occurred! Can't delete, related to other data!";
                    }
                    else
                        errMsg = "Error occurred, no data found with this id!";
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required id is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Marking Criterias Badges Related Methods"
        public async Task<IActionResult> CriteriasBadges()
        {
            IEnumerable<VmSelectList> markingCriterias = Enumerable.Empty<VmSelectList>();
            IEnumerable<VmSelectList> markingBadges = Enumerable.Empty<VmSelectList>();
            try
            {
                markingCriterias = _db.MarkingCriterias.OrderBy(o => o.MarkingCriteriaId).ToList()
                    .Select(s => new VmSelectList { Id = s.MarkingCriteriaId, Name = s.MarkingCriteriaName });
                markingBadges = _db.MarkingBadges.ToList().Select(s => new VmSelectList { Id = s.MarkingBadgeId, Name = s.MarkingBadgeName });
            }
            catch (Exception ex)
            {
                markingCriterias = Enumerable.Empty<VmSelectList>();
                markingBadges = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.MarkingCriterias = markingCriterias;
            ViewBag.MarkingBadges = markingBadges;
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxCriteriasBadges(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.MarkingCriteriasBadges.Where(w => w.CriteriasBadgeId > 0).Include("MarkingCriteria").Include("MarkingBadge").ToList();
                var allUsers = await _db.AppUsers.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.MarkingCriteria.MarkingCriteriaName) && w.MarkingCriteria.MarkingCriteriaName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.MarkingBadge.MarkingBadgeName) && w.MarkingBadge.MarkingBadgeName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.MarkingBadge.BadgeColorCode) && w.MarkingBadge.BadgeColorCode.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Details) && w.Details.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.CriteriasBadgeId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }

                var displayedValues = allResults.OrderByDescending(o => o.CriteriasBadgeId).Skip(param.start).Take(displayLength)
                    .Select(s => new
                    {
                        criteriasBadgeId = s.CriteriasBadgeId,
                        markingCriteriaId = s.MarkingCriteriaId,
                        markingCriteriaName = s.MarkingCriteria != null ? s.MarkingCriteria.MarkingCriteriaName : "",
                        markingCriteriaDetails = s.MarkingCriteria != null ? s.MarkingCriteria.MarkingCriteriaName : "",
                        markingBadgeId = s.MarkingBadgeId,
                        markingBadgeName = s.MarkingBadge.MarkingBadgeName,
                        markingBadgeColor = !string.IsNullOrEmpty(s.MarkingBadge.BadgeColorCode) ? s.MarkingBadge.BadgeColorCode : (s.MarkingBadge.MarkingBadgeName.ToLower().Contains("excellent") ? "#1cc88a" : (s.MarkingBadge.MarkingBadgeName.ToLower().Contains("very good") ? "#4e73df" : (s.MarkingBadge.MarkingBadgeName.ToLower().Contains("good") ? "#36b9cc" : (s.MarkingBadge.MarkingBadgeName.ToLower().Contains("average") ? "#f6c23e" : (s.MarkingBadge.MarkingBadgeName.ToLower().Contains("poor") ? "#e74a3b" : "#000000"))))),
                        markingBadgeDetails = s.MarkingBadge.Details,
                        criteriasBadgeMinimum = s.MinimumValue,
                        criteriasBadgeMaximum = s.MaximumValue,
                        criteriasBadgeDetails = s.Details,
                        createdBy = allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy) != null ? allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy).UserName : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.criteriasBadgeId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }

        [HttpPost]
        public async Task<IActionResult> CriteriasBadgesAddEdit(VmCriteriasBadge model)
        {
            string sucMsg = "";
            string errMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.MarkingCriteriasBadges.FirstOrDefault(f => f.CriteriasBadgeId == model.CriteriasBadgeId);
                    if (existingData != null)
                    {
                        existingData.MarkingCriteriaId = model.MarkingCriteriaId;
                        existingData.MarkingBadgeId = model.MarkingBadgeId;
                        existingData.MinimumValue = model.CriteriasBadgeMinimum;
                        existingData.MaximumValue = model.CriteriasBadgeMaximum;
                        existingData.Details = model.CriteriasBadgeDetails;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        sucMsg = ConstantUserMessages.MARKING_CRITERIAS_BADGE_UPDATED;
                    }
                    else
                    {
                        var newData = new MarkingCriteriasBadge
                        {
                            MarkingCriteriaId = model.MarkingCriteriaId,
                            MarkingBadgeId = model.MarkingBadgeId,
                            MinimumValue = model.CriteriasBadgeMinimum,
                            MaximumValue = model.CriteriasBadgeMaximum,
                            Details = model.CriteriasBadgeDetails,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime
                        };
                        _db.MarkingCriteriasBadges.Add(newData);
                        sucMsg = ConstantUserMessages.MARKING_CRITERIAS_BADGE_CREATED;
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required filed data is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }

        public async Task<IActionResult> DeleteCriteriasBadges(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.MarkingCriteriasBadges.Where(f => f.CriteriasBadgeId == id).FirstOrDefault();
                    if (existingData != null)
                    {
                        _db.MarkingCriteriasBadges.Remove(existingData);
                        await _db.SaveChangesAsync();
                        sucMsg = ConstantUserMessages.MARKING_CRITERIAS_BADGE_DELETED;
                    }
                    else
                        errMsg = "Error occurred, no data found with this id!";
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required id is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion

        #region "Data Filter/Fetch From Ajax Call"
        public async Task<JsonResult> GetAllDepartments()
        {
            var datas = new List<VmSelectList>();
            try
            {
                datas = await _db.Departments.Where(w => w.DepartmentId > 0).Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName }).ToListAsync();
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllSemesters()
        {
            var datas = new List<VmSelectList>();
            try
            {
                datas = await _db.Semesters.Where(w => w.SemesterId > 0).Select(s => new VmSelectList { Id = s.SemesterId, Name = s.SemesterName }).ToListAsync();
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllBatches()
        {
            var datas = new List<VmSelectList>();
            try
            {
                datas = await _db.StudentBatches.Where(w => w.BatchId > 0).Select(s => new VmSelectList { Id = s.BatchId, Name = s.BatchName }).ToListAsync();
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllStudents()
        {
            var datas = new List<VmSelectList>();
            try
            {
                datas = await _db.Students.Where(w => w.StudentId > 0).Select(s => new VmSelectList { Id = s.StudentId, Name = (s.FirstName + (!string.IsNullOrEmpty(s.LastName) ? (" " + s.LastName) : "") + "(Roll#" + s.RollNumber + ")") }).ToListAsync();
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllStudentsByBatch(int batchId)
        {
            var datas = new List<VmSelectList>();
            try
            {
                if (batchId > 0)
                {
                    datas = await _db.Students.Where(w => w.BatchId == batchId).Select(s => new VmSelectList { Id = s.StudentId, Name = (s.FirstName + (!string.IsNullOrEmpty(s.LastName) ? (" " + s.LastName) : "") + "(Roll#" + s.RollNumber + ")") }).ToListAsync();
                }
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllCourses()
        {
            var datas = new List<VmSelectList>();
            try
            {
                datas = await _db.Courses.Where(w => w.CourseId > 0).Select(s => new VmSelectList { Id = s.CourseId, Name = s.CourseName }).ToListAsync();
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllCoursesNameByDepartmentAndSemester(int departmentId, int semesterId)
        {
            var datas = new List<VmSelectList>();
            try
            {
                if (departmentId > 0 && semesterId > 0)
                {
                    datas = await _db.Courses.Where(w => w.DepartmentId == departmentId && w.SemesterId == semesterId).Select(s => new VmSelectList { Id = s.CourseId, Name = s.CourseName }).ToListAsync();
                }
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllCoursesNameWithCodeByDepartmentAndSemester(int departmentId, int semesterId)
        {
            var datas = new List<VmSelectList>();
            try
            {
                if (departmentId > 0 && semesterId > 0)
                {
                    datas = await _db.Courses.Where(w => w.DepartmentId == departmentId && w.SemesterId == semesterId).Select(s => new VmSelectList { Id = s.CourseId, Name = s.CourseName + (!string.IsNullOrEmpty(s.CourseCode) ? ("(" + s.CourseCode + ")") : "") }).ToListAsync();
                }
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllCoursesCodeByDepartmentAndSemester(int departmentId, int semesterId)
        {
            var datas = new List<VmSelectList>();
            try
            {
                if (departmentId > 0 && semesterId > 0)
                {
                    datas = await _db.Courses.Where(w => w.DepartmentId == departmentId && w.SemesterId == semesterId).Select(s => new VmSelectList { Id = s.CourseId, Name = s.CourseCode }).ToListAsync();
                }
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllMarkingCriterias()
        {
            var datas = new List<VmSelectList>();
            try
            {
                datas = await _db.MarkingCriterias.Where(w => w.MarkingCriteriaId > 0).OrderBy(o => o.MarkingCriteriaId)
                    .Select(s => new VmSelectList { Id = s.MarkingCriteriaId, Name = s.MarkingCriteriaName }).ToListAsync();
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }

        public async Task<JsonResult> GetAllMarkingBadges()
        {
            var datas = new List<VmSelectList>();
            try
            {
                datas = await _db.MarkingBadges.Where(w => w.MarkingBadgeId > 0).Select(s => new VmSelectList { Id = s.MarkingBadgeId, Name = s.MarkingBadgeName }).ToListAsync();
            }
            catch (Exception ex)
            { datas = new List<VmSelectList>(); }
            return Json(new { data = datas });
        }
        #endregion
    }
}
