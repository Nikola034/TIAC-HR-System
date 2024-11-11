using Application.Common.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ProjectServiceInfrastructure.Persistence.Project;

public class ProjectRepository(ProjectDbContext dbContext) : IProjectRepository
{
    public async Task<Core.Entities.Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Projects.Include(x => x.Client).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Core.Entities.Project?> CreateProjectAsync(Core.Entities.Project project, CancellationToken cancellationToken = default)
    {
        var newProject = await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return newProject.Entity;
    }

    public async Task<Core.Entities.Project?> UpdateProjectAsync(Core.Entities.Project project, CancellationToken cancellationToken = default)
    {
        var oldProject = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == project.Id, cancellationToken);
        if (oldProject == null)
        {
            return null;
        }

        dbContext.ChangeTracker.Clear();
        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync(cancellationToken);
        return project;
    }

    public async Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (project == null)
        {
            return false;
        }
        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<Core.Entities.Project>> GetAllProjectsAsync(int page, CancellationToken cancellationToken = default)
    {
        var projects = await dbContext.Projects.Include(x => x.Client).OrderBy(x => x.Id)
            .Skip((page - 1) * 10)
            .Take(10)
            .ToListAsync(cancellationToken);
        return projects;
    }
    
    public async Task<IEnumerable<Core.Entities.Project>> GetAllProjectsByIdAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var projects = await dbContext.Projects.Include(x => x.Client)
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
        return projects;
    }

    public async Task<IEnumerable<Core.Entities.Project>> GetAllProjectsWithoutPagingAsync(CancellationToken ct = default)
    {
        var projects = await dbContext.Projects.Include(x => x.Client).OrderBy(x => x.Id)
            .ToListAsync(ct);
        return projects;
    }
}