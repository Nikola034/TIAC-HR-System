﻿using EmployeeService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using EmployeeService.Application.Common.Mappers;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Enums;
using EmployeeService.Core.Primitives.Result;
using EmployeeService.Core.Errors;

namespace EmployeeService.Application.Commands.Employee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Core.Entities.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
    
        public CreateEmployeeCommandHandler(IEmployeeRepository userRepository)
        {
            _employeeRepository = userRepository;
        }
        public async Task<Core.Entities.Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(domainEntity.Id, cancellationToken);
            if (existingEmployee is not null)
            {
                throw new EmployeeAlreadyExistException();
            }
            domainEntity.Id = new Guid();
            var persistedEmployee = await _employeeRepository.CreateEmployeeAsync(domainEntity, cancellationToken);
            return persistedEmployee;
        }

    }

    public record CreateEmployeeCommand(string Name, string Surname, int DaysOff, EmployeeRole Role, Guid AccountId) : IRequest<Core.Entities.Employee>;
}
