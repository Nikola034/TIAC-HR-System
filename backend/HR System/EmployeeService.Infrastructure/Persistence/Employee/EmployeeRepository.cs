using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            await _context.SaveChangesAsync(cancellationToken);
            return savedEntity.Entity;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Core.Entities.Employee deletedEntity = await _context.Employees.FindAsync(id, cancellationToken);
            if (deletedEntity == null)
            {
                return false;
            }
            _context.Employees.Remove(deletedEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<Core.Entities.Employee?> GetFirstManagerAsync(Guid senderId, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.OrderBy(x => x.Id)
            .FirstOrDefaultAsync(x => x.Id != senderId && x.Role == Core.Enums.EmployeeRole.Manager, cancellationToken);
            return employee;
        }
        public async Task<IEnumerable<Core.Entities.Employee>> GetAllEmployeesAsync(int page, int items, string name, string surname, string role, CancellationToken cancellationToken = default)
        {
            Core.Enums.EmployeeRole roleEnum = Core.Enums.EmployeeRole.Manager;
            if (role != "all")
            {
                if(role == "Manager")
                {
                    roleEnum = Core.Enums.EmployeeRole.Manager;
                }
                else if (role == "Developer")
                {
                    roleEnum = Core.Enums.EmployeeRole.Developer;
                }
            }
            var employees = await _context.Employees.OrderBy(x => x.Name)
            .Where(x => (name.Equals("") || x.Name.ToLower().Contains(name)) && (surname.Equals("") || x.Surname.ToLower().Contains(surname)) && (role.Equals("all") || x.Role == roleEnum))
            .Skip((page - 1) * items)
            .Take(items)
            .ToListAsync(cancellationToken);
            return employees;
        }

        public async Task<Core.Entities.Employee?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> GetTotalPagesAsync(int page, int items, string name, string surname, string role, CancellationToken cancellationToken = default)
        {
            Core.Enums.EmployeeRole roleEnum = Core.Enums.EmployeeRole.Manager;
            if (role != "all")
            {
                if (role == "Manager")
                {
                    roleEnum = Core.Enums.EmployeeRole.Manager;
                }
                else if (role == "Developer")
                {
                    roleEnum = Core.Enums.EmployeeRole.Developer;
                }
            }
            var count = await _context.Employees
                .Where(x => (name.Equals("") || x.Name.ToLower().Contains(name)) && (surname.Equals("") || x.Surname.ToLower().Contains(surname)) && (role.Equals("all") || x.Role == roleEnum))
                .CountAsync(cancellationToken);
            return (int)Math.Ceiling((double)count / items);
        }
        public async Task<Core.Entities.Employee?> UpdateEmployeeAsync(Core.Entities.Employee user, CancellationToken cancellationToken = default)
        {
            _context.Employees.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
        
        public async Task<Core.Entities.Employee?> GetEmployeeByAccountIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.AccountId == id, cancellationToken);
        }

        public async Task<IEnumerable<Core.Entities.Employee>> GetAllEmployeesWithoutPagingAsync(CancellationToken cancellationToken = default)
        {
            var employees = await _context.Employees.OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
            return employees;
        }

        public async Task<IEnumerable<Core.Entities.Employee>> GetAllDevelopersAsync(CancellationToken cancellationToken = default)
        {
            var employees = await _context.Employees.OrderBy(x => x.Id)
                .Where(x => x.Role == Core.Enums.EmployeeRole.Developer)
                .ToListAsync(cancellationToken);
            return employees;
        }
    }
}
