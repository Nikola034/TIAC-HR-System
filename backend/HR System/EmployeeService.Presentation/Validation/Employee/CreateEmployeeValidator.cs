using EmployeeService.Presentation.Contracts.Employee;
using FastEndpoints;
using FluentValidation;

namespace EmployeeService.Presentation.Validation.Employee
{
    public class CreateEmployeeValidator : Validator<CreateEmployeeRequest>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(employee => employee.Name).NotEmpty().NotNull();
            RuleFor(employee => employee.Surname).NotEmpty().NotNull();
            RuleFor(employee => employee.DaysOff).GreaterThan(-1).LessThanOrEqualTo(30).NotNull();
            RuleFor(employee => employee.Role).IsInEnum().NotNull();
        }
    }
}
