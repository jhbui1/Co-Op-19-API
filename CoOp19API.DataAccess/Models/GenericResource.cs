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
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }
        public int? LocId { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual MapData Loc { get; set; }
        public virtual ICollection<ConsumableResource> ConsumableResource { get; set; }
        public virtual ICollection<HealthResource> HealthResource { get; set; }
        public virtual ICollection<ShelterResource> ShelterResource { get; set; }
    }
}
