using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using EmployeeService.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Persistence.HolidayRequest
{
    public class HolidayRequestRepository : IHolidayRequestRepository
    {
        private EmployeeDbContext _context;
        public HolidayRequestRepository(EmployeeDbContext context)
        {
            _context = context;
        }
        public async Task<Core.Entities.HolidayRequest> CreateHolidayRequestAsync(Core.Entities.HolidayRequest holidayRequest, CancellationToken cancellationToken = default)
        {
            var savedEntity = await _context.HolidayRequests.AddAsync(holidayRequest);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public async Task<Core.Entities.HolidayRequest?> DeleteHolidayRequestAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Core.Entities.HolidayRequest deletedEntity = await _context.HolidayRequests.FindAsync(id);
            _context.HolidayRequests.Remove(deletedEntity);
            await _context.SaveChangesAsync();
            return deletedEntity;
        }

        public async Task<IEnumerable<Core.Entities.HolidayRequest>> GetAllHolidayRequestsAsync(int page, CancellationToken cancellationToken = default)
        {
            var holidayRequests = await _context.HolidayRequests.OrderBy(x => x.Id)
            .Skip((page - 1) * 10)
            .Take(10)
            .ToListAsync(cancellationToken);
            return holidayRequests;
        }

        public async Task<Core.Entities.HolidayRequest?> GetHolidayRequestByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.HolidayRequests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Core.Entities.HolidayRequest?> UpdateHolidayRequestAsync(Core.Entities.HolidayRequest holidayRequest, CancellationToken cancellationToken = default)
        {
            _context.HolidayRequests.Update(holidayRequest);
            await _context.SaveChangesAsync();
            return holidayRequest;
        }
    }
}
