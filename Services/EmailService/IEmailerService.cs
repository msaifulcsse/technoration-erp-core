using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Helpers.Interfaces
{
    public interface IEmailerService
    {
        bool SendMail(string toEmail, string subject, string body, string fromEmail = null, string fromName = null, List<string> attachmentFilePathList = null, Stream mailStreamAttachment = null, string ccEmail = null, string bccEmail = null);

        Task<bool> SendMailAsync(string toEmail, string subject, string body, string fromEmail = null, string fromName = null, List<string> attachmentFilePathList = null, Stream mailStreamAttachment = null, string ccEmail = null, string bccEmail = null);

        bool SendMailWithStreamFileAttachment(string attachmentFileName, Stream mailStreamAttachment, string toEmail, string subject, string body, string fromEmail = null, string fromName = null, string ccEmail = null, string bccEmail = null);

        bool SendMailWithMemoryStreamFileAttachment(string attachmentFileName, byte[] mailStreamAttachment, string toEmail, string subject, string body, string fromEmail = null, string fromName = null, string ccEmail = null, string bccEmail = null);

        Task<bool> SendMailWithMemoryStreamFileAttachmentAsync(string attachmentFileName, byte[] mailStreamAttachment, string toEmail, string subject, string body, string fromEmail = null, string fromName = null, string ccEmail = null, string bccEmail = null);
        string GetEmailHtmlTemplate(string templatePath);

        bool SendMailWithMultipleMemoryStreamFileAttachment(List<VmEmailAttactment> attactments, string toEmail, string subject, string body, string fromEmail = null, string fromName = null, string ccEmail = null, string bccEmail = null);
    }
}
