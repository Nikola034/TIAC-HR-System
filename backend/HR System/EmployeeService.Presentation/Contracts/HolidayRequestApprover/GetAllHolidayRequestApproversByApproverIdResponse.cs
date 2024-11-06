namespace EmployeeService.Presentation.Contracts.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByApproverIdResponse
    {
        public IEnumerable<Core.Entities.HolidayRequest> HolidayRequests { get; set; }

    }
}
