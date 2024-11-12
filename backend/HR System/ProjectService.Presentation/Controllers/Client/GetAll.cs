using Application.Queries.Client;
using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Controllers.Client;

public class GetAll : EndpointWithoutRequest<GetAllClientsResponse>
{
    IMediator _mediator;

    public GetAll(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/projects/clients/all/{page}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var page = Route<int>("page");
        var clients = await _mediator.Send(new GetAllClientsQuery(page), ct);
        await SendOkAsync(new GetAllClientsResponse { Clients = clients }, ct);
    }
}