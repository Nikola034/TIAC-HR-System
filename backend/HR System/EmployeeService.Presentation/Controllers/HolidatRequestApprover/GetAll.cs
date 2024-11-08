using EmployeeService.Application.Queries.HolidayRequest;
using EmployeeService.Application.Queries.HolidayRequestApprover;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Contracts.HolidayRequestApprover;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidatRequestApprover
{
    public class GetAll : EndpointWithoutRequest<GetAllHolidayRequestApproversResponse>
    {
        IMediator _mediator;

        public GetAll(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/holidayRequestApprovers/all/{page}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var holidayRequests = await _mediator.Send(new GetAllHolidayRequestsApproversQuery(Route<int>("page")));
            if (holidayRequests is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(holidayRequests.ToApiResponseFromGetAll(), ct);
        }
    }
}
