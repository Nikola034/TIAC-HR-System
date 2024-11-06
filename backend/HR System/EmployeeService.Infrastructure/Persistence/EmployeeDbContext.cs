using EmployeeService.Core.Entities;
using EmployeeService.Infrastructure.Persistance.Employee;
using EmployeeService.Infrastructure.Persistence.HolidayRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Persistance
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Core.Entities.Employee> Employees { get; set; }
        public DbSet<Core.Entities.HolidayRequest> HolidayRequests { get; set; }
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new HolidayRequestConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
