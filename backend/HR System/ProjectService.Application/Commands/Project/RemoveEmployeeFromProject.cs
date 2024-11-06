using Application.Common.Repositories;
using Core.Entities;
using MediatR;

namespace ProjectServiceApplication.Commands.Project
{
    public class RemoveEmployeeFromProjectHandler : IRequestHandler<RemoveEmployeeFromProjectCommand, bool>
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public RemoveEmployeeFromProjectHandler(IEmployeeProjectRepository employeeProjectRepository)
        {
            _employeeProjectRepository = employeeProjectRepository;
        }

        public async Task<bool> Handle(RemoveEmployeeFromProjectCommand req, CancellationToken ct)
        {
            var employeeProject = new EmployeeProject
                { EmployeeId = req.EmployeeId, ProjectId = req.ProjectId };
            var result = await _employeeProjectRepository.RemoveEmployeeFromProjectAsync(employeeProject, ct);
            return result;
        }
    }

    public record RemoveEmployeeFromProjectCommand(Guid EmployeeId, Guid ProjectId) : IRequest<bool>;
}