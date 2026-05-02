using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CPC.Models.CarPark;

namespace CPC.Data
{
    public partial class CarParkContext : DbContext
    {
        public CarParkContext()
        {
        }

        public CarParkContext(DbContextOptions<CarParkContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CPC.Models.CarPark.VDriver>().HasNoKey();

            builder.Entity<CPC.Models.CarPark.VTripRequest>().HasNoKey();

            builder.Entity<CPC.Models.CarPark.VTrip>().HasNoKey();

            builder.Entity<CPC.Models.CarPark.VUser>().HasNoKey();

            builder.Entity<CPC.Models.CarPark.VVehicle>().HasNoKey();

            builder.Entity<CPC.Models.CarPark.Driver>()
              .HasOne(i => i.User)
              .WithMany(i => i.Drivers)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<CPC.Models.CarPark.Driver>()
              .HasOne(i => i.Vehicle)
              .WithMany(i => i.Drivers)
              .HasForeignKey(i => i.VehicleId)
              .HasPrincipalKey(i => i.VehicleId);

            builder.Entity<CPC.Models.CarPark.TripRequest>()
              .HasOne(i => i.Driver)
              .WithMany(i => i.TripRequests)
              .HasForeignKey(i => i.DriverId)
              .HasPrincipalKey(i => i.DriverId);

            builder.Entity<CPC.Models.CarPark.TripRequest>()
              .HasOne(i => i.User)
              .WithMany(i => i.TripRequests)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<CPC.Models.CarPark.TripRequest>()
              .HasOne(i => i.Vehicle)
              .WithMany(i => i.TripRequests)
              .HasForeignKey(i => i.VehicleId)
              .HasPrincipalKey(i => i.VehicleId);

            builder.Entity<CPC.Models.CarPark.Trip>()
              .HasOne(i => i.TripRequest)
              .WithMany(i => i.Trips)
              .HasForeignKey(i => i.RequestId)
              .HasPrincipalKey(i => i.RequestId);

            builder.Entity<CPC.Models.CarPark.Trip>()
              .HasOne(i => i.Vehicle)
              .WithMany(i => i.Trips)
              .HasForeignKey(i => i.VehicleId)
              .HasPrincipalKey(i => i.VehicleId);

            builder.Entity<CPC.Models.CarPark.Vehicle>()
              .HasOne(i => i.Driver)
              .WithMany(i => i.Vehicles)
              .HasForeignKey(i => i.DriverId)
              .HasPrincipalKey(i => i.DriverId);

            builder.Entity<CPC.Models.CarPark.Driver>()
              .Property(p => p.IsActive)
              .HasDefaultValueSql(@"true");

            builder.Entity<CPC.Models.CarPark.TripRequest>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"CURRENT_TIMESTAMP");

            builder.Entity<CPC.Models.CarPark.TripRequest>()
              .Property(p => p.Status)
              .HasDefaultValueSql(@"'pending'::character varying");

            builder.Entity<CPC.Models.CarPark.Trip>()
              .Property(p => p.Status)
              .HasDefaultValueSql(@"'planned'::character varying");

            builder.Entity<CPC.Models.CarPark.User>()
              .Property(p => p.IsActive)
              .HasDefaultValueSql(@"true");

            builder.Entity<CPC.Models.CarPark.Vehicle>()
              .Property(p => p.Mileage)
              .HasDefaultValueSql(@"0");

            builder.Entity<CPC.Models.CarPark.Trip>()
              .Property(p => p.StartOdometer)
              .HasPrecision(12,1);

            builder.Entity<CPC.Models.CarPark.Trip>()
              .Property(p => p.EndOdometer)
              .HasPrecision(12,1);

            builder.Entity<CPC.Models.CarPark.Vehicle>()
              .Property(p => p.Mileage)
              .HasPrecision(12,1);

            builder.Entity<CPC.Models.CarPark.VTrip>()
              .Property(p => p.ПробегВНачале)
              .HasPrecision(12,1);

            builder.Entity<CPC.Models.CarPark.VTrip>()
              .Property(p => p.ПробегВКонце)
              .HasPrecision(12,1);

            builder.Entity<CPC.Models.CarPark.VVehicle>()
              .Property(p => p.Пробег)
              .HasPrecision(12,1);
            this.OnModelBuilding(builder);
        }

        public DbSet<CPC.Models.CarPark.Driver> Drivers { get; set; }

        public DbSet<CPC.Models.CarPark.TripRequest> TripRequests { get; set; }

        public DbSet<CPC.Models.CarPark.Trip> Trips { get; set; }

        public DbSet<CPC.Models.CarPark.User> Users { get; set; }

        public DbSet<CPC.Models.CarPark.Vehicle> Vehicles { get; set; }

        public DbSet<CPC.Models.CarPark.VDriver> VDrivers { get; set; }

        public DbSet<CPC.Models.CarPark.VTripRequest> VTripRequests { get; set; }

        public DbSet<CPC.Models.CarPark.VTrip> VTrips { get; set; }

        public DbSet<CPC.Models.CarPark.VUser> VUsers { get; set; }

        public DbSet<CPC.Models.CarPark.VVehicle> VVehicles { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}