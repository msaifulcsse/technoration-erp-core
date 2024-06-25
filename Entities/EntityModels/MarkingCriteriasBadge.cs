using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class MarkingCriteriasBadge : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CriteriasBadgeId { get; set; }

        public int MarkingCriteriaId { get; set; }
        public virtual MarkingCriteria MarkingCriteria { get; set; }

        public int MarkingBadgeId { get; set; }
        public virtual MarkingBadge MarkingBadge { get; set; }

        public decimal MinimumValue { get; set; }

        public decimal MaximumValue { get; set; }

        [StringLength(500)]
        public string Details { get; set; }
    }
}
