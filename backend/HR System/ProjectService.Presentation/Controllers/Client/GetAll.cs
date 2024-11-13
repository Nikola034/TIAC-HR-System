using Application.Queries.Client;
using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

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
        Get("/projects/clients/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var pageQuery = Query<string>("page", isRequired: false);
        var itemsPerPageQuery = Query<string>("items-per-page", isRequired: false);
        var page = 1;
        if (pageQuery != null) 
        {
            page = Convert.ToInt32(pageQuery);
        }
        var itemsPerPage = 10;
        if (itemsPerPageQuery != null)
        {
            itemsPerPage = Convert.ToInt32(itemsPerPageQuery);

        }
        var result = await _mediator.Send(new GetAllClientsQuery(page,itemsPerPage), ct);
        await SendOkAsync(result.ToApiResponse(), ct);
    }
}