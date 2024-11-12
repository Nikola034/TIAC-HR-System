using Application.Commands;
using FastEndpoints;
using MediatR;

namespace ProjectServicePresentation.Controllers.Project;

public class Delete : EndpointWithoutRequest<bool>
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("projects/{projectId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var projectId = Route<Guid>("projectId");
        var result = await _mediator.Send(new DeleteProjectCommand(projectId), ct);
        if (!result)
            await SendNotFoundAsync(ct);
        await SendOkAsync(ct);
    }
}