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
using Web.Constants;
using Web.Helpers;
using Web.Helpers.Interfaces;

namespace Web.Controllers
{
    public class StudentController : Controller
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

        public StudentController(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, ICookieHelperService _cookieHelperService, ICredentialManagerService _credentialManager, IDisplayMessageHelper _displayMessageHelper, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService, IFileUploadHelperService _fileUploadHelperService)
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

        #region "Student Listing Related Methods"
        public async Task<IActionResult> Index()
        {
            IEnumerable<VmSelectList> departments = Enumerable.Empty<VmSelectList>();
            IEnumerable<VmSelectList> studentTypes = Enumerable.Empty<VmSelectList>();
            IEnumerable<VmSelectList> studentBatches = Enumerable.Empty<VmSelectList>();
            try
            {
                studentBatches = _db.StudentBatches.ToList().Select(s => new VmSelectList { Id = s.BatchId, Name = s.BatchName });
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
                studentTypes = from StudentType e in Enum.GetValues(typeof(StudentType)) select new VmSelectList { Id = (int)e, Name = e.ToString() };
            }
            catch (Exception ex)
            {
                departments = Enumerable.Empty<VmSelectList>();
                studentTypes = Enumerable.Empty<VmSelectList>();
                studentBatches = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            ViewBag.Batches = studentBatches;
            ViewBag.StudentTypes = studentTypes;
            _displayMessageHelper.ErrorMessageSetOrGet(this, false);
            _displayMessageHelper.SuccessMessageSetOrGet(this, false);
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxStudents(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.Students.Where(w => w.StudentId > 0).Include("DepartmentInfo").Include("BatchInfo").ToList();
                var allUsers = await _db.AppUsers.ToListAsync();
                var allBatches = await _db.StudentBatches.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.FirstName) && w.FirstName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.LastName) && w.LastName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.ContactNumber) && w.ContactNumber.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.EmailAddress) && w.EmailAddress.ToLower().Contains(search.ToLower())) || w.RollNumber.ToString().Contains(search.ToLower()) || (!string.IsNullOrEmpty(w.RegistrationNumber) && w.RegistrationNumber.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.StudentId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }

                var displayedValues = allResults.OrderBy(o => o.StudentId).Skip(param.start).Take(displayLength)
                    .Select(s => new
                    {
                        studentId = s.StudentId,
                        studentName = s.FirstName + (!string.IsNullOrEmpty(s.LastName) ? (" " + s.LastName) : ""),
                        registrationNumber = !string.IsNullOrEmpty(s.RegistrationNumber) ? s.RegistrationNumber : "",
                        registrationDate = s.RegistrationDate != null ? s.RegistrationDate.Value.ToString("dd/MM/yyyy") : "",
                        rollNumber = s.RollNumber,
                        batchId = s.BatchId,
                        batchName = allBatches.FirstOrDefault(f => f.BatchId == s.BatchId) != null ? allBatches.FirstOrDefault(f => f.BatchId == s.BatchId).BatchName : "",
                        departmentId = s.DepartmentId,
                        departmentName = s.DepartmentInfo != null ? s.DepartmentInfo.DepartmentName : "",
                        studentType = s.Type.ToString(),
                        studentGender = !string.IsNullOrEmpty(s.Gender) ? s.Gender : "",
                        contactNumber = !string.IsNullOrEmpty(s.ContactNumber) ? s.ContactNumber : "",
                        religion = !string.IsNullOrEmpty(s.Religion) ? s.Religion : "",
                        nationalID = !string.IsNullOrEmpty(s.NationalID) ? s.NationalID : "",
                        emailAddress = !string.IsNullOrEmpty(s.EmailAddress) ? s.EmailAddress : "",
                        presentAddress = !string.IsNullOrEmpty(s.PresentAddress) ? s.PresentAddress : "",
                        permanentAddress = !string.IsNullOrEmpty(s.PermanentAddress) ? s.PermanentAddress : "",
                        profilePictureUrl = !string.IsNullOrEmpty(s.ProfilePicture) ? s.ProfilePicture : "",
                        fatherName = !string.IsNullOrEmpty(s.FatherName) ? s.FatherName : "",
                        motherName = !string.IsNullOrEmpty(s.MotherName) ? s.MotherName : "",
                        gurdianContactNumber = !string.IsNullOrEmpty(s.GurdianNumber) ? s.GurdianNumber : "",
                        createdBy = allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy) != null ? allUsers.FirstOrDefault(f => f.UserId == s.CreatedBy).UserName : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.studentId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }
        #endregion

        #region "Student Add,Edit&Update Related Methods"
        public async Task<IActionResult> AddNew()
        {
            var departments = Enumerable.Empty<VmSelectList>();
            var studentTypes = Enumerable.Empty<VmSelectList>();
            var studentBatches = Enumerable.Empty<VmSelectList>();
            try
            {
                studentBatches = _db.StudentBatches.ToList().Select(s => new VmSelectList { Id = s.BatchId, Name = s.BatchName });
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
                studentTypes = from StudentType e in Enum.GetValues(typeof(StudentType)) select new VmSelectList { Id = (int)e, Name = e.ToString() };
            }
            catch (Exception ex)
            {
                departments = Enumerable.Empty<VmSelectList>();
                studentTypes = Enumerable.Empty<VmSelectList>();
                studentBatches = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            ViewBag.Batches = studentBatches;
            ViewBag.StudentTypes = studentTypes;
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vmStudentData = new VmStudentData();
            var departments = Enumerable.Empty<VmSelectList>();
            var studentTypes = Enumerable.Empty<VmSelectList>();
            var studentBatches = Enumerable.Empty<VmSelectList>();
            try
            {
                if (id > 0)
                {
                    var studentInfo = _db.Students.FirstOrDefault(w => w.StudentId == id);
                    if (studentInfo != null)
                    {
                        vmStudentData.StudentId = studentInfo.StudentId;
                        vmStudentData.FirstName = studentInfo.FirstName;
                        vmStudentData.LastName = studentInfo.LastName;
                        vmStudentData.DepartmentId = studentInfo.DepartmentId;
                        vmStudentData.BatchId = studentInfo.BatchId;
                        vmStudentData.StudentTypeId = (int)studentInfo.Type;
                        vmStudentData.RegistrationNumber = studentInfo.RegistrationNumber;
                        vmStudentData.RegistrationDate = studentInfo.RegistrationDate != null ? studentInfo.RegistrationDate.Value.ToString("dd/MM/yyyy") : "";
                        vmStudentData.RollNumber = studentInfo.RollNumber;
                        vmStudentData.EmailAddress = studentInfo.EmailAddress;
                        vmStudentData.ContactNumber = studentInfo.ContactNumber;
                        vmStudentData.FatherName = studentInfo.FatherName;
                        vmStudentData.MotherName = studentInfo.MotherName;
                        vmStudentData.GurdianContactNumber = studentInfo.GurdianNumber;
                        vmStudentData.Gender = studentInfo.Gender;
                        vmStudentData.Religion = studentInfo.Religion;
                        vmStudentData.NationalID = studentInfo.NationalID;
                        vmStudentData.PresentAddress = studentInfo.PresentAddress;
                        vmStudentData.PermanentAddress = studentInfo.PermanentAddress;
                        vmStudentData.AppliedTermsAndCondition = studentInfo.AppliedTermsAndCondition;
                    }
                }
                studentBatches = _db.StudentBatches.ToList().Select(s => new VmSelectList { Id = s.BatchId, Name = s.BatchName });
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
                studentTypes = from StudentType e in Enum.GetValues(typeof(StudentType)) select new VmSelectList { Id = (int)e, Name = e.ToString() };
            }
            catch (Exception ex)
            {
                vmStudentData = new VmStudentData();
                departments = Enumerable.Empty<VmSelectList>();
                studentTypes = Enumerable.Empty<VmSelectList>();
                studentBatches = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            ViewBag.Batches = studentBatches;
            ViewBag.StudentTypes = studentTypes;
            await Task.Delay(0);
            return View(vmStudentData);
        }

        [HttpPost]
        public async Task<IActionResult> StudentAddEdit(VmStudentData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.Students.FirstOrDefault(f => f.StudentId == model.StudentId);
                    string proPicFilePath = _fileUploadHelperService.UploadFile(model.ProfilePicture, "student-images");
                    var registrationDate = _dateTimeHelperService.ConvertBDDateStringToDateTimeObject(model.RegistrationDate);
                    if (existingData != null)
                    {
                        existingData.FirstName = model.FirstName;
                        existingData.LastName = model.LastName;
                        existingData.DepartmentId = model.DepartmentId;
                        existingData.BatchId = model.BatchId;
                        existingData.Type = (StudentType)model.StudentTypeId;
                        existingData.RegistrationNumber = model.RegistrationNumber;
                        if (!string.IsNullOrEmpty(registrationDate))
                            existingData.RegistrationDate = Convert.ToDateTime(registrationDate);
                        existingData.RollNumber = model.RollNumber;
                        existingData.EmailAddress = model.EmailAddress;
                        existingData.ContactNumber = model.ContactNumber;
                        existingData.FatherName = model.FatherName;
                        existingData.MotherName = model.MotherName;
                        existingData.GurdianNumber = model.GurdianContactNumber;
                        existingData.Gender = model.Gender;
                        existingData.Religion = model.Religion;
                        existingData.NationalID = model.NationalID;
                        existingData.PresentAddress = model.PresentAddress;
                        existingData.PermanentAddress = model.PermanentAddress;
                        if (!string.IsNullOrEmpty(proPicFilePath))
                            existingData.ProfilePicture = proPicFilePath;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.STUDENT_UPDATED);
                    }
                    else
                    {
                        var newData = new Student
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            DepartmentId = model.DepartmentId,
                            BatchId = model.BatchId,
                            Type = (StudentType)model.StudentTypeId,
                            RegistrationNumber = model.RegistrationNumber,
                            RegistrationDate = !string.IsNullOrEmpty(registrationDate) ? Convert.ToDateTime(registrationDate) : null,
                            RollNumber = model.RollNumber,
                            EmailAddress = model.EmailAddress,
                            ContactNumber = model.ContactNumber,
                            FatherName = model.FatherName,
                            MotherName = model.MotherName,
                            GurdianNumber = model.GurdianContactNumber,
                            Gender = model.Gender,
                            Religion = model.Religion,
                            NationalID = model.NationalID,
                            PresentAddress = model.PresentAddress,
                            PermanentAddress = model.PermanentAddress,
                            ProfilePicture = !string.IsNullOrEmpty(proPicFilePath) ? proPicFilePath : null,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime,
                            AppliedTermsAndCondition = model.AppliedTermsAndCondition
                        };
                        _db.Students.Add(newData);
                        _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.STUDENT_CREATED);
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex) { _displayMessageHelper.ErrorMessageSetOrGet(this, true, ex.Message); }
            }
            else
            { _displayMessageHelper.ErrorMessageSetOrGet(this, true, ConstantUserMessages.MODEL_STATE_INVALID); }
            return RedirectToAction("Index");
        }
        #endregion

        #region "Student Details Related Methods"
        public async Task<IActionResult> Details(int id)
        {
            var vmStudentData = new VmStudentData();
            var departments = Enumerable.Empty<VmSelectList>();
            var studentTypes = Enumerable.Empty<VmSelectList>();
            var studentBatches = Enumerable.Empty<VmSelectList>();
            try
            {
                if (id > 0)
                {
                    var studentInfo = _db.Students.FirstOrDefault(w => w.StudentId == id);
                    if (studentInfo != null)
                    {
                        vmStudentData.StudentId = studentInfo.StudentId;
                        vmStudentData.FirstName = studentInfo.FirstName;
                        vmStudentData.LastName = studentInfo.LastName;
                        vmStudentData.DepartmentId = studentInfo.DepartmentId;
                        vmStudentData.BatchId = studentInfo.BatchId;
                        vmStudentData.StudentTypeId = (int)studentInfo.Type;
                        vmStudentData.RegistrationNumber = studentInfo.RegistrationNumber;
                        vmStudentData.RegistrationDate = studentInfo.RegistrationDate != null ? studentInfo.RegistrationDate.Value.ToString("dd/MM/yyyy") : "";
                        vmStudentData.RollNumber = studentInfo.RollNumber;
                        vmStudentData.EmailAddress = studentInfo.EmailAddress;
                        vmStudentData.ContactNumber = studentInfo.ContactNumber;
                        vmStudentData.FatherName = studentInfo.FatherName;
                        vmStudentData.MotherName = studentInfo.MotherName;
                        vmStudentData.GurdianContactNumber = studentInfo.GurdianNumber;
                        vmStudentData.Gender = studentInfo.Gender;
                        vmStudentData.Religion = studentInfo.Religion;
                        vmStudentData.NationalID = studentInfo.NationalID;
                        vmStudentData.PresentAddress = studentInfo.PresentAddress;
                        vmStudentData.PermanentAddress = studentInfo.PermanentAddress;
                        vmStudentData.AppliedTermsAndCondition = studentInfo.AppliedTermsAndCondition;
                    }
                }
                studentBatches = _db.StudentBatches.ToList().Select(s => new VmSelectList { Id = s.BatchId, Name = s.BatchName });
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
                studentTypes = from StudentType e in Enum.GetValues(typeof(StudentType)) select new VmSelectList { Id = (int)e, Name = e.ToString() };
            }
            catch (Exception ex)
            {
                vmStudentData = new VmStudentData();
                departments = Enumerable.Empty<VmSelectList>();
                studentTypes = Enumerable.Empty<VmSelectList>();
                studentBatches = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            ViewBag.Batches = studentBatches;
            ViewBag.StudentTypes = studentTypes;
            await Task.Delay(0);
            return View(vmStudentData);
        }
        #endregion

        #region "Student Delete Related Methods"
        public async Task<IActionResult> DeleteStudent(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.Students.Where(f => f.StudentId == id)
                        .Include("StudentMarks").ToList()
                        .FirstOrDefault();
                    if (existingData != null)
                    {
                        if (!existingData.StudentMarks.Any())
                        {
                            var data = _db.Students.FirstOrDefault(f => f.StudentId == id);
                            //remove uploaded student image
                            _fileUploadHelperService.RemoveFile(data.ProfilePicture, "student-images");
                            _db.Students.Remove(data);
                            await _db.SaveChangesAsync();
                            sucMsg = ConstantUserMessages.STUDENT_DELETED;
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

        #region "Student Download(Using CsvExport)"
        public async Task<IActionResult> Download(int? id)
        {
            var fileName = "result.csv";
            var fileExport = new CsvExportHelper();
            try
            {
                var currentDate = _dateTimeHelperService.Now();
                var allBatches = await _db.StudentBatches.ToListAsync();
                var students = _db.Students
                    .Where(w => w.StudentId > 0)
                    .Include("DepartmentInfo").Include("BatchInfo").Include("StudentMarks")
                    .ToList();
                if (id != null && id > 0)
                {
                    students = _db.Students
                      .Where(w => w.StudentId == id)
                      .Include("DepartmentInfo").Include("BatchInfo").Include("StudentMarks")
                      .ToList();
                }

                foreach (var studentInfo in students)
                {
                    fileExport.AddRow();
                    fileExport["Student Name"] = studentInfo.FullName;
                    fileExport["Roll No."] = studentInfo.RollNumber;
                    fileExport["Registration No."] = studentInfo.RegistrationNumber;
                    fileExport["Email Address"] = studentInfo.EmailAddress;
                    fileExport["Contact Number"] = studentInfo.ContactNumber;
                    fileExport["National ID"] = studentInfo.NationalID;
                    fileExport["Father Name"] = studentInfo.FatherName;
                    fileExport["Mother Name"] = studentInfo.MotherName;
                    fileExport["Present Address"] = studentInfo.PresentAddress;
                    fileExport["Batch"] = allBatches.FirstOrDefault(f => f.BatchId == studentInfo.BatchId) != null ? allBatches.FirstOrDefault(f => f.BatchId == studentInfo.BatchId).BatchName : "";
                    fileExport["Department"] = studentInfo.DepartmentInfo != null ? studentInfo.DepartmentInfo.DepartmentName : "";
                    fileExport["Registration Date"] = studentInfo.RegistrationDate != null ? studentInfo.RegistrationDate.Value.ToString("dd/MM/yyyy") : "";
                }
                fileName = "student-info_" + currentDate.Year + currentDate.Month + currentDate.Day + currentDate.Minute + currentDate.Second + ".csv";
            }
            catch (Exception ex)
            { fileExport = new CsvExportHelper(); }
            return File(fileExport.ExportToBytes(), "text/csv", fileName);
        }

        public IActionResult MarksInfo(int id)
        {
            var studentInfo = _db.Students
                     .Where(w => w.StudentId == id)
                     .Include("DepartmentInfo").Include("BatchInfo").Include("StudentMarks")
                     .FirstOrDefault();
            //var studentInfos = studentInfo.StudentCoursesMarks;
            return Json(studentInfo.StudentCoursesMarks);
        }
        #endregion
    }
}
