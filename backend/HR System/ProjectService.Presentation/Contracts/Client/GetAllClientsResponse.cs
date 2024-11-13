using Core.Entities;

namespace ProjectServicePresentation.Contracts;

public class GetAllClientsResponse
{
    public IEnumerable<Client> Clients { get; set; }
    public int Page {  get; set; }
    public int ItemsPerPage {  get; set; }
    public decimal TotalPages { get; set; }
}