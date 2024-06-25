using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class AuthenticationTracer : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TracerId { get; set; }
        public int? UserType { get; set; } //(int)CompanyUserType.[?] Enum: User Type
        public string ReferenceId { get; set; } //Here UserId will be save, also UnRegistered user's code will be saved, that's why this field is string
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }
        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
        public string Organization { get; set; }
        public string UserAgent { get; set; }
        public string RemarksOrNote { get; set; }
        public string RedirectURL { get; set; }
    }
}
