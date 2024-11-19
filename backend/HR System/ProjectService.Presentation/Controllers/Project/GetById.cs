using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Project;

public class GetById : EndpointWithoutRequest<ProjectByIdResponse>
{
    IMediator _mediator;

    public GetById(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/projects/{projectId}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
        var projectId = Route<Guid>("projectId");
        var project = await _mediator.Send(new GetProjectByIdQuery(projectId,authHeader),ct);
        if (project is null)
        {
            await SendNotFoundAsync(ct);
        }

        await SendOkAsync(project.ToApiResponse(), ct);
    }

}