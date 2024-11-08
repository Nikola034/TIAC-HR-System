using Common.Exceptions;
using Common.HttpCLients;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeService.Application.Commands.Employee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountServiceHttpClient _accountServiceHttpClient;
        public DeleteEmployeeCommandHandler(IEmployeeRepository userRepository, IAccountServiceHttpClient accountServiceHttpClient)
        {
            _employeeRepository = userRepository;
            _accountServiceHttpClient = accountServiceHttpClient;
        }
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(domainEntity.Id, cancellationToken);
            if (existingEmployee is null)
            {
                throw new NotFoundException("Employee with that ID doesn't exist!");
            }
            var persistedEmployee = await _employeeRepository.DeleteEmployeeAsync(domainEntity.Id, cancellationToken);

            var deletedAccount = await _accountServiceHttpClient.DeleteEmployeeAccount(existingEmployee.AccountId, cancellationToken);

            if (!deletedAccount)
            {
                return false;
            }

            return persistedEmployee;
        }

    }

    public record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;
}
