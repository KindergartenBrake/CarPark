using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC.Models.CarPark
{
    [Table("users", Schema = "public")]
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("username")]
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Column("password_hash")]
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Column("email")]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("is_active")]
        public bool? IsActive { get; set; }

        [Column("role")]
        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        public ICollection<TripRequest> TripRequests { get; set; }

        public ICollection<Driver> Drivers { get; set; }
    }
}