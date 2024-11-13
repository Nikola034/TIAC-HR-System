using Core.Entities;

namespace Application.Common.Repositories;

public interface IProjectRepository
{
    public Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken ct = default(CancellationToken));
    public Task<IEnumerable<Project>> GetAllProjectsAsync(int pageNumber,int itemNumber, CancellationToken ct = default(CancellationToken));
    public Task<IEnumerable<Project>> GetAllProjectsWithoutPagingAsync(CancellationToken ct = default(CancellationToken));
    public Task<Project?> CreateProjectAsync(Project project, CancellationToken ct = default(CancellationToken));
    public Task<Project?> UpdateProjectAsync(Project project, CancellationToken ct = default(CancellationToken));
    public Task<bool> DeleteProjectAsync(Guid id, CancellationToken ct = default(CancellationToken));
    public Task<IEnumerable<Project>> GetAllProjectsByIdAsync(IEnumerable<Guid> ids, CancellationToken ct = default(CancellationToken));
    public Task<int> GetTotalPageNumber(int itemNumber, CancellationToken ct = default(CancellationToken));
    public Task<IEnumerable<Project>> GetAllProjectsByClientIdAsync(Guid clientId, CancellationToken ct = default(CancellationToken));
    public int GetNumberOfProjectsByClient(Guid clientId);

}