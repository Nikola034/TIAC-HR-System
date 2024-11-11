using Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServiceApplication.Queries.Project
{
    public class GetProjectsReportHandler : IRequestHandler<GetProjectsReportQuery, GetProjectsReportQueryResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        public GetProjectsReportHandler(IProjectRepository projectRepository, IClientRepository clientRepository, IEmployeeProjectRepository employeeProjectRepository)
        {
            _projectRepository = projectRepository;
            _clientRepository = clientRepository;
            _employeeProjectRepository = employeeProjectRepository;
        }
        public async Task<GetProjectsReportQueryResponse> Handle(GetProjectsReportQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllProjectsWithoutPagingAsync(cancellationToken);
            List<ProjectsReport> reports = new List<ProjectsReport>();
            // logika....
            return new GetProjectsReportQueryResponse(reports);
        }
    }


    public record GetProjectsReportQuery() : IRequest<GetProjectsReportQueryResponse>;

    public record GetProjectsReportQueryResponse(IEnumerable<ProjectsReport> Reports);
    public record ProjectsReport(/*ovde ubacujes ono sta ide u report, zaposleni, klijent, projekat...*/);
}
