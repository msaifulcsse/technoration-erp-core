using Entities.EntityModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public class AccountController : Controller
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

        public AccountController(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, ICookieHelperService _cookieHelperService, ICredentialManagerService _credentialManager, IDisplayMessageHelper _displayMessageHelper, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService, IFileUploadHelperService _fileUploadHelperService)
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


        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CurrentIPAddress = _credentialManager.GetCurrentUserPublicIPAddress();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(VmUserLogin model)
        {
            if (ModelState.IsValid)
            {
                var encryptedPassword = _cryptoHelperService.Encrypt(model.Password);
                var userData = _db.AppUsers.Where(f => f.UserName == model.UserName && f.Password == encryptedPassword).Include("UserRoles").FirstOrDefault();
                if (userData != null && userData.IsActive != false)
                {
                    string actionName = "Index";
                    var currentIPAddress = _credentialManager.GetCurrentUserPublicIPAddress();
                    var roleId = userData.UserRoles.FirstOrDefault().RoleId;
                    var roleInfo = _db.AppRoles.FirstOrDefault(f => f.RoleId == roleId);
                    _cookieHelperService.SetUserInfoInCookie(userData.UserId + "_" + userData.UserName + "_" + userData.UserType + "_" + roleInfo.RoleName + "_" + model.RememberMe + "_" + currentIPAddress);
                    await _credentialManager.UserAuthenticationTraceByIPAndUserInfo(userData, model.VmAuthTracer, currentIPAddress);
                    string controllerName = _credentialManager.GetUserRedirectControllerAndActionByUserInfo(userData, out actionName);
                    _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.LOGIN_SUCCESS);
                    return RedirectToAction(actionName, controllerName);
                }
            }
            return View();
        }


        public IActionResult UserCredentialsCheck(string usrName, string usrPassword)
        {
            bool status = false;
            var encryptedPassword = _cryptoHelperService.Encrypt(usrPassword);
            var userData = _db.AppUsers.Where(f => f.UserName == usrName && f.Password == encryptedPassword).FirstOrDefault();
            if (userData != null && userData.IsActive != false)
                status = true;
            return Json(new { CHKStatus = status }, new JsonSerializerOptions { WriteIndented = true });
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LoggedOut()
        {
            await Task.Delay(0);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(VmStudentRegistration model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string proPicFilePath = _fileUploadHelperService.UploadFile(model.ProfilePicture, "Student");
                    var newStudent = new Student
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailAddress = model.EmailAddress,
                        ContactNumber = model.ContactNumber,
                        Gender = model.Gender,
                        Religion = model.Religion,
                        FatherName = model.FatherName,
                        MotherName = model.MotherName,
                        GurdianNumber = model.GurdianContactNumber,
                        PresentAddress = model.PresentAddress,
                        PermanentAddress = model.PermanentAddress,
                        NationalID = model.NationalID,
                        ProfilePicture = !string.IsNullOrEmpty(proPicFilePath) ? proPicFilePath : null,
                        AppliedTermsAndCondition = model.AppliedTermsAndCondition,
                        Type = StudentType.Regular,
                        CreatedBy = 1,
                        CreatedDate = _dateTimeHelperService.Now(),
                        UpdatedBy = 1,
                        UpdatedDate = _dateTimeHelperService.Now()
                    };
                    _db.Students.Add(newStudent);
                    await _db.SaveChangesAsync();
                    _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.REGISTRATION_SUCCESS);
                }
                catch (Exception ex) { }
            }
            return RedirectToAction("Registration");
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.REGISTRATION_SUCCESS);
            #region "Set .Net Core Claims"
            //var response = _userServiceManager.Login(model);
            //if (response.isSuccess == true)
            //{
            //    var userObject = (VMLoginModel)(response.data);

            //    var clims = new List<Claim>()
            //        {
            //            new Claim(ClaimTypes.Sid, userObject.Id.ToString()),
            //            new Claim(ClaimTypes.Name, userObject.Name),
            //            new Claim(ClaimTypes.Email, userObject.Email),
            //            new Claim(ClaimTypes.Role, (userObject.Role == null)? "User": userObject.Role.ToString())
            //        };


            //    var claimsIdentity = new ClaimsIdentity(clims, CookieAuthenticationDefaults.AuthenticationScheme);

            //    var authProperties = new AuthenticationProperties
            //    {
            //        // Remember me
            //        IsPersistent = model.RememberMe,

            //        //Till
            //        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)

            //    };

            //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //        new ClaimsPrincipal(claimsIdentity),
            //        authProperties);


            //    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.LOGIN_SUCCESS);
            //    return RedirectToAction("", "dashboard", new { area = "admin" });
            //}
            #endregion
            #region "Retrive .Net Core Claims"
            //var claimsId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            //Int64.TryParse(claimsId, out userId);
            //collection.CreatedId = userId;

            //var response = await _newsService.SaveUpdateNews(collection);

            //if (response.isSuccess == true)
            //{
            //    DisplayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.DATA_CREATED);
            //    return RedirectToAction("", "news", new { area = "admin" });
            //}
            #endregion
            return View();
        }
    }
}
