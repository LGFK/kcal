using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class KcalContext : DbContext
    {
        public KcalContext (DbContextOptions<KcalContext> options)
            : base(options)
        {
        }

        public DbSet<UserDescription> UserDescriptions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Goals> Goals { get; set; }
        public DbSet<DailyRatio> DailyRatio { get; set; }
        public DbSet<EatenFood> EatenDishes { get; set; }

    }
}
