namespace EmployeeService.Presentation.Contracts.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversResponse
    {
        public IEnumerable<Core.Entities.HolidayRequestApprover> HolidayRequestApprovers { get; set; }

    }
}
