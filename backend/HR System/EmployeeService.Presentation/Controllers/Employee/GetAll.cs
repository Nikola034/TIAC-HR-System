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
            Get("employees");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var pageQuery = Query<string>("page", isRequired: false);
            var itemsPerPageQuery = Query<string>("items-per-page", isRequired: false);
            int page = 1;
            if (pageQuery != null) 
            {
                page = Convert.ToInt32(pageQuery);
            }
            int itemsPerPage = 10;
            if (itemsPerPageQuery != null)
            {
                itemsPerPage = Convert.ToInt32(itemsPerPageQuery);

            }
            var employee = await _mediator.Send(new GetAllEmployeesQuery(page, itemsPerPage));
            if (employee is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(employee.ToApiResponse(), ct);
        }
    }
}
