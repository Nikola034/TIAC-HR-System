using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpCLients
{
    public interface IAccountServiceHttpClient
    {
        public Task<bool> DeleteEmployeeAccount(Guid employeeId, string token, CancellationToken cancellationToken = default(CancellationToken));
    }
}
