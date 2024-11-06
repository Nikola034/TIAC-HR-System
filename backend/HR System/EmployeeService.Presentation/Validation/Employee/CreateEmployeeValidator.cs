using EmployeeService.Presentation.Contracts.Employee;
using FastEndpoints;
using FluentValidation;

namespace EmployeeService.Presentation.Validation.Employee
{
    public class CreateEmployeeValidator : Validator<CreateEmployeeRequest>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(employee => employee.Name).NotEmpty()
                                            .NotNull()
                                            .WithMessage("Name is required.");
            RuleFor(employee => employee.Surname).NotEmpty();
            RuleFor(employee => employee.DaysOff).NotEmpty();
            RuleFor(employee => employee.Role).NotEmpty();
        }
    }
}
