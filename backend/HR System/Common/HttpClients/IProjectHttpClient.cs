using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.HttpCLients.Dtos;

namespace Common.HttpCLients
{
    public interface IProjectHttpClient
    {
        public Task<IEnumerable<Guid>> GetTeamLeadsForEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<Guid>> GetProjectsForEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> RemoveEmployeeFromProjectAsync(RemoveEmployeeFromProjectDto dto, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<Guid>> GetLeadingProjectIdsForEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default(CancellationToken));
        public Task<HttpResponseMessage> GetProjectByIdAsync(Guid projectId, CancellationToken cancellationToken = default);
        public Task<bool> RemoveTeamLeadFromProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}
