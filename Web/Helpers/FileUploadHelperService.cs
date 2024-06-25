using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers.Interfaces;

namespace Web.Helpers
{
    public class FileUploadHelperService : IFileUploadHelperService
    {
        #region "Constructor"
        private readonly IWebHostEnvironment _env;
        public FileUploadHelperService(IWebHostEnvironment env)
        {
            _env = env;
        }
        #endregion

        public string UploadFile(IFormFile file, string directoryName)
        {
            string returnPath = "";
            Guid fileNameInGuid = Guid.NewGuid();
            try
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = "";
                    string savingPath = (_env.WebRootPath + "\\uploads\\" + directoryName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\").Trim();

                    if (!Directory.Exists(savingPath))
                    {
                        Directory.CreateDirectory(savingPath);
                    }
                    //Use Namespace called :  System.IO  
                    fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    //To Get File Extension  
                    string extension = Path.GetExtension(file.FileName);
                    //Add Current Date To Attached File Name  
                    fileName = fileNameInGuid.ToString() + "-" + fileName.Trim() + extension;

                    savingPath = savingPath + fileName;
                    using (var fileSteam = new FileStream(savingPath, FileMode.Create))
                    {
                        file.CopyTo(fileSteam);
                    }
                    returnPath = "/uploads/" + directoryName + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + fileName;
                }
                return returnPath;
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                throw;
            }
        }
    }
}
