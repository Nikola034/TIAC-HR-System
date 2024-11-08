using EmployeeService.Application.Queries;
using EmployeeService.Application.Queries.Employee;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class GetAll : EndpointWithoutRequest<GetAllEmployeesResponse>
    {
        IMediator _mediator;

        public GetAll(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/all/{page}/{items-per-page}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var employee = await _mediator.Send(new GetAllEmployeesQuery(Route<int>("page"), Route<int>("items-per-page")));
            if (employee is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(employee.ToApiResponse(), ct);
        }
    }
}
