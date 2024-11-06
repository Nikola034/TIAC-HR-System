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
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Core.Entities.Employee>>
    {

        private readonly IEmployeeRepository _employeeRepository;
        public GetAllEmployeesHandler(IEmployeeRepository productRepository)
        {
            _employeeRepository = productRepository;
        }
        public async Task<IEnumerable<Core.Entities.Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAllEmployeesAsync(request.page, cancellationToken);
        }
    }


    public record GetAllEmployeesQuery(int page) : IRequest<IEnumerable<Core.Entities.Employee>>;
}
