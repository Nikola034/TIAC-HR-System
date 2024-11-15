using ProjectServiceApplication.Commands.Project;

namespace ProjectServicePresentation.Contracts;

public class AddOrRemoveEmployeeFromProjectResponse
{
    public IEnumerable<EmployeeDto> Working { get; set; }
    public IEnumerable<EmployeeDto> NotWorking { get; set; }
}

