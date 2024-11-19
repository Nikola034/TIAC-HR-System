using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Controllers.Project;

public class GetAllEmployeesOnProject : EndpointWithoutRequest<GetAllEmployeesOnProjectResponse>
{
    IMediator _mediator;

    public GetAllEmployeesOnProject(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/projects/allEmployees/{projectId}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("projectId");
        var employeesIds = await _mediator.Send(new GetAllEmployeesOnProjectQuery(id), ct);
        await SendOkAsync(new GetAllEmployeesOnProjectResponse { EmployeeIds = employeesIds }, ct);
    }
}