using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpCLients
{
    public interface IProjectHttpClient
    {
        public Task<IEnumerable<Guid>> GetTeamLeadsForEmployee(Guid employeeId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
