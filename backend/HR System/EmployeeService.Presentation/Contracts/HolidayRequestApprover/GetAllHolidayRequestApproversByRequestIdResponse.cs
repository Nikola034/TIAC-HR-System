namespace EmployeeService.Presentation.Contracts.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByRequestIdResponse
    {
        public IEnumerable<Core.Entities.HolidayRequest> HolidayRequests { get; set; }
    }
}
