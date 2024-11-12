using System.Collections;
using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Project
{
    public class
        GetAllProjectsForEmployeeHandler : IRequestHandler<GetAllProjectsForEmployeeQuery,
        IEnumerable<Core.Entities.Project>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public GetAllProjectsForEmployeeHandler(IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository)
        {
            _projectRepository = projectRepository;
            _employeeProjectRepository = employeeProjectRepository;
        }
        
        public async Task<IEnumerable<Core.Entities.Project>> Handle(GetAllProjectsForEmployeeQuery req,
            CancellationToken ct)
        {
            var projectIds = await _employeeProjectRepository.GetAllProjectsForEmployeeAsync(req.EmployeeId,ct);
            var projects = await _projectRepository.GetAllProjectsByIdAsync(projectIds, ct);
            return projects;
        }
    }

    public record GetAllProjectsForEmployeeQuery(Guid EmployeeId) : IRequest<IEnumerable<Core.Entities.Project>>;
}