using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectServiceApplication.Commands.Project;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Project;

public class Create: Endpoint<CreateProjectRequest,CreateProjectResponse>
{
    IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/projects");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CreateProjectRequest req,CancellationToken ct)
    {
        var project = await _mediator.Send(req.ToCommand(), ct);
        await SendOkAsync(project.ToApiResponseFromCreate(), ct);
    }

}