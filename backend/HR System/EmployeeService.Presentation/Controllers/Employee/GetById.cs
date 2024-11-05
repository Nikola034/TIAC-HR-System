using EmployeeService.Application.Queries;
using EmployeeService.Presentation.Contracts;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee
{
    public class GetById : EndpointWithoutRequest<EmployeeByIdResponse>
    {
        IMediator _mediator;

        public GetById(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/employees/{employeeId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var employeeId = Route<Guid>("employeeId");
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(employeeId));
            if (employee is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(employee.ToApiResponse(), ct);
        }
    }
}
