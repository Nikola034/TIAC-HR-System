namespace EmployeeService.Presentation.Contracts.HolidayRequest
{
    public class GetAllHolidayRequestsToApproveResponse
    {
        public IEnumerable<Core.Entities.HolidayRequest> HolidayRequests { get; set; }
    }
}
