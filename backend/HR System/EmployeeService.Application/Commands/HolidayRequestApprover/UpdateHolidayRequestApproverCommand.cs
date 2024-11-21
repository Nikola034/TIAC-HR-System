using Common.Exceptions;
using Core.Exceptions;
using EmployeeService.Application.Common.Mappers;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Commands.HolidayRequestApprover
{
    public class UpdateHolidayRequestApproverCommandHandler : IRequestHandler<UpdateHolidayRequestApproverCommand, Core.Entities.HolidayRequestApprover>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public UpdateHolidayRequestApproverCommandHandler(IEmployeeRepository employeeRepository, IHolidayRequestRepository holidayRequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _employeeRepository = employeeRepository;
            _holidayRequestRepository = holidayRequestRepository;
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<Core.Entities.HolidayRequestApprover> Handle(UpdateHolidayRequestApproverCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingHolidayRequestApprover = await _holidayRequestApproverRepository.GetHolidayRequestApproverByRequestIdAndApproverIdAsync(domainEntity.RequestId, domainEntity.ApproverId, cancellationToken);
            if (existingHolidayRequestApprover is null)
            {
                throw new NotFoundException("Holiday request approver with provided Request and Approver Ids doesn't exist!");
            }
            domainEntity.Id = existingHolidayRequestApprover.Id;
            domainEntity.RequestId = existingHolidayRequestApprover.RequestId;
            domainEntity.ApproverId = existingHolidayRequestApprover.ApproverId;
            domainEntity.Status = request.Status;

            var holidayRequestApproversForRequest = await _holidayRequestApproverRepository.GetHolidayRequestApproversByRequestIdAsync(domainEntity.RequestId, cancellationToken);
            var holidayRequestForApproval = await _holidayRequestRepository.GetHolidayRequestByIdAsync(existingHolidayRequestApprover.RequestId, cancellationToken);
            var employeeForApproval = holidayRequestForApproval.Sender;

            if (request.Status == HolidayRequestStatus.Denied)
            {
                int wantedDays = (holidayRequestForApproval.End - holidayRequestForApproval.Start).Days + 1 - CountWeekendDays(holidayRequestForApproval.Start, holidayRequestForApproval.End);
                employeeForApproval.DaysOff += wantedDays;
                await _employeeRepository.UpdateEmployeeAsync(employeeForApproval, cancellationToken);

                holidayRequestForApproval.Status = HolidayRequestStatus.Denied;
                holidayRequestForApproval.Sender = employeeForApproval;
                await _holidayRequestRepository.UpdateHolidayRequestAsync(holidayRequestForApproval, cancellationToken);   
            }
            else if(!holidayRequestApproversForRequest
                .Where(x => (x.Status == HolidayRequestStatus.Denied || x.Status == HolidayRequestStatus.Pending) && x.Id != domainEntity.Id)
                .Any())
            {
                holidayRequestForApproval.Status = HolidayRequestStatus.Approved;
                await _holidayRequestRepository.UpdateHolidayRequestAsync(holidayRequestForApproval, cancellationToken);
            }

            var persistedHolidayRequestApprover = await _holidayRequestApproverRepository.UpdateHolidayRequestApproverAsync(domainEntity, cancellationToken);
            return persistedHolidayRequestApprover;
        }
        private int CountWeekendDays(DateTime start, DateTime end)
        {
            int weekendDays = 0;

            // Iterate through each day in the range
            for (DateTime date = start.Date; date <= end.Date; date = date.AddDays(1))
            {
                // Check if the day is Saturday or Sunday
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekendDays++;
                }
            }

            return weekendDays;
        }
    }

    public record UpdateHolidayRequestApproverCommand(Guid RequestId, Guid ApproverId, HolidayRequestStatus Status) : IRequest<Core.Entities.HolidayRequestApprover>;
}
