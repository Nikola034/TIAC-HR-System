using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Controllers.Project;

public class GetAll : EndpointWithoutRequest<GetAllProjectsResponse>
{
    IMediator _mediator;

    public GetAll(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/projects/all/{page}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var page = Route<int>("page");
        var projects = await _mediator.Send(new GetAllProjectsQuery(page), ct);
        await SendOkAsync(new GetAllProjectsResponse { Projects = projects }, ct);
    }
}