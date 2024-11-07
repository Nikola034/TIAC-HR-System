using EmployeeService.Application.Queries.HolidayRequest;
using EmployeeService.Application.Queries.HolidayRequestApprover;
using EmployeeService.Presentation.Contracts.HolidayRequest;
using EmployeeService.Presentation.Contracts.HolidayRequestApprover;
using EmployeeService.Presentation.Mappers;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.HolidatRequestApprover
{
    public class GetById : EndpointWithoutRequest<GetHolidayRequestApproverByIdResponse>
    {
        IMediator _mediator;

        public GetById(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/holidayrequestApprovers/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var request = await _mediator.Send(new GetHolidayRequestApproverByIdQuery(id));
            if (request is null)
            {
                await SendNotFoundAsync(ct);
            }

            await SendOkAsync(request.ToApiResponseFromGetById(), ct);
        }
    }
}
