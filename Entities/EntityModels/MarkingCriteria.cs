using Entities.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.EntityModels
{
    public class MarkingCriteria : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarkingCriteriaId { get; set; }

        [StringLength(100)]
        public string MarkingCriteriaName { get; set; }

        [StringLength(500)]
        public string Details { get; set; }

        public decimal MinimumValue { get; set; }

        public decimal MaximumValue { get; set; }

        public decimal PassingValue { get; set; }

        public virtual ICollection<MarkingCriteriasBadge> MarkingCriteriasBadges { get; set; }
    }
}
