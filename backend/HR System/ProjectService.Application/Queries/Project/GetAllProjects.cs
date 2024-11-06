using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Project
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<Core.Entities.Project>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Core.Entities.Project>> Handle(GetAllProjectsQuery req, CancellationToken ct)
        {
            var projects = await _projectRepository.GetAllProjectsAsync(req.Page, ct);
            return projects;
        }
        
    }

    public record GetAllProjectsQuery(int Page) : IRequest<IEnumerable<Core.Entities.Project>>;

}