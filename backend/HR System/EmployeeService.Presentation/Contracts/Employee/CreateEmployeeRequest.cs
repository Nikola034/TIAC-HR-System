using EmployeeService.Core.Enums;

namespace EmployeeService.Presentation.Contracts.Employee
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public EmployeeRole Role { get; set; }
        public int DaysOff { get; set; }
    }
}
