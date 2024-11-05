using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class Create : Endpoint<CreateEmployeeRequest, CreateEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("employees");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateEmployeeRequest req, CancellationToken ct)
        {
            var employee = await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync(employee.ToApiResponseFromCreate(), ct);
        }
    }
}
