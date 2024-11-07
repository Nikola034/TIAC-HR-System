namespace EmployeeService.Presentation.Contracts.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByRequestIdResponse
    {
        public IEnumerable<Core.Entities.HolidayRequestApprover> HolidayRequestApprovers { get; set; }
    }
}
