using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Repository.utility;
using Services.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web.FilterAttributes
{
    //internal class TestToastAuthorizeAttribute : AuthorizeAttribute
    //{
    //    public override new string Roles()
    //    {
    //        return "";
    //    }
    //}

    //public class PermissionProvider : IPermissionProvider
    //{
    //    private readonly IServerConfiguration _ServerConfiguration;
    //    private readonly IUserCache _UserCache;
    //    private readonly string _AdministratorPermission;

    //    public PermissionProvider(
    //        IServerConfiguration serverConfiguration
    //        , IUserCache userCache)
    //    {

    //        _ServerConfiguration = serverConfiguration;
    //        _UserCache = userCache;
    //        _AdministratorPermission = _ServerConfiguration.AdministratorPermissionName;
    //    }

    //    public bool IsUserAuthorized(IPrincipal principal, string permission)
    //    {
    //        if (string.IsNullOrWhiteSpace(permission)) return true;
    //        var user = _UserCache.GetUserFromPrincipal(principal);
    //        if (user == null) return false;

    //        //User has Permission and Permission is Valid 
    //        if (user.ApplicationPermissions.Any(i => i.IsValid
    //               && i.Permission.Equals(permission, StringComparison.OrdinalIgnoreCase)))
    //        {

    //            return true;
    //        }

    //        //User has Administrator Permission and Permission is Valid 
    //        if (user.ApplicationPermissions.Any(i => i.IsValid
    //               && i.Permission.Equals(_AdministratorPermission, StringComparison.OrdinalIgnoreCase)))
    //        {

    //            return true;
    //        }
    //        return false;
    //    }
    //}

    //public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    //{
    //    public IEnumerable<string> RequiredPermissions { get; }

    //    public PermissionAuthorizationRequirement(IEnumerable<string> requiredPermissions)
    //    {
    //        RequiredPermissions = requiredPermissions;
    //    }
    //}

    //public class RequiresPermissionAttribute : TypeFilterAttribute
    //{
    //    public RequiresPermissionAttribute(params string[] permissions) : base(typeof(RequiresPermissionAttributeExecutor))
    //    {
    //        Arguments = new[] { new PermissionAuthorizationRequirement(permissions) };
    //    }


    //    private class RequiresPermissionAttributeExecutor : Attribute
    //    {
    //        private readonly Microsoft.Extensions.Logging.ILogger _logger;
    //        private readonly PermissionAuthorizationRequirement _requiredPermissions;
    //        private readonly ICookieHelperService _cookieHelperService;

    //        public RequiresPermissionAttributeExecutor(ILogger<RequiresPermissionAttribute> logger, PermissionAuthorizationRequirement requiredPermissions, ICookieHelperService _cookieHelperService)
    //        {

    //            _logger = logger;
    //            _requiredPermissions = requiredPermissions;
    //            this._cookieHelperService = _cookieHelperService;
    //        }

    //        public async Task OnResourceExecutionAsync(ResourceExecutingContext context)
    //        {
    //            bool isInOneOfThisRole = false;
    //            if (isInOneOfThisRole == false)
    //            {
    //                context.Result = new UnauthorizedResult();
    //                await context.Result.ExecuteResultAsync(context);
    //            }
    //        }
    //    }
    //}

    //public class RequiresPermissionAttribute : TypeFilterAttribute
    //{

    //    public RequiresPermissionAttribute(params string[] permissions) : base(typeof(RequiresPermissionAttributeExecutor))
    //    {
    //        Arguments = new[] { new PermissionAuthorizationRequirement(permissions) };
    //    }


    //    private class RequiresPermissionAttributeExecutor : Attribute, IAsyncResourceFilter
    //    {
    //        private readonly ICookieHelperService _cookieHelperService;

    //        public RequiresPermissionAttributeExecutor(ICookieHelperService cookieHelperService)
    //        {
    //            _cookieHelperService = cookieHelperService;
    //        }

    //        public async Task OnResourceExecutionAsync(ResourceExecutingContext context)
    //        {

    //            //    var principal = new AppPrincipal(_PermissionProvider, context.HttpContext.User.Identity);
    //            //bool isInOneOfThisRole = false; 

    //            //    foreach (var item in _requiredPermissions.RequiredPermissions) { 
    //            //        if (principal.IsInRole(item)) { 
    //            //            isInOneOfThisRole = true; 
    //            //        }

    //            bool isInOneOfThisRole = true;
    //            if (isInOneOfThisRole == false)
    //            {
    //                context.Result = new UnauthorizedResult();
    //                await context.Result.ExecuteResultAsync(context);
    //            }
    //        }
    //    }
    //}


    //internal class MinimumAgeAuthorizeAttribute : AuthorizeAttribute
    //{
    //    const string POLICY_PREFIX = "MinimumAge";

    //    public MinimumAgeAuthorizeAttribute(int age) => Age = age;

    //    // Get or set the Age property by manipulating the underlying Policy property
    //    public int Age
    //    {
    //        get
    //        {
    //            if (int.TryParse(Policy.Substring(POLICY_PREFIX.Length), out var age))
    //            {
    //                return age;
    //            }
    //            return default(int);
    //        }
    //        set
    //        {
    //            Policy = $"{POLICY_PREFIX}{value.ToString()}";
    //        }
    //    }
    //}

    //public class AdminDynamicAuthorizeAttribute : AuthorizeAttribute
    //{
    //    public IUnitOfWork unitOfWork { get; set; }
    //    public ApplicationDbContext _db { get; set; }

    //    protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
    //    {
    //        if (httpContext.Request.Url.ToString().Contains("Images"))
    //        {
    //        }
    //        try
    //        {
    //            int? cookieBranchId = CookieHelperService.CompanyBranchInCookie;
    //            if (cookieBranchId == 0) { cookieBranchId = null; }
    //            var cookieUserId = CookieHelperService.UserIdInCookie;
    //            var cookieUserType = CookieHelperService.CompanyUserTypeInCookie;
    //            var cookieUserName = CookieHelperService.UserNameInCookie;
    //            var cookieUserRole = CookieHelperService.UserRoleInCookie;
    //            var cookieIpAddress = CookieHelperService.UserIPAddressInCookie;

    //            var authTracerInfo = unitOfWork.AuthenticationTracerRepo
    //                .Find(f => f.UserType == cookieUserType && f.ReferenceId == cookieUserId.ToString() && f.IPAddress == cookieIpAddress && f.UserAgent == httpContext.Request.UserAgent)
    //                .OrderByDescending(o => o.TracerId).FirstOrDefault();
    //            if (authTracerInfo == null || authTracerInfo.Status == (int)UserLoginActivity.LoggedOut)
    //            { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            if (httpContext.Request.IsAjaxRequest()) { CookieHelperService.RefreshAllCookiesExpireTime(); return true; }
    //            if (!AccessHelperService.HasSubscription(unitOfWork)) { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }

    //            var today = DateTime.Today;
    //            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"];
    //            var actionName = httpContext.Request.RequestContext.RouteData.Values["action"];
    //            var url = "/" + controllerName + "/" + actionName;
    //            if (actionName.Equals("Index"))
    //            { url = "/" + controllerName; }

    //            bool isExistsInRole = false;
    //            var menuIds = new List<int>();
    //            var userroles = new List<int>();
    //            var userInfo = _db.Users.FirstOrDefault(f => f.BranchId == cookieBranchId && f.Id == cookieUserId);
    //            if (userInfo != null)
    //            { userroles = userInfo.Roles.Select(s => s.RoleId).ToList(); }

    //            if (cookieUserRole.ToLower() == AppRole.MACResellerAdmin.ToString().ToLower())
    //            {

    //            }

    //            if (!isExistsInRole)
    //            { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            CookieHelperService.RefreshAllCookiesExpireTime();
    //            var userrolenames = string.Join(",", _db.Roles.Where(w => userroles.Contains(w.Id)).ToList().Select(s => s.Name.Split('-')[0]).ToList());
    //            if (userrolenames.Length == 0) { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            Roles = userrolenames;

    //            return base.AuthorizeCore(httpContext);
    //        }
    //        catch (Exception ex)
    //        { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //    }
    //}

    //public class DynamicBandwidthResellerAuthorize : AuthorizeAttribute
    //{
    //    public IUnitOfWork unitOfWork { get; set; }
    //    public ApplicationDbContext _db { get; set; }

    //    protected override bool AuthorizeCore(HttpContextBase httpContext)
    //    {
    //        try
    //        {
    //            int? cookieBranchId = CookieHelperService.CompanyBranchInCookie; if (cookieBranchId == 0) { cookieBranchId = null; }
    //            var cookieUserId = CookieHelperService.UserIdInCookie;
    //            var cookieUserType = CookieHelperService.CompanyUserTypeInCookie;
    //            var cookieUserName = CookieHelperService.UserNameInCookie;
    //            var cookieUserRole = CookieHelperService.UserRoleInCookie;
    //            var cookieIpAddress = CookieHelperService.UserIPAddressInCookie;

    //            var authTracerInfo = unitOfWork.AuthenticationTracerRepo
    //                .Find(f => f.UserType == cookieUserType && f.ReferenceId == cookieUserId.ToString() && f.IPAddress == cookieIpAddress && f.UserAgent == httpContext.Request.UserAgent)
    //                .OrderByDescending(o => o.TracerId).FirstOrDefault();
    //            if (authTracerInfo == null || authTracerInfo.Status == (int)UserLoginActivity.LoggedOut)
    //            { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            if (httpContext.Request.IsAjaxRequest()) { CookieHelperService.RefreshAllCookiesExpireTime(); return true; }
    //            if (!AccessHelperService.HasSubscription(unitOfWork)) { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }

    //            bool hasAccessPermission = false;
    //            var userroles = new List<int>();

    //            if (cookieUserId > 0 && !string.IsNullOrEmpty(cookieUserName) && cookieUserRole.ToLower() == AppRole.BandwidthResellerAdmin.ToString().ToLower())
    //            {
    //                var userInfo = _db.Users.FirstOrDefault(f => f.BranchId == cookieBranchId && f.Id == cookieUserId);
    //                if (userInfo != null)
    //                { userroles = userInfo.Roles.Select(s => s.RoleId).ToList(); }

    //                if (userInfo != null && userInfo.UserType != null && userInfo.UserType.Contains("BRA-"))
    //                {
    //                    var resellerId = Convert.ToInt32(userInfo.UserType.Remove(0, 4));
    //                    var resellerInfo = unitOfWork.ResellerRepo.Get(resellerId);
    //                    if (resellerInfo != null && !string.IsNullOrEmpty(resellerInfo.CompanyName))
    //                    { hasAccessPermission = true; }
    //                }
    //            }

    //            if (!hasAccessPermission)
    //            { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            CookieHelperService.RefreshAllCookiesExpireTime();
    //            var userrolenames = string.Join(",", _db.Roles.Where(w => userroles.Contains(w.Id)).ToList().Select(s => s.Name.Split('-')[0]).ToList());
    //            if (userrolenames.Length == 0) { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            Roles = userrolenames;

    //            return base.AuthorizeCore(httpContext);
    //        }
    //        catch (Exception ex)
    //        { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //    }
    //}

    //public class ClientPortalDynamicAuthorize : AuthorizeAttribute
    //{
    //    public IUnitOfWork unitOfWork { get; set; }
    //    public ApplicationDbContext _db { get; set; }

    //    protected override bool AuthorizeCore(HttpContextBase httpContext)
    //    {
    //        try
    //        {
    //            int? cookieBranchId = CookieHelperService.CompanyBranchInCookie; if (cookieBranchId == 0) { cookieBranchId = null; }
    //            var cookieUserId = CookieHelperService.UserIdInCookie;
    //            var cookieUserType = CookieHelperService.CompanyUserTypeInCookie;
    //            var cookieUserName = CookieHelperService.UserNameInCookie;
    //            var cookieUserRole = CookieHelperService.UserRoleInCookie;
    //            var cookieIpAddress = CookieHelperService.UserIPAddressInCookie;

    //            var authTracerInfo = unitOfWork.AuthenticationTracerRepo
    //              .Find(f => f.UserType == cookieUserType && f.ReferenceId == cookieUserId.ToString() && f.IPAddress == cookieIpAddress && f.UserAgent == httpContext.Request.UserAgent)
    //              .OrderByDescending(o => o.TracerId).FirstOrDefault();
    //            if (authTracerInfo == null || authTracerInfo.Status == (int)UserLoginActivity.LoggedOut)
    //            { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            if (httpContext.Request.IsAjaxRequest()) { CookieHelperService.RefreshAllCookiesExpireTime(); return true; }
    //            if (!AccessHelperService.HasSubscription(unitOfWork)) { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }

    //            bool hasAccessPermission = false;
    //            var userroles = new List<int>();

    //            if (cookieUserId > 0 && !string.IsNullOrEmpty(cookieUserName) && !string.IsNullOrEmpty(cookieUserRole) && cookieUserRole.ToLower() == AppRole.Client.ToString().ToLower())
    //            {
    //                var userInfo = _db.Users.FirstOrDefault(f => f.BranchId == cookieBranchId && f.Id == cookieUserId);
    //                if (userInfo != null)
    //                { userroles = userInfo.Roles.Select(s => s.RoleId).ToList(); }

    //                if (userInfo != null && userInfo.UserType != null && userInfo.UserType.Contains("CL-") && userInfo.IsActive == true)
    //                {
    //                    var cusHeadId = Convert.ToInt32(userInfo.UserType.Remove(0, 3));
    //                    var customerInfo = unitOfWork.CustomerRepo.Get(cusHeadId);
    //                    if (customerInfo != null && customerInfo.CustomerHeaderId > 0 && !string.IsNullOrEmpty(customerInfo.CustomerName) && !string.IsNullOrEmpty(customerInfo.UserName))
    //                    {
    //                        hasAccessPermission = true;
    //                    }
    //                }
    //            }

    //            if (!hasAccessPermission)
    //            { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            CookieHelperService.RefreshAllCookiesExpireTime();
    //            var userrolenames = string.Join(",", _db.Roles.Where(w => userroles.Contains(w.Id)).ToList().Select(s => s.Name.Split('-')[0]).ToList());
    //            if (userrolenames.Length == 0) { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            Roles = userrolenames;
    //            return base.AuthorizeCore(httpContext);
    //        }
    //        catch (Exception ex)
    //        { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //    }
    //}

    //public class OutsidePaymentByURLDA : AuthorizeAttribute
    //{
    //    public IUnitOfWork unitOfWork { get; set; }
    //    protected override bool AuthorizeCore(HttpContextBase httpContext)
    //    {
    //        try
    //        {
    //            var cookieUserId = CookieHelperService.CurrentClientCode;
    //            var cookieUserType = CookieHelperService.CompanyUserTypeInCookie;
    //            var cookieIpAddress = CookieHelperService.UserIPAddressInCookie;

    //            var authTracerInfo = unitOfWork.AuthenticationTracerRepo
    //                .Find(f => f.UserType == cookieUserType && f.ReferenceId == cookieUserId.ToString() && f.IPAddress == cookieIpAddress && f.UserAgent == httpContext.Request.UserAgent)
    //                .OrderByDescending(o => o.TracerId).FirstOrDefault();
    //            if (authTracerInfo == null || authTracerInfo.Status == (int)UserLoginActivity.LoggedOut)
    //            { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //            return true;
    //        }
    //        catch (Exception ex)
    //        { CookieHelperService.ClearCurrentUserSessionCookie(); return false; }
    //    }
    //}
}