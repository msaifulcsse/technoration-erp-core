using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class MailConfiguration : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MailConfigurationId { get; set; }
        [ForeignKey("CompanySetting")]
        public int CompanyId { get; set; }
        public MailConfigType ConfigType { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Host { get; set; }
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public MailEncryptionType? EncryptionType { get; set; }
        public virtual CompanySettings CompanySetting { get; set; }
    }

    public enum MailConfigType
    {
        Mail = 1,
        SMTP = 2
    }

    public enum MailEncryptionType
    {
        SSL = 1,
        None = 2
    }
}
