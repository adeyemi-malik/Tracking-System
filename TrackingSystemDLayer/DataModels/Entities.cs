using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{
    public class Entities<T> : DbContext
    {
        private IConfigurationRoot configuration;

        public Entities(DbContextOptions<Entities<T>> options)
           : base(options)
        {
        }
        public Entities(IConfigurationRoot configuration, DbContextOptions<Entities<T>> options)
            : base(options)
        {
            this.configuration = configuration;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer(configuration["ConnectionStrings:Entities"]);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User<T>>()
              .HasMany(s => s.UserRoles)
              .WithOne(s => s.User);

            modelBuilder.Entity<User<T>>()
             .HasMany(s => s.Claims)
             .WithOne(s => s.User);

            modelBuilder.Entity<User<T>>()
            .HasMany(s => s.Logins)
            .WithOne(s => s.User);

            modelBuilder.Entity<User<T>>()
            .HasMany(s => s.Devices)
            .WithOne(s => s.User);

            modelBuilder.Entity<Role<T>>()
              .HasMany(s => s.UserRoles)
              .WithOne(s => s.Role);

            modelBuilder.Entity<Location<T>>()
            .HasOne(s => s.Device)
            .WithMany(s => s.Locations);

            modelBuilder.Entity<UserLogin<T>>()
             .HasKey(s => new {s.Id, s.LoginProvider, s.ProviderKey });
          

        }

        public virtual DbSet<User<int>> Users { get; set; }

        public virtual DbSet<Role<int>> Roles { get; set; }

        public virtual DbSet<UserClaim<int>> Claims { get; set; }

        public virtual DbSet<UserLogin<int>> Logins { get; set; }

        public virtual DbSet<Device<int>> Devices { get; set; }

        public virtual DbSet<Location<int>> Locations { get; set; }

        public virtual DbSet<UserRole<int>> UserRoles { get; set; }




    }
}
