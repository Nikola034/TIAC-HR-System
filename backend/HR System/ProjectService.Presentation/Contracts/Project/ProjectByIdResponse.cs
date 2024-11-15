using Core.Entities;
using ProjectServiceApplication.Commands.Project;

namespace ProjectServicePresentation.Contracts;

public class ProjectByIdResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Client Client { get; set; }
    public Guid? TeamLeadId { get; set; }
    public IEnumerable<EmployeeDto> Working { get; set; }
    public IEnumerable<EmployeeDto> NotWorking { get; set; }
}