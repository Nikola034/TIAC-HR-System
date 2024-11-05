using EmployeeService.Core.Enums;

namespace EmployeeService.Presentation.Contracts
{
    public class CreateEmployeeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public EmployeeRole Role { get; set; }
        public int DaysOff { get; set; }
    }
}
