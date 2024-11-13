using Application.Common.Repositories;
using MediatR;

namespace Application.Commands
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        public DeleteProjectCommandHandler(IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository)
        {
            _projectRepository = projectRepository;
            _employeeProjectRepository = employeeProjectRepository;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            await _employeeProjectRepository.RemoveProjectAsync(request.Id, cancellationToken);
            var result = await _projectRepository.DeleteProjectAsync(request.Id,cancellationToken);
            return result;
        }
    }

    public record DeleteProjectCommand(Guid Id) : IRequest<bool>;
}