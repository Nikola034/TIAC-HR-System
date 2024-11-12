using Core.Entities;

namespace ProjectServicePresentation.Contracts;

public class ProjectByIdResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Client Client { get; set; }
    public Guid? TeamLeadId { get; set; }
}