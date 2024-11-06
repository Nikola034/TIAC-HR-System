using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectServiceInfrastructure.Persistence.EmployeeProject;

public class EmployeeProjectConfiguration : IEntityTypeConfiguration<Core.Entities.EmployeeProject>
{
    public void Configure(EntityTypeBuilder<Core.Entities.EmployeeProject> builder)
    {
        builder.ToTable("employeeprojects");

        builder.Property(x => x.Id).HasColumnName("id").HasConversion(id => id.ToString(), id => new Guid(id));
        builder.Property(x => x.EmployeeId).HasColumnName("employeeid").IsRequired().HasConversion(id => id.ToString(), id => new Guid(id));;
        builder.Property(x => x.ProjectId).HasColumnName("projectid").IsRequired().HasConversion(id => id.ToString(), id => new Guid(id));;

        builder.HasKey(x => x.Id);
    }
}