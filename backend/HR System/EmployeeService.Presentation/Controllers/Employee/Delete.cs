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
            AllowAnonymous(); 
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(Route<Guid>("id")), ct);
            if (result == Result.Failure<Core.Entities.Employee>(DomainErrors.Employee.NotFound))
                await SendNotFoundAsync(ct);
            await SendOkAsync(ct);
        }
    }
}
