using Entities.EntityModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class VmStudentMarksEntry
    {
        public int DepartmentId { get; set; }

        public int SemesterId { get; set; }

        public int StudentId { get; set; }

        public List<VmStudentCourses> StudentCourses { get; set; }
    }

    public class VmStudentMarksUpload
    {
        public int DepartmentId { get; set; }
        public int SemesterId { get; set; }
        public int? CourseId { get; set; }
        public int? StudentId { get; set; }
        public IFormFile ExcelOrCsvFile { get; set; }
    }

    public class VmStudentCourses
    {
        public int CourseId { get; set; }

        public List<VmStudentCourseMarks> CourseMarks { get; set; }
    }

    public class VmStudentsSemesterCoursesAndMarks
    {
        public int DepartmentId { get; set; }

        public int SemesterId { get; set; }

        public int StudentId { get; set; }

        public List<VmMarkingCriteria> AllMarkingCriterias { get; set; }

        public List<VmSemesterCourses> AllCoursesWithMarks { get; set; }
    }

    public class VmSemesterCourses
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string CourseDetails { get; set; }

        public List<VmStudentCourseMarks> CourseMarks { get; set; }
    }

    public class VmStudentCourseMarks
    {
        public int MarkingCriteriaId { get; set; }

        public decimal CourseCriteriaMarks { get; set; }

        public VmMarkingCriteria CriteriaInfo { get; set; }
    }

    #region "For Student Performance Analysis"
    public class VmStudentsSemesterCoursesAndPerformance
    {
        public VmStudentCustomData StudentInfo { get; set; }

        public List<VmMarkingCriteria> AllMarkingCriterias { get; set; }

        public List<VmMarkingBadge> AllMarkingBadges { get; set; }

        public List<VmDepartmentSemesterCourses> AllCoursesWithPerformance { get; set; }
    }

    public class VmDepartmentSemesterCourses
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string CourseDetails { get; set; }

        public List<VmStudentCoursePerformance> CoursePerformance { get; set; }
    }

    public class VmStudentCoursePerformance
    {
        public VmMarkingCriteria CriteriaInfo { get; set; }
        public VmMarkingBadge BadgeInfo { get; set; }
    }
    #endregion
}
