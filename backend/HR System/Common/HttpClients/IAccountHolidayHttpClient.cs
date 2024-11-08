using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpCLients
{
    public interface IAccountHolidayHttpClient
    {
        public Task<bool> DeleteEmployeeAccount(Guid employeeId);
    }
}
