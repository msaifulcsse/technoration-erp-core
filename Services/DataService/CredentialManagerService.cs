using Entities.EntityModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Context;
using Repository.utility;
using Repository.Utility;
using Services.DataService.Interfaces;
using Services.EmailService;
using Services.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.DataService
{
    public class CredentialManagerService : ICredentialManagerService
    {
        #region "Declared objects & Constructor"
        private readonly ProjectDbContext _db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDateTimeHelperService _dateTimeHelperService;
        public Dictionary<string, int> userSignUpAttempt = new Dictionary<string, int>();

        public CredentialManagerService(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, IDateTimeHelperService _dateTimeHelperService)
        {
            this._db = _db;
            this.httpContextAccessor = httpContextAccessor;
            this._dateTimeHelperService = _dateTimeHelperService;
        }
        #endregion

        public string GetCurrentUserPublicIPAddress()
        {
            string ipAddress = "";
            try
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    ipAddress = stream.ReadToEnd();
                }
                int first = ipAddress.IndexOf("Address: ") + 9;
                int last = ipAddress.LastIndexOf("</body>");
                ipAddress = ipAddress.Substring(first, last - first);
            }
            catch (Exception ex)
            {
                ipAddress = "8.8.8.8";//If Not Found Any IPAddress Then Return Google IPAddress By Default
            }
            return ipAddress;
        }

        //public async Task<AuthenticationTracer> GetAuthenticationTracerInfoByBranchAndIP(int userId, int userType, string ipAddress)
        //{
        //    var prevLoginTraceData = new AuthenticationTracer();
        //    try
        //    {
        //        prevLoginTraceData = _db.AuthenticationTracers
        //       .Where(f => f.UserType == userType && f.ReferenceId == userId.ToString() && f.IPAddress == ipAddress && f.UserAgent == httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString())
        //       .OrderByDescending(o => o.TracerId).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        prevLoginTraceData = new AuthenticationTracer();
        //    }
        //    await Task.Delay(0);
        //    return prevLoginTraceData;
        //}

        public async Task UserAuthenticationTraceByIPAndUserInfo(AppUser userInfo, VmAuthenticationTracer vmAuthTracer, string ipAddress)
        {
            try
            {
                if (userInfo != null && !string.IsNullOrEmpty(ipAddress))
                {
                    var httpRequest = httpContextAccessor.HttpContext.Request;
                    var prevLoginTraceData = _db.AuthenticationTracers
                        .Where(f => f.UserType == userInfo.UserType && f.ReferenceId == userInfo.UserId.ToString() && f.IPAddress == ipAddress && f.UserAgent == httpRequest.Headers["User-Agent"].ToString())
                        .OrderByDescending(o => o.TracerId).FirstOrDefault();
                    if (prevLoginTraceData != null)
                    {
                        prevLoginTraceData.CountryName = vmAuthTracer.CountryName != null ? vmAuthTracer.CountryName : prevLoginTraceData.CountryName;
                        prevLoginTraceData.Region = vmAuthTracer.Region != null ? vmAuthTracer.Region : prevLoginTraceData.Region;
                        prevLoginTraceData.CityName = vmAuthTracer.CityName != null ? vmAuthTracer.CityName : prevLoginTraceData.CityName;
                        prevLoginTraceData.PostalCode = vmAuthTracer.PostalCode != null ? vmAuthTracer.PostalCode : prevLoginTraceData.PostalCode;
                        prevLoginTraceData.Latitude = vmAuthTracer.Latitude != null ? vmAuthTracer.Latitude : prevLoginTraceData.Latitude;
                        prevLoginTraceData.Longitude = vmAuthTracer.Longitude != null ? vmAuthTracer.Longitude : prevLoginTraceData.Longitude;
                        prevLoginTraceData.TimeZone = vmAuthTracer.TimeZone != null ? vmAuthTracer.TimeZone : prevLoginTraceData.TimeZone;
                        prevLoginTraceData.Organization = vmAuthTracer.Organization != null ? vmAuthTracer.Organization : prevLoginTraceData.Organization;
                        prevLoginTraceData.UserAgent = httpRequest.Headers["User-Agent"].ToString();
                        prevLoginTraceData.Status = (int)UserLoginActivity.LoggedIn;
                        prevLoginTraceData.UserType = userInfo.UserType;
                        prevLoginTraceData.ReferenceId = userInfo.UserId.ToString();
                        prevLoginTraceData.UpdatedBy = userInfo.UserId;
                        prevLoginTraceData.UpdatedDate = _dateTimeHelperService.Now();
                    }
                    else
                    {
                        var authUserTracer = new AuthenticationTracer()
                        {
                            IPAddress = ipAddress,
                            CountryName = vmAuthTracer.CountryName != null ? vmAuthTracer.CountryName : "",
                            Region = vmAuthTracer.Region != null ? vmAuthTracer.Region : "",
                            CityName = vmAuthTracer.CityName != null ? vmAuthTracer.CityName : "",
                            PostalCode = vmAuthTracer.PostalCode != null ? vmAuthTracer.PostalCode : "",
                            Latitude = vmAuthTracer.Latitude != null ? vmAuthTracer.Latitude : "",
                            Longitude = vmAuthTracer.Longitude != null ? vmAuthTracer.Longitude : "",
                            TimeZone = vmAuthTracer.TimeZone != null ? vmAuthTracer.TimeZone : "",
                            Organization = vmAuthTracer.Organization != null ? vmAuthTracer.Organization : "",
                            UserAgent = httpRequest.Headers["User-Agent"].ToString(),
                            Status = (int)UserLoginActivity.LoggedIn,
                            UserType = userInfo.UserType,
                            ReferenceId = userInfo.UserId.ToString(),
                            CreatedBy = userInfo.UserId,
                            CreatedDate = _dateTimeHelperService.Now(),
                            UpdatedBy = userInfo.UserId,
                            UpdatedDate = _dateTimeHelperService.Now()
                        };
                        _db.AuthenticationTracers.Add(authUserTracer);
                    }
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex) { }
        }

        public string GetUserRedirectControllerAndActionByUserInfo(AppUser userInfo, out string actionName)
        {
            actionName = "Index";
            string controllerName = "Account";
            try
            {
                var userRole = userInfo.UserRoles.FirstOrDefault().RoleInfo.RoleName;
                if (!string.IsNullOrEmpty(userRole) && userRole == ApplicationRoles.SuperAdmin.ToString())
                    controllerName = "AdminDashboard";
                else if (!string.IsNullOrEmpty(userRole) && userRole == ApplicationRoles.AppAdmin.ToString())
                    controllerName = "AdminDashboard";
                else if (!string.IsNullOrEmpty(userRole) && userRole == ApplicationRoles.Employee.ToString())
                    controllerName = "EmployeeDashboard";
                else if (!string.IsNullOrEmpty(userRole) && userRole == ApplicationRoles.Stundent.ToString())
                    controllerName = "StudentDashboard";
            }
            catch (Exception ex)
            {
                actionName = "Index";
                controllerName = "Account";
            }
            return controllerName;
        }

        /// <summary>
        /// Methods For SignUp Attempt Check & IP Block
        /// </summary>
        /// <param name="VmAuthTracer"></param>
        /// <returns></returns>
        public async Task UserSignUpCountCheek(VmAuthenticationTracer VmAuthTracer)
        {
            var authincationTracer = new AuthenticationTracer()
            {
                UserAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString(),
                IPAddress = VmAuthTracer.IPAddress != null ? VmAuthTracer.IPAddress : "",
                CountryName = VmAuthTracer.CountryName != null ? VmAuthTracer.CountryName : "",
                Region = VmAuthTracer.Region != null ? VmAuthTracer.Region : "",
                CityName = VmAuthTracer.CityName != null ? VmAuthTracer.CityName : "",
                PostalCode = VmAuthTracer.PostalCode != null ? VmAuthTracer.PostalCode : "",
                Latitude = VmAuthTracer.Latitude != null ? VmAuthTracer.Latitude : "",
                Longitude = VmAuthTracer.Longitude != null ? VmAuthTracer.Longitude : "",
                TimeZone = VmAuthTracer.TimeZone != null ? VmAuthTracer.TimeZone : "",
                Organization = VmAuthTracer.Organization != null ? VmAuthTracer.Organization : "",
                UserType = 0,
                ReferenceId = "0"
            };

            // repeat offense? add to the number of attempts
            if (!string.IsNullOrEmpty(VmAuthTracer.IPAddress) && !string.IsNullOrWhiteSpace(VmAuthTracer.IPAddress))
            {
                if (userSignUpAttempt.Any() && userSignUpAttempt.ContainsKey(VmAuthTracer.IPAddress))
                {
                    // increment number of failed attempts
                    ++userSignUpAttempt[VmAuthTracer.IPAddress];
                }
                else // first failed login attempt for given username
                {
                    // first failed attempt, so initialize it
                    userSignUpAttempt[VmAuthTracer.IPAddress] = 1;
                }
                var falideCount = this.userSignUpAttempt[VmAuthTracer.IPAddress];
                if (falideCount > 3)
                {
                    authincationTracer.Status = (int)UserLoginActivity.Blocked;
                    authincationTracer.RemarksOrNote = "Client signup attempted more than 3 times.";
                    await BlockUserIPFromClientSignUp(authincationTracer);
                }
            }
        }

        private async Task BlockUserIPFromClientSignUp(AuthenticationTracer authincationTracer)
        {
            try
            {
                if (authincationTracer != null)
                {
                    if (!string.IsNullOrEmpty(authincationTracer.IPAddress))
                    {
                        var existingData = _db.AuthenticationTracers.OrderByDescending(o => o.TracerId).FirstOrDefault(f => f.IPAddress == authincationTracer.IPAddress);
                        if (existingData != null)
                        {
                            existingData.Status = authincationTracer.Status;
                            existingData.RemarksOrNote = authincationTracer.RemarksOrNote;
                            existingData.UpdatedDate = _dateTimeHelperService.Now();
                        }
                        else
                        {
                            authincationTracer.UpdatedDate = _dateTimeHelperService.Now();
                            _db.AuthenticationTracers.Add(authincationTracer);
                        }
                        await _db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex) { }
        }

    }//End Of The Service
}//End Of The NameSpace