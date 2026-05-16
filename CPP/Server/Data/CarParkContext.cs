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

            // ========== Identity ==========
            builder.Entity<CP.Server.Models.CarPark.AspNetUserLogin>().HasKey(table => new {
                table.LoginProvider, table.ProviderKey
            });

            builder.Entity<CP.Server.Models.CarPark.AspNetUserRole>().HasKey(table => new {
                table.UserId, table.RoleId
            });

            builder.Entity<CP.Server.Models.CarPark.AspNetUserToken>().HasKey(table => new {
                table.UserId, table.LoginProvider, table.Name
            });

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

            //  Driver 
            builder.Entity<Driver>(entity =>
            {
                entity.ToTable("drivers", "public");
                entity.HasKey(e => e.DriverId);
                entity.Property(e => e.DriverId).HasColumnName("driver_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(50).IsRequired();
                entity.Property(e => e.MiddleName).HasColumnName("middle_name").HasMaxLength(50);
                entity.Property(e => e.BirthDate).HasColumnName("birth_date");
                entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100);
                entity.Property(e => e.LicenseNumber).HasColumnName("license_number").HasMaxLength(20).IsRequired();
                entity.Property(e => e.LicenseIssueDate).HasColumnName("license_issue_date");
                entity.Property(e => e.LicenseExpiryDate).HasColumnName("license_expiry_date");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Vehicle)
                      .WithMany(v => v.Drivers)
                      .HasForeignKey(e => e.VehicleId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Vehicle 
            builder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("vehicles", "public");
                entity.HasKey(e => e.VehicleId);
                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
                entity.Property(e => e.Brand).HasColumnName("brand").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Model).HasColumnName("model").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Year).HasColumnName("year");
                entity.Property(e => e.LicensePlate).HasColumnName("license_plate").HasMaxLength(12).IsRequired();
                entity.Property(e => e.Vin).HasColumnName("vin").HasMaxLength(17).IsRequired();
                entity.Property(e => e.VehicleType).HasColumnName("vehicle_type").HasMaxLength(20);
                entity.Property(e => e.FuelType).HasColumnName("fuel_type").HasMaxLength(20);
                entity.Property(e => e.Mileage).HasColumnName("mileage").HasPrecision(12, 1).HasDefaultValue(0);
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20).HasDefaultValue("Available");
                entity.Property(e => e.Insurance).HasColumnName("insurance").HasMaxLength(20);
                entity.Property(e => e.InsuranceExpiryDate).HasColumnName("insurance_expiry_date");
                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.HasOne(e => e.Driver)
                      .WithMany()
                      .HasForeignKey(e => e.DriverId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // TripRequest
            builder.Entity<TripRequest>(entity =>
            {
                entity.ToTable("trip_requests", "public");
                entity.HasKey(e => e.RequestId);
                entity.Property(e => e.RequestId).HasColumnName("request_id");
                entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id").IsRequired();
                entity.Property(e => e.VehicleType).HasColumnName("vehicle_type").HasMaxLength(20);
                entity.Property(e => e.TripDate).HasColumnName("trip_date");
                entity.Property(e => e.StartTime).HasColumnName("start_time");
                entity.Property(e => e.EndTime).HasColumnName("end_time");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
                entity.Property(e => e.DriverId).HasColumnName("driver_id");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(e => e.RejectionReason).HasColumnName("rejection_reason");

                entity.HasOne(e => e.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(e => e.CreatedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Vehicle)
                      .WithMany(v => v.TripRequests)
                      .HasForeignKey(e => e.VehicleId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Driver)
                      .WithMany(d => d.TripRequests)
                      .HasForeignKey(e => e.DriverId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            //  Trip
            builder.Entity<Trip>(entity =>
            {
                entity.ToTable("trips", "public");
                entity.HasKey(e => e.TripId);
                entity.Property(e => e.TripId).HasColumnName("trip_id");
                entity.Property(e => e.RequestId).HasColumnName("request_id");
                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
                entity.Property(e => e.DriverId).HasColumnName("driver_id");
                entity.Property(e => e.TripDate).HasColumnName("trip_date");
                entity.Property(e => e.StartTime).HasColumnName("start_time");
                entity.Property(e => e.EndTime).HasColumnName("end_time");
                entity.Property(e => e.StartOdometer).HasColumnName("start_odometer").HasPrecision(12, 1);
                entity.Property(e => e.EndOdometer).HasColumnName("end_odometer").HasPrecision(12, 1);
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20).HasDefaultValue("Scheduled");

                entity.HasOne(e => e.TripRequest)
                      .WithOne(r => r.Trip)
                      .HasForeignKey<Trip>(e => e.RequestId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Vehicle)
                      .WithMany(v => v.Trips)
                      .HasForeignKey(e => e.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Driver)
                      .WithMany(d => d.Trips)
                      .HasForeignKey(e => e.DriverId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelBuilding(builder);
        }

        // Identity DbSets 
        public DbSet<CP.Server.Models.CarPark.AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public DbSet<CP.Server.Models.CarPark.AspNetRole> AspNetRoles { get; set; }
        public DbSet<CP.Server.Models.CarPark.AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<CP.Server.Models.CarPark.AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<CP.Server.Models.CarPark.AspNetUserRole> AspNetUserRoles { get; set; }
        public DbSet<CP.Server.Models.CarPark.AspNetUser> AspNetUsers { get; set; }
        public DbSet<CP.Server.Models.CarPark.AspNetUserToken> AspNetUserTokens { get; set; }

        //  Domain DbSets 
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TripRequest> TripRequests { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}
