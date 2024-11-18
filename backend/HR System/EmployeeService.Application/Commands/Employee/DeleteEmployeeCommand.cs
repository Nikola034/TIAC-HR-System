using Common.Exceptions;
using Common.HttpCLients;
using Core.Exceptions;
using EmployeeService.Application.Common.Mappers;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using EmployeeService.Core.Enums;
using EmployeeService.Core.Errors;
using EmployeeService.Core.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Common.HttpCLients.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeService.Application.Commands.Employee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        private readonly IAccountServiceHttpClient _accountServiceHttpClient;
        private readonly IProjectHttpClient _projectHttpClient;
        public DeleteEmployeeCommandHandler(IEmployeeRepository userRepository, IHolidayRequestRepository holidayRequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository, IAccountServiceHttpClient accountServiceHttpClient, IProjectHttpClient projectHttpClient)
        {
            _employeeRepository = userRepository;
            _holidayRequestRepository = holidayRequestRepository;
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
            _accountServiceHttpClient = accountServiceHttpClient;
            _projectHttpClient = projectHttpClient;
        }
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(domainEntity.Id, cancellationToken);
            if (existingEmployee is null)
            {
                return false;
            }

            var holidayRequests = await _holidayRequestRepository.GetAllHolidayRequestsBySenderIdAsync(existingEmployee.Id, -1, -1, cancellationToken);

            if (holidayRequests.Any())
            {
                foreach (var holidayRequest in holidayRequests)
                {
                    var holidayRequestApprovers = await _holidayRequestApproverRepository.GetHolidayRequestApproversByRequestIdAsync(holidayRequest.Id, cancellationToken);

                    if (holidayRequestApprovers.Any())
                    {
                        foreach(var holidayRequestApprover in holidayRequestApprovers)
                        {
                            await _holidayRequestApproverRepository.DeleteHolidayRequestApproverAsync(holidayRequestApprover.Id, cancellationToken);
                        }
                    }

                    await _holidayRequestRepository.DeleteHolidayRequestAsync(holidayRequest.Id, cancellationToken);
                }
            }

            var holidayRequestsApproversToApprove = await _holidayRequestApproverRepository.GetHolidayRequestApproversByApproverIdAsync(existingEmployee.Id, cancellationToken);
            foreach (var holidayRequestApprover in holidayRequestsApproversToApprove)
            {
                await _holidayRequestApproverRepository.DeleteHolidayRequestApproverAsync(holidayRequestApprover.Id, cancellationToken);
                var requestForApprover = await _holidayRequestRepository.GetHolidayRequestByIdAsync(holidayRequestApprover.RequestId, cancellationToken);
                if(!(await _holidayRequestApproverRepository.GetHolidayRequestApproversByRequestIdAsync(requestForApprover.Id)).Any())
                {
                    await _holidayRequestRepository.DeleteHolidayRequestAsync(requestForApprover.Id, cancellationToken);
                }
            }

            var employeeProjectsIds = await _projectHttpClient.GetProjectsForEmployeeAsync(existingEmployee.Id, cancellationToken);

            if (employeeProjectsIds.Any())
            {
                foreach(var projectId in employeeProjectsIds)
                {
                    var dto = new RemoveEmployeeFromProjectDto {EmployeeId = existingEmployee.Id, ProjectId = projectId};
                    await _projectHttpClient.RemoveEmployeeFromProjectAsync(dto, cancellationToken);
                }
            }

            var leadingProjectsIds = await _projectHttpClient.GetLeadingProjectIdsForEmployeeAsync(existingEmployee.Id, cancellationToken);

            if(leadingProjectsIds.Any())
            {
                foreach(var leadingProject in leadingProjectsIds)
                {
                    await _projectHttpClient.RemoveTeamLeadFromProjectAsync(leadingProject, cancellationToken);
                }
            }

            var persistedEmployee = await _employeeRepository.DeleteEmployeeAsync(existingEmployee.Id, cancellationToken);

            var deletedAccount = await _accountServiceHttpClient.DeleteEmployeeAccount(existingEmployee.AccountId, cancellationToken);

            return true;
        }
    }

    public record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;
}