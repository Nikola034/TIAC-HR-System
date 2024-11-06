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
        public Task<HolidayRequest?> DeleteHolidayRequestAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        public Task<HolidayRequest?> UpdateHolidayRequestAsync(HolidayRequest holidayRequest, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<HolidayRequest>> GetAllHolidayRequestsAsync(int page, CancellationToken cancellationToken = default(CancellationToken));
    }
}
