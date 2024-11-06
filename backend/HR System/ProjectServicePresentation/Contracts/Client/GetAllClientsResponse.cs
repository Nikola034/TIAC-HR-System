using Core.Entities;

namespace ProjectServicePresentation.Contracts;

public class GetAllClientsResponse
{
    public IEnumerable<Client> clients { get; set; }
}