using EmployeeService.Application.Queries.HolidayRequest;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidayRequest
{
    public class GetAllBySenderId : EndpointWithoutRequest<GetAllHolidayRequestsBySenderIdResponse>
    {
        IMediator _mediator;

        public GetAllBySenderId(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/holidayRequests/bySender/{senderId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var senderId = Route<Guid>("senderId");
            var pageQuery = Query<string>("page", isRequired: false);
            var itemsPerPageQuery = Query<string>("items-per-page", isRequired: false);
            int page = -1;
            if (pageQuery != null)
            {
                page = Convert.ToInt32(pageQuery);
            }
            int itemsPerPage = -1;
            if (itemsPerPageQuery != null)
            {
                itemsPerPage = Convert.ToInt32(itemsPerPageQuery);

            }
            var holidayRequest = await _mediator.Send(new GetAllHolidayRequestsBySenderIdQuery(senderId, page, itemsPerPage));
            if (holidayRequest is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(holidayRequest.ToApiResponse(), ct);
        }
    }
}
