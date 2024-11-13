using EmployeeService.Application.Queries;
using EmployeeService.Application.Queries.Employee;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class GetEmployeeByAccountId : EndpointWithoutRequest<EmployeeByIdResponse>
    {
        IMediator _mediator;

        public GetEmployeeByAccountId(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/getByAccountId/{employeeAccountId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var employeeAccountId = Route<Guid>("employeeAccountId");
            var employee = await _mediator.Send(new GetRoleByAccountIdQuery(employeeAccountId),ct);
            await SendOkAsync(employee.ToApiResponse(), ct);
        }
    }
}