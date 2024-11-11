using Application.Queries.Project;
using FastEndpoints;
using MediatR;
using ProjectService.Presentation.Contracts.Project;
using ProjectServiceApplication.Queries.Project;
using ProjectServicePresentation.Contracts;

namespace ProjectService.Presentation.Controllers.Project
{
    public class GetProjectsReport : EndpointWithoutRequest<GetProjectsReportResponse>
    {
        IMediator _mediator;

        public GetProjectsReport(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/projects/report");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var reports = await _mediator.Send(new GetProjectsReportQuery(), ct);
            await SendOkAsync(new GetProjectsReportResponse { Reports = reports.Reports }, ct);
        }
    }
}
