using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Helpers.Interfaces
{
    public interface IValidationHelperService
    {
        bool isValidMobileNumber(string number);
        bool isValidEmail(string email);
        bool isValidNationalId(string nationalId);
        bool isValidUserName(string username);
        bool IsValidFile(IFormFile file, string[] allowedExtensions, int maxFileSizeInByte, out string errorMsg);
        string GenerateUniqueName(IFormFile file);
    }
}
