using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Project

{
    public class GetAllEmployeesOnProjectHandler : IRequestHandler<GetAllEmployeesOnProjectQuery, IEnumerable<Guid>>
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public GetAllEmployeesOnProjectHandler(IEmployeeProjectRepository employeeProjectRepository)
        {
            _employeeProjectRepository = employeeProjectRepository;
        }

        public async Task<IEnumerable<Guid>> Handle(GetAllEmployeesOnProjectQuery req, CancellationToken ct)
        {
            var employeeIds = await _employeeProjectRepository.GetAllEmployeesOnProjectAsync(req.ProjectId, ct);
            return employeeIds;
        }
    }

    public record GetAllEmployeesOnProjectQuery(Guid ProjectId) : IRequest<IEnumerable<Guid>>;
}