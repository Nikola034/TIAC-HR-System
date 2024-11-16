using Application.Commands;
using Application.Commands.Project;
using Application.Queries.Project;
using Core.Entities;
using ProjectService.Presentation.Contracts.Project;
using ProjectServiceApplication.Commands.Project;
using ProjectServiceApplication.Queries.Project;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Mapper
{
    public static class ProjectMapper
    {
        public static ProjectByIdResponse ToApiResponse(this GetProjectByIdQueryResponse response)
        {
            return new ProjectByIdResponse
            {
                Id = response.Project.Id,
                Title = response.Project.Title,
                Description = response.Project.Description,
                Client = response.Project.Client,
                TeamLeadId = response.Project.TeamLeadId,
                Working = response.Working,
                NotWorking = response.NotWorking
            };
        }

        public static GetProjectsReportResponse ToApiResponse(this GetProjectsReportQueryResponse response)
        {
            return new GetProjectsReportResponse
            {
                Reports = response.Reports
            };
        }

        public static GetProjectInfoResponse ToApiResponse(this ProjectInfo projectInfo)
        {
            return new GetProjectInfoResponse
            {
                ProjectInfo = projectInfo
            };
        }

        public static CreateProjectCommand ToCommand(this CreateProjectRequest request)
            => new CreateProjectCommand(request.Title, request.Description, request.ClientId, request.TeamLeadId);

        public static CreateProjectResponse ToApiResponseFromCreate(this Project project)
        {
            return new CreateProjectResponse()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ClientId = project.ClientId,
                TeamLeadId = project.TeamLeadId
            };
        }

        public static UpdateProjectCommand ToCommand(this UpdateProjectRequest request)
            => new UpdateProjectCommand(request.Id, request.Title, request.Description, request.ClientId, request.TeamLeadId);

        public static UpdateProjectResponse ToApiResponseFromUpdate(this Project project)
        {
            return new UpdateProjectResponse()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ClientId = project.ClientId,
                TeamLeadId = project.TeamLeadId
            };
        }

        public static GetAllProjectsResponse ToApiResponse(this GetAllProjectsQueryResponse response)
        {
            return new GetAllProjectsResponse()
            {
                Projects = response.Projects,
                Page = response.PageNumber,
                ItemsPerPage = response.ItemNumber,
                TotalPages = response.TotalPages
            };
        }


        public static GetAllProjectsWithoutPagingResponse ToApiResponse(this GetAllProjectsWithoutPagingQueryResponse response)
        {
            return new GetAllProjectsWithoutPagingResponse()
            {
                Projects = response.Projects
            };
        }
    }
}