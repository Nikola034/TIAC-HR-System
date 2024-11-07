using EmployeeService.Presentation.Contracts.Employee;
using FastEndpoints;
using FluentValidation;

namespace EmployeeService.Presentation.Validation.Employee
{

    public class UpdateEmployeeValidator : Validator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(employee => employee.Id).NotEmpty().NotNull();
            RuleFor(employee => employee.Name).NotEmpty().NotNull();
            RuleFor(employee => employee.Surname).NotEmpty().NotNull();
            RuleFor(employee => employee.DaysOff).GreaterThan(-1).LessThanOrEqualTo(30).NotNull();
            RuleFor(employee => employee.Role).IsInEnum().NotNull();
        }
    }

}
