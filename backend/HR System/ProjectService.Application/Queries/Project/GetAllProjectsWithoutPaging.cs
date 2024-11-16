using Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServiceApplication.Queries.Project
{
    public class GetAllProjectsWithoutPagingHandler : IRequestHandler<GetAllProjectsWithoutPagingQuery, GetAllProjectsWithoutPagingQueryResponse>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsWithoutPagingHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<GetAllProjectsWithoutPagingQueryResponse> Handle(GetAllProjectsWithoutPagingQuery req, CancellationToken ct)
        {
            var projects = await _projectRepository.GetAllProjectsWithoutPagingAsync(ct);
            return new GetAllProjectsWithoutPagingQueryResponse(projects);
        }

    }

    public record GetAllProjectsWithoutPagingQuery() : IRequest<GetAllProjectsWithoutPagingQueryResponse>;
    public record GetAllProjectsWithoutPagingQueryResponse(IEnumerable<Core.Entities.Project> Projects);
}
