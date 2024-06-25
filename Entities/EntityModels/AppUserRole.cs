using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class AppUserRole : BaseEntity
    {
        [Column(Order = 0), Key]
        public int UserId { get; set; }
        public virtual AppUser UserInfo { get; set; }

        [Column(Order = 1), Key]
        public int RoleId { get; set; }
        public virtual AppRole RoleInfo { get; set; }
    }
}
