using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectService.Presentation.Contracts.Project;
using ProjectServiceApplication.Queries.Project;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectService.Presentation.Controllers.Project
{
    public class GetProjectInfo : EndpointWithoutRequest<GetProjectInfoResponse>
    {
        IMediator _mediator;

        public GetProjectInfo(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/projects/info/{projectId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var projectId = Route<Guid>("projectId");
            var project = await _mediator.Send(new GetProjectInfoQuery(projectId));
            if (project is null)
            {
                await SendNotFoundAsync();
            }

            await SendOkAsync(project.ToApiResponse(), ct);
        }
    }
}