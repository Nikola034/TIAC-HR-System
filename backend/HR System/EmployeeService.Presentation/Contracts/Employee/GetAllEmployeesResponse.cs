using EmployeeService.Core.Entities;

namespace EmployeeService.Presentation.Contracts.Employee
{
    public class GetAllEmployeesResponse
    {
        public IEnumerable<Core.Entities.Employee> Employees { get; set; }
    }
}
