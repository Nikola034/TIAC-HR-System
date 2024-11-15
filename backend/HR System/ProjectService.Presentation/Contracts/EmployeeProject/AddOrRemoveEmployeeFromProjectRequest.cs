namespace ProjectServicePresentation.Contracts;

public class AddOrRemoveEmployeeFromProjectRequest
{
    public Guid EmployeeId { get; set; }
    public Guid ProjectId { get; set; }
}