using Core.Entities;

namespace ProjectServicePresentation.Contracts;

public class GetAllProjectsResponse
{
    public IEnumerable<Project> Projects { get; set; }
}