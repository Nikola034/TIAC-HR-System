using EmployeeService.Application.Queries.Employee;

namespace EmployeeService.Presentation.Contracts.Employee
{
    public class GetDaysOffForEmployeesResponse
    {
        public IEnumerable<GetDaysOffForEmployeeReport> Reports { get; set; }
    }
}
