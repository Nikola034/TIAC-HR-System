using EmployeeService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Common.Repositories
{
    public interface IEmployeeRepository
    {

        public Task<Employee?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        public Task<Employee> CreateEmployeeAsync(Employee user, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        public Task<Employee?> UpdateEmployeeAsync(Employee user, CancellationToken cancellationToken = default(CancellationToken));
        public Task<decimal> GetTotalPagesAsync(int page, int items, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<Employee>> GetAllEmployeesAsync(int page, int items, CancellationToken cancellationToken = default(CancellationToken));
    }
}
