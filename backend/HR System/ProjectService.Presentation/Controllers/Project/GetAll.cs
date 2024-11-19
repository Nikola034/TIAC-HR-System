using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

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
        Get("/projects/");
        Policies("ManagersOnly");
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
        var result = await _mediator.Send(new GetAllProjectsQuery(page,itemsPerPage), ct);
        await SendOkAsync(result.ToApiResponse(), ct);
    }
}