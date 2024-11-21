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
        Get("/projects/clients/{clientId}");
        Policies("ManagersOnly");
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var clientId = Route<Guid>("clientId");
        var response = await _mediator.Send(new GetClientByIdQuery(clientId),ct);
        if (response.Client is null)
        {
            await SendNotFoundAsync(ct);
        }

        await SendOkAsync(response.ToApiResponse(), ct);
    }

}