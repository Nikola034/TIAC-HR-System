using EmployeeService.Application.Queries.Employee;
using EmployeeService.Application.Queries.HolidayRequest;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class GetById : EndpointWithoutRequest<HolidayRequestByIdResponse>
    {
        IMediator _mediator;

        public GetById(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/holidayRequests/{requestId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var requestId = Route<Guid>("requestId");
            var request = await _mediator.Send(new GetHolidayRequestByIdQuery(requestId));
            if (request is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(request.ToApiResponse(), ct);
        }
    }
}
