using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOp19API.Models
{
    public partial class ShelterResource
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Resource_ID")]
        public int ResourceId { get; set; }
        public int Vacancy { get; set; }
        public byte Rating { get; set; }
        public bool IsSafe { get; set; }

        [ForeignKey(nameof(ResourceId))]
        public virtual GenericResource Resource { get; set; }
    }
}
