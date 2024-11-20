using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using EmployeeService.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<bool> DeleteHolidayRequestAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Core.Entities.HolidayRequest deletedEntity = await _context.HolidayRequests.FindAsync(id);
            if (deletedEntity is null)
            {
                return false;
            }
            _context.HolidayRequests.Remove(deletedEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<Core.Entities.HolidayRequest>> GetAllHolidayRequestsAsync(int page, int items, CancellationToken cancellationToken = default)
        {
            var holidayRequests = new List<Core.Entities.HolidayRequest>();
            if (page != -1 && items != -1)
            {
                holidayRequests = await _context.HolidayRequests.OrderBy(x => x.Id)
                .Include(x => x.Sender)
                .Skip((page - 1) * items)
                .Take(items)
                .ToListAsync(cancellationToken);
                return holidayRequests;
            }
            holidayRequests = await _context.HolidayRequests.OrderBy(x => x.Id)
                .Include(x => x.Sender)
                .ToListAsync(cancellationToken);
            return holidayRequests;
        }
        public async Task<IEnumerable<Core.Entities.HolidayRequest>> GetAllHolidayRequestsBySenderIdAsync(Guid senderId, int page, int items, string status, CancellationToken cancellationToken = default)
        {
            var holidayRequests = new List<Core.Entities.HolidayRequest>();
            Core.Enums.HolidayRequestStatus statusEnum = Core.Enums.HolidayRequestStatus.Approved;
            if(status != "all")
            {
                if(status == "Approved")
                {
                    statusEnum = Core.Enums.HolidayRequestStatus.Approved;
                }
                else if (status == "Pending")
                {
                    statusEnum = Core.Enums.HolidayRequestStatus.Pending;
                }
                else if (status == "Denied")
                {
                    statusEnum = Core.Enums.HolidayRequestStatus.Denied;
                }
            }
            if (page != -1 && items != -1)
            {
                holidayRequests = await _context.HolidayRequests.OrderBy(x => x.Id)
                .Include(x => x.Sender)
                .Where(x => x.SenderId == senderId && (status.Equals("all") || x.Status == statusEnum))
                .OrderBy(x => x.Start)
                .Skip((page - 1) * items)
                .Take(items)
                .ToListAsync(cancellationToken);
                return holidayRequests;
            }
            holidayRequests = await _context.HolidayRequests.OrderBy(x => x.Id)
                .Include(x => x.Sender)
                .Where(x => x.SenderId == senderId && (status.Equals("all") || x.Status == statusEnum))
                .OrderBy(x => x.Start)
                .ToListAsync(cancellationToken);
            return holidayRequests;
        }
        public async Task<int> GetTotalPagesAsync(int page, int items, CancellationToken cancellationToken = default)
        {
            var count = await _context.HolidayRequests.CountAsync(cancellationToken);
            return (int)Math.Ceiling((double)count / items);
        }

        public async Task<Core.Entities.HolidayRequest?> GetHolidayRequestByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.HolidayRequests.Include(x => x.Sender).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Core.Entities.HolidayRequest?> UpdateHolidayRequestAsync(Core.Entities.HolidayRequest holidayRequest, CancellationToken cancellationToken = default)
        {
            _context.HolidayRequests.Update(holidayRequest);
            await _context.SaveChangesAsync(cancellationToken);
            return holidayRequest;
        }

        public async Task<bool> CheckHolidayRequestExistenceAsync(Guid senderId, DateTime start, DateTime end, CancellationToken cancellationToken = default)
        {
            return await _context.HolidayRequests.AsNoTracking().FirstOrDefaultAsync(x => x.SenderId == senderId && x.Status != Core.Enums.HolidayRequestStatus.Denied && ((x.Start.Date >= start.Date && x.Start.Date < end.Date) || (x.Start.Date < start.Date && x.End.Date > start.Date))) is not null;
        }
    }
}
