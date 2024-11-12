using EmployeeService.Application.Commands.HolidayRequest;
using EmployeeService.Application.Commands.HolidayRequestApprover;
using EmployeeService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Common.Mappers
{
    public static class HolidayRequestApproverMapper
    {

        public static HolidayRequestApprover ToDomainEntity(this CreateHolidayRequestApproverCommand holidayRequestApproverCommand)
        {
            return new HolidayRequestApprover
            {
                ApproverId = holidayRequestApproverCommand.ApproverId,
                RequestId = holidayRequestApproverCommand.RequestId,
                Status = holidayRequestApproverCommand.Status,
            };
        }
        public static HolidayRequestApprover ToDomainEntity(this UpdateHolidayRequestApproverCommand holidayRequestApproverCommand)
        {
            return new HolidayRequestApprover
            {
                RequestId = holidayRequestApproverCommand.RequestId,
                ApproverId = holidayRequestApproverCommand.ApproverId,
                Status = holidayRequestApproverCommand.Status,
            };
        }
    }
}
