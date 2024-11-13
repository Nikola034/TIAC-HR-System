using System.Collections;
using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Client
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, GetAllClientsQueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;

        public GetAllClientsHandler(IClientRepository clientRepository, IProjectRepository projectRepository)
        {
            _clientRepository = clientRepository;
            _projectRepository = projectRepository;
        }

        public async Task<GetAllClientsQueryResponse> Handle(GetAllClientsQuery req, CancellationToken ct)
        {
            var clients = await _clientRepository.GetAllClientsAsync(req.PageNumber,req.ItemNumber, ct);
            IEnumerable<ClientWithNumberOfProjects> list =  Enumerable.Empty<ClientWithNumberOfProjects>();
            foreach (var client in clients)
            {
                var pair = new ClientWithNumberOfProjects(client, _projectRepository.GetNumberOfProjectsByClient(client.Id));
                list = list.Append(pair);
            }
            var totalPages = await _clientRepository.GetTotalPageNumber(req.ItemNumber,ct);
            return new GetAllClientsQueryResponse(list,req.PageNumber,req.ItemNumber,totalPages);
        }
        
    }

    public record GetAllClientsQuery(int PageNumber, int ItemNumber) : IRequest<GetAllClientsQueryResponse>;
    public record GetAllClientsQueryResponse(IEnumerable<ClientWithNumberOfProjects> Clients, int PageNumber, int ItemNumber, int TotalPages);
    public record ClientWithNumberOfProjects(Core.Entities.Client Client, int NumberOfProjects);

}