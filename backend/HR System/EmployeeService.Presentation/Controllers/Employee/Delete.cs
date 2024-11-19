using EmployeeService.Application.Commands.Employee;
using EmployeeService.Core.Errors;
using EmployeeService.Core.Primitives.Result;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class Delete : EndpointWithoutRequest<bool>
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete("employees/{id}");
            Policies("ManagersOnly");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(Route<Guid>("id")), ct);
            if (!result)
                await SendNotFoundAsync(ct);
            await SendOkAsync(ct);
        }
    }
}
