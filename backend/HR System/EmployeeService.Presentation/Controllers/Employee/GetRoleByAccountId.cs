using EmployeeService.Application.Queries;
using EmployeeService.Application.Queries.Employee;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class GetRoleByAccountId : EndpointWithoutRequest<GetRoleByAccountIdResponse>
    {
        IMediator _mediator;

        public GetRoleByAccountId(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/getRole/{employeeAccountId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var employeeAccountId = Route<Guid>("employeeAccountId");
            var role = await _mediator.Send(new GetRoleByAccountIdQuery(employeeAccountId),ct);
            await SendOkAsync(new GetRoleByAccountIdResponse{Role = role}, ct);
        }
    }
}