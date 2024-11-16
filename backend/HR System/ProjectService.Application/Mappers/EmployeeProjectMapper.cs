using Core.Entities;
using Newtonsoft.Json.Linq;
using ProjectServiceApplication.Commands.Project;

namespace Application.Mappers;

public static class EmployeeProjectMapper
{
    public static EmployeeProject ToDomainEntity(this AddEmployeeToProjectCommand command)
    {
        return new EmployeeProject()
        {
            Id = new Guid(),
            EmployeeId = command.EmployeeId,
            ProjectId = command.ProjectId
        };
    }

    public static AddOrRemoveEmployeeFromProjectCommandResponse MapJsonStringToResponse(string jsonString, IEnumerable<Guid> workingIds)
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
        return new AddOrRemoveEmployeeFromProjectCommandResponse(working, notWorking );
    }
}
