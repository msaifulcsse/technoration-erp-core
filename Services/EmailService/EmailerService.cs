using Entities.ViewModels;
using Repository.Context;
using Services.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.EmailService
{
    public class EmailerService : IEmailerService
    {
        #region Constructor & Default Data
        /// <summary>
        /// UserName: ispdigital2020@gmail.com
        /// Password: NewGen-Digital#20@SoftifyBD
        /// Server: smtp.gmail.com
        /// Port = 587;
        ///EnableSsl = true;
        /// </summary>

        /// <summary>
        /// UserName: noreply@softifybd.net
        /// Password: whisper-121
        /// Server: mail.softifybd.net
        /// Port = 587;
        ///EnableSsl = false;
        /// </summary>

        /// <summary>
        /// UserName: noreply@softifybd.com
        /// Password: Whisper-121@20
        /// Server: mail.softifybd.com
        /// Port = 25;
        ///EnableSsl = false;
        /// </summary>
        private NetworkCredential networkCredential = new NetworkCredential("noreply@softifybd.com", @"Whisper-121@20");
        private string fromEmail = "noreply@softifybd.com";
        private string fromName = "ISP Digital";
        private string host = "mail.softifybd.com";
        private int port = 25;
        private bool EnableSsl = false;

        public EmailerService(ProjectDbContext _db)
        {
            //var mailConfig = _db.MailConfigurations.Fir();
            //if (mailConfig != null)
            //{
            //    networkCredential = !string.IsNullOrWhiteSpace(mailConfig.Username) && !string.IsNullOrWhiteSpace(mailConfig.Username) ? new NetworkCredential { UserName = mailConfig.Username, Password = mailConfig.Password } : networkCredential;
            //    fromEmail = String.IsNullOrWhiteSpace(mailConfig.FromEmail) ? fromEmail : mailConfig.FromEmail;
            //    fromName = String.IsNullOrWhiteSpace(mailConfig.FromName) ? fromName : mailConfig.FromName;
            //    host = !string.IsNullOrWhiteSpace(mailConfig.Host) ? mailConfig.Host : host;
            //    port = mailConfig.Port != null ? mailConfig.Port.Value : port;
            //    EnableSsl = mailConfig.EncryptionType != null ? (mailConfig.EncryptionType == MailEncryptionType.SSL ? true : false) : EnableSsl;
            //}
        }
        #endregion

        //General Email Send
        public bool SendMail(string toEmail, string subject, string body, string fromEmail = null, string fromName = null, List<string> attachmentFilePathList = null, Stream mailStreamAttachment = null, string ccEmail = null, string bccEmail = null)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMsg = new MailMessage();
            MailAddressCollection addressCollection = new MailAddressCollection();
            addressCollection.Add("softifybd@gmail.com");
            try
            {
                fromEmail = this.fromEmail;
                fromName = this.fromName;
                if (attachmentFilePathList != null && attachmentFilePathList.Count > 0)
                {
                    foreach (string filePath in attachmentFilePathList)
                    {
                        Attachment attachFile = new Attachment(filePath);
                        mailMsg.Attachments.Add(attachFile);
                    }
                }
                mailMsg.From = new MailAddress(fromEmail, fromName);
                mailMsg.To.Add(toEmail);
                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true;
                mailMsg.BodyEncoding = Encoding.UTF8;
                mailMsg.Body = body;
                mailMsg.Priority = MailPriority.Normal;
                if (!string.IsNullOrEmpty(bccEmail))
                {
                    MailAddress addressBCC = new MailAddress(bccEmail);
                    mailMsg.Bcc.Add(addressBCC);
                }
                if (!string.IsNullOrEmpty(ccEmail))
                {
                    MailAddress addressCC = new MailAddress(ccEmail);
                    mailMsg.CC.Add(addressCC);
                }
                smtpClient.Credentials = networkCredential;
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = EnableSsl;
                smtpClient.Send(mailMsg);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }//end function

        //General Email Send
        public async Task<bool> SendMailAsync(string toEmail, string subject, string body, string fromEmail = null, string fromName = null, List<string> attachmentFilePathList = null, Stream mailStreamAttachment = null, string ccEmail = null, string bccEmail = null)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMsg = new MailMessage();
            MailAddressCollection addressCollection = new MailAddressCollection();
            addressCollection.Add("softifybd@gmail.com");
            try
            {
                fromEmail = this.fromEmail;
                fromName = this.fromName;
                if (attachmentFilePathList != null && attachmentFilePathList.Count > 0)
                {
                    foreach (string filePath in attachmentFilePathList)
                    {
                        Attachment attachFile = new Attachment(filePath);
                        mailMsg.Attachments.Add(attachFile);
                    }
                }
                mailMsg.From = new MailAddress(fromEmail, fromName);
                mailMsg.To.Add(toEmail);
                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true;
                mailMsg.BodyEncoding = Encoding.UTF8;
                mailMsg.Body = body;
                mailMsg.Priority = MailPriority.Normal;
                if (!string.IsNullOrEmpty(bccEmail))
                {
                    MailAddress addressBCC = new MailAddress(bccEmail);
                    mailMsg.Bcc.Add(addressBCC);
                }
                if (!string.IsNullOrEmpty(ccEmail))
                {
                    MailAddress addressCC = new MailAddress(ccEmail);
                    mailMsg.CC.Add(addressCC);
                }
                smtpClient.Credentials = networkCredential;
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = EnableSsl;
                await smtpClient.SendMailAsync(mailMsg);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }//end function

        //Email Send with Exact File Attachment
        public bool SendMailWithStreamFileAttachment(string attachmentFileName, Stream mailStreamAttachment, string toEmail, string subject, string body, string fromEmail = null, string fromName = null, string ccEmail = null, string bccEmail = null)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMsg = new MailMessage();
            try
            {
                fromEmail = this.fromEmail;
                fromName = this.fromName;
                Attachment attachment = new Attachment(mailStreamAttachment, attachmentFileName);
                mailMsg.Attachments.Add(attachment);
                mailMsg.From = new MailAddress(fromEmail, fromName);
                mailMsg.To.Add(toEmail);
                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true;
                mailMsg.BodyEncoding = Encoding.UTF8;
                mailMsg.Body = body;
                mailMsg.Priority = MailPriority.Normal;
                if (!string.IsNullOrEmpty(bccEmail))
                {
                    MailAddress addressBCC = new MailAddress(bccEmail);
                    mailMsg.Bcc.Add(addressBCC);
                }
                if (!string.IsNullOrEmpty(ccEmail))
                {
                    MailAddress addressCC = new MailAddress(ccEmail);
                    mailMsg.CC.Add(addressCC);
                }
                smtpClient.Credentials = networkCredential;
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = EnableSsl;
                smtpClient.Send(mailMsg);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }//end function

        //Email Send with Byte File Attachment
        public bool SendMailWithMemoryStreamFileAttachment(string attachmentFileName, byte[] mailStreamAttachment, string toEmail, string subject, string body, string fromEmail = null, string fromName = null, string ccEmail = null, string bccEmail = null)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMsg = new MailMessage();
            try
            {
                fromEmail = this.fromEmail;
                fromName = this.fromName;
                List<MemoryStream> memoStreamList = new List<MemoryStream>();
                MemoryStream mst = new MemoryStream(mailStreamAttachment);
                mailMsg.Attachments.Add(new Attachment(mst, attachmentFileName));
                memoStreamList.Add(mst);
                mailMsg.From = new MailAddress(fromEmail, fromName);
                mailMsg.To.Add(toEmail);
                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true;
                mailMsg.BodyEncoding = Encoding.UTF8;
                mailMsg.Body = body;
                mailMsg.Priority = MailPriority.Normal;
                if (!string.IsNullOrEmpty(bccEmail))
                {
                    MailAddress addressBCC = new MailAddress(bccEmail);
                    mailMsg.Bcc.Add(addressBCC);
                }
                if (!string.IsNullOrEmpty(ccEmail))
                {
                    MailAddress addressCC = new MailAddress(ccEmail);
                    mailMsg.CC.Add(addressCC);
                }
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = EnableSsl;
                smtpClient.Send(mailMsg);
                foreach (MemoryStream stream in memoStreamList)
                {
                    stream.Dispose();
                }
                return true;
            }
            catch (SmtpException smtpEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Email Send with Byte File Attachment Async Method
        public async Task<bool> SendMailWithMemoryStreamFileAttachmentAsync(string attachmentFileName, byte[] mailStreamAttachment, string toEmail, string subject, string body, string fromEmail = null, string fromName = null, string ccEmail = null, string bccEmail = null)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMsg = new MailMessage();
            try
            {
                fromEmail = this.fromEmail;
                fromName = this.fromName;
                List<MemoryStream> memoStreamList = new List<MemoryStream>();
                MemoryStream mst = new MemoryStream(mailStreamAttachment);
                mailMsg.Attachments.Add(new Attachment(mst, attachmentFileName));
                memoStreamList.Add(mst);
                mailMsg.From = new MailAddress(fromEmail, fromName);
                mailMsg.To.Add(toEmail);
                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true;
                mailMsg.BodyEncoding = Encoding.UTF8;
                mailMsg.Body = body;
                mailMsg.Priority = MailPriority.Normal;
                if (!string.IsNullOrEmpty(bccEmail))
                {
                    MailAddress addressBCC = new MailAddress(bccEmail);
                    mailMsg.Bcc.Add(addressBCC);
                }
                if (!string.IsNullOrEmpty(ccEmail))
                {
                    MailAddress addressCC = new MailAddress(ccEmail);
                    mailMsg.CC.Add(addressCC);
                }
                smtpClient.Credentials = networkCredential;
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = EnableSsl;
                await smtpClient.SendMailAsync(mailMsg);
                foreach (MemoryStream stream in memoStreamList)
                {
                    stream.Dispose();
                }
                return true;
            }
            catch (SmtpException smtpEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetEmailHtmlTemplate(string templatePath)
        {
            string emailBodyContent = "";
            try
            {
                if (File.Exists(templatePath))
                {
                    using (StreamReader textReader = new StreamReader(templatePath))
                    {
                        emailBodyContent = textReader.ReadToEnd();
                    }
                }
                else
                {
                    emailBodyContent = "";
                }
            }
            catch (Exception)
            {
                emailBodyContent = "";
            }
            return emailBodyContent;
        }

        //Email Send with Byte File Attachment
        public bool SendMailWithMultipleMemoryStreamFileAttachment(List<VmEmailAttactment> attactments, string toEmail, string subject, string body, string fromEmail = null, string fromName = null, string ccEmail = null, string bccEmail = null)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMsg = new MailMessage();
            try
            {
                fromEmail = this.fromEmail;
                fromName = this.fromName;
                List<MemoryStream> memoStreamList = new List<MemoryStream>();
                if (attactments != null)
                {
                    foreach (var att in attactments)
                    {
                        try
                        {
                            MemoryStream mst = new MemoryStream(att.mailStreamAttachment);
                            mailMsg.Attachments.Add(new Attachment(mst, att.attachmentFileName));
                            memoStreamList.Add(mst);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                mailMsg.From = new MailAddress(fromEmail, fromName);
                mailMsg.To.Add(toEmail);
                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true;
                mailMsg.BodyEncoding = Encoding.UTF8;
                mailMsg.Body = body;
                mailMsg.Priority = MailPriority.Normal;
                if (!string.IsNullOrEmpty(bccEmail))
                {
                    MailAddress addressBCC = new MailAddress(bccEmail);
                    mailMsg.Bcc.Add(addressBCC);
                }
                if (!string.IsNullOrEmpty(ccEmail))
                {
                    MailAddress addressCC = new MailAddress(ccEmail);
                    mailMsg.CC.Add(addressCC);
                }
                smtpClient.Credentials = networkCredential;
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = EnableSsl;
                smtpClient.Send(mailMsg);
                foreach (MemoryStream stream in memoStreamList)
                {
                    stream.Dispose();
                }
                return true;
            }
            catch (SmtpException smtpEx)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
