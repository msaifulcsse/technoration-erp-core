using Entities.EntityModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.utility;
using Services.DataService.Interfaces;
using Services.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Constants;
using Web.Helpers.Interfaces;

namespace Web.Controllers
{
    public class EmployeeController : Controller
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

        public EmployeeController(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, ICookieHelperService _cookieHelperService, ICredentialManagerService _credentialManager, IDisplayMessageHelper _displayMessageHelper, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService, IFileUploadHelperService _fileUploadHelperService)
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

        #region "Employee Listing Related Methods"
        public async Task<IActionResult> Index()
        {
            IEnumerable<VmSelectList> markingCriterias = Enumerable.Empty<VmSelectList>();
            try
            {
                markingCriterias = _db.MarkingCriterias.OrderBy(o => o.MarkingCriteriaId).ToList()
                    .Select(s => new VmSelectList { Id = s.MarkingCriteriaId, Name = s.MarkingCriteriaName });
            }
            catch (Exception ex)
            {
                markingCriterias = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.MarkingCriterias = markingCriterias;
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxEmployees(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];
                var allResults = _db.Employees.Where(w => w.EmployeeId > 0).Include("DepartmentInfo").ToList();
                var allUsers = await _db.AppUsers.ToListAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.FirstName) && w.FirstName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.LastName) && w.LastName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.ContactNumber) && w.ContactNumber.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.EmailAddress) && w.EmailAddress.ToLower().Contains(search.ToLower())) || w.NationalID.ToString().Contains(search.ToLower()) || (!string.IsNullOrEmpty(w.RegistrationNumber) && w.RegistrationNumber.ToLower().Contains(search.ToLower()))).ToList();
                }

                totalLength = allResults.Select(s => s.EmployeeId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }

                var displayedValues = allResults.OrderBy(o => o.EmployeeId).Skip(param.start).Take(displayLength)
                    .Select(s => new
                    {
                        employeeId = s.EmployeeId,
                        employeeName = s.FirstName + (!string.IsNullOrEmpty(s.LastName) ? (" " + s.LastName) : ""),
                        registrationNumber = !string.IsNullOrEmpty(s.RegistrationNumber) ? s.RegistrationNumber : "",
                        joiningDate = s.JoiningDate != null ? s.JoiningDate.Value.ToString("dd/MM/yyyy") : "",
                        departmentId = s.DepartmentId,
                        departmentName = s.DepartmentInfo != null ? s.DepartmentInfo.DepartmentName : "",
                        designation = !string.IsNullOrEmpty(s.Attribute1) ? s.Attribute1 : "",
                        employeeType = s.Type.ToString(),
                        employeeGender = !string.IsNullOrEmpty(s.Gender) ? s.Gender : "",
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
                    iTotalRecords = displayedValues.Select(s => s.employeeId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            { return Json(new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, data = "" }); }
        }
        #endregion

        #region "Employee Add,Edit&Update Related Methods"
        public async Task<IActionResult> AddNew()
        {
            var departments = Enumerable.Empty<VmSelectList>();
            var employeeTypes = Enumerable.Empty<VmSelectList>();
            try
            {
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
                employeeTypes = from EmployeeType e in Enum.GetValues(typeof(EmployeeType)) select new VmSelectList { Id = (int)e, Name = e.ToString() };
            }
            catch (Exception ex)
            {
                departments = Enumerable.Empty<VmSelectList>();
                employeeTypes = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            ViewBag.EmployeeTypes = employeeTypes;
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vmEmployeeData = new VmEmployeeData();
            var departments = Enumerable.Empty<VmSelectList>();
            var employeeTypes = Enumerable.Empty<VmSelectList>();
            try
            {
                if (id > 0)
                {
                    var employeeInfo = _db.Employees.Where(w => w.EmployeeId == id).Include("DepartmentInfo").FirstOrDefault();
                    if (employeeInfo != null)
                    {
                        vmEmployeeData.EmployeeId = employeeInfo.EmployeeId;
                        vmEmployeeData.FirstName = employeeInfo.FirstName;
                        vmEmployeeData.LastName = employeeInfo.LastName;
                        vmEmployeeData.DepartmentId = employeeInfo.DepartmentId ?? 0;
                        vmEmployeeData.Designation = !string.IsNullOrEmpty(employeeInfo.Attribute1) ? employeeInfo.Attribute1 : "";
                        vmEmployeeData.EmployeeTypeId = (int)employeeInfo.Type;
                        vmEmployeeData.RegistrationNumber = employeeInfo.RegistrationNumber;
                        vmEmployeeData.JoiningDate = employeeInfo.JoiningDate != null ? employeeInfo.JoiningDate.Value.ToString("dd/MM/yyyy") : "";
                        vmEmployeeData.EmailAddress = employeeInfo.EmailAddress;
                        vmEmployeeData.ContactNumber = employeeInfo.ContactNumber;
                        vmEmployeeData.FatherName = employeeInfo.FatherName;
                        vmEmployeeData.MotherName = employeeInfo.MotherName;
                        vmEmployeeData.GurdianContactNumber = employeeInfo.GurdianNumber;
                        vmEmployeeData.Gender = employeeInfo.Gender;
                        vmEmployeeData.Religion = employeeInfo.Religion;
                        vmEmployeeData.NationalID = employeeInfo.NationalID;
                        vmEmployeeData.PresentAddress = employeeInfo.PresentAddress;
                        vmEmployeeData.PermanentAddress = employeeInfo.PermanentAddress;
                        //Employee User Information
                        var empUserInfo = _db.AppUsers.FirstOrDefault(f => f.UserType == (int)ApplicationRoles.Employee && f.ReferenceId == employeeInfo.EmployeeId);
                        if (empUserInfo != null)
                        {
                            vmEmployeeData.Password = _cryptoHelperService.Decrypt(empUserInfo.Password);
                            vmEmployeeData.ConfirmPassword = _cryptoHelperService.Decrypt(empUserInfo.Password);
                        }
                    }
                }
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
                employeeTypes = from EmployeeType e in Enum.GetValues(typeof(EmployeeType)) select new VmSelectList { Id = (int)e, Name = e.ToString() };
            }
            catch (Exception ex)
            {
                vmEmployeeData = new VmEmployeeData();
                departments = Enumerable.Empty<VmSelectList>();
                employeeTypes = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            ViewBag.EmployeeTypes = employeeTypes;
            await Task.Delay(0);
            return View(vmEmployeeData);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeAddEdit(VmEmployeeData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = _db.Employees.FirstOrDefault(f => f.EmployeeId == model.EmployeeId);
                    string proPicFilePath = _fileUploadHelperService.UploadFile(model.ProfilePicture, "employee-images");
                    var joiningDate = _dateTimeHelperService.ConvertBDDateStringToDateTimeObject(model.JoiningDate);
                    if (existingData != null)
                    {
                        existingData.FirstName = model.FirstName;
                        existingData.LastName = model.LastName;
                        existingData.DepartmentId = model.DepartmentId;
                        existingData.Attribute1 = model.Designation;
                        existingData.Type = (EmployeeType)model.EmployeeTypeId;
                        existingData.RegistrationNumber = model.RegistrationNumber;
                        if (!string.IsNullOrEmpty(joiningDate))
                            existingData.JoiningDate = Convert.ToDateTime(joiningDate);
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
                        await _db.SaveChangesAsync();
                        await EmployeeUserAddOrUpdate(model, existingData.EmployeeId, userId);
                        _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.EMPLOYEE_UPDATED);
                    }
                    else
                    {
                        var newData = new Employee
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            DepartmentId = model.DepartmentId,
                            Attribute1 = model.Designation,
                            Type = (EmployeeType)model.EmployeeTypeId,
                            RegistrationNumber = model.RegistrationNumber,
                            JoiningDate = !string.IsNullOrEmpty(joiningDate) ? Convert.ToDateTime(joiningDate) : null,
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
                            UpdatedDate = currentDateTime
                        };
                        _db.Employees.Add(newData);
                        await _db.SaveChangesAsync();
                        await EmployeeUserAddOrUpdate(model, newData.EmployeeId, userId);
                        _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.EMPLOYEE_CREATED);
                    }
                }
                catch (Exception ex) { _displayMessageHelper.ErrorMessageSetOrGet(this, true, ex.Message); }
            }
            else
            { _displayMessageHelper.ErrorMessageSetOrGet(this, true, ConstantUserMessages.MODEL_STATE_INVALID); }
            return RedirectToAction("Index");
        }

        private async Task<bool> EmployeeUserAddOrUpdate(VmEmployeeData employeeInfo, int emplyeeHeaderId, int userId)
        {
            bool result = false;
            try
            {
                if (employeeInfo != null && emplyeeHeaderId > 0 && userId > 0)
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var encryptedPassword = _cryptoHelperService.Encrypt(employeeInfo.ConfirmPassword);
                    var existingData = _db.AppUsers.FirstOrDefault(f => f.UserType == (int)ApplicationRoles.Employee && f.ReferenceId == emplyeeHeaderId);
                    if (existingData != null)
                    {
                        existingData.UserName = employeeInfo.RegistrationNumber;
                        existingData.Password = encryptedPassword;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                    }
                    else
                    {
                        var empRoleInfo = _db.AppRoles.FirstOrDefault(f => f.RoleName == ApplicationRoles.Employee.ToString());
                        var newEmpUser = new AppUser
                        {
                            UserName = employeeInfo.RegistrationNumber,
                            Password = encryptedPassword,
                            UserType = (int)ApplicationRoles.Employee,
                            ReferenceId = emplyeeHeaderId,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime,
                            IsActive = true
                        };
                        _db.AppUsers.Add(newEmpUser);
                        await _db.SaveChangesAsync();
                        var newEmpUserRole = new AppUserRole
                        {
                            RoleId = empRoleInfo != null ? empRoleInfo.RoleId : 0,
                            UserId = newEmpUser != null ? newEmpUser.UserId : 0,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime,
                            IsActive = true
                        };
                        _db.AppUserRoles.Add(newEmpUserRole);
                    }
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region "Employee Details Related Methods"
        public async Task<IActionResult> Details(int id)
        {
            var vmEmployeeData = new VmEmployeeData();
            var departments = Enumerable.Empty<VmSelectList>();
            var employeeTypes = Enumerable.Empty<VmSelectList>();
            try
            {
                if (id > 0)
                {
                    var employeeInfo = _db.Employees.Where(w => w.EmployeeId == id).Include("DepartmentInfo").FirstOrDefault();
                    if (employeeInfo != null)
                    {
                        vmEmployeeData.EmployeeId = employeeInfo.EmployeeId;
                        vmEmployeeData.FirstName = employeeInfo.FirstName;
                        vmEmployeeData.LastName = employeeInfo.LastName;
                        vmEmployeeData.DepartmentId = employeeInfo.DepartmentId ?? 0;
                        vmEmployeeData.DepartmentName = employeeInfo.DepartmentInfo.DepartmentName;
                        vmEmployeeData.Designation = !string.IsNullOrEmpty(employeeInfo.Attribute1) ? employeeInfo.Attribute1 : "";
                        vmEmployeeData.EmployeeTypeId = (int)employeeInfo.Type;
                        vmEmployeeData.RegistrationNumber = employeeInfo.RegistrationNumber;
                        vmEmployeeData.JoiningDate = employeeInfo.JoiningDate != null ? employeeInfo.JoiningDate.Value.ToString("dd/MM/yyyy") : "";
                        vmEmployeeData.EmailAddress = employeeInfo.EmailAddress;
                        vmEmployeeData.ContactNumber = employeeInfo.ContactNumber;
                        vmEmployeeData.FatherName = employeeInfo.FatherName;
                        vmEmployeeData.MotherName = employeeInfo.MotherName;
                        vmEmployeeData.GurdianContactNumber = employeeInfo.GurdianNumber;
                        vmEmployeeData.Gender = employeeInfo.Gender;
                        vmEmployeeData.Religion = employeeInfo.Religion;
                        vmEmployeeData.NationalID = employeeInfo.NationalID;
                        vmEmployeeData.PresentAddress = employeeInfo.PresentAddress;
                        vmEmployeeData.PermanentAddress = employeeInfo.PermanentAddress;
                        //Employee User Information
                        var empUserInfo = _db.AppUsers.FirstOrDefault(f => f.UserType == (int)ApplicationRoles.Employee && f.ReferenceId == employeeInfo.EmployeeId);
                        if (empUserInfo != null)
                        {
                            vmEmployeeData.Password = _cryptoHelperService.Decrypt(empUserInfo.Password);
                            vmEmployeeData.ConfirmPassword = _cryptoHelperService.Decrypt(empUserInfo.Password);
                        }
                    }
                }
                departments = _db.Departments.ToList().Select(s => new VmSelectList { Id = s.DepartmentId, Name = s.DepartmentName });
                employeeTypes = from EmployeeType e in Enum.GetValues(typeof(EmployeeType)) select new VmSelectList { Id = (int)e, Name = e.ToString() };
            }
            catch (Exception ex)
            {
                vmEmployeeData = new VmEmployeeData();
                departments = Enumerable.Empty<VmSelectList>();
                employeeTypes = Enumerable.Empty<VmSelectList>();
            }
            ViewBag.Departments = departments;
            ViewBag.EmployeeTypes = employeeTypes;
            await Task.Delay(0);
            return View(vmEmployeeData);
        }
        #endregion

        #region "Employee Delete Related Methods"
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = _db.Employees.FirstOrDefault(f => f.EmployeeId == id);
                    if (existingData != null)
                    {
                        var empUserInfo = _db.AppUsers.FirstOrDefault(f => f.UserType == (int)ApplicationRoles.Employee && f.ReferenceId == existingData.EmployeeId);
                        if (empUserInfo != null)
                        {
                            //remove uploaded employee image
                            _fileUploadHelperService.RemoveFile(existingData.ProfilePicture, "employee-images");
                            _db.AppUsers.Remove(empUserInfo);
                        }
                        _db.Employees.Remove(existingData);
                        await _db.SaveChangesAsync();
                        sucMsg = ConstantUserMessages.EMPLOYEE_DELETED;
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
    }
}
