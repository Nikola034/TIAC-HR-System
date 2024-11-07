using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Account
{
    public class AccountConfiguration : IEntityTypeConfiguration<Core.Entities.Account>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Account> builder)
        {
            builder.ToTable("accounts");


            builder.Property(x => x.Id).HasColumnName("id").HasConversion(id => id.ToString(), id => new Guid(id));
            builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(30)");
            builder.Property(x => x.Password).HasColumnName("password").HasColumnType("varchar(100)");
            builder.Property(x => x.RefreshToken).HasColumnName("refreshtoken").HasColumnType("varchar(100)");
            builder.Property(x => x.RefreshTokenValidTo).HasColumnName("refreshtokenvalidto").HasColumnType("timestamp");
            builder.Property(x => x.PasswordResetToken).HasColumnName("passwordresettoken").HasColumnType("varchar(100)");
            builder.Property(x => x.PasswordResetTokenValidTo).HasColumnName("passwordresettokenvalidto").HasColumnType("timestamp");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}