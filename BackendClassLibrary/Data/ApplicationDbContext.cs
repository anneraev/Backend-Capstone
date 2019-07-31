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
                IsAdmin = true,
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

            ApplicationUser user2 = new ApplicationUser
            {
                Name = "notadmin",
                UserName = "notadmin@notadmin.com",
                NormalizedUserName = "NOTADMIN@NOTADMIN.COM",
                Email = "notadmin@notadmin.com",
                NormalizedEmail = "NOTADMIN@NOTADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "8f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "10000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash2 = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash2.HashPassword(user2, "NotAdmin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user2);


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

            // Create two plugins
            modelBuilder.Entity<Plugin>().HasData(
                new Plugin()
                {
                    PluginId = 1,
                    Title = "YEP Battle System - CTB",
                    UserId = user.Id,
                    EngineId = 1,
                    PluginTypeId = 1,
                },
                new Plugin()
                {
                    PluginId = 2,
                    Title = "Mog Status Menu",
                    UserId = user2.Id,
                    EngineId = 1,
                    PluginTypeId = 2,
                },
                new Plugin()
                {
                    PluginId = 3,
                    Title = "Mog Save Menu",
                    UserId = user2.Id,
                    EngineId = 1,
                    PluginTypeId = 2,
                },
                new Plugin()
                {
                    PluginId = 3,
                    Title = "Mog Area Target",
                    UserId = user2.Id,
                    EngineId = 1,
                    PluginTypeId = 1,
                },
                new Plugin()
                {
                    PluginId = 4,
                    Title = "Game Maker Start Menu",
                    UserId = user.Id,
                    EngineId = 2,
                    PluginTypeId = 2,
                }
            );

            modelBuilder.Entity<Version>().HasData(
                new Version()
                {
                    VersionId = 1,
                    Name = "1.1",
                    PluginId = 1,
                },
                new Version()
                {
                    VersionId = 2,
                    Name = "1.55",
                    PluginId = 1,
                },
                new Version()
                {
                    VersionId = 3,
                    Name = "0.2Beta",
                    PluginId = 2,
                },
                new Version()
                {
                    VersionId = 4,
                    Name = "Beta",
                    PluginId = 3,
                },
                new Version()
                {
                    VersionId = 5,
                    Name = "Final",
                    PluginId = 3,
                },
                new Version()
                {
                    VersionId = 6,
                    Name = "42",
                    PluginId = 4,
                }
            );

            modelBuilder.Entity<UserVersion>().HasData(
                //test case for when user has a previous version and the latest.
                new UserVersion()
                {
                    UserVersionId = 1,
                    UserId = user.Id,
                    VersionId = 1,
                },
                new UserVersion()
                {
                    UserVersionId = 2,
                    UserId = user.Id,
                    VersionId = 2,
                },
                //test case for when user only has an outdated version
                new UserVersion()
                {
                    UserVersionId = 3,
                    UserId = user2.Id,
                    VersionId = 4,
                }
                ); 
        }
    }
    }
