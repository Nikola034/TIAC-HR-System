using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProjectServiceInfrastructure.Persistence.Client;

public class ClientRepository(ProjectDbContext dbContext) : IClientRepository
{
    public async Task<Core.Entities.Client?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Core.Entities.Client?> CreateClientAsync(Core.Entities.Client client, CancellationToken cancellationToken = default)
    {
        var newClient = await dbContext.Clients.AddAsync(client, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return newClient.Entity;
    }

    public async Task<Core.Entities.Client?> UpdateClientAsync(Core.Entities.Client client, CancellationToken cancellationToken = default)
    {
        var oldClient = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == client.Id, cancellationToken);
        if (oldClient == null)
        {
            return null;
        }
        dbContext.ChangeTracker.Clear();
        dbContext.Clients.Update(client);
        await dbContext.SaveChangesAsync(cancellationToken);
        return client;
    }

    public async Task<bool> DeleteClientAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (client == null)
        {
            return false;
        }
        dbContext.Clients.Remove(client);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<Core.Entities.Client>> GetAllClientsAsync(int pageNumber, int itemNumber, string name, string country, CancellationToken cancellationToken = default)
    {
        var clients = await dbContext.Clients.OrderBy(x => x.Id)
            .Where(x => (x.Name.ToLower().Contains(name) || name.Equals("")) 
                        && (x.Country.ToLower().Contains(country) || country.Equals("")))
            .Skip((pageNumber - 1) * itemNumber)
            .Take(itemNumber)
            .ToListAsync(cancellationToken);
        return clients;
    }
    
    public async Task<int> GetTotalPageNumber(int itemNumber, string name, string country, CancellationToken ct = default(CancellationToken))
    {
        var count = await dbContext.Clients.
            Where(x => (x.Name.ToLower().Contains(name) || name.Equals("")) 
                       && (x.Country.ToLower().Contains(country) || country.Equals("")))
            .CountAsync(ct);
        return (int)Math.Ceiling((double)count / itemNumber);

    }
}