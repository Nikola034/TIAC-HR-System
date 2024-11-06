using EmployeeService.Core.Enums;

namespace EmployeeService.Presentation.Contracts.HolidayRequestApprover
{
    public class UpdateHolidayRequestApproverRequest
    {
        public Guid RequestId { get; set; }
        public Guid ApproverId { get; set; }
        public HolidayRequestStatus Status { get; set; }
    }
}
