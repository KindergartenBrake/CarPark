using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC.Models.CarPark
{
    [Table("trip_requests", Schema = "public")]
    public partial class TripRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("request_id")]
        public int RequestId { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Column("driver_id")]
        [Required]
        public int DriverId { get; set; }

        public Driver Driver { get; set; }

        [Column("vehicle_id")]
        [Required]
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("status")]
        [MaxLength(20)]
        public string Status { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}