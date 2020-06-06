using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOp19API.Models
{
    public partial class GenericResource
    {
        public GenericResource()
        {
            ConsumableResource = new HashSet<ConsumableResource>();
            HealthResource = new HashSet<HealthResource>();
            ShelterResource = new HashSet<ShelterResource>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(MapData.GenericResource))]
        public virtual MapData IdNavigation { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<ConsumableResource> ConsumableResource { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<HealthResource> HealthResource { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<ShelterResource> ShelterResource { get; set; }
    }
}
