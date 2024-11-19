using Application.Commands;
using FastEndpoints;
using MediatR;
using ProjectServiceApplication.Commands.Project;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Project;

public class RemoveEmployeeFromProject : Endpoint<AddOrRemoveEmployeeFromProjectRequest, AddOrRemoveEmployeeFromProjectResponse>
{
    IMediator _mediator;

    public RemoveEmployeeFromProject(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("/projects/removeFromProject");
    }

    public override async Task HandleAsync(AddOrRemoveEmployeeFromProjectRequest req, CancellationToken ct)
    {
        var project = await _mediator.Send(req.ToRemoveCommand(), ct);
        await SendOkAsync(project.ToApiResponse(), ct);
    }
}