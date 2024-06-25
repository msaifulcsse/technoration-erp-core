using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class StudentCourseMarks : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseMarksId { get; set; }

        public int StudentId { get; set; }
        public virtual Student StudentInfo { get; set; }

        public int CourseId { get; set; }
        public virtual Course CourseInfo { get; set; }

        public int MarkingCriteriaId { get; set; }
        public virtual MarkingCriteria MarkingCriteriaInfo { get; set; }

        public decimal CourseCriteriaMarks { get; set; }

        //public MarkingBadge GetMarkingBadge(int markingCriteriaId, decimal courseCriteriaMarks)
        //{
        //    MarkingBadge markingBadge = new MarkingBadge();
        //    try
        //    {
        //        if (markingCriteriaId > 0 && courseCriteriaMarks >= 0)
        //        {
        //            var listOfMarkBadges = 0;
        //        }
        //    }
        //    catch (Exception ex){ markingBadge = new MarkingBadge(); }
        //    return markingBadge;
        //}
    }
}
