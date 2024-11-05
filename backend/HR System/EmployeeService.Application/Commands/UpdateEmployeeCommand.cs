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

namespace EmployeeService.Application.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository userRepository)
        {
            _employeeRepository = userRepository;
        }
        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(domainEntity.Id);
            if (existingEmployee is null)
            {
                throw new NotFoundException("Employee with that ID doesn't exist!");
            }
            var persistedEmployee = await _employeeRepository.UpdateEmployeeAsync(domainEntity, cancellationToken);
            return persistedEmployee;
        }

    }

    public record UpdateEmployeeCommand(Guid Id, string Name, string Surname, int DaysOff, EmployeeRole Role) : IRequest<Employee>;
}
