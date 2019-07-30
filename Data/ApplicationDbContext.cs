using backendcapstone.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version = backendcapstone.Models.Version;

namespace backendcapstone.Data
{
    public class ApplicationDbContext {

        //Database Set. a list of the models/objects derriving from that database.
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<PluginType> PluginTypes { get; set; }
        public DbSet<Plugin> Plugins { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<UserVersion> UserVersions { get; set; }

    }
}
