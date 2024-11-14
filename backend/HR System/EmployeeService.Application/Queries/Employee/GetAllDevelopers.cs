using EmployeeService.Application.Common.Repositories;
using MediatR;

namespace EmployeeService.Application.Queries.Employee
{
    public class GetAllDevelopersHandler : IRequestHandler<GetAllDevelopersQuery, IEnumerable<Core.Entities.Employee>>
    {

        private readonly IEmployeeRepository _employeeRepository;
        public GetAllDevelopersHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<Core.Entities.Employee>> Handle(GetAllDevelopersQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllDevelopersAsync(cancellationToken);
            return employees;
        }
    }
    public record GetAllDevelopersQuery() : IRequest<IEnumerable<Core.Entities.Employee>>;
    
}