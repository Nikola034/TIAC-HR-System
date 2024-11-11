using EmployeeService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Common.Repositories
{
    public interface IHolidayRequestRepository
    {
        public Task<HolidayRequest?> GetHolidayRequestByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        public Task<HolidayRequest> CreateHolidayRequestAsync(HolidayRequest holidayRequest, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> DeleteHolidayRequestAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        public Task<HolidayRequest?> UpdateHolidayRequestAsync(HolidayRequest holidayRequest, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<HolidayRequest>> GetAllHolidayRequestsAsync(int page, int items, CancellationToken cancellationToken = default(CancellationToken));
        public Task<int> GetTotalPagesAsync(int page, int items, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<Core.Entities.HolidayRequest>> GetAllHolidayRequestsBySenderIdAsync(Guid senderId, CancellationToken cancellationToken = default);
        public Task<bool> CheckHolidayRequestExistenceAsync(Guid senderId, DateTime start, DateTime end, CancellationToken cancellationToken = default(CancellationToken));
    }
}
