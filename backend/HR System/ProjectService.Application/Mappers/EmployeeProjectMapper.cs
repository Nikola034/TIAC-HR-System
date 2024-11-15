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
        var employeesArray = JArray.Parse(jsonString);
        IEnumerable<EmployeeDto> working = employeesArray
            .Where(emp => workingIds.Contains(Guid.Parse(emp["Id"].ToString())))
            .Select(emp => new EmployeeDto
            {
                Id = Guid.Parse(emp["Id"].ToString()),
                Name = emp["Name"].ToString(),
                Surname = emp["Surname"].ToString()
            });

        IEnumerable<EmployeeDto> notWorking = employeesArray
            .Where(emp => !workingIds.Contains(Guid.Parse(emp["Id"].ToString())))
            .Select(emp => new EmployeeDto
            {
                Id = Guid.Parse(emp["Id"].ToString()),
                Name = emp["Name"].ToString(),
                Surname = emp["Surname"].ToString()
            });
        return new AddOrRemoveEmployeeFromProjectCommandResponse(working, notWorking );
    }
}
