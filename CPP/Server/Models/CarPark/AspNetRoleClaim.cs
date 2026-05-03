using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CP.Server.Models.CarPark
{
    [Table("AspNetRoleClaims", Schema = "public")]
    public partial class AspNetRoleClaim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        [Required]
        public string RoleId { get; set; }

        public AspNetRole AspNetRole { get; set; }
    }
}