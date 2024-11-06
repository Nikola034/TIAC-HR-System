using EmployeeService.Core.Enums;

namespace EmployeeService.Presentation.Contracts.HolidayRequest
{
    public class CreateHolidayRequestRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public HolidayRequestStatus Status { get; set; }
    }
}
