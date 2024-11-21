using Application.Common.Repositories;
using Application.Mappers;
using Common.HttpCLients;
using Core.Entities;
using MediatR;

namespace ProjectServiceApplication.Commands.Project
{
    public class RemoveEmployeeFromProjectHandler : IRequestHandler<RemoveEmployeeFromProjectCommand, AddOrRemoveEmployeeFromProjectCommandResponse>
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        private readonly IEmployeeHttpClient _employeeHttpClient;
        private readonly IProjectRepository _projectRepository;

        public RemoveEmployeeFromProjectHandler(IEmployeeProjectRepository employeeProjectRepository,
            IEmployeeHttpClient employeeHttpClient, IProjectRepository projectRepository)
        {
            _employeeProjectRepository = employeeProjectRepository;
            _employeeHttpClient = employeeHttpClient;
            _projectRepository = projectRepository;
        }

        public async Task<AddOrRemoveEmployeeFromProjectCommandResponse> Handle(RemoveEmployeeFromProjectCommand req, CancellationToken ct)
        {
            await _employeeProjectRepository.RemoveEmployeeFromProjectAsync(req.EmployeeId,req.ProjectId, ct);
            var project = await _projectRepository.GetProjectByIdAsync(req.ProjectId, ct);
            if (project.TeamLeadId.ToString().Equals(req.EmployeeId.ToString()))
            {
                project.TeamLeadId = null;
                await _projectRepository.UpdateProjectAsync(project, ct);
            }
            var employeesOnProject = await _employeeProjectRepository.GetAllEmployeesOnProjectAsync(req.ProjectId, ct);
            var jsonString = await _employeeHttpClient.GetAllDevelopersAsync(req.Token,ct);
            return EmployeeProjectMapper.MapJsonStringToResponse(jsonString,employeesOnProject);
        }
    }
    public record RemoveEmployeeFromProjectCommand(Guid EmployeeId, Guid ProjectId, string Token) : IRequest<AddOrRemoveEmployeeFromProjectCommandResponse>;
}