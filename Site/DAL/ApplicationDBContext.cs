using Microsoft.EntityFrameworkCore;
using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.DAL
{
    public class ApplicationDBContext : DbContext
    {

        public DbSet<Vechicle> Vechicles { get; set; }
        public DbSet<User> Users { get; set; }


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            :base(options)
        { }

    }
}
