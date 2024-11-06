using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Commands.HolidayRequest;
using EmployeeService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Common.Mappers
{
    public static class HolidayRequestMapper
    {
        public static HolidayRequest ToDomainEntity(this CreateHolidayRequestCommand holidayRequestCommand)
        {
            return new HolidayRequest
            {
                Start = holidayRequestCommand.Start,
                End = holidayRequestCommand.End,
                Status = holidayRequestCommand.Status,
            };
        }
    }
}
