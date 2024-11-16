using Application.Common.Repositories;
using Application.Queries.Project;
using MediatR;

namespace ProjectServiceApplication.Queries.Project
{
    public class GetAllProjectsByClientIdHandler : IRequestHandler<GetAllProjectsByClientIdQuery, IEnumerable<Core.Entities.Project>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsByClientIdHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Core.Entities.Project>> Handle(GetAllProjectsByClientIdQuery req, CancellationToken ct)
        {
            var projects = await _projectRepository.GetAllProjectsByClientIdAsync(req.clientId, ct);
            return projects;
        }
        
    }

    public record GetAllProjectsByClientIdQuery(Guid clientId) : IRequest<IEnumerable<Core.Entities.Project>>;
    
}