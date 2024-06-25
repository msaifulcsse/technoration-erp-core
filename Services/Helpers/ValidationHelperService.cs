using Microsoft.AspNetCore.Http;
using Services.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Services.Helpers
{
    public class ValidationHelperService : IValidationHelperService
    {
        public bool isValidEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool isValidMobileNumber(string number)
        {
            return Regex.IsMatch(number, "^01[0-9]{9}$") || Regex.IsMatch(number, @"^(\+88|88)[0-9]{11}$");
        }

        public bool isValidNationalId(string nationalId)
        {
            throw new NotImplementedException();
        }

        public bool isValidUserName(string username)
        {
            throw new NotImplementedException();
        }

        public bool isValidUrl(string url)
        {
            return false;
        }

        public bool isValidIp(string ip)
        {
            return Regex.IsMatch(ip, "^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$");
        }

        public bool IsValidFile(IFormFile file, string[] allowedExtensions, int maxFileSizeInByte, out string errorMsg)
        {
            errorMsg = null;
            var valid = true;
            try
            {
                var checkextension = Path.GetExtension(file.FileName) != null ? Path.GetExtension(file.FileName).ToLower() : Path.GetExtension(file.FileName);
                if (!allowedExtensions.Contains(checkextension))
                {
                    valid = false;
                    errorMsg = "Unsupported File extension! Supported extionsions are: " + string.Join(",", allowedExtensions.Select(p => p));
                }
                if (file.Length > maxFileSizeInByte)
                {
                    valid = false;
                    errorMsg = "File size is too large! Maximum Length is: " + maxFileSizeInByte;
                }
            }
            catch (Exception e)
            {

            }
            return valid;
        }

        public string GenerateUniqueName(IFormFile file)
        {
            string name = "";
            try
            {
                name = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            }
            catch (Exception e)
            {
                name = "unknown_file_" + Guid.NewGuid() +".jpg";
            }
            return name;
        }
    }
}
