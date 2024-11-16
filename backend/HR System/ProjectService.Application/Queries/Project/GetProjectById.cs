using Application.Common.Repositories;
using Application.Mappers;
using Common.HttpCLients;
using MediatR;
using ProjectServiceApplication.Commands.Project;

namespace Application.Queries.Project
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, GetProjectByIdQueryResponse?>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        private readonly IEmployeeHttpClient _employeeHttpClient;

        public GetProjectByIdHandler(IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository, IEmployeeHttpClient employeeHttpClient)
        {
            _projectRepository = projectRepository;
            _employeeProjectRepository = employeeProjectRepository;
            _employeeHttpClient = employeeHttpClient;
        }

        public async Task<GetProjectByIdQueryResponse?> Handle(GetProjectByIdQuery req, CancellationToken ct)
        {
            var project = await _projectRepository.GetProjectByIdAsync(req.Id, ct);
            if (project is null)
                return null;
            var workingIds = await _employeeProjectRepository.GetAllEmployeesOnProjectAsync(req.Id, ct);
            var jsonString = await _employeeHttpClient.GetAllDevelopersAsync(ct);
            return ProjectMapper.MapJsonStringToResponse(jsonString,workingIds,project);
        }
        
    }

    public record GetProjectByIdQuery(Guid Id) : IRequest<GetProjectByIdQueryResponse?>;
    public record GetProjectByIdQueryResponse(Core.Entities.Project Project, IEnumerable<EmployeeDto> Working, IEnumerable<EmployeeDto> NotWorking);
}