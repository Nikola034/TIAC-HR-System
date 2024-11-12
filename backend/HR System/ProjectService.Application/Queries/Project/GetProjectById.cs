using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Project
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, Core.Entities.Project?>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Core.Entities.Project?> Handle(GetProjectByIdQuery req, CancellationToken ct)
        {
            var project = await _projectRepository.GetProjectByIdAsync(req.Id, ct);
            return project;
        }
        
    }

    public record GetProjectByIdQuery(Guid Id) : IRequest<Core.Entities.Project?>;
}