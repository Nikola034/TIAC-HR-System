using EmployeeService.Core.Entities;

namespace EmployeeService.Presentation.Contracts.Employee
{
    public class GetAllEmployeesResponse
    {
        public IEnumerable<Core.Entities.Employee> Employees { get; set; }
        public int Page {  get; set; }
        public int ItemsPerPage {  get; set; }
        public int TotalPages { get; set; }
    }
}
