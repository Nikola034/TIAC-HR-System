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
        public Task<Employee> CreateAsync(Employee user, CancellationToken cancellationToken = default(CancellationToken));

    }
}
