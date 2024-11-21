using EmployeeService.Application.Queries.HolidayRequestApprover;
using EmployeeService.Presentation.Contracts.HolidayRequestApprover;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidatRequestApprover
{
    public class GetAllByApproverId : EndpointWithoutRequest<GetAllHolidayRequestApproversByApproverIdResponse>
    {
        IMediator _mediator;

        public GetAllByApproverId(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/holidayRequestApprovers/byApprover/{approverId}");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var holidayRequests = await _mediator.Send(new GetAllHolidayRequestsApproversByApproverIdQuery(Route<Guid>("approverId")));
            if (holidayRequests is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(holidayRequests.ToApiResponse(), ct);
        }
    }
}
