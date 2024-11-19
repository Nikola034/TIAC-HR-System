using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class Create : Endpoint<CreateHolidayRequestRequest, CreateHolidayRequestResponse>
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("employees/holidayRequests");
        }

        public override async Task HandleAsync(CreateHolidayRequestRequest req, CancellationToken ct)
        {
            var holidayRequest = await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync(holidayRequest.ToApiResponseFromCreate(), ct);
        }
    }
}
