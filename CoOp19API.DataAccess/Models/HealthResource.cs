using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOp19API.Models
{
    public partial class HealthResource
    {
        public HealthResource()
        {
            HealthResourceServices = new HashSet<HealthResourceServices>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Resource_ID")]
        public int ResourceId { get; set; }
        public bool ProvidesTests { get; set; }
        [Column(TypeName = "money")]
        public decimal? TestPrice { get; set; }

        [ForeignKey(nameof(ResourceId))]
        public virtual GenericResource Resource { get; set; }
        public virtual ICollection<HealthResourceServices> HealthResourceServices { get; set; }
    }
}
