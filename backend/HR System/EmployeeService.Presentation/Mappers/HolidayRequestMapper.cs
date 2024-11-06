using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Commands.HolidayRequest;
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

        public static GetAllHolidayRequestsResponse ToApiResponse(this IEnumerable<Core.Entities.HolidayRequest> holidayRequests)
        {
            return new GetAllHolidayRequestsResponse
            {
                HolidayRequests = holidayRequests
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

        public static DeleteHolidayRequestResponse ToApiResponseFromDelete(this HolidayRequest holidayRequest)
        {
            return new DeleteHolidayRequestResponse
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
        public static DeleteHolidayRequestCommand ToCommand(this DeleteHolidayRequestRequest request) => new DeleteHolidayRequestCommand(request.Id);
    }
}
