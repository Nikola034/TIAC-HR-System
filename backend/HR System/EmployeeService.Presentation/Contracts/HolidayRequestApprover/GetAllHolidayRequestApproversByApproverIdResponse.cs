namespace EmployeeService.Presentation.Contracts.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByApproverIdResponse
    {
        public IEnumerable<GetAllHolidayRequestApproversByApproverIdDto> HolidayRequestApprovers { get; set; }
    }

    public class GetAllHolidayRequestApproversByApproverIdDto
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
