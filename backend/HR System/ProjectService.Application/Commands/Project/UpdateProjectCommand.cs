using MediatR;
using Application.Common.Repositories;
using Application.Mappers;
using Core.Exceptions;

namespace Application.Commands.Project
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Core.Entities.Project>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<Core.Entities.Project> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingUser = await _projectRepository.GetProjectByIdAsync(domainEntity.Id, cancellationToken);
            if (existingUser is null)
            {
                throw new ProjectDoesNotExistException();
            }

            var persistedProject = await _projectRepository.UpdateProjectAsync(domainEntity,cancellationToken);
            return persistedProject;
        }

    }

    public record UpdateProjectCommand(Guid Id, string Title, string Description, Guid ClientId, Guid? TeamLeadId) : IRequest<Core.Entities.Project>;
}