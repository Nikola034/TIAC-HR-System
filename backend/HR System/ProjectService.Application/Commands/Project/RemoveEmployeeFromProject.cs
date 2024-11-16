using Application.Common.Repositories;
using Application.Mappers;
using Common.HttpCLients;
using Core.Entities;
using MediatR;

namespace ProjectServiceApplication.Commands.Project
{
    public class RemoveEmployeeFromProjectHandler : IRequestHandler<RemoveEmployeeFromProjectCommand, AddOrRemoveEmployeeFromProjectCommandResponse>
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        private readonly IEmployeeHttpClient _employeeHttpClient;

        public RemoveEmployeeFromProjectHandler(IEmployeeProjectRepository employeeProjectRepository, IEmployeeHttpClient employeeHttpClient)
        {
            _employeeProjectRepository = employeeProjectRepository;
            _employeeHttpClient = employeeHttpClient;
        }

        public async Task<AddOrRemoveEmployeeFromProjectCommandResponse> Handle(RemoveEmployeeFromProjectCommand req, CancellationToken ct)
        {
            await _employeeProjectRepository.RemoveEmployeeFromProjectAsync(req.EmployeeId,req.ProjectId, ct);
            var employeesOnProject = await _employeeProjectRepository.GetAllEmployeesOnProjectAsync(req.ProjectId, ct);
            var jsonString = await _employeeHttpClient.GetAllDevelopersAsync(ct);
            return EmployeeProjectMapper.MapJsonStringToResponse(jsonString,employeesOnProject);
        }
    }
    public record RemoveEmployeeFromProjectCommand(Guid EmployeeId, Guid ProjectId) : IRequest<AddOrRemoveEmployeeFromProjectCommandResponse>;
}