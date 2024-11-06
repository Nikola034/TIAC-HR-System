using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class Delete : Endpoint<DeleteHolidayRequestRequest, DeleteHolidayRequestResponse>
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

        public override async Task HandleAsync(DeleteHolidayRequestRequest req, CancellationToken ct)
        {
            var holidayRequest = await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync(holidayRequest.ToApiResponseFromDelete(), ct);
        }
    }
}
