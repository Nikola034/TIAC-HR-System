namespace EmployeeService.Presentation.Contracts.Employee;

public class GetAllDevelopersResponse
{
    public IEnumerable<Core.Entities.Employee> Developers { get; set; }
}