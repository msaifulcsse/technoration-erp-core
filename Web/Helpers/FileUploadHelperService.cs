using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

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
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    fileName = $"{fileNameInGuid}-{fileName.Trim()}{extension}";
                    string savingPath = Path.Combine(_env.WebRootPath, "uploads", directoryName);

                    if (!Directory.Exists(savingPath))
                    {
                        Directory.CreateDirectory(savingPath);
                    }

                    var filePath = Path.Combine(savingPath, fileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileSteam);
                    }
                    returnPath = $"/uploads/{directoryName}/{fileName}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on File Uploading: {ex.Message}");
                returnPath = "";
            }
            return returnPath;
        }

        public bool RemoveFile(string fileImgPath, string directoryName)
        {
            try
            {
                var fileName = Path.GetFileName(fileImgPath);
                var fullImgPath = Path.Combine(_env.WebRootPath, "uploads", directoryName, fileName);
                if (!File.Exists(fullImgPath))
                {
                    return false;
                }

                File.Delete(fullImgPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on File Deleting: {ex.Message}");
                return false;
            }
        }
    }//End of the class
}//End of the namespace