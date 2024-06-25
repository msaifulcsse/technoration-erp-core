using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    [Table("CompanySettings")]
    public class CompanySettings : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string TagLine { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string SMSUserName { get; set; }
        public string SMSPassword { get; set; }
        public string SMSSender { get; set; }
        public string MikrotikUrl { get; set; }
        public string MikrotikUserName { get; set; }
        public string MikrotikPassword { get; set; }
        public int? UserType { get; set; }
        public int? ReferenceId { get; set; }
        public int? BusinessPackage { get; set; }
        public int? SmsProvider { get; set; }
        public bool IsCustomerIdEditable { get; set; }

        public virtual ICollection<MailConfiguration> MailConfiguration { get; set; }
    }
}
