using Entities.EntityModels;
using Entities.ViewModels;
using Repository.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Services.DataService.Interfaces
{
    public interface ICredentialManagerService
    {
        string GetCurrentUserPublicIPAddress();
        //Task<AuthenticationTracer> GetAuthenticationTracerInfoByBranchAndIP(int? branchId, int userId, int userType, string ipAddress);
        Task UserAuthenticationTraceByIPAndUserInfo(AppUser userInfo, VmAuthenticationTracer vmAuthTracer, string ipAddress);
        string GetUserRedirectControllerAndActionByUserInfo(AppUser userInfo, out string actionName);
        Task UserSignUpCountCheek(VmAuthenticationTracer VmAuthTracer);
    }
}