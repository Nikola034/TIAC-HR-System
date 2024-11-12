using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class Update : Endpoint<UpdateHolidayRequestRequest, UpdateHolidayRequestResponse>
    {
        private readonly IMediator _mediator;

        public Update(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Put("employees/holidayRequests");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateHolidayRequestRequest req, CancellationToken ct)
        {
            var holidayRequest = await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync(holidayRequest.ToApiResponseFromUpdate(), ct);
        }
    }
}
