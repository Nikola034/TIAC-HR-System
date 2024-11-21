using Common.HttpCLients;
using Core.Exceptions;
using EmployeeService.Application.Common.Mappers;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Enums;
using EmployeeService.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Collections;

namespace EmployeeService.Application.Commands.HolidayRequest
{
    public class CreateHolidayRequestCommandHandler : IRequestHandler<CreateHolidayRequestCommand, Core.Entities.HolidayRequest>
    {
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectHttpClient _projectHttpClient;
        private readonly IHubContext<NotificationHub> _hubContext;
        public CreateHolidayRequestCommandHandler(IHolidayRequestRepository holidayRequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository,
            IEmployeeRepository employeeRepository, IProjectHttpClient projectHttpClient, IHubContext<NotificationHub> hubContext)
        {
            _holidayRequestRepository = holidayRequestRepository;
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
            _employeeRepository = employeeRepository;
            _projectHttpClient = projectHttpClient;
            _hubContext = hubContext;
        }
        public async Task<Core.Entities.HolidayRequest> Handle(CreateHolidayRequestCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingHolidayRequest = await _holidayRequestRepository.CheckHolidayRequestExistenceAsync(request.SenderId, request.Start, request.End, cancellationToken);
            if (existingHolidayRequest)
            {
                throw new HolidayRequestAlreadyExistException();
            }
            domainEntity.Id = new Guid();

            var sender = await _employeeRepository.GetEmployeeByIdAsync(domainEntity.SenderId);
            int wantedDays = (request.End - request.Start).Days - CountWeekendDays(request.Start, request.End) + 1;
            if (sender.DaysOff >= wantedDays)
            {
                sender.DaysOff -= wantedDays;
                await _employeeRepository.UpdateEmployeeAsync(sender, cancellationToken);
                domainEntity.Status = HolidayRequestStatus.Pending;
                var persistedHolidayRequest = await _holidayRequestRepository.CreateHolidayRequestAsync(domainEntity, cancellationToken);

                IEnumerable<Guid> teamLeadIds = await _projectHttpClient.GetTeamLeadsForEmployeeAsync(domainEntity.SenderId, request.Token, cancellationToken);
                if (teamLeadIds.Where(x => x != sender.Id).Any())
                {
                    foreach (var teamLeadId in teamLeadIds)
                    {
                        Core.Entities.HolidayRequestApprover holidayRequestApprover = new Core.Entities.HolidayRequestApprover();
                        holidayRequestApprover.Id = new Guid();
                        holidayRequestApprover.RequestId = persistedHolidayRequest.Id;
                        holidayRequestApprover.ApproverId = teamLeadId;
                        holidayRequestApprover.Status = HolidayRequestStatus.Pending;
                        var persistedHolidayRequestApprover = await _holidayRequestApproverRepository.CreateHolidayRequestApproverAsync(holidayRequestApprover, cancellationToken);
                        //await _hubContext.Clients.User(teamLeadId.ToString()).SendAsync("ReceiveNotification",
                        //    $"{sender.Name} {sender.Surname} has sent a holiday request from {request.Start} until {request.End}");
                    }
                }
                else
                {
                    Core.Entities.Employee? manager = await _employeeRepository.GetFirstManagerAsync(request.SenderId, cancellationToken);
                    Core.Entities.HolidayRequestApprover holidayRequestApprover = new Core.Entities.HolidayRequestApprover();
                    holidayRequestApprover.Id = new Guid();
                    holidayRequestApprover.RequestId = persistedHolidayRequest.Id;
                    holidayRequestApprover.ApproverId = manager != null ? manager.Id : request.SenderId;
                    holidayRequestApprover.Status = HolidayRequestStatus.Pending;
                    //await _hubContext.Clients.User(manager.Id.ToString()).SendAsync("ReceiveNotification",
                    //        $"{sender.Name} {sender.Surname} has sent a holiday request from {request.Start} until {request.End}");
                    var persistedHolidayRequestApprover = await _holidayRequestApproverRepository.CreateHolidayRequestApproverAsync(holidayRequestApprover, cancellationToken);
                }

                return persistedHolidayRequest;
            }
            throw new NoAvailableDaysOffException();
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

    public record CreateHolidayRequestCommand(DateTime Start, DateTime End, HolidayRequestStatus Status, Guid SenderId, string Token) : IRequest<Core.Entities.HolidayRequest>;
}
