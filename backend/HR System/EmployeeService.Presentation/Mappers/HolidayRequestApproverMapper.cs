using EmployeeService.Application.Commands.HolidayRequest;
using EmployeeService.Application.Commands.HolidayRequestApprover;
using EmployeeService.Core.Entities;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Contracts.HolidayRequestApprover;

namespace EmployeeService.Presentation.Mappers
{
    public static class HolidayRequestApproverMapper
    {
        public static GetHolidayRequestApproverByIdResponse ToApiResponse(this Core.Entities.HolidayRequestApprover holidayRequest)
        {
            return new GetHolidayRequestApproverByIdResponse
            {
                HolidayRequestApprover = holidayRequest
            };
        }

        public static GetAllHolidayRequestApproversResponse ToApiResponse(this IEnumerable<Core.Entities.HolidayRequestApprover> holidayRequests)
        {
            return new GetAllHolidayRequestApproversResponse
            {
                HolidayRequestApprovers = holidayRequests
            };
        }
        public static UpdateHolidayRequestApproverCommand ToCommand(this UpdateHolidayRequestApproverRequest request) => new UpdateHolidayRequestApproverCommand(request.RequestId, request.ApproverId, request.Status);

        public static UpdateHolidayRequestApproverResponse ToApiResponseFromUpdate(this HolidayRequestApprover holidayRequestApprover)
        {
            return new UpdateHolidayRequestApproverResponse
            {
                Id = holidayRequestApprover.Id,
                RequestId = holidayRequestApprover.RequestId,
                ApproverId = holidayRequestApprover.ApproverId,
                Status = holidayRequestApprover.Status,
            };
        }
    }
}
