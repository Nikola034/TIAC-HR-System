using Core.Entities;

namespace Application.Common.Repositories;

public interface IEmployeeProjectRepository
{
    public Task<EmployeeProject> AddEmployeeToProjectAsync(EmployeeProject employeeProject, CancellationToken ct = default(CancellationToken));
    public Task<bool> RemoveEmployeeFromProjectAsync(EmployeeProject employeeProject, CancellationToken ct = default(CancellationToken));
    public Task<IEnumerable<Guid>> GetAllProjectsForEmployeeAsync(Guid EmployeeId, CancellationToken ct = default(CancellationToken));
    public Task<IEnumerable<Guid>> GetAllEmployeesOnProjectAsync(Guid ProjectId, CancellationToken ct = default(CancellationToken));
}