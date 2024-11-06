using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Client;

public class ClientConfiguration : IEntityTypeConfiguration<Core.Entities.Client>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Client> builder)
    {
        builder.ToTable("clients");

        builder.Property(x => x.Id).HasColumnName("id").HasConversion(id => id.ToString(), id => new Guid(id));
        builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(30)").IsRequired();
        builder.Property(x => x.Country).HasColumnName("country").HasColumnType("varchar(30)").IsRequired();

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.Name, x.Country }).IsUnique();
    }
}