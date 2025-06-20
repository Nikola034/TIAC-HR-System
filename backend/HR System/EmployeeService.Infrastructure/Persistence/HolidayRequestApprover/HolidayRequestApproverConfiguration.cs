﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Persistence.HolidayRequestApprover
{
    public class HolidayRequestApproverConfiguration : IEntityTypeConfiguration<Core.Entities.HolidayRequestApprover>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.HolidayRequestApprover> builder)
        {
            builder.ToTable("holidayrequestapprovers");


            builder.Property(x => x.Id).HasColumnName("id").HasConversion(id => id.ToString(), id => new Guid(id));
            builder.Property(x => x.ApproverId).HasColumnName("approverid").HasConversion(id => id.ToString(), id => new Guid(id));
            builder.Property(x => x.RequestId).HasColumnName("requestid").HasConversion(id => id.ToString(), id => new Guid(id));
            builder.Property(x => x.Status).HasColumnName("status");

            builder.HasKey(x => x.Id);
        }
    }
}
