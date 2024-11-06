using Application.Common.Repositories;
using Application.Mappers;
using Core.Entities;
using MediatR;

namespace ProjectServiceApplication.Commands.Project
{
    public class AddEmployeeToProjectHandler : IRequestHandler<AddEmployeeToProjectCommand, EmployeeProject>
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public AddEmployeeToProjectHandler(IEmployeeProjectRepository employeeProjectRepository)
        {
            _employeeProjectRepository = employeeProjectRepository;
        }

        public async Task<EmployeeProject> Handle(AddEmployeeToProjectCommand req, CancellationToken ct)
        {
            var employeeProject = req.ToDomainEntity();
            var persistedEmployeeProject =
                await _employeeProjectRepository.AddEmployeeToProjectAsync(employeeProject, ct);
            return persistedEmployeeProject;
        }
    }

    public record AddEmployeeToProjectCommand(Guid EmployeeId, Guid ProjectId) : IRequest<EmployeeProject>;
}