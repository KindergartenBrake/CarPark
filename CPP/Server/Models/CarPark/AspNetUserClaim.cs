using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CP.Server.Models.CarPark
{
    [Table("AspNetUserClaims", Schema = "public")]
    public partial class AspNetUserClaim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        [Required]
        public string UserId { get; set; }

        public AspNetUser AspNetUser { get; set; }
    }
}