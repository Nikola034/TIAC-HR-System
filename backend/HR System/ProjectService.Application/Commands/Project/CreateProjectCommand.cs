using Application.Common.Repositories;
using Application.Mappers;
using Core.Entities;
using MediatR;

namespace ProjectServiceApplication.Commands.Project
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Core.Entities.Project>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository)
        {
            _projectRepository = projectRepository;
            _employeeProjectRepository = employeeProjectRepository;
        }

        public async Task<Core.Entities.Project> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var persistedProject = await _projectRepository.CreateProjectAsync(domainEntity, cancellationToken);
            if (persistedProject?.TeamLeadId == null)
                return persistedProject;
            
            var employeeProject = new EmployeeProject
                { EmployeeId = (Guid)persistedProject.TeamLeadId, ProjectId = persistedProject.Id };
            await _employeeProjectRepository.AddEmployeeToProjectAsync(employeeProject, cancellationToken);
            return persistedProject;
        }
    }

    public record CreateProjectCommand(string Title, string Description, Guid ClientId, Guid? TeamLeadId)
        : IRequest<Core.Entities.Project>;
}