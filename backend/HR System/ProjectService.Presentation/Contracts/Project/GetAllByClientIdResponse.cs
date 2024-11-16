namespace ProjectService.Presentation.Contracts.Project;

public class GetAllByClientIdResponse
{
    public IEnumerable<Core.Entities.Project> Projects { get; set; }
}