using EmployeeService.Presentation.Contracts.HolidayRequest;
using FastEndpoints;
using FluentValidation;

namespace EmployeeService.Presentation.Validation.HolidayRequest
{   public class UpdateHolidayRequestValidator : Validator<UpdateHolidayRequestRequest>
    {
        public UpdateHolidayRequestValidator()
        {
            RuleFor(holidayRequest => holidayRequest.Id).NotEmpty().NotNull();
            RuleFor(holidayRequest => holidayRequest.Start).NotEmpty().NotNull().GreaterThanOrEqualTo(x => DateTime.Now);
            RuleFor(holidayRequest => holidayRequest.End).NotEmpty().NotNull().GreaterThanOrEqualTo(x => x.Start);
            RuleFor(holidayRequest => holidayRequest.Status).IsInEnum().NotNull();
        }
    }
}
