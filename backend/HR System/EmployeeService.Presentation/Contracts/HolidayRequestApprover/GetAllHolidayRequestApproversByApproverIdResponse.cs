namespace EmployeeService.Presentation.Contracts.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByApproverIdResponse
    {
        public IEnumerable<Core.Entities.HolidayRequestApprover> HolidayRequestApprovers { get; set; }

    }
}
