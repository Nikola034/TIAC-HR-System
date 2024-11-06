using Application.Commands;
using Application.Commands.Project;
using Core.Entities;
using ProjectServiceApplication.Commands.Project;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Mapper
{
    public static class ProjectMapper
    {
        public static ProjectByIdResponse ToApiResponse(this Project project)
        {
            return new ProjectByIdResponse
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Client = project.Client,
                TeamLeadId = project.TeamLeadId
            };
        }
        
        public static CreateProjectCommand ToCommand(this CreateProjectRequest request)
            => new CreateProjectCommand(request.Title, request.Description,request.ClientId,request.TeamLeadId);
        
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
            => new UpdateProjectCommand(request.Id,request.Title, request.Description,request.ClientId,request.TeamLeadId);
        
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
    }
    
}