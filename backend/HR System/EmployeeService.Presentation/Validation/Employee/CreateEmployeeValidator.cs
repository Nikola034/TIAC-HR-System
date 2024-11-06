using EmployeeService.Presentation.Contracts.Employee;
using FastEndpoints;
using FluentValidation;

namespace EmployeeService.Presentation.Validation.Employee
{
    public class CreateEmployeeValidator : Validator<CreateEmployeeRequest>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(employee => employee.Name).NotEmpty();
            RuleFor(employee => employee.Surname).NotEmpty();
            RuleFor(employee => employee.DaysOff).NotEmpty();
        }
    }
}
