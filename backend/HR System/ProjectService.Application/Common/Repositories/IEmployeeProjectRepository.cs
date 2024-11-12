using Core.Entities;

namespace Application.Common.Repositories;

public interface IEmployeeProjectRepository
{
    public Task<EmployeeProject> AddEmployeeToProjectAsync(EmployeeProject employeeProject, CancellationToken ct = default(CancellationToken));
    public Task<bool> RemoveEmployeeFromProjectAsync(Guid employeeId,Guid projectId, CancellationToken ct = default(CancellationToken));
    public Task<IEnumerable<Guid>> GetAllProjectsForEmployeeAsync(Guid employeeId, CancellationToken ct = default(CancellationToken));
    public Task<IEnumerable<Guid>> GetAllEmployeesOnProjectAsync(Guid projectId, CancellationToken ct = default(CancellationToken));
}