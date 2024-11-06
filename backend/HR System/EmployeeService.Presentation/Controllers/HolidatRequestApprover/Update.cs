using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Contracts.HolidayRequestApprover;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidatRequestApprover
{
    public class Update : Endpoint<UpdateHolidayRequestApproverRequest, UpdateHolidayRequestApproverResponse>
    {
        private readonly IMediator _mediator;

        public Update(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Put("holidayRequestApprovers");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateHolidayRequestApproverRequest req, CancellationToken ct)
        {
            var holidayRequestApprover = await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync(holidayRequestApprover.ToApiResponseFromUpdate(), ct);
        }
    }
}
