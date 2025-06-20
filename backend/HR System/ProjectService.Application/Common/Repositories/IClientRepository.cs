using Core.Entities;

namespace Application.Common.Repositories;

public interface IClientRepository
{
    public Task<Client?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    public Task<Client?> CreateClientAsync(Client client, CancellationToken cancellationToken = default(CancellationToken));
    public Task<Client?> UpdateClientAsync(Client client, CancellationToken cancellationToken = default(CancellationToken));
    public Task<bool> DeleteClientAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    public Task<IEnumerable<Client>> GetAllClientsAsync(int pageNumber,int itemNumber, string name, string country, CancellationToken cancellationToken = default(CancellationToken));
    public Task<int> GetTotalPageNumber(int itemNumber, string name, string country, CancellationToken ct = default(CancellationToken));
}