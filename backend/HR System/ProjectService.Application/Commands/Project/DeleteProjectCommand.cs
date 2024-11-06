using Application.Common.Repositories;
using MediatR;

namespace Application.Commands
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        public DeleteProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.DeleteProjectAsync(request.Id,cancellationToken);
            return result;
        }
    }

    public record DeleteProjectCommand(Guid Id) : IRequest<bool>;
}