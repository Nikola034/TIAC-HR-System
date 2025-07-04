﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Entities;

namespace EmployeeService.Infrastructure.Persistance.Employee
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Core.Entities.Employee>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Employee> builder)
        {
            builder.ToTable("employees");


            builder.Property(x => x.Id).HasColumnName("id").HasConversion(id => id.ToString(), id => new Guid(id));
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(20)");
            builder.Property(x => x.Surname).HasColumnName("surname").HasColumnType("varchar(20)");
            builder.Property(x => x.DaysOff).HasColumnName("daysoff").HasColumnType("int");
            builder.Property(x => x.Role).HasColumnName("role").HasColumnType("int");
            builder.Property(x => x.AccountId).HasColumnName("accountid").HasConversion(id => id.ToString(), id => new Guid(id));

            builder.HasKey(x => x.Id);
        }
    }
}
