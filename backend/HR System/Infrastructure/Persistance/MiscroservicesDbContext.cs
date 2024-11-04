using Infrastructure.Persistance.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class MicroservicesDbContext : DbContext
    {
        public DbSet<Core.Entities.User> Users { get; set; }
        public MicroservicesDbContext(DbContextOptions<MicroservicesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
