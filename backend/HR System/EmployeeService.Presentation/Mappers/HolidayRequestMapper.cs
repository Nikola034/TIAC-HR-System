using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Commands.HolidayRequest;
using EmployeeService.Application.Queries.HolidayRequest;
using EmployeeService.Core.Entities;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;

namespace EmployeeService.Presentation.Mappers
{
    public static class HolidayRequestMapper
    {

        public static HolidayRequestByIdResponse ToApiResponse(this HolidayRequest holidayRequest)
        {
            return new HolidayRequestByIdResponse
            {
                Id = holidayRequest.Id,
                Start = holidayRequest.Start,
                End = holidayRequest.End,
                Status = holidayRequest.Status,
                SenderId = holidayRequest.SenderId,
                Sender = holidayRequest.Sender
            };
        }

        public static GetAllHolidayRequestsResponse ToApiResponse(this GetAllHolidayRequestsQueryResponse response)
        {
            return new GetAllHolidayRequestsResponse
            {
                HolidayRequests = response.HolidayRequests,
                ItemsPerPage = response.ItemsPerPage,
                TotalPages = response.TotalPages,
                Page = response.Page,
            };
        }

        public static GetAllHolidayRequestsBySenderIdResponse ToApiResponse(this GetAllHolidayRequestsBySenderIdQueryResponse response)
        {
            return new GetAllHolidayRequestsBySenderIdResponse
            {
                HolidayRequests = response.HolidayRequests,
                TotalPages = (int)response.TotalPages,
                Page = (int)response.Page,
                ItemsPerPage = response.Items
            };
        }
        public static GetAllHolidayRequestsToApproveResponse ToApiResponse(this GetAllHolidayRequestsToApproveQueryResponse response)
        {
            return new GetAllHolidayRequestsToApproveResponse
            {
                HolidayRequests = response.HolidayRequests,
            };
        }
        public static CreateHolidayRequestResponse ToApiResponseFromCreate(this HolidayRequest holidayRequest)
        {
            return new CreateHolidayRequestResponse
            {
                Id = holidayRequest.Id,
                Start = holidayRequest.Start,
                End = holidayRequest.End,
                Status = holidayRequest.Status,
                Sender = holidayRequest.Sender
            };
        }
        public static UpdateHolidayRequestResponse ToApiResponseFromUpdate(this HolidayRequest holidayRequest)
        {
            return new UpdateHolidayRequestResponse
            {
                Id = holidayRequest.Id,
                Start = holidayRequest.Start,
                End = holidayRequest.End,
                Status = holidayRequest.Status,
                Sender = holidayRequest.Sender
            };
        }

        public static CreateHolidayRequestCommand ToCommand(this CreateHolidayRequestRequest request) => new CreateHolidayRequestCommand(request.Start, request.End, request.Status, request.SenderId);
        public static UpdateHolidayRequestCommand ToCommand(this UpdateHolidayRequestRequest request) => new UpdateHolidayRequestCommand(request.Id, request.Start, request.End, request.Status);
    }
}
