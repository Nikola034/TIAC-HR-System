using EmployeeService.Application.Queries.Employee;
using EmployeeService.Application.Queries.HolidayRequest;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class GetAll : EndpointWithoutRequest<GetAllHolidayRequestsResponse>
    {
        IMediator _mediator;

        public GetAll(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("holidayRequests/all/{page}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var holidayRequests = await _mediator.Send(new GetAllHolidayRequestsQuery(Route<int>("page")));
            if (holidayRequests is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(holidayRequests.ToApiResponse(), ct);
        }
    }
}
