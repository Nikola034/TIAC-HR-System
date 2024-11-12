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
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Core.Entities.Employee>
    {

        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeByIdHandler(IEmployeeRepository productRepository)
        {
            _employeeRepository = productRepository;
        }
        public async Task<Core.Entities.Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id, cancellationToken);
            if(employee is null)
            {
                throw new NotFoundException("Employee with provided ID doesn't exist!");
            }
            return employee;
        }
    }


    public record GetEmployeeByIdQuery(Guid Id) : IRequest<Core.Entities.Employee>;
}
