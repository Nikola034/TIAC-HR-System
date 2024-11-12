using Application.Common.Repositories;
using Common.HttpCLients;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServiceApplication.Queries.Project
{
    public  class GetProjectInfoHandler : IRequestHandler<GetProjectInfoQuery, ProjectInfo>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        private readonly IEmployeeHttpClient _employeeHttpClient;
        public GetProjectInfoHandler(IProjectRepository projectRepository, IClientRepository clientRepository, IEmployeeProjectRepository employeeProjectRepository, IEmployeeHttpClient employeeHttpClient)
        {
            _projectRepository = projectRepository;
            _clientRepository = clientRepository;
            _employeeProjectRepository = employeeProjectRepository;
            _employeeHttpClient = employeeHttpClient;
        }

        public async Task<ProjectInfo> Handle(GetProjectInfoQuery req, CancellationToken ct)
        {
            var project = await _projectRepository.GetProjectByIdAsync(req.Id, ct);

            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            IEnumerable<Guid> employeeIds = await _employeeProjectRepository.GetAllEmployeesOnProjectAsync(req.Id, ct);

            foreach (var employeeId in employeeIds)
            {
                var employee = await _employeeHttpClient.GetEmployeeByIdAsync(employeeId, ct);
                var jsonObject = JObject.Parse(employee);
                employees.Add(new EmployeeDTO(Guid.Parse(jsonObject["id"].ToString()), jsonObject["name"].ToString(), jsonObject["surname"].ToString()));
            }

            return new ProjectInfo(project, employees);
        }

    }

    public record GetProjectInfoQuery(Guid Id) : IRequest<ProjectInfo>;

    public record ProjectInfo(Core.Entities.Project Project, List<EmployeeDTO> Employees);

    public record EmployeeDTO(Guid Id, string Name, string Surname);
}
