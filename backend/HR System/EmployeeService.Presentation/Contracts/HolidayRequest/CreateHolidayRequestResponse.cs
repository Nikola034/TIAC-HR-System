using EmployeeService.Core.Enums;

namespace EmployeeService.Presentation.Contracts.HolidayRequest
{
    public class CreateHolidayRequestResponse
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public HolidayRequestStatus Status { get; set; }
        public Core.Entities.Employee Sender { get; set; }
    }
}
