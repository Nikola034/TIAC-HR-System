using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Commands.HolidayRequest;
using EmployeeService.Core.Entities;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;

namespace EmployeeService.Presentation.Mappers
{
    public static class HolidayRequestMapper
    {
        public static CreateHolidayRequestResponse ToApiResponseFromCreate(this HolidayRequest holidayRequest)
        {
            return new CreateHolidayRequestResponse
            {
                Start = holidayRequest.Start,
                End = holidayRequest.End,
                Status = holidayRequest.Status,
            };
        }

        public static CreateHolidayRequestCommand ToCommand(this CreateHolidayRequestRequest request) => new CreateHolidayRequestCommand(request.Start, request.End, request.Status);

    }
}
