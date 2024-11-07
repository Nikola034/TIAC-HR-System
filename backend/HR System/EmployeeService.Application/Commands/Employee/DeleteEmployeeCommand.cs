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
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Core.Entities.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeCommandHandler(IEmployeeRepository userRepository)
        {
            _employeeRepository = userRepository;
        }
        public async Task<Core.Entities.Employee> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(domainEntity.Id, cancellationToken);
            if (existingEmployee is null)
            {
                throw new NotFoundException("Employee with that ID doesn't exist!");
            }
            var persistedEmployee = await _employeeRepository.DeleteEmployeeAsync(domainEntity.Id, cancellationToken);
            return persistedEmployee;
            //TODO Delete account in identity-service
        }

    }

    public record DeleteEmployeeCommand(Guid Id) : IRequest<Core.Entities.Employee>;
}
