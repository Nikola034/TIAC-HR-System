using ProjectServiceApplication.Queries.Project;

namespace ProjectService.Presentation.Contracts.Project
{
    public class GetProjectsReportResponse
    {
        public IEnumerable<ProjectsReport> Reports { get; set; }
    }
}
