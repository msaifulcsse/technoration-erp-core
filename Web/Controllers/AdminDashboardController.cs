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
using Web.Helpers.Interfaces;

namespace Web.Controllers
{
    public class AdminDashboardController : Controller
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

        public AdminDashboardController(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, ICookieHelperService _cookieHelperService, ICredentialManagerService _credentialManager, IDisplayMessageHelper _displayMessageHelper, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService, IFileUploadHelperService _fileUploadHelperService)
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
            _displayMessageHelper.ErrorMessageSetOrGet(this, false);
            _displayMessageHelper.SuccessMessageSetOrGet(this, false);
            int totalNumberOfDepartments = 0;
            int totalNumberOfBatches = 0;
            int totalNumberOfTeachers = 0;
            int totalNumberOfStudents = 0;
            try
            {
                totalNumberOfDepartments = _db.Departments.AsQueryable().Select(s => s.DepartmentId).Count();
                totalNumberOfBatches = _db.StudentBatches.AsQueryable().Select(s => s.BatchId).Count();
                totalNumberOfTeachers = _db.Employees.AsQueryable().Select(s => s.EmployeeId).Count();
                totalNumberOfStudents = _db.Students.AsQueryable().Select(s => s.StudentId).Count();
            }
            catch (Exception ex)
            {
                totalNumberOfDepartments = 0;
                totalNumberOfBatches = 0;
                totalNumberOfTeachers = 0;
                totalNumberOfStudents = 0;
            }
            ViewBag.TotalDepartments = totalNumberOfDepartments;
            ViewBag.TotalBatches = totalNumberOfBatches;
            ViewBag.TotalTeachers = totalNumberOfTeachers;
            ViewBag.TotalStudents = totalNumberOfStudents;
            await Task.Delay(0);
            return View();
        }

        public IActionResult MyProfile()
        {
            return View();
        }
    }
}
