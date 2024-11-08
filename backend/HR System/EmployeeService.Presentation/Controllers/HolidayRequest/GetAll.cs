using EmployeeService.Application.Queries.Employee;
using EmployeeService.Application.Queries.HolidayRequest;
using EmployeeService.Core.Entities;
using EmployeeService.Presentation.Contracts.Employee;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class GetAll : EndpointWithoutRequest<GetAllHolidayRequestsResponse>
    {
        IMediator _mediator;

        public GetAll(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/holidayRequests");
            AllowAnonymous();
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
            var holidayRequest = await _mediator.Send(new GetAllHolidayRequestsQuery(page, itemsPerPage));
            if (holidayRequest is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(holidayRequest.ToApiResponse(), ct);
        }
    }
}
