using System;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.utility;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.FilterAttributes
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        //public IUnitOfWork unitOfWork { get; set; }
        //public ApplicationDbContext _db { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var controllerName = filterContext.RouteData.Values["controller"];
            //var actionName = filterContext.RouteData.Values["action"];
            //    //ActionLog log = new ActionLog()
            //    //{
            //    //    Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            //    //    Action = string.Concat(filterContext.ActionDescriptor.ActionName, " (Logged By: Custom Action Filter)"),
            //    //    IP = filterContext.HttpContext.Request.UserHostAddress,
            //    //    DateTime = filterContext.HttpContext.Timestamp
            //    //};
            //    //db.ActionLogs.Add(log);
            //    //db.SaveChanges();
            //var message = String.Format("{0} controller:{1} action:{2}", "onactionexecuting", controllerName, actionName);
            //Debug.WriteLine(message, "Action Filter Log");
            base.OnActionExecuting(filterContext);
        }
    }

    public class RefreshCookiesExpActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {
                //var now = BdDateTime.Now();
                //var curCtxReq = HttpContext.Current.Request;
                //var curCtxRes = HttpContext.Current.Response;
                //var cookieUserInfo = curCtxReq.Cookies[UserCookieData.UserIdUserRoleAndUserName.ToString()];//60Minutes
                //var userInfoTicket = cookieUserInfo != null && !string.IsNullOrEmpty(cookieUserInfo.Value) ? FormsAuthentication.Decrypt(cookieUserInfo.Value) : null;

                //var cookieRememberMe = curCtxReq.Cookies[UserCookieData.RememberMePassword.ToString()];//365Days
                //var rememberMeTicket = cookieRememberMe != null && !string.IsNullOrEmpty(cookieRememberMe.Value) ? FormsAuthentication.Decrypt(cookieRememberMe.Value) : null;

                //var cookieUCreatedDate = curCtxReq.Cookies[UserCookieData.UserCreatedDate.ToString()];//60Minutes
                //var uCreatedDateTicket = cookieUCreatedDate != null && !string.IsNullOrEmpty(cookieUCreatedDate.Value) ? FormsAuthentication.Decrypt(cookieUCreatedDate.Value) : null;

                //var cookieUVerifyCode = curCtxReq.Cookies[UserCookieData.AuthClientMobileVerification.ToString()];//10Minutes
                //var uVerifyCodeTicket = cookieUVerifyCode != null && !string.IsNullOrEmpty(cookieUVerifyCode.Value) ? FormsAuthentication.Decrypt(cookieUVerifyCode.Value) : null;

                //if (userInfoTicket != null && !userInfoTicket.Expired && userInfoTicket.Expiration >= now)
                //{
                //    var newticket = new FormsAuthenticationTicket(userInfoTicket.Version, userInfoTicket.Name, userInfoTicket.IssueDate, now.AddMinutes(59.99), userInfoTicket.IsPersistent, userInfoTicket.UserData, userInfoTicket.CookiePath);
                //    cookieUserInfo.Value = FormsAuthentication.Encrypt(newticket);
                //    cookieUserInfo.Expires = newticket.Expiration;
                //    curCtxRes.Cookies.Set(cookieUserInfo);
                //}
                //if (rememberMeTicket != null && !rememberMeTicket.Expired && rememberMeTicket.Expiration >= now)
                //{
                //    var newticket = new FormsAuthenticationTicket(rememberMeTicket.Version, rememberMeTicket.Name, rememberMeTicket.IssueDate, now.AddDays(364.99), rememberMeTicket.IsPersistent, rememberMeTicket.UserData, rememberMeTicket.CookiePath);
                //    cookieRememberMe.Value = FormsAuthentication.Encrypt(newticket);
                //    cookieRememberMe.Expires = newticket.Expiration;
                //    curCtxRes.Cookies.Set(cookieRememberMe);
                //}
                //if (uCreatedDateTicket != null && !uCreatedDateTicket.Expired && uCreatedDateTicket.Expiration >= now)
                //{
                //    var newticket = new FormsAuthenticationTicket(uCreatedDateTicket.Version, uCreatedDateTicket.Name, uCreatedDateTicket.IssueDate, now.AddMinutes(59.99), uCreatedDateTicket.IsPersistent, uCreatedDateTicket.UserData, uCreatedDateTicket.CookiePath);
                //    cookieUCreatedDate.Value = FormsAuthentication.Encrypt(newticket);
                //    cookieUCreatedDate.Expires = newticket.Expiration;
                //    curCtxRes.Cookies.Set(cookieUCreatedDate);
                //}
                //if (uVerifyCodeTicket != null && !uVerifyCodeTicket.Expired && uVerifyCodeTicket.Expiration >= now)
                //{
                //    var newticket = new FormsAuthenticationTicket(uVerifyCodeTicket.Version, uVerifyCodeTicket.Name, uVerifyCodeTicket.IssueDate, now.AddMinutes(9.99), uVerifyCodeTicket.IsPersistent, uVerifyCodeTicket.UserData, uVerifyCodeTicket.CookiePath);
                //    cookieUVerifyCode.Value = FormsAuthentication.Encrypt(newticket);
                //    cookieUVerifyCode.Expires = newticket.Expiration;
                //    curCtxRes.Cookies.Set(cookieUVerifyCode);
                //}
            }
            catch (Exception ex) { }
            base.OnResultExecuted(filterContext);
        }
    }
}