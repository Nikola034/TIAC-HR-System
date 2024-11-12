using Core.Entities;
using ProjectServiceApplication.Commands.Client;
using ProjectServiceApplication.Commands.Project;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Mapper;

public static class EmployeeProjectMapper
{
    public static AddEmployeeToProjectCommand ToCommand(this AddEmployeeToProjectRequest request) => new AddEmployeeToProjectCommand(request.EmployeeId, request.ProjectId);
    public static AddEmployeeToProjectResponse ToApiResponseFromAdd(this EmployeeProject employeeProject)
    {
        return new AddEmployeeToProjectResponse()
        {
            Id = employeeProject.Id,
            EmployeeId = employeeProject.EmployeeId,
            ProjectId = employeeProject.ProjectId
        };
    }
}