using EmployeeService.Application.Commands.Employee;
using EmployeeService.Core.Entities;
using EmployeeService.Presentation.Contracts.Employee;

namespace EmployeeService.Presentation.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeByIdResponse ToApiResponse(this Core.Entities.Employee employee)
        {
            return new EmployeeByIdResponse
            {

                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Role = employee.Role,
                DaysOff = employee.DaysOff,
                AccountId = employee.AccountId
            };
        }

        public static GetAllEmployeesResponse ToApiResponse(this IEnumerable<Core.Entities.Employee> employees)
        {
            return new GetAllEmployeesResponse
            {
                Employees = employees
            };
        }

        public static CreateEmployeeCommand ToCommand(this CreateEmployeeRequest request) => new CreateEmployeeCommand(request.Name, request.Surname, request.DaysOff, request.Role, request.AccountId);
        public static UpdateEmployeeCommand ToCommand(this UpdateEmployeeRequest request) => new UpdateEmployeeCommand(request.Id, request.Name, request.Surname, request.DaysOff, request.Role);
        public static DeleteEmployeeCommand ToCommand(this DeleteEmployeeRequest request) => new DeleteEmployeeCommand(request.Id);


        public static CreateEmployeeResponse ToApiResponseFromCreate(this Employee employee)
        {
            return new CreateEmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Role = employee.Role,
                DaysOff = employee.DaysOff,
                AccountId = employee.AccountId
            };
        }
        public static UpdateEmployeeResponse ToApiResponseFromUpdate(this Employee employee)
        {
            return new UpdateEmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Role = employee.Role,
                DaysOff = employee.DaysOff
            };
        }

        public static DeleteEmployeeResponse ToApiResponseFromDelete(this Employee employee)
        {
            return new DeleteEmployeeResponse
            {
                Id = employee.Id,
            };
        }
    }
}
