using EmployeeService.Application.Commands;
using EmployeeService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Common.Mappers
{
    public static class EmployeeMapper
    {
        public static Employee ToDomainEntity(this CreateEmployeeCommand employeeCommand)
        {
            return new Employee
            {
                Name = employeeCommand.Name,
                Surname = employeeCommand.Surname,
                DaysOff = employeeCommand.DaysOff,
                Role = employeeCommand.Role,
            };
        }

    }
}
