using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Controllers.Project;

public class GetAllByEmployeeId : EndpointWithoutRequest<GetAllProjectsResponse>
{
    IMediator _mediator;

    public GetAllByEmployeeId(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/projects/allForEmployee/{employeeId}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("employeeId");
        var projects = await _mediator.Send(new GetAllProjectsForEmployeeQuery(id), ct);
        await SendOkAsync(new GetAllProjectsResponse { Projects = projects }, ct);
    }
}