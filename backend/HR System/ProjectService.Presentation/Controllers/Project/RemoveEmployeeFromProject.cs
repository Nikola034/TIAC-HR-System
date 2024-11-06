using Application.Commands;
using FastEndpoints;
using MediatR;
using ProjectServiceApplication.Commands.Project;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Project;

public class RemoveEmployeeFromProject: EndpointWithoutRequest<bool>
{
    private readonly IMediator _mediator;

    public RemoveEmployeeFromProject(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("projects/remove/{employeeId}/{projectId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var employeeId = Route<Guid>("employeeId");
        var projectId = Route<Guid>("projectId");
        var result = await _mediator.Send(new RemoveEmployeeFromProjectCommand(employeeId, projectId),ct);
        if (!result)
            await SendNotFoundAsync(ct);
        await SendOkAsync(ct);
    }
}