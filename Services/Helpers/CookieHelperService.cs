using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Repository.utility;
using Repository.Utility;
using Services.Helpers.Interfaces;

namespace Services.Helpers
{
    public class CookieHelperService : ICookieHelperService
    {
        #region "Declared objects & Constructor"
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICryptoHelperService _cryptoHelperService;

        public CookieHelperService(IHttpContextAccessor _httpContextAccessor, ICryptoHelperService _cryptoHelperService)
        {
            this._httpContextAccessor = _httpContextAccessor;
            this._cryptoHelperService = _cryptoHelperService;
        }
        #endregion

        /// <summary>
        /// [0] => userData.UserId
        /// [1] => userData.UserName
        /// [2] => userData.UserType
        /// [3] => userRoles.RoleInfo.RoleName
        /// [4] => model.RememberMe
        /// [5] => currentIPAddress
        /// </summary>
        /// <param name="userInfo"></param>
        public void SetUserInfoInCookie(string userInfo)
        {
            var cookieOption = new CookieOptions();
            cookieOption.Expires = DateTime.Now.AddHours(60);
            var encryptedInfo = _cryptoHelperService.Encrypt(userInfo);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(UsersCookieInfo.AuthUserInfoInCookie.ToString(), encryptedInfo, cookieOption);
        }
        public int GetUserIdFromCookie()
        {
            int result = 0;
            try
            {
                #region "Old/Previous Way"
                //string loggedInUserID = "";
                //HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                //if (authCookie == null || string.IsNullOrWhiteSpace(authCookie.Value)) { loggedInUserID = "-1"; }
                //else
                //{
                //    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                //    if (!authTicket.Expired)
                //        loggedInUserID = authTicket.Name.Split(':')[0];
                //}
                //return int.Parse(loggedInUserID);

                //var claimInfo = _httpContextAccessor.HttpContext.User.FindAll(x => x.Type == "UserCookieData").FirstOrDefault();
                //if (claimInfo != null)
                //{
                //    //return long.Parse(claimInfo.Value);
                //    _httpContextAccessor.HttpContext.Authentication.SignIn("MyCookieMiddlewareInstance", claimInfo, new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddMinutes(20) });
                //}

                ////return 0;
                //var cookieData = request.Cookies["UserCookieData"];
                //if (cookieData != null)
                //{
                //    var expiry = cookieData;
                //    //if (String.IsNullOrEmpty(displayName) == false)
                //}
                //return 0;
                #endregion
                var authUserId = "0";
                var cookieData = _httpContextAccessor.HttpContext.Request.Cookies[UsersCookieInfo.AuthUserInfoInCookie.ToString()];
                if (!string.IsNullOrEmpty(cookieData))
                {
                    var userInfo = _cryptoHelperService.Decrypt(cookieData);
                    authUserId = !string.IsNullOrEmpty(userInfo) ? userInfo.Split('_')[0] : "0";
                    result = int.Parse(authUserId);
                }
            }
            catch (Exception ex) { result = 0; }
            return result;
        }
        public string GetUserNameFromCookie()
        {
            string result = "";
            try
            {
                var cookieData = _httpContextAccessor.HttpContext.Request.Cookies[UsersCookieInfo.AuthUserInfoInCookie.ToString()];
                if (!string.IsNullOrEmpty(cookieData))
                {
                    var userInfo = _cryptoHelperService.Decrypt(cookieData);
                    result = !string.IsNullOrEmpty(userInfo) ? userInfo.Split('_')[1] : "";
                }
            }
            catch (Exception ex) { result = ""; }
            return result;
        }
        public string GetUserTypeFromCookie()
        {
            string result = "";
            try
            {
                var cookieData = _httpContextAccessor.HttpContext.Request.Cookies[UsersCookieInfo.AuthUserInfoInCookie.ToString()];
                if (!string.IsNullOrEmpty(cookieData))
                {
                    var userInfo = _cryptoHelperService.Decrypt(cookieData);
                    result = !string.IsNullOrEmpty(userInfo) ? userInfo.Split('_')[2] : "";
                }
            }
            catch (Exception ex) { result = ""; }
            return result;
        }
        public string GetUserRoleFromCookie()
        {
            string result = "";
            try
            {
                var cookieData = _httpContextAccessor.HttpContext.Request.Cookies[UsersCookieInfo.AuthUserInfoInCookie.ToString()];
                if (!string.IsNullOrEmpty(cookieData))
                {
                    var userInfo = _cryptoHelperService.Decrypt(cookieData);
                    result = !string.IsNullOrEmpty(userInfo) ? userInfo.Split('_')[3] : "";
                }
            }
            catch (Exception ex) { result = ""; }
            return result;
        }
        public bool GetRememberMeFromCookie()
        {
            bool result = false;
            try
            {
                var rememberMe = "false";
                var cookieData = _httpContextAccessor.HttpContext.Request.Cookies[UsersCookieInfo.AuthUserInfoInCookie.ToString()];
                if (!string.IsNullOrEmpty(cookieData))
                {
                    var userInfo = _cryptoHelperService.Decrypt(cookieData);
                    rememberMe = !string.IsNullOrEmpty(userInfo) ? userInfo.Split('_')[4] : "false";
                    result = bool.Parse(rememberMe);
                }
            }
            catch (Exception ex) { result = false; }
            return result;
        }
        public string GetIPAddressFromCookie()
        {
            string result = "";
            try
            {
                var cookieData = _httpContextAccessor.HttpContext.Request.Cookies[UsersCookieInfo.AuthUserInfoInCookie.ToString()];
                if (!string.IsNullOrEmpty(cookieData))
                {
                    var userInfo = _cryptoHelperService.Decrypt(cookieData);
                    result = !string.IsNullOrEmpty(userInfo) ? userInfo.Split('_')[5] : "";
                }
            }
            catch (Exception ex) { result = ""; }
            return result;
        }

        #region "Comming Soon"
        //public static string UserRoleInCookie
        //{
        //    get
        //    {
        //        try
        //        {
        //            string role = "";
        //            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        //            if (authCookie == null || string.IsNullOrWhiteSpace(authCookie.Value)) { role = ""; }
        //            else
        //            {
        //                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //                if (!authTicket.Expired)
        //                    role = authTicket.Name.Split(':')[1];
        //            }
        //            return role;
        //        }
        //        catch (Exception ex) { return ""; }
        //    }
        //}

        //public static string UserNameInCookie
        //{
        //    get
        //    {
        //        try
        //        {
        //            var userName = "";
        //            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        //            if (authCookie == null || string.IsNullOrWhiteSpace(authCookie.Value)) { userName = ""; }
        //            else
        //            {
        //                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //                if (!authTicket.Expired)
        //                    userName = authTicket.Name.Split(':')[2];
        //            }
        //            return userName;
        //        }
        //        catch (Exception ex) { return ""; }
        //    }
        //}

        //public static bool UserRememberMeOptionsInCoockie
        //{
        //    set
        //    {
        //        try
        //        {
        //            string rememberMe = "false";
        //            if (value == true)
        //            { rememberMe = "true"; }
        //            var authTicket = new FormsAuthenticationTicket(rememberMe, true, 525600);//7200Mins~5Days => 525600Mins~365Days
        //            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        //            var authCookie = new HttpCookie(UserCookieData.RememberMePassword.ToString(), encryptedTicket);
        //            HttpContext.Current.Response.Cookies.Add(authCookie);
        //        }
        //        catch (Exception ex) { }
        //    }
        //}

        //public static bool RememberMeInCookie
        //{
        //    get
        //    {
        //        try
        //        {
        //            string rememberMe = "false";
        //            HttpCookie authCookie = HttpContext.Current.Request.Cookies[UserCookieData.RememberMePassword.ToString()];
        //            if (authCookie == null || string.IsNullOrWhiteSpace(authCookie.Value)) return false;
        //            else
        //            {
        //                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //                if (!authTicket.Expired)
        //                    rememberMe = authTicket.Name;
        //            }
        //            return bool.Parse(rememberMe);
        //        }
        //        catch (Exception ex) { return false; }
        //    }
        //}

        //public static string UserIPAddressInCookie
        //{
        //    get
        //    {
        //        try
        //        {
        //            var userIp = "";
        //            HttpCookie authCookie = HttpContext.Current.Request.Cookies[UserCookieData.AuthUserAnonymousInfoInCookie.ToString()];
        //            if (authCookie == null || string.IsNullOrWhiteSpace(authCookie.Value)) { userIp = ""; }
        //            else
        //            {
        //                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //                if (!authTicket.Expired)
        //                    userIp = authTicket.Name.Split('_')[1];
        //            }
        //            return userIp;
        //        }
        //        catch (Exception ex) { return ""; }
        //    }
        //}

        //public static void RefreshAllCookiesExpireTime()
        //{
        //    try
        //    {
        //        var now = DateTime.Now;
        //        var curCtxReq = HttpContext.Current.Request;
        //        var curCtxRes = HttpContext.Current.Response;
        //        //User Role info in cookie
        //        var cookieUserInfo = curCtxReq.Cookies[UserCookieData.UserIdUserRoleAndUserName.ToString()];//60Minutes
        //        var userInfoTicket = cookieUserInfo != null && !string.IsNullOrEmpty(cookieUserInfo.Value) ? FormsAuthentication.Decrypt(cookieUserInfo.Value) : null;
        //        //User Remember in cookie
        //        var cookieRememberMe = curCtxReq.Cookies[UserCookieData.RememberMePassword.ToString()];//365Days
        //        var rememberMeTicket = cookieRememberMe != null && !string.IsNullOrEmpty(cookieRememberMe.Value) ? FormsAuthentication.Decrypt(cookieRememberMe.Value) : null;
        //        //UserCreated Date in cookie
        //        var cookieUCreatedDate = curCtxReq.Cookies[UserCookieData.UserCreatedDate.ToString()];//60Minutes
        //        var uCreatedDateTicket = cookieUCreatedDate != null && !string.IsNullOrEmpty(cookieUCreatedDate.Value) ? FormsAuthentication.Decrypt(cookieUCreatedDate.Value) : null;
        //        //User mobile verification in cookie
        //        var cookieUVerifyCode = curCtxReq.Cookies[UserCookieData.AuthClientMobileVerification.ToString()];//10Minutes
        //        var uVerifyCodeTicket = cookieUVerifyCode != null && !string.IsNullOrEmpty(cookieUVerifyCode.Value) ? FormsAuthentication.Decrypt(cookieUVerifyCode.Value) : null;
        //        //Anonymous Info in cookie
        //        var anonymousInfo = curCtxReq.Cookies[UserCookieData.AuthUserAnonymousInfoInCookie.ToString()];
        //        var anonymousInfoTik = anonymousInfo != null && !string.IsNullOrEmpty(anonymousInfo.Value) ? FormsAuthentication.Decrypt(anonymousInfo.Value) : null;

        //        var timeout = 59.99;
        //        if (RememberMeInCookie)//If remember me in cookie
        //            timeout = 525599.99; //Refreshed 1 year from now 
        //        else
        //            timeout = 59.99; //Refreshed 1 hour from now 

        //        if (userInfoTicket != null && !userInfoTicket.Expired && userInfoTicket.Expiration >= now)
        //        {
        //            var newticket = new FormsAuthenticationTicket(userInfoTicket.Version, userInfoTicket.Name, userInfoTicket.IssueDate, now.AddMinutes(timeout), userInfoTicket.IsPersistent, userInfoTicket.UserData, userInfoTicket.CookiePath);
        //            cookieUserInfo.Value = FormsAuthentication.Encrypt(newticket);
        //            cookieUserInfo.Expires = DateTimeHelpers.ConvertAnyDateTimeToSystemDateTimeObject(newticket.Expiration);
        //            curCtxRes.Cookies.Set(cookieUserInfo);
        //        }
        //        if (rememberMeTicket != null && !rememberMeTicket.Expired && rememberMeTicket.Expiration >= now)
        //        {
        //            var newticket = new FormsAuthenticationTicket(rememberMeTicket.Version, rememberMeTicket.Name, rememberMeTicket.IssueDate, now.AddMinutes(timeout), rememberMeTicket.IsPersistent, rememberMeTicket.UserData, rememberMeTicket.CookiePath);
        //            cookieRememberMe.Value = FormsAuthentication.Encrypt(newticket);
        //            cookieRememberMe.Expires = DateTimeHelpers.ConvertAnyDateTimeToSystemDateTimeObject(newticket.Expiration);
        //            curCtxRes.Cookies.Set(cookieRememberMe);
        //        }
        //        if (uCreatedDateTicket != null && !uCreatedDateTicket.Expired && uCreatedDateTicket.Expiration >= now)
        //        {
        //            var newticket = new FormsAuthenticationTicket(uCreatedDateTicket.Version, uCreatedDateTicket.Name, uCreatedDateTicket.IssueDate, now.AddMinutes(timeout), uCreatedDateTicket.IsPersistent, uCreatedDateTicket.UserData, uCreatedDateTicket.CookiePath);
        //            cookieUCreatedDate.Value = FormsAuthentication.Encrypt(newticket);
        //            cookieUCreatedDate.Expires = DateTimeHelpers.ConvertAnyDateTimeToSystemDateTimeObject(newticket.Expiration);
        //            curCtxRes.Cookies.Set(cookieUCreatedDate);
        //        }
        //        if (uVerifyCodeTicket != null && !uVerifyCodeTicket.Expired && uVerifyCodeTicket.Expiration >= now)
        //        {
        //            var newticket = new FormsAuthenticationTicket(uVerifyCodeTicket.Version, uVerifyCodeTicket.Name, uVerifyCodeTicket.IssueDate, now.AddMinutes(9.99), uVerifyCodeTicket.IsPersistent, uVerifyCodeTicket.UserData, uVerifyCodeTicket.CookiePath);
        //            cookieUVerifyCode.Value = FormsAuthentication.Encrypt(newticket);
        //            cookieUVerifyCode.Expires = DateTimeHelpers.ConvertAnyDateTimeToSystemDateTimeObject(newticket.Expiration);
        //            curCtxRes.Cookies.Set(cookieUVerifyCode);
        //        }
        //        if (anonymousInfoTik != null && !anonymousInfoTik.Expired && anonymousInfoTik.Expiration >= now)
        //        {
        //            var newticket = new FormsAuthenticationTicket(anonymousInfoTik.Version, anonymousInfoTik.Name, anonymousInfoTik.IssueDate, now.AddMinutes(timeout), anonymousInfoTik.IsPersistent, anonymousInfoTik.UserData, anonymousInfoTik.CookiePath);
        //            anonymousInfo.Value = FormsAuthentication.Encrypt(newticket);
        //            anonymousInfo.Expires = DateTimeHelpers.ConvertAnyDateTimeToSystemDateTimeObject(newticket.Expiration);
        //            curCtxRes.Cookies.Set(anonymousInfo);
        //        }
        //    }
        //    catch (Exception ex) { }
        //}

