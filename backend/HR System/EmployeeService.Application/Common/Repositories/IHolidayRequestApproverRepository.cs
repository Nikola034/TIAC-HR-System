using EmployeeService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Common.Repositories
{
    public interface IHolidayRequestApproverRepository
    {
        public Task<HolidayRequestApprover?> GetHolidayRequestApproverByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        public Task<HolidayRequestApprover> CreateHolidayRequestApproverAsync(HolidayRequestApprover holidayRequestApprover, CancellationToken cancellationToken = default(CancellationToken));
        public Task<HolidayRequestApprover?> DeleteHolidayRequestApproverAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        public Task<HolidayRequestApprover?> UpdateHolidayRequestApproverAsync(HolidayRequestApprover holidayRequestApprover, CancellationToken cancellationToken = default(CancellationToken));
        public Task<IEnumerable<HolidayRequestApprover>> GetAllHolidayRequestApproversAsync(int page, CancellationToken cancellationToken = default(CancellationToken));
    }
}
