using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProjectServiceInfrastructure.Persistence.EmployeeProject;

public class EmployeeProjectRepository(ProjectDbContext dbContext) : IEmployeeProjectRepository
{
    public async Task<Core.Entities.EmployeeProject> AddEmployeeToProjectAsync(Core.Entities.EmployeeProject employeeProject, CancellationToken ct = default(CancellationToken))
    {
        var newEmployeeProject = await dbContext.EmployeeProjects.AddAsync(employeeProject, ct);
        await dbContext.SaveChangesAsync(ct);
        return newEmployeeProject.Entity;
    }

    public async Task<bool> RemoveEmployeeFromProjectAsync(Core.Entities.EmployeeProject employeeProject, CancellationToken ct = default(CancellationToken))
    {
        var existingEmployeeProject = await dbContext.EmployeeProjects.
            FirstOrDefaultAsync(x => x.EmployeeId == employeeProject.EmployeeId && x.ProjectId == employeeProject.ProjectId, ct);
        if (existingEmployeeProject == null)
        {
            return false;
        }
        dbContext.EmployeeProjects.Remove(existingEmployeeProject);
        await dbContext.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IEnumerable<Guid>> GetAllProjectsForEmployeeAsync(Guid EmployeeId, CancellationToken ct = default(CancellationToken))
    {
        var projects = await dbContext.EmployeeProjects
            .Where(x => x.EmployeeId == EmployeeId).Select(x=> x.ProjectId).ToListAsync(ct);
        return projects;
    }

    public async Task<IEnumerable<Guid>> GetAllEmployeesOnProjectAsync(Guid ProjectId, CancellationToken ct = default(CancellationToken))
    {
        var employees = await dbContext.EmployeeProjects
            .Where(x => x.ProjectId == ProjectId).Select(x=> x.EmployeeId).ToListAsync(ct);
        return employees;
    }
}