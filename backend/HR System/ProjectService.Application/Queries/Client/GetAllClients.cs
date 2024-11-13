using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Client
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, GetAllClientsQueryResponse>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientsHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<GetAllClientsQueryResponse> Handle(GetAllClientsQuery req, CancellationToken ct)
        {
            var clients = await _clientRepository.GetAllClientsAsync(req.PageNumber,req.ItemNumber, ct);
            var totalPages = await _clientRepository.GetTotalPageNumber(req.ItemNumber,ct);
            return new GetAllClientsQueryResponse(clients,req.PageNumber,req.ItemNumber,totalPages);
        }
        
    }

    public record GetAllClientsQuery(int PageNumber, int ItemNumber) : IRequest<GetAllClientsQueryResponse>;
    public record GetAllClientsQueryResponse(IEnumerable<Core.Entities.Client> Clients, int PageNumber, int ItemNumber, int TotalPages);

}