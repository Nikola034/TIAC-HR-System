using EmployeeService.Application.Commands;
using EmployeeService.Core.Entities;
using EmployeeService.Presentation.Contracts;

namespace EmployeeService.Presentation.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeByIdResponse ToApiResponse(this Employee employee)
        {
            return new EmployeeByIdResponse
            {

                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Role = employee.Role,
                DaysOff = employee.DaysOff
            };
        }

        public static CreateEmployeeCommand ToCommand(this CreateEmployeeRequest request) => new CreateEmployeeCommand(request.Name, request.Surname, request.DaysOff, request.Role);

        public static CreateEmployeeResponse ToApiResponseFromCommand(this Employee employee)
        {
            return new CreateEmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Role = employee.Role,
                DaysOff = employee.DaysOff
            };
        }
    }
}
