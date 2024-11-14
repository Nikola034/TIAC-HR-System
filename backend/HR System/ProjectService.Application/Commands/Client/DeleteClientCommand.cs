using Application.Common.Repositories;
using MediatR;

namespace Application.Commands
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;
        public DeleteClientCommandHandler(IClientRepository clientRepository,IProjectRepository projectRepository)
        {
            _clientRepository = clientRepository;
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            await _projectRepository.DeleteAllProjectsForClientAsync(request.Id,cancellationToken);
            var result = await _clientRepository.DeleteClientAsync(request.Id, cancellationToken);
            return result;
        }
    }

    public record DeleteClientCommand(Guid Id) : IRequest<bool>;
}