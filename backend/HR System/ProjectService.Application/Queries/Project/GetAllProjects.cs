using Application.Common.Repositories;
using MediatR;

namespace Application.Queries.Project
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, GetAllProjectsQueryResponse>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<GetAllProjectsQueryResponse> Handle(GetAllProjectsQuery req, CancellationToken ct)
        {
            var projects = 
                await _projectRepository.GetAllProjectsAsync(req.PageNumber,req.ItemNumber,req.Title,req.Description,req
                    .ClientName, ct);
            var totalPages = await _projectRepository.GetTotalPageNumber(req.ItemNumber,req.Title,req.Description,req
                .ClientName, ct);
            return new GetAllProjectsQueryResponse(projects,req.PageNumber,req.ItemNumber,totalPages);
        }
        
    }

    public record GetAllProjectsQuery(int PageNumber, int ItemNumber, string Title, string Description, string ClientName) : IRequest<GetAllProjectsQueryResponse>;
    public record GetAllProjectsQueryResponse(IEnumerable<Core.Entities.Project> Projects, int PageNumber, int ItemNumber, int TotalPages);

}