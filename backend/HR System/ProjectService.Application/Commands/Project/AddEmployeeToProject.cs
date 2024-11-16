using Application.Common.Repositories;
using Application.Mappers;
using Common.HttpCLients;
using Core.Entities;
using MediatR;

namespace ProjectServiceApplication.Commands.Project
{
    public class AddEmployeeToProjectHandler : IRequestHandler<AddEmployeeToProjectCommand, AddOrRemoveEmployeeFromProjectCommandResponse>
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        private readonly IEmployeeHttpClient _employeeHttpClient;

        public AddEmployeeToProjectHandler(IEmployeeProjectRepository employeeProjectRepository, IEmployeeHttpClient employeeHttpClient)
        {
            _employeeProjectRepository = employeeProjectRepository;
            _employeeHttpClient = employeeHttpClient;
        }

        public async Task<AddOrRemoveEmployeeFromProjectCommandResponse> Handle(AddEmployeeToProjectCommand req, CancellationToken ct)
        {
            var persistedEmployeeProject = await _employeeProjectRepository.AddEmployeeToProjectAsync(req.ToDomainEntity(), ct);
            var employeesOnProject = await _employeeProjectRepository.GetAllEmployeesOnProjectAsync(req.ProjectId, ct);
            var jsonString = await _employeeHttpClient.GetAllDevelopersAsync(ct);
            return EmployeeProjectMapper.MapJsonStringToResponse(jsonString,employeesOnProject);
        }
    }

    public record AddEmployeeToProjectCommand(Guid EmployeeId, Guid ProjectId) : IRequest<AddOrRemoveEmployeeFromProjectCommandResponse>;
    public record AddOrRemoveEmployeeFromProjectCommandResponse(IEnumerable<EmployeeDto> Working, IEnumerable<EmployeeDto> NotWorking);
    
    public class EmployeeDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}