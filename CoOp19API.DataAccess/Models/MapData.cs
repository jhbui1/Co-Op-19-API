using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOp19API.Models
{
    public partial class MapData
    {
        public MapData()
        {
            ConsumableResource = new HashSet<ConsumableResource>();
            Users = new HashSet<Users>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("GPSN", TypeName = "decimal(9, 6)")]
        public decimal? Gpsn { get; set; }
        [Column("GPSW", TypeName = "decimal(9, 6)")]
        public decimal? Gpsw { get; set; }
        [StringLength(30)]
        public string City { get; set; }
        [StringLength(60)]
        public string Address { get; set; }
        [StringLength(40)]
        public string State { get; set; }

        [InverseProperty("IdNavigation")]
        public virtual GenericResource GenericResource { get; set; }
        [InverseProperty("Loc")]
        public virtual ICollection<ConsumableResource> ConsumableResource { get; set; }
        [InverseProperty("LocNavigation")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
