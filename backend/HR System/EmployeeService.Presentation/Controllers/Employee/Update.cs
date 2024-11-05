using FastEndpoints;
using MediatR;
using EmployeeService.Presentation.Mappers;
using EmployeeService.Presentation.Contracts.Employee;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class Update : Endpoint<UpdateEmployeeRequest, UpdateEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public Update(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Put("employees");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateEmployeeRequest req, CancellationToken ct)
        {
            var employee = await _mediator.Send(req.ToCommand(), ct);
            await SendOkAsync(employee.ToApiResponseFromUpdate(), ct);
        }
    }
}
