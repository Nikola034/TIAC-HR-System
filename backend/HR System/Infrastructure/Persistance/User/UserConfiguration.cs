using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Core.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.User> builder)
        {
            builder.ToTable("user");


            builder.Property(x => x.Id).HasColumnName("id").HasConversion(id => id.ToString(), id => new Guid(id));
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(20)");
            builder.Property(x => x.LastName).HasColumnName("last_name").HasColumnType("varchar(20)");
            builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(20)");
            builder.Property(x => x.Password).HasColumnName("password");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
