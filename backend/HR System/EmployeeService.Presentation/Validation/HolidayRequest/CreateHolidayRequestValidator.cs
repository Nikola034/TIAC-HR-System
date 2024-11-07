using EmployeeService.Presentation.Contracts.HolidayRequest;
using FastEndpoints;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Presentation.Validation.HolidayRequest
{
    public class CreateHolidayRequestValidator : Validator<CreateHolidayRequestRequest>
    {
        public CreateHolidayRequestValidator()
        {
            RuleFor(holidayRequest => holidayRequest.Start).NotEmpty().NotNull().GreaterThanOrEqualTo(x => DateTime.Now);
            RuleFor(holidayRequest => holidayRequest.End).NotEmpty().NotNull().GreaterThanOrEqualTo(x => x.Start);
            RuleFor(holidayRequest => holidayRequest.Status).IsInEnum().NotNull();
            RuleFor(holidayRequest => holidayRequest.SenderId).NotEmpty().NotNull();
        }
    }
}
