namespace ProjectServicePresentation.Contracts;

public class CreateProjectResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ClientId { get; set; }
    public Guid? TeamLeadId { get; set; }
}