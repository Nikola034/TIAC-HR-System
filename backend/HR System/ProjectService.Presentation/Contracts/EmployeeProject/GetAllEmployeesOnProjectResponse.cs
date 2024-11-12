namespace ProjectServicePresentation.Contracts;

public class GetAllEmployeesOnProjectResponse
{
    public IEnumerable<Guid> EmployeeIds { get; set; }
}