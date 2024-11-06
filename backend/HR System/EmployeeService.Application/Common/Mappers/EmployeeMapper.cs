using EmployeeService.Application.Commands.Employee;
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

        public static Employee ToDomainEntity(this UpdateEmployeeCommand employeeCommand)
        {
            return new Employee
            {
                Id = employeeCommand.Id,
                Name = employeeCommand.Name,
                Surname = employeeCommand.Surname,
                DaysOff = employeeCommand.DaysOff,
                Role = employeeCommand.Role,
            };
        }

        public static Employee ToDomainEntity(this DeleteEmployeeCommand employeeCommand)
        {
            return new Employee
            {
                Id = employeeCommand.Id,
            };
        }
    }
}