        //public static void ClearCurrentUserSessionCookie()
        //{
        //    try
        //    {
        //        var authCookie = HttpContext.Current.Request.Cookies[UserCookieData.UserIdUserRoleAndUserName.ToString()];
        //        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Name))
        //        {
        //            authCookie.Expires = DateTime.Today.AddDays(-1);
        //            HttpContext.Current.Response.Cookies.Add(authCookie);
        //        }
        //        FormsAuthentication.SignOut();
        //    }
        //    catch (Exception ex) { }

        //    List<string> cookieKeyNameList = new List<string>();
        //    foreach (var key in HttpContext.Current.Request.Cookies.Keys)
        //    {
        //        try
        //        {
        //            cookieKeyNameList.Add(key.ToString());
        //        }
        //        catch (Exception ex) { }
        //    }
        //    if (cookieKeyNameList != null && cookieKeyNameList.Count > 0)
        //    {
        //        var response = HttpContext.Current.Response;
        //        var request = HttpContext.Current.Request;
        //        foreach (string cookieKey in cookieKeyNameList)
        //        {
        //            try
        //            {
        //                if (cookieKey != "__RequestVerificationToken")
        //                {
        //                    HttpCookie cookie = request.Cookies[cookieKey];
        //                    response.Cookies.Remove(cookieKey);
        //                    cookie.Value = null;
        //                    cookie.Expires = DateTime.Now.AddDays(-1);
        //                    response.SetCookie(cookie);
        //                }
        //            }
        //            catch (Exception ex) { }
        //        }
        //    }
        //}

        //public static void ClearCurrentUserCustomSessionCookie()
        //{
        //    try
        //    {
        //        var authCookie = HttpContext.Current.Request.Cookies[UserCookieData.UserIdUserRoleAndUserName.ToString()];
        //        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Name))
        //        {
        //            authCookie.Expires = DateTime.Today.AddDays(-1);
        //            HttpContext.Current.Response.Cookies.Add(authCookie);
        //        }
        //        FormsAuthentication.SignOut();
        //    }
        //    catch (Exception ex) { }

        //    List<string> cookieKeyNameList = new List<string>();
        //    foreach (var key in HttpContext.Current.Request.Cookies.Keys)
        //    {
        //        try
        //        {
        //            cookieKeyNameList.Add(key.ToString());
        //        }
        //        catch (Exception ex) { }
        //    }
        //    if (cookieKeyNameList != null && cookieKeyNameList.Count > 0)
        //    {
        //        var response = HttpContext.Current.Response;
        //        var request = HttpContext.Current.Request;
        //        foreach (string cookieKey in cookieKeyNameList)
        //        {
        //            try
        //            {
        //                if (cookieKey != null)
        //                {
        //                    HttpCookie cookie = request.Cookies[cookieKey];
        //                    response.Cookies.Remove(cookieKey);
        //                    cookie.Value = null;
        //                    cookie.Expires = DateTime.Now.AddDays(-1);
        //                    response.SetCookie(cookie);
        //                }
        //            }
        //            catch (Exception ex) { }
        //        }
        //    }
        //}
        #endregion
    }//End Class
}//End Namesoace
