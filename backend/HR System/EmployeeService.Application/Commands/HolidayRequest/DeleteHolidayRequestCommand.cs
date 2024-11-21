using Common.Exceptions;
using EmployeeService.Application.Common.Mappers;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Commands.HolidayRequest
{
    public class DeleteHolidayRequestCommandHandler : IRequestHandler<DeleteHolidayRequestCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHolidayRequestRepository _holidayrequestRepository;
        private readonly IHolidayRequestApproverRepository _holidayrequestApproversRepository;
        public DeleteHolidayRequestCommandHandler(IEmployeeRepository employeeRepository, IHolidayRequestRepository holidayrequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _employeeRepository = employeeRepository;
            _holidayrequestRepository = holidayrequestRepository;
            _holidayrequestApproversRepository = holidayRequestApproverRepository;
        }
        public async Task<bool> Handle(DeleteHolidayRequestCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingHolidayRequest = await _holidayrequestRepository.GetHolidayRequestByIdAsync(domainEntity.Id);
            if (existingHolidayRequest is null)
            {
                throw new NotFoundException("Holiday request with that ID doesn't exist!");
            }

            IEnumerable<Core.Entities.HolidayRequestApprover> holidayRequestApprovers = await _holidayrequestApproversRepository.GetHolidayRequestApproversByRequestIdAsync(existingHolidayRequest.Id, cancellationToken);
            foreach(var holidayRequest in holidayRequestApprovers)
            {
                await _holidayrequestApproversRepository.DeleteHolidayRequestApproverAsync(holidayRequest.Id, cancellationToken);
            }

            int wantedDays = (existingHolidayRequest.End - existingHolidayRequest.Start).Days - CountWeekendDays(existingHolidayRequest.Start, existingHolidayRequest.End) + 1;
            existingHolidayRequest.Sender.DaysOff += wantedDays;
            await _employeeRepository.UpdateEmployeeAsync(existingHolidayRequest.Sender, cancellationToken);

            var persistedHolidayRequest = await _holidayrequestRepository.DeleteHolidayRequestAsync(domainEntity.Id, cancellationToken);
            return persistedHolidayRequest;
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

    public record DeleteHolidayRequestCommand(Guid Id) : IRequest<bool>;
}
