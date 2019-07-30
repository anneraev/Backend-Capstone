using backendcapstone.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendcapstone.Data
{
    public class ApplicationDbContext {

        //Database Set. a list of the models/objects derriving from that database.
        public DbSet<ApplicationUser> Users { get; set; }

    }
}
