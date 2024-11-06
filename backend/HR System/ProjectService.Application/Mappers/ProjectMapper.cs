using Application.Commands.Project;
using Core.Entities;
using ProjectServiceApplication.Commands.Project;

namespace Application.Mappers;

public static class ProjectMapper
{
    public static Project ToDomainEntity(this CreateProjectCommand command)
    {
        return new Project
        {
            Title = command.Title,
            Description = command.Description,
            ClientId = command.ClientId,
            TeamLeadId = command.TeamLeadId
        };
    }
    
    public static Project ToDomainEntity(this UpdateProjectCommand command)
    {
        return new Project
        {
            Id = command.Id,
            Title = command.Title,
            Description = command.Description,
            ClientId = command.ClientId,
            TeamLeadId = command.TeamLeadId
        };
    }
}