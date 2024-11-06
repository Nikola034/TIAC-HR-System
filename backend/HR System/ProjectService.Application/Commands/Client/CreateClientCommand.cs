using Application.Common.Repositories;
using Application.Mappers;
using MediatR;

namespace ProjectServiceApplication.Commands.Client
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Core.Entities.Client>
    {

        private readonly IClientRepository _clientRepository;

        public CreateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Core.Entities.Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            domainEntity.Id = new Guid();
            var persistedClient = await _clientRepository.CreateClientAsync(domainEntity, cancellationToken);
            return persistedClient;
        }
    }

    public record CreateClientCommand(string Name, string Country) : IRequest<Core.Entities.Client>;
}
