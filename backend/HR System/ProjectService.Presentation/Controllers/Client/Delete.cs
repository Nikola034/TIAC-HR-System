using Application.Commands;
using FastEndpoints;
using MediatR;

namespace ProjectServicePresentation.Controllers.Client;

public class Delete : EndpointWithoutRequest<bool>
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("projects/clients/{clientId}");
        Policies("ManagersOnly");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var clientId = Route<Guid>("clientId");
        var result = await _mediator.Send(new DeleteClientCommand(clientId), ct);
        if (!result)
            await SendNotFoundAsync(ct);
        await SendOkAsync(ct);
    }
}