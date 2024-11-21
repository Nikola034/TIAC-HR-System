namespace EmployeeService.Presentation.Contracts.HolidayRequest
{
    public class GetAllHolidayRequestsBySenderIdResponse
    {
        public IEnumerable<Core.Entities.HolidayRequest> HolidayRequests { get; set; }
        public int TotalPages {  get; set; }
        public int Page {  get; set; }
        public int ItemsPerPage {  get; set; }
    }
}
