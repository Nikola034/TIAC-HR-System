using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Commands.HolidayRequest;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class Delete : EndpointWithoutRequest<bool>
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete("holidayRequests/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteHolidayRequestCommand(Route<Guid>("id")), ct);
            if (!result)
                await SendNotFoundAsync(ct);
            await SendOkAsync(ct);
        }
    }
}
