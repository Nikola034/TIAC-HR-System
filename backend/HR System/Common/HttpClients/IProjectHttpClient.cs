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
        public Task<IEnumerable<Guid>> GetTeamLeadsForEmployeeAsync(Guid employeeId, string token, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<Guid>> GetProjectsForEmployeeAsync(Guid employeeId, string token, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> RemoveEmployeeFromProjectAsync(RemoveEmployeeFromProjectDto dto, string token, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<Guid>> GetLeadingProjectIdsForEmployeeAsync(Guid employeeId, string token, CancellationToken cancellationToken = default(CancellationToken));
        public Task<HttpResponseMessage> GetProjectByIdAsync(Guid projectId, string token, CancellationToken cancellationToken = default);
        public Task<bool> RemoveTeamLeadFromProjectAsync(Guid projectId, string token, CancellationToken cancellationToken = default);
    }
}
