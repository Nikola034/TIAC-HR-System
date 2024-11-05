using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Persistance.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeDbContext _context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }
        public async Task<Core.Entities.Employee> CreateEmployeeAsync(Core.Entities.Employee employee, CancellationToken cancellationToken = default(CancellationToken))
        {
            var savedEntity = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public async Task<Core.Entities.Employee?> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Core.Entities.Employee deletedEntity = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(deletedEntity);
            await _context.SaveChangesAsync();
            return deletedEntity;
        }

        public async Task<IEnumerable<Core.Entities.Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Core.Entities.Employee?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Core.Entities.Employee?> UpdateEmployeeAsync(Core.Entities.Employee user, CancellationToken cancellationToken = default)
        {
            _context.Employees.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
