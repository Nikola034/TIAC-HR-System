using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using EmployeeService.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Persistence.HolidayRequestApprover
{
    public class HolidayRequestApproverRepository : IHolidayRequestApproverRepository
    {
        private EmployeeDbContext _context;
        public HolidayRequestApproverRepository(EmployeeDbContext context)
        {
            _context = context;
        }
        public async Task<Core.Entities.HolidayRequestApprover> CreateHolidayRequestApproverAsync(Core.Entities.HolidayRequestApprover holidayRequestApprover, CancellationToken cancellationToken = default)
        {
            var savedEntity = await _context.HolidayRequestApprovers.AddAsync(holidayRequestApprover);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public async Task<Core.Entities.HolidayRequestApprover?> DeleteHolidayRequestApproverAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Core.Entities.HolidayRequestApprover deletedEntity = await _context.HolidayRequestApprovers.FindAsync(id);
            _context.HolidayRequestApprovers.Remove(deletedEntity);
            await _context.SaveChangesAsync();
            return deletedEntity;
        }

        public async Task<IEnumerable<Core.Entities.HolidayRequestApprover>> GetAllHolidayRequestApproversAsync(int page, CancellationToken cancellationToken = default)
        {
            var holidayRequestApprovers = await _context.HolidayRequestApprovers.OrderBy(x => x.Id)
            .Skip((page - 1) * 10)
            .Take(10)
            .ToListAsync(cancellationToken);
            return holidayRequestApprovers;
        }

        public async Task<Core.Entities.HolidayRequestApprover?> GetHolidayRequestApproverByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.HolidayRequestApprovers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Core.Entities.HolidayRequestApprover?> UpdateHolidayRequestApproverAsync(Core.Entities.HolidayRequestApprover holidayRequestApprover, CancellationToken cancellationToken = default)
        {
            _context.HolidayRequestApprovers.Update(holidayRequestApprover);
            await _context.SaveChangesAsync();
            return holidayRequestApprover;
        }
    }
}
