using Core.Entities;
using ProjectServiceApplication.Commands.Client;
using ProjectServiceApplication.Commands.Project;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Mapper;

public static class EmployeeProjectMapper
{
    public static AddEmployeeToProjectCommand ToAddCommand(this AddOrRemoveEmployeeFromProjectRequest request) => new AddEmployeeToProjectCommand(request.EmployeeId, request.ProjectId);
    public static RemoveEmployeeFromProjectCommand ToRemoveCommand(this AddOrRemoveEmployeeFromProjectRequest request) => new RemoveEmployeeFromProjectCommand(request.EmployeeId, request.ProjectId);
    public static AddOrRemoveEmployeeFromProjectResponse ToApiResponse(this AddOrRemoveEmployeeFromProjectCommandResponse response)
    {
        return new AddOrRemoveEmployeeFromProjectResponse()
        {
            Working = response.Working,
            NotWorking = response.NotWorking
        };
    }
}