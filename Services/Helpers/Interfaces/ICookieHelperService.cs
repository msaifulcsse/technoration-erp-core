using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers.Interfaces
{
    public interface ICookieHelperService
    {
        void SetUserInfoInCookie(string userInfo);
        int GetUserIdFromCookie();
        string GetUserNameFromCookie();
        string GetUserTypeFromCookie();
        string GetUserRoleFromCookie();
        bool GetRememberMeFromCookie();
        string GetIPAddressFromCookie();
    }
}
