using MediatR;
using FastEndpoints;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Project;

public class Update: Endpoint<UpdateProjectRequest,UpdateProjectResponse>
{
    private readonly IMediator _mediator;

    public Update(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Put("projects");
    }

    public override async Task HandleAsync(UpdateProjectRequest req, CancellationToken ct)
    {
        var project = await _mediator.Send(req.ToCommand(), ct);
        await SendOkAsync(project.ToApiResponseFromUpdate(), ct);
    }
}