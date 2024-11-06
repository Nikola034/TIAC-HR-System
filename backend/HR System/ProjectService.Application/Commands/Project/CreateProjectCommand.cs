using Application.Common.Repositories;
using Application.Mappers;
using MediatR;

namespace ProjectServiceApplication.Commands.Project
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Core.Entities.Project>
    {

        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Core.Entities.Project> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            domainEntity.Id = new Guid();
            var persistedProject = await _projectRepository.CreateProjectAsync(domainEntity, cancellationToken);
            return persistedProject;
        }
    }

    public record CreateProjectCommand(string Title, string Description, Guid ClientId, Guid? TeamLeadId)
        : IRequest<Core.Entities.Project>;
}