using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOp19API.Models
{
    public partial class Users
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int? Loc { get; set; }
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        [Required]
        [Column("FName")]
        [StringLength(30)]
        public string Fname { get; set; }
        [Column("LName")]
        [StringLength(30)]
        public string Lname { get; set; }
        [Column(TypeName = "decimal(15, 0)")]
        public decimal? Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }

        [ForeignKey(nameof(Loc))]
        [InverseProperty(nameof(MapData.Users))]
        public virtual MapData LocNavigation { get; set; }
    }
}
