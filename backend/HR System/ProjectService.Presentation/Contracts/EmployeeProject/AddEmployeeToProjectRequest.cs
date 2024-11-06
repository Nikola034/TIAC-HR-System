namespace ProjectServicePresentation.Contracts;

public class AddEmployeeToProjectRequest
{
    public Guid EmployeeId { get; set; }
    public Guid ProjectId { get; set; }
}