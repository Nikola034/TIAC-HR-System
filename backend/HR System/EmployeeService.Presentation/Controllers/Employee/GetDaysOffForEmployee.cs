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
            Get("employees/daysOff/{employeeId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var employeeId = Route<Guid>("employeeId");
            var report = await _mediator.Send(new GetDaysOffForEmployeesQuery(employeeId));
            if (report is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(report.ToApiResponse(), ct);
        }
    }
}
