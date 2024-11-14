using EmployeeService.Application.Queries.Employee;
using EmployeeService.Presentation.Contracts.Employee;
using FastEndpoints;
using MediatR;

namespace EmployeeService.Presentation.Controllers.Employee;

public class GetAllDevelopers: EndpointWithoutRequest<GetAllDevelopersResponse>
{
    IMediator _mediator;

    public GetAllDevelopers(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/employees/developers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllDevelopersQuery(),ct);
        await SendOkAsync(new GetAllDevelopersResponse{Developers = result}, ct);
    }
}