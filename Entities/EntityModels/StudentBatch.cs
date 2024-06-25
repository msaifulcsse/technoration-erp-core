using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class StudentBatch : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchId { get; set; }

        [StringLength(50)]
        public string BatchName { get; set; }

        [StringLength(500)]
        public string Details { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
