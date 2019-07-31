using BackendClassLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version = BackendClassLibrary.Models.Version;

namespace BackendClassLibrary.Data

{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Database Set. a list of the models/objects derriving from that database.
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<PluginType> PluginTypes { get; set; }
        public DbSet<Plugin> Plugins { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<UserVersion> UserVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                Name = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            // Create two engines
            modelBuilder.Entity<Engine>().HasData(
                new Engine()
                {
                    EngineId = 1,
                    Title = "RPG Maker MV"
                },
                new Engine()
                {
                    EngineId = 2,
                    Title = "Game Maker"
                }
            );

            // Create two Plugin Types
            modelBuilder.Entity<PluginType>().HasData(
                new PluginType()
                {
                    PluginTypeId = 1,
                    Name = "Battle System"
                },
                new PluginType()
                {
                    PluginTypeId = 2,
                    Name = "Menu"
                }
            );



        }
    }
    }
