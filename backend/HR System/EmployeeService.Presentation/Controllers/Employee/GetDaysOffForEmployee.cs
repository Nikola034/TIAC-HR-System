using EmployeeService.Application.Queries.Employee;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class GetDaysOffForEmployees : EndpointWithoutRequest<GetDaysOffForEmployeesResponse>
    {
        IMediator _mediator;

        public GetDaysOffForEmployees(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/daysOff");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var report = await _mediator.Send(new GetDaysOffForEmployeesQuery());
            if (report is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(report.ToApiResponse(), ct);
        }
    }
}
