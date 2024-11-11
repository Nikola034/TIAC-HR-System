using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Queries.Employee;
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

        public static GetAllEmployeesResponse ToApiResponse(this GetAllEmployeesQueryResponse response)
        {
            return new GetAllEmployeesResponse
            {
                Employees = response.Employees,
                Page = response.Page,
                TotalPages = response.TotalPages,
                ItemsPerPage = response.ItemsPerPage,
            };
        }

        public static GetDaysOffForEmployeesResponse ToApiResponse(this GetDaysOffForEmployeesQueryResponse response)
        {
            return new GetDaysOffForEmployeesResponse
            {
                Reports = response.Reports,
            };
        }

        public static CreateEmployeeCommand ToCommand(this CreateEmployeeRequest request) => new CreateEmployeeCommand(request.Name, request.Surname, request.DaysOff, request.Role, request.AccountId);
        public static UpdateEmployeeCommand ToCommand(this UpdateEmployeeRequest request) => new UpdateEmployeeCommand(request.Id, request.Name, request.Surname, request.DaysOff, request.Role);

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
    }
}
