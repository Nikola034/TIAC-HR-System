using Common.Exceptions;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.Employee
{
    public class GetRoleByAccountIdHandler : IRequestHandler<GetRoleByAccountIdQuery, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public GetRoleByAccountIdHandler(IEmployeeRepository productRepository)
        {
            _employeeRepository = productRepository;
        }
        public async Task<string> Handle(GetRoleByAccountIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByAccountIdAsync(request.Id, cancellationToken);
            if(employee is null)
            {
                throw new NotFoundException("Employee with provided Account ID doesn't exist!");
            }
            return employee.Role.ToString();
        }
    }


    public record GetRoleByAccountIdQuery(Guid Id) : IRequest<string>;
}