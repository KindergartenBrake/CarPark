using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CP.Server.Models.CarPark;

namespace CP.Server.Data
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

            builder.Entity<CP.Server.Models.CarPark.VDriver>().HasNoKey();

            builder.Entity<CP.Server.Models.CarPark.VTripRequest>().HasNoKey();

            builder.Entity<CP.Server.Models.CarPark.VTrip>().HasNoKey();

            builder.Entity<CP.Server.Models.CarPark.VUser>().HasNoKey();

            builder.Entity<CP.Server.Models.CarPark.VVehicle>().HasNoKey();

            builder.Entity<CP.Server.Models.CarPark.AspNetUserLogin>().HasKey(table => new {
                table.LoginProvider, table.ProviderKey
            });

            builder.Entity<CP.Server.Models.CarPark.AspNetUserRole>().HasKey(table => new {
                table.UserId, table.RoleId
            });

            builder.Entity<CP.Server.Models.CarPark.AspNetUserToken>().HasKey(table => new {
                table.UserId, table.LoginProvider, table.Name
            });

            builder.Entity<CP.Server.Models.CarPark.Driver>()
              .HasOne(i => i.User)
              .WithMany(i => i.Drivers)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<CP.Server.Models.CarPark.Driver>()
              .HasOne(i => i.Vehicle)
              .WithMany(i => i.Drivers)
              .HasForeignKey(i => i.VehicleId)
              .HasPrincipalKey(i => i.VehicleId);

            builder.Entity<CP.Server.Models.CarPark.TripRequest>()
              .HasOne(i => i.Driver)
              .WithMany(i => i.TripRequests)
              .HasForeignKey(i => i.DriverId)
              .HasPrincipalKey(i => i.DriverId);

            builder.Entity<CP.Server.Models.CarPark.TripRequest>()
              .HasOne(i => i.User)
              .WithMany(i => i.TripRequests)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<CP.Server.Models.CarPark.TripRequest>()
              .HasOne(i => i.Vehicle)
              .WithMany(i => i.TripRequests)
              .HasForeignKey(i => i.VehicleId)
              .HasPrincipalKey(i => i.VehicleId);

            builder.Entity<CP.Server.Models.CarPark.Trip>()
              .HasOne(i => i.TripRequest)
              .WithMany(i => i.Trips)
              .HasForeignKey(i => i.RequestId)
              .HasPrincipalKey(i => i.RequestId);

            builder.Entity<CP.Server.Models.CarPark.Trip>()
              .HasOne(i => i.Vehicle)
              .WithMany(i => i.Trips)
              .HasForeignKey(i => i.VehicleId)
              .HasPrincipalKey(i => i.VehicleId);

            builder.Entity<CP.Server.Models.CarPark.Vehicle>()
              .HasOne(i => i.Driver)
              .WithMany(i => i.Vehicles)
              .HasForeignKey(i => i.DriverId)
              .HasPrincipalKey(i => i.DriverId);

            builder.Entity<CP.Server.Models.CarPark.AspNetRoleClaim>()
              .HasOne(i => i.AspNetRole)
              .WithMany(i => i.AspNetRoleClaims)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CP.Server.Models.CarPark.AspNetUserClaim>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.AspNetUserClaims)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CP.Server.Models.CarPark.AspNetUserLogin>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.AspNetUserLogins)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CP.Server.Models.CarPark.AspNetUserRole>()
              .HasOne(i => i.AspNetRole)
              .WithMany(i => i.AspNetUserRoles)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CP.Server.Models.CarPark.AspNetUserRole>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.AspNetUserRoles)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CP.Server.Models.CarPark.AspNetUserToken>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.AspNetUserTokens)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CP.Server.Models.CarPark.Driver>()
              .Property(p => p.IsActive)
              .HasDefaultValueSql(@"true");

            builder.Entity<CP.Server.Models.CarPark.TripRequest>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"CURRENT_TIMESTAMP");

            builder.Entity<CP.Server.Models.CarPark.TripRequest>()
              .Property(p => p.Status)
              .HasDefaultValueSql(@"'pending'::character varying");

            builder.Entity<CP.Server.Models.CarPark.Trip>()
              .Property(p => p.Status)
              .HasDefaultValueSql(@"'planned'::character varying");

            builder.Entity<CP.Server.Models.CarPark.User>()
              .Property(p => p.IsActive)
              .HasDefaultValueSql(@"true");

            builder.Entity<CP.Server.Models.CarPark.Vehicle>()
              .Property(p => p.Mileage)
              .HasDefaultValueSql(@"0");

            builder.Entity<CP.Server.Models.CarPark.Trip>()
              .Property(p => p.StartOdometer)
              .HasPrecision(12,1);

            builder.Entity<CP.Server.Models.CarPark.Trip>()
              .Property(p => p.EndOdometer)
              .HasPrecision(12,1);

            builder.Entity<CP.Server.Models.CarPark.Vehicle>()
              .Property(p => p.Mileage)
              .HasPrecision(12,1);

            builder.Entity<CP.Server.Models.CarPark.VTrip>()
              .Property(p => p.ПробегВНачале)
              .HasPrecision(12,1);

            builder.Entity<CP.Server.Models.CarPark.VTrip>()
              .Property(p => p.ПробегВКонце)
              .HasPrecision(12,1);

            builder.Entity<CP.Server.Models.CarPark.VVehicle>()
              .Property(p => p.Пробег)
              .HasPrecision(12,1);
            this.OnModelBuilding(builder);
        }

        public DbSet<CP.Server.Models.CarPark.Driver> Drivers { get; set; }

        public DbSet<CP.Server.Models.CarPark.TripRequest> TripRequests { get; set; }

        public DbSet<CP.Server.Models.CarPark.Trip> Trips { get; set; }

        public DbSet<CP.Server.Models.CarPark.User> Users { get; set; }

        public DbSet<CP.Server.Models.CarPark.Vehicle> Vehicles { get; set; }

        public DbSet<CP.Server.Models.CarPark.VDriver> VDrivers { get; set; }

        public DbSet<CP.Server.Models.CarPark.VTripRequest> VTripRequests { get; set; }

        public DbSet<CP.Server.Models.CarPark.VTrip> VTrips { get; set; }

        public DbSet<CP.Server.Models.CarPark.VUser> VUsers { get; set; }

        public DbSet<CP.Server.Models.CarPark.VVehicle> VVehicles { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetRoleClaim> AspNetRoleClaims { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetRole> AspNetRoles { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUserClaim> AspNetUserClaims { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUserLogin> AspNetUserLogins { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUserRole> AspNetUserRoles { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUser> AspNetUsers { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUserToken> AspNetUserTokens { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}