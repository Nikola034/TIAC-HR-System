using EmployeeService.Presentation.Contracts.HolidayRequestApprover;
using FastEndpoints;
using FluentValidation;

namespace EmployeeService.Presentation.Validation.HolidayRequestApprover
{
    public class UpdateHolidayRequestApproverValidator : Validator<UpdateHolidayRequestApproverRequest>
    {
        public UpdateHolidayRequestApproverValidator()
        {
            RuleFor(holidayRequestApprover => holidayRequestApprover.RequestId).NotEmpty().NotNull();
            RuleFor(holidayRequestApprover => holidayRequestApprover.ApproverId).NotEmpty().NotNull();
            RuleFor(holidayRequestApprover => holidayRequestApprover.Status).IsInEnum().NotNull();
        }
    }
}
