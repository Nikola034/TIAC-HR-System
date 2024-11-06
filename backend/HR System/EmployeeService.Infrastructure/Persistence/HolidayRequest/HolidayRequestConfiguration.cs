using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Persistence.HolidayRequest
{
    public class HolidayRequestConfiguration : IEntityTypeConfiguration<Core.Entities.HolidayRequest>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.HolidayRequest> builder)
        {
            builder.ToTable("holidayrequests");


            builder.Property(x => x.Id).HasColumnName("id").HasConversion(id => id.ToString(), id => new Guid(id));
            builder.Property(x => x.Start).HasColumnName("start").HasColumnType("date");
            builder.Property(x => x.End).HasColumnName("end").HasColumnType("date");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.SenderId).HasColumnName("senderid");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Sender).WithMany().HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
