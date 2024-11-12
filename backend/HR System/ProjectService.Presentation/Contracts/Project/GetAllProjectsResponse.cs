using Core.Entities;

namespace ProjectServicePresentation.Contracts;

public class GetAllProjectsResponse
{
    public IEnumerable<Project> Projects { get; set; }
    public int Page {  get; set; }
    public int ItemsPerPage {  get; set; }
    public decimal TotalPages { get; set; }
}