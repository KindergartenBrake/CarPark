using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC.Models.CarPark
{
    [Table("trips", Schema = "public")]
    public partial class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("trip_id")]
        public int TripId { get; set; }

        [Column("request_id")]
        [Required]
        public int RequestId { get; set; }

        public TripRequest TripRequest { get; set; }

        [Column("vehicle_id")]
        [Required]
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        [Column("trip_date")]
        [Required]
        public DateTime TripDate { get; set; }

        [Column("start_time")]
        [Required]
        public DateTime StartTime { get; set; }

        [Column("end_time")]
        public DateTime? EndTime { get; set; }

        [Column("start_odometer")]
        [Required]
        public decimal StartOdometer { get; set; }

        [Column("end_odometer")]
        public decimal? EndOdometer { get; set; }

        [Column("status")]
        [MaxLength(20)]
        public string Status { get; set; }
    }
}