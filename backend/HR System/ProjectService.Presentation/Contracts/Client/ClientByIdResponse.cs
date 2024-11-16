using Core.Entities;

namespace ProjectServicePresentation.Contracts;

public class ClientByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public IEnumerable<Project> Projects { get; set; }
}