namespace Common.HttpCLients.Dtos;

public class RemoveEmployeeFromProjectDto
{
    public Guid EmployeeId { get; set; }
    public Guid ProjectId { get; set; }
}