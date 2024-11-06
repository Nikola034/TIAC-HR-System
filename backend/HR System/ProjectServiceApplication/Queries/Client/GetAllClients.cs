using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Client
{
    public class GetAllClients : IRequestHandler<GetAllClientsQuery, IEnumerable<Core.Entities.Client>>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClients(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Core.Entities.Client>> Handle(GetAllClientsQuery req, CancellationToken ct)
        {
            var clients = await _clientRepository.GetAllAsync(req.Page, ct);
            return clients;
        }
        
    }

    public record GetAllClientsQuery(int Page) : IRequest<IEnumerable<Core.Entities.Client>>;

}