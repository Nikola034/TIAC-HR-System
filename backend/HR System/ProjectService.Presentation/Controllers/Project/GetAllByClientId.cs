using FastEndpoints;
using MediatR;
using ProjectService.Presentation.Contracts.Project;
using ProjectServiceApplication.Queries.Project;

namespace ProjectService.Presentation.Controllers.Project;

public class GetAllByClientId : EndpointWithoutRequest<GetAllByClientIdResponse>
{
    IMediator _mediator;

    public GetAllByClientId(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/projects/getByClientId/{clientId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var clientId = Route<Guid>("clientId");
        var projects = await _mediator.Send(new GetAllProjectsByClientIdQuery(clientId), ct);
        await SendOkAsync(new GetAllByClientIdResponse { Projects = projects }, ct);
    }
}