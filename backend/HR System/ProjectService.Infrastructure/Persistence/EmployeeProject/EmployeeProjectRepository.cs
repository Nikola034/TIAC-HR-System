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

    public async Task<bool> RemoveEmployeeFromProjectAsync(Guid employeeId,Guid projectId, CancellationToken ct = default(CancellationToken))
    {
        var existingEmployeeProject = await dbContext.EmployeeProjects.
            FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.ProjectId == projectId, ct);
        if (existingEmployeeProject == null)
        {
            return false;
        }
        dbContext.EmployeeProjects.Remove(existingEmployeeProject);
        await dbContext.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IEnumerable<Guid>> GetAllProjectsForEmployeeAsync(Guid employeeId, CancellationToken ct = default(CancellationToken))
    {
        var projects = await dbContext.EmployeeProjects
            .Where(x => x.EmployeeId == employeeId).Select(x=> x.ProjectId).ToListAsync(ct);
        return projects;
    }

    public async Task<IEnumerable<Guid>> GetAllEmployeesOnProjectAsync(Guid projectId, CancellationToken ct = default(CancellationToken))
    {
        var employees = await dbContext.EmployeeProjects
            .Where(x => x.ProjectId == projectId).Select(x=> x.EmployeeId).ToListAsync(ct);
        return employees;
    }

    public async Task RemoveProjectAsync(Guid projectId, CancellationToken ct = default(CancellationToken))
    {
        var existingEmployeeProjects = await dbContext.EmployeeProjects.
            Where(x => x.ProjectId == projectId).ToListAsync(ct);
        if (existingEmployeeProjects.Count == 0)
        {
            return;
        }
        dbContext.EmployeeProjects.RemoveRange(existingEmployeeProjects);
        await dbContext.SaveChangesAsync(ct);
        return;
    }

    public async Task RemoveProjectsAsync(IEnumerable<Guid> projectIds, CancellationToken ct = default(CancellationToken))
    {
        var existingEmployeeProjects = await dbContext.EmployeeProjects.
            Where(x => projectIds.Contains(x.ProjectId)).ToListAsync(ct);
        if (existingEmployeeProjects.Count == 0)
        {
            return;
        }
        dbContext.EmployeeProjects.RemoveRange(existingEmployeeProjects);
        await dbContext.SaveChangesAsync(ct);
        return;
    }
}