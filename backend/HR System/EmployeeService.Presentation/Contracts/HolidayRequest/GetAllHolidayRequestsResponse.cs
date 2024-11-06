namespace EmployeeService.Presentation.Contracts.HolidayRequest
{
    public class GetAllHolidayRequestsResponse
    {
        public IEnumerable<Core.Entities.HolidayRequest> HolidayRequests { get; set; }

    }
}
