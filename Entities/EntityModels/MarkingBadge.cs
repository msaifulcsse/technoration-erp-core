using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class MarkingBadge : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarkingBadgeId { get; set; }

        [StringLength(100)]
        public string MarkingBadgeName { get; set; }

        [StringLength(10)]
        public string BadgeColorCode { get; set; }

        [StringLength(500)]
        public string Details { get; set; }

        public virtual ICollection<MarkingCriteriasBadge> MarkingCriteriasBadges { get; set; }
    }
}
