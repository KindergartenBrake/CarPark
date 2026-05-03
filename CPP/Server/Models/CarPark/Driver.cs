using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CP.Server.Models.CarPark
{
    [Table("drivers", Schema = "public")]
    public partial class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("driver_id")]
        public int DriverId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        public User User { get; set; }

        [Column("vehicle_id")]
        public int? VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        [Column("first_name")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Column("middle_name")]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Column("license_number")]
        [Required]
        [MaxLength(20)]
        public string LicenseNumber { get; set; }

        [Column("license_issue_date")]
        [Required]
        public DateTime LicenseIssueDate { get; set; }

        [Column("license_expiry_date")]
        [Required]
        public DateTime LicenseExpiryDate { get; set; }

        [Column("birth_date")]
        [Required]
        public DateTime BirthDate { get; set; }

        [Column("phone")]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("is_active")]
        public bool? IsActive { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<TripRequest> TripRequests { get; set; }
    }
}