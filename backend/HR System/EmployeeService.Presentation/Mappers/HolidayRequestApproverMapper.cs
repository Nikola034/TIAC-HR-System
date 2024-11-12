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
        public static GetHolidayRequestApproverByIdResponse ToApiResponseFromGetById(this Core.Entities.HolidayRequestApprover holidayRequest)
        {
            return new GetHolidayRequestApproverByIdResponse
            {
                HolidayRequestApprover = holidayRequest
            };
        }
        public static GetHolidayRequestApproverByApproverIdAndRequestIdResponse ToApiResponseFromGetByResponseIdAndApproverId(this Core.Entities.HolidayRequestApprover holidayRequest)
        {
            return new GetHolidayRequestApproverByApproverIdAndRequestIdResponse
            {
                HolidayRequestApprover = holidayRequest
            };
        }

        public static GetAllHolidayRequestApproversResponse ToApiResponseFromGetAll(this IEnumerable<Core.Entities.HolidayRequestApprover> holidayRequests)
        {
            return new GetAllHolidayRequestApproversResponse
            {
                HolidayRequestApprovers = holidayRequests
            };
        }
        public static GetAllHolidayRequestApproversByRequestIdResponse ToApiResponseFromGetAllByRequestId(this IEnumerable<Core.Entities.HolidayRequestApprover> holidayRequests)
        {
            return new GetAllHolidayRequestApproversByRequestIdResponse
            {
                HolidayRequestApprovers = holidayRequests
            };
        }
        public static GetAllHolidayRequestApproversByApproverIdResponse ToApiResponseFromGetAllByApproverId(this IEnumerable<Core.Entities.HolidayRequestApprover> holidayRequests)
        {
            return new GetAllHolidayRequestApproversByApproverIdResponse
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
