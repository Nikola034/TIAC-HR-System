using EmployeeService.Presentation.Contracts.HolidayRequestApprover;
using FastEndpoints;

namespace EmployeeService.Presentation.Validation.HolidayRequestApprover
{
    public class CreateHolidayRequestApproverValidator : Validator<CreateHolidayRequestApproverRequest>
    {
        public CreateHolidayRequestApproverValidator()
        {
            //RuleFor(holidayRequestApprover => holidayRequestApprover.Start).NotEmpty().NotNull().GreaterThanOrEqualTo(x => DateTime.Now);
            //RuleFor(holidayRequestApprover => holidayRequestApprover.End).NotEmpty().NotNull().GreaterThanOrEqualTo(x => x.Start);
            //RuleFor(holidayRequestApprover => holidayRequestApprover.Status).IsInEnum().NotNull();
            //RuleFor(holidayRequestApprover => holidayRequestApprover.SenderId).NotEmpty().NotNull();
        }
    }
}
