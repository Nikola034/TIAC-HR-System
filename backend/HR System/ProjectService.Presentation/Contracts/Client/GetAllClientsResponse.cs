using Application.Queries.Client;
using Core.Entities;

namespace ProjectServicePresentation.Contracts;

public class GetAllClientsResponse
{
    public IEnumerable<ClientWithNumberOfProjects> Clients { get; set; }
    public int Page {  get; set; }
    public int ItemsPerPage {  get; set; }
    public decimal TotalPages { get; set; }
}