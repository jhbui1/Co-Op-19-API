using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOp19API.Models
{
    public partial class ConsumableResource
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Resource_ID")]
        public int ResourceId { get; set; }
        [Column("Loc_ID")]
        public int LocId { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(LocId))]
        [InverseProperty(nameof(MapData.ConsumableResource))]
        public virtual MapData Loc { get; set; }
        [ForeignKey(nameof(ResourceId))]
        [InverseProperty(nameof(GenericResource.ConsumableResource))]
        public virtual GenericResource Resource { get; set; }
    }
}
