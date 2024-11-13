using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Client
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, GetClientByIdQueryResponse?>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;

        public GetClientByIdHandler(IClientRepository clientRepository, IProjectRepository projectRepository)
        {
            _clientRepository = clientRepository;
            _projectRepository = projectRepository;
        }

        public async Task<GetClientByIdQueryResponse?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByIdAsync(request.Id,cancellationToken);
            var projects = await _projectRepository.GetAllProjectsByClientIdAsync(request.Id, cancellationToken);
            return new GetClientByIdQueryResponse(client,projects);
        }
    }

    public record GetClientByIdQuery(Guid Id) : IRequest<GetClientByIdQueryResponse>;

    public record GetClientByIdQueryResponse(Core.Entities.Client? Client, IEnumerable<Core.Entities.Project> Projects);
}