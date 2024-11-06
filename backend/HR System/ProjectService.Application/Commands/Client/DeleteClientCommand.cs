using Application.Common.Repositories;
using MediatR;

namespace Application.Commands
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        public DeleteClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.DeleteClientAsync(request.Id,cancellationToken);
            return result;
        }
    }

    public record DeleteClientCommand(Guid Id) : IRequest<bool>;
}