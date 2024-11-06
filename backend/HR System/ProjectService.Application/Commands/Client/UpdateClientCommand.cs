using MediatR;
using Application.Common.Repositories;
using Application.Mappers;
using Core.Exceptions;

namespace Application.Commands.Client
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Core.Entities.Client>
    {
        private readonly IClientRepository _clientRepository;

        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Core.Entities.Client> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingUser = await _clientRepository.GetClientByIdAsync(domainEntity.Id);
            if (existingUser is null)
            {
                throw new ClientDoesNotExistException();
            }

            var persistedClient = await _clientRepository.UpdateClientAsync(domainEntity);
            return persistedClient;
        }

    }

    public record UpdateClientCommand(Guid Id, string Name, string Country) : IRequest<Core.Entities.Client>;
}
