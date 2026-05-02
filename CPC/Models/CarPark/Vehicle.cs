using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC.Models.CarPark
{
    [Table("vehicles", Schema = "public")]
    public partial class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("vehicle_id")]
        public int VehicleId { get; set; }

        [Column("driver_id")]
        public int? DriverId { get; set; }

        public Driver Driver { get; set; }

        [Column("license_plate")]
        [Required]
        [MaxLength(12)]
        public string LicensePlate { get; set; }

        [Column("vin")]
        [Required]
        [MaxLength(17)]
        public string Vin { get; set; }

        [Column("brand")]
        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Column("model")]
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Column("year")]
        [Required]
        public int Year { get; set; }

        [Column("mileage")]
        public decimal? Mileage { get; set; }

        [Column("fuel_type")]
        [MaxLength(20)]
        public string FuelType { get; set; }

        [Column("vehicle_type")]
        [MaxLength(20)]
        public string VehicleType { get; set; }

        [Column("status")]
        [MaxLength(20)]
        public string Status { get; set; }

        [Column("insurance")]
        [MaxLength(20)]
        public string Insurance { get; set; }

        public ICollection<Trip> Trips { get; set; }

        public ICollection<TripRequest> TripRequests { get; set; }

        public ICollection<Driver> Drivers { get; set; }
    }
}