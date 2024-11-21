using EmployeeService.Application.Queries.HolidayRequest;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class GetAllToApprove : EndpointWithoutRequest<GetAllHolidayRequestsToApproveResponse>
    {
        IMediator _mediator;

        public GetAllToApprove(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/holidayRequests/toApprove/{approverId}");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var approverId = Route<Guid>("approverId");
            var holidayRequests = await _mediator.Send(new GetAllHolidayRequestsToApprovesQuery(approverId));
            if (holidayRequests is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(holidayRequests.ToApiResponse(), ct);
        }
    }
}
