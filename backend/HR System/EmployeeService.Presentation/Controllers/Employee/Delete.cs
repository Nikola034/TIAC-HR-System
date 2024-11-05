using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class Delete : Endpoint<DeleteEmployeeRequest, DeleteEmployeeResponse>
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

        public override async Task HandleAsync(DeleteEmployeeRequest req, CancellationToken ct)
        {
            var employee = await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync(employee.ToApiResponseFromDelete(), ct);
        }
    }
}
