using Core.Entities;
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
}