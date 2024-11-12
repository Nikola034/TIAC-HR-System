using MediatR;
using FastEndpoints;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Client;

public class Update: Endpoint<UpdateClientRequest,UpdateClientResponse>
{
    private readonly IMediator _mediator;

    public Update(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Put("/projects/clients");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateClientRequest req, CancellationToken ct)
    {
        var client = await _mediator.Send(req.ToCommand(), ct);
        await SendOkAsync(client.ToApiResponseFromUpdate(), ct);
    }
}