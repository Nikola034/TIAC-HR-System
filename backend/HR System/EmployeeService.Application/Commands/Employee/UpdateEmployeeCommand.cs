using Common.Exceptions;
using Core.Exceptions;
using EmployeeService.Application.Common.Mappers;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using EmployeeService.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Commands.Employee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Core.Entities.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository userRepository)
        {
            _employeeRepository = userRepository;
        }
        public async Task<Core.Entities.Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(domainEntity.Id, cancellationToken);
            if (existingEmployee is null)
            {
                throw new NotFoundException("Employee with that ID doesn't exist!");
            }
            domainEntity.AccountId = existingEmployee.AccountId;
            var persistedEmployee = await _employeeRepository.UpdateEmployeeAsync(domainEntity, cancellationToken);
            return persistedEmployee;
        }

    }

    public record UpdateEmployeeCommand(Guid Id, string Name, string Surname, int DaysOff, EmployeeRole Role) : IRequest<Core.Entities.Employee>;
}
