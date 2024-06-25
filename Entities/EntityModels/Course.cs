using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class Course : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseCode { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [StringLength(500)]
        public string Details { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department DepartmentInfo { get; set; }

        public int SemesterId { get; set; }
        public virtual Semester SemesterInfo { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        //public virtual ICollection<StudentCourseMarks> CourseMarks { get; set; }
    }
}
