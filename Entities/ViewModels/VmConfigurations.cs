using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels
{
    public class VmDepartment
    {
        [Required(ErrorMessage = "Please provide the department id")]
        public int DepartmentId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please provide the department name")]
        public string DepartmentName { get; set; }

        [StringLength(500)]
        public string DepartmentDetails { get; set; }
    }

    public class VmStudentBatch
    {
        [Required(ErrorMessage = "Please provide the batch id")]
        public int BatchId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please provide the batch name")]
        public string BatchName { get; set; }

        [StringLength(500)]
        public string BatchDetails { get; set; }
    }

    public class VmAcademicSemester
    {
        [Required(ErrorMessage = "Please provide the semester id")]
        public int SemesterId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please provide the semester name")]
        public string SemesterName { get; set; }

        [StringLength(500)]
        public string SemesterDetails { get; set; }
    }

    public class VmAcademicCourse
    {
        [Required(ErrorMessage = "Please provide the course id")]
        public int CourseId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please provide the course code")]
        public string CourseCode { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Please provide the course name")]
        public string CourseName { get; set; }

        [StringLength(500)]
        public string CourseDetails { get; set; }

        [Required(ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please select a semester")]
        public int SemesterId { get; set; }
    }

    public class VmMarkingCriteria
    {
        [Required(ErrorMessage = "Please provide the criteria id")]
        public int MarkingCriteriaId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Please provide the criteria name")]
        public string MarkingCriteriaName { get; set; }

        [Required(ErrorMessage = "Please provide the criteria minimum value")]
        public decimal MinimumValue { get; set; }

        [Required(ErrorMessage = "Please provide the criteria maximum value")]
        public decimal MaximumValue { get; set; }

        [Required(ErrorMessage = "Please provide the criteria passing value")]
        public decimal PassingValue { get; set; }

        [StringLength(500)]
        public string MarkingCriteriaDetails { get; set; }

        public decimal? AchievedMarks { get; set; }
    }

    public class VmMarkingBadge
    {
        [Required(ErrorMessage = "Please provide the badge id")]
        public int MarkingBadgeId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Please provide the badge name")]
        public string MarkingBadgeName { get; set; }

        [StringLength(10)]
        public string MarkingBadgeColor { get; set; }   

        [StringLength(500)]
        public string MarkingBadgeDetails { get; set; }
    }

    public class VmCriteriasBadge
    {
        [Required(ErrorMessage = "Please provide the badge id")]
        public int CriteriasBadgeId { get; set; }

        [Required(ErrorMessage = "Please select a marking criteria")]
        public int MarkingCriteriaId { get; set; }

        [Required(ErrorMessage = "Please select the marking badges")]
        public int MarkingBadgeId { get; set; }

        [Required(ErrorMessage = "Please provide the badge minimum value")]
        public decimal CriteriasBadgeMinimum{ get; set; }

        [Required(ErrorMessage = "Please provide the badge maximum value")]
        public decimal CriteriasBadgeMaximum { get; set; }

        [StringLength(500)]
        public string CriteriasBadgeDetails { get; set; }
    }
}
