using EmployeeService.Application.Queries.HolidayRequestApprover;
using EmployeeService.Presentation.Contracts.HolidayRequestApprover;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidatRequestApprover
{
    public class GetAllByRequestId : EndpointWithoutRequest<GetAllHolidayRequestApproversByRequestIdResponse>
    {
        IMediator _mediator;

        public GetAllByRequestId(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("holidayRequestApprovers/byRequest/{requestId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var holidayRequests = await _mediator.Send(new GetAllHolidayRequestsApproversByRequestIdQuery(Route<Guid>("requestId")));
            if (holidayRequests is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(holidayRequests.ToApiResponseFromGetAllByRequestId(), ct);
        }
    }
}
