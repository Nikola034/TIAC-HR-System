using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Persistance.Account;
using Infrastructure.Persistance.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Core.Entities.Account> Accounts { get; set; }
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}