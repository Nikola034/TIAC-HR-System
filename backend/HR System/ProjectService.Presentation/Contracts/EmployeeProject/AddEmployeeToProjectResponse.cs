namespace ProjectServicePresentation.Contracts;

public class AddEmployeeToProjectResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ProjectId { get; set; }
}