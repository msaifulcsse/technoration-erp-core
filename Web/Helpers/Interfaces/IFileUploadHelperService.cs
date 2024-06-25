using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers.Interfaces
{
    public interface IFileUploadHelperService
    {
        string UploadFile(IFormFile file, string directoryName);
    }
}
