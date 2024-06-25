using Entities.BaseModels;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.EntityModels
{
    public class Student : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid positive integer Number")]
        public int RollNumber { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string ContactNumber { get; set; }

        [StringLength(50)]
        public string FatherName { get; set; }

        [StringLength(50)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string GurdianNumber { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(20)]
        public string Religion { get; set; }

        [StringLength(20)]
        public string NationalID { get; set; }

        [StringLength(100)]
        public string PresentAddress { get; set; }

        [StringLength(100)]
        public string PermanentAddress { get; set; }

        [StringLength(500)]
        public string ProfilePicture { get; set; }

        public bool AppliedTermsAndCondition { get; set; }

        public StudentType Type { get; set; }

        public int BatchId { get; set; }
        public virtual StudentBatch BatchInfo { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department DepartmentInfo { get; set; }

        public virtual ICollection<StudentCourseMarks> StudentMarks { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + (!string.IsNullOrEmpty(LastName) ? (" " + LastName) : "");
            }
        }

        [NotMapped]
        public decimal TotalAchievedMarks
        {
            get { return StudentMarks.Sum(x => x.CourseCriteriaMarks); }
        }

        [NotMapped]
        public ICollection<StudentCoursesMarksRM> StudentCoursesMarks
        {
            get
            {
                return StudentMarks.GroupBy(gb => gb.CourseId).Select(s => new StudentCoursesMarksRM
                {
                    CourseId = s.FirstOrDefault().CourseId,
                    //CourseName = s.FirstOrDefault().CourseInfo.CourseName,
                    //CourseCode = s.FirstOrDefault().CourseInfo.CourseCode,
                    //CourseDetails = s.FirstOrDefault().CourseInfo.Details,
                    CourseMarksTotal = Math.Round(s.OrderBy(ob => ob.MarkingCriteriaId).Sum(ss => ss.CourseCriteriaMarks), 2),
                    CourseMarksPercentage = Math.Round((s.OrderBy(ob => ob.MarkingCriteriaId).Sum(ss => ss.CourseCriteriaMarks) / 100) * 100, 2)
                }).ToList();
            }
        }
    }

    public enum StudentType
    {
        Regular = 1,
        Irregular = 2,
        Disabled = 3,
        Poor = 4,
        Special = 5,
        PassedOut = 6
    }

    public class StudentCoursesMarksRM //RelationalModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string CourseDetails { get; set; }
        public decimal CourseMarksTotal { get; set; }
        public decimal CourseMarksPercentage { get; set; }
    }
}
