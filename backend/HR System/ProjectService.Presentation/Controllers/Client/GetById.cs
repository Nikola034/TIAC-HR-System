using Application.Queries.Client;
using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Client;

public class GetById : EndpointWithoutRequest<ClientByIdResponse>
{
    IMediator _mediator;

    public GetById(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/clients/{clientId}");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var clientId = Route<Guid>("clientId");
        var client = await _mediator.Send(new GetClientByIdQuery(clientId));
        if (client is null)
        {
            await SendNotFoundAsync();
        }

        await SendOkAsync(client.ToApiResponse(), ct);
    }

}