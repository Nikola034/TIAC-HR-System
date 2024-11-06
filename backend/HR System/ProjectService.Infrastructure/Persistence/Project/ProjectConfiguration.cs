using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectServiceInfrastructure.Persistence.Project;

public class ProjectConfiguration : IEntityTypeConfiguration<Core.Entities.Project>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Project> builder)
    {
        builder.ToTable("projects");

        builder.Property(x => x.Id).HasColumnName("id").HasConversion(id => id.ToString(), id => new Guid(id));
        builder.Property(x => x.Title).HasColumnName("title").HasColumnType("varchar(30)").IsRequired();
        builder.Property(x => x.Description).HasColumnName("description").HasColumnType("varchar(200)").IsRequired();
        builder.Property(x => x.ClientId).HasColumnName("clientid").IsRequired().HasConversion(id => id.ToString(), id => new Guid(id));;
        builder.Property(x => x.TeamLeadId).HasColumnName("teamleadid").HasConversion(id => id.ToString(), id => new Guid(id));;

        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Client)
            .WithMany()
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}