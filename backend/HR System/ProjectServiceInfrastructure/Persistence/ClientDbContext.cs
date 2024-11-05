using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjectServiceInfrastructure.Persistence;

public class ClientDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Core.Entities.Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}