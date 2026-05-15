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

        }

        public DbSet<CP.Server.Models.CarPark.AspNetRoleClaim> AspNetRoleClaims { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetRole> AspNetRoles { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUserClaim> AspNetUserClaims { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUserLogin> AspNetUserLogins { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUserRole> AspNetUserRoles { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUser> AspNetUsers { get; set; }

        public DbSet<CP.Server.Models.CarPark.AspNetUserToken> AspNetUserTokens { get; set; }
    }
}