using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectService.Presentation.Contracts.Project;
using ProjectServiceApplication.Queries.Project;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectService.Presentation.Controllers.Project
{
    public class GetAllWithoutPaging : EndpointWithoutRequest<GetAllProjectsWithoutPagingResponse>
    {
        IMediator _mediator;

        public GetAllWithoutPaging(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/projects/without-paging");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllProjectsWithoutPagingQuery(), ct);
            await SendOkAsync(result.ToApiResponse(), ct);
        }
    }
}
