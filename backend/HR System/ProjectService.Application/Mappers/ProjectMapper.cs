using Application.Commands.Project;
using Application.Queries.Project;
using Core.Entities;
using Newtonsoft.Json.Linq;
using ProjectServiceApplication.Commands.Project;

namespace Application.Mappers;

public static class ProjectMapper
{
    public static Project ToDomainEntity(this CreateProjectCommand command)
    {
        return new Project
        {
            Id = new Guid(),
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
    
    public static GetProjectByIdQueryResponse MapJsonStringToResponse(string jsonString, IEnumerable<Guid> workingIds, Project project)
    {
        var jsonObject = JObject.Parse(jsonString);
        var employeesArray  = (JArray)jsonObject["developers"];
        IEnumerable<EmployeeDto> working = employeesArray
            .Where(emp => emp != null && emp["id"] != null && workingIds.Contains(Guid.Parse(emp["id"].ToString())))
            .Select(emp => new EmployeeDto
            {
                Id = Guid.Parse(emp["id"].ToString()),
                Name = emp["name"]?.ToString(), // Use null-conditional operator for safety
                Surname = emp["surname"]?.ToString() // Use null-conditional operator for safety
            });

        IEnumerable<EmployeeDto> notWorking = employeesArray
            .Where(emp => emp != null && emp["id"] != null && !workingIds.Contains(Guid.Parse(emp["id"].ToString())))
            .Select(emp => new EmployeeDto
            {
                Id = Guid.Parse(emp["id"].ToString()),
                Name = emp["name"]?.ToString(),
                Surname = emp["surname"]?.ToString()
            });
        return new GetProjectByIdQueryResponse(project, working, notWorking );
    }
}