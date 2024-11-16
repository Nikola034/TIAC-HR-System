using Application.Common.Repositories;
using MediatR;

namespace Application.Commands
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        public DeleteClientCommandHandler(IClientRepository clientRepository,IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository)
        {
            _clientRepository = clientRepository;
            _projectRepository = projectRepository;
            _employeeProjectRepository = employeeProjectRepository;
        }

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllProjectsByClientIdAsync(request.Id, cancellationToken);
            await _employeeProjectRepository.RemoveProjectsAsync(projects.Select(x => x.Id),cancellationToken);
            await _projectRepository.DeleteAllProjectsForClientAsync(request.Id,cancellationToken);
            var result = await _clientRepository.DeleteClientAsync(request.Id, cancellationToken);
            return result;
        }
    }

    public record DeleteClientCommand(Guid Id) : IRequest<bool>;
}