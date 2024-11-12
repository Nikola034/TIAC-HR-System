using Application.Queries.Client;
using FastEndpoints;
using MediatR;
using ProjectServiceApplication.Commands.Client;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Client;

public class Create: Endpoint<CreateClientRequest,CreateClientResponse>
{
    IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/projects/clients");
        Policies("ManagersOnly");
    }
    
    public override async Task HandleAsync(CreateClientRequest req,CancellationToken ct)
    {
        var client = await _mediator.Send(req.ToCommand(), ct);
        await SendOkAsync(client.ToApiResponseFromCreate(), ct);
    }

}