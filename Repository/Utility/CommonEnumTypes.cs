using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Utility
{
    public enum CompanyUserType
    {
        SuperAdmin = 1,
        AppAdmin = 2,
        Employee = 3,
        Student = 4,
        Others = 5
    }

    public enum UserLoginActivity
    {
        LoggedIn = 1,
        LoggedOut = 2,
        Blocked = 3,
        Registration = 4,
        Anonymous = 5
    }
}
