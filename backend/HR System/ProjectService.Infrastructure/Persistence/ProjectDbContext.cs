using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjectServiceInfrastructure.Persistence;

public class ProjectDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Core.Entities.Client> Clients { get; set; }
    public DbSet<Core.Entities.Project> Projects { get; set; }
    public DbSet<Core.Entities.EmployeeProject> EmployeeProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}