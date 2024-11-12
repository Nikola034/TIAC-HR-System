using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.Employee
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, GetAllEmployeesQueryResponse>
    {

        private readonly IEmployeeRepository _employeeRepository;
        public GetAllEmployeesHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<GetAllEmployeesQueryResponse> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync(request.page, request.items, cancellationToken);
            var totalPages = await _employeeRepository.GetTotalPagesAsync(request.page, request.items, cancellationToken);
            return new GetAllEmployeesQueryResponse(employees, request.page, totalPages, request.items);
        }
    }


    public record GetAllEmployeesQuery(int page, int items) : IRequest<GetAllEmployeesQueryResponse>;

    public record GetAllEmployeesQueryResponse(IEnumerable<Core.Entities.Employee> Employees, int Page, decimal TotalPages, int ItemsPerPage);
}
