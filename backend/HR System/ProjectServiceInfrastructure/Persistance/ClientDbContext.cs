using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class ClientDbContext :DbContext
{
    public DbSet<Core.Entities.Client> Clients { get; set; }
    public ClientDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}