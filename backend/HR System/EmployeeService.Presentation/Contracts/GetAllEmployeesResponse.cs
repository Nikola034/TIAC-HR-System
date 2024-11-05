using EmployeeService.Core.Entities;

namespace EmployeeService.Presentation.Contracts
{
    public class GetAllEmployeesResponse
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
