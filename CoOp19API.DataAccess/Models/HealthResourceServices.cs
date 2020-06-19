using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOp19API.Models
{
    public partial class HealthResourceServices
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int HealthRes { get; set; }
        [Required]
        [StringLength(50)]
        public string ServiceName { get; set; }
        [Required]
        [StringLength(50)]
        public string ServiceDesc { get; set; }
        public int? AvgWaitHours { get; set; }
        public int ResourceId { get; set; }
        [Column(TypeName = "money")]
        public decimal EstCost { get; set; }

        [ForeignKey(nameof(HealthRes))]
        public virtual HealthResource HealthResNavigation { get; set; }
    }
}
