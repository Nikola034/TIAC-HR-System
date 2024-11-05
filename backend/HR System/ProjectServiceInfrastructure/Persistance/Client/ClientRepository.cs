using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Client;

public class ClientRepository : IClientRepository
{
    private readonly ClientDbContext _dbContext;

    public ClientRepository(ClientDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Core.Entities.Client?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Core.Entities.Client?> CreateClientAsync(Core.Entities.Client client, CancellationToken cancellationToken = default)
    {
        var newClient = await _dbContext.Clients.AddAsync(client, cancellationToken);
        await _dbContext.SaveChangesAsync();
        return newClient.Entity;
    }

    public async Task<Core.Entities.Client?> UpdateClientAsync(Core.Entities.Client client, CancellationToken cancellationToken = default)
    {
        var oldClient = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == client.Id, cancellationToken);
        if (oldClient == null)
        {
            return null;
        }
        
        oldClient.Name = client.Name;
        oldClient.Country = client.Country;
        await _dbContext.SaveChangesAsync();
        
        return client;
    }

    public async Task<bool> DeleteClientAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (client == null)
        {
            return false;
        }
        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}