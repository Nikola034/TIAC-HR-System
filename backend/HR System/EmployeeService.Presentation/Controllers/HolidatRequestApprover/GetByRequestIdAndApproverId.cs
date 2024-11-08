using EmployeeService.Application.Queries.HolidayRequestApprover;
using EmployeeService.Presentation.Contracts.HolidayRequestApprover;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidatRequestApprover
{
    public class GetByRequestIdAndApproverId : EndpointWithoutRequest<GetHolidayRequestApproverByApproverIdAndRequestIdResponse>
    {
        IMediator _mediator;

        public GetByRequestIdAndApproverId(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/holidayrequestApprovers/{requestId}/{approverId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var requestId = Route<Guid>("requestId");
            var approverId = Route<Guid>("approverId");
            var request = await _mediator.Send(new GetHolidayRequestsApproverByApproverIdAndRequestIdQuery(requestId, approverId));
            if (request is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(request.ToApiResponseFromGetByResponseIdAndApproverId(), ct);
        }
    }
}
