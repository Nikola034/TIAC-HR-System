namespace EmployeeService.Presentation.Contracts.Employee;

public class EmployeeByAccountIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Role { get; set; }
    public int DaysOff { get; set; }
    public Guid AccountId { get; set; }
}