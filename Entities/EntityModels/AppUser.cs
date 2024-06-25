using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class AppUser : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? UserType { get; set; }
        public int? ReferenceId { get; set; }
        public bool? IsTemporaryPasswordOn { get; set; }
        public string TemporaryPassword { get; set; }
        public bool? IsMobileVerified { get; set; }
        public bool? IsEmailVerified { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
