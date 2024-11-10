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

namespace EmployeeService.Application.Commands.HolidayRequest
{
    public class CreateHolidayRequestCommandHandler : IRequestHandler<CreateHolidayRequestCommand, Core.Entities.HolidayRequest>
    {
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectHttpClient _projectHttpClient;
        public CreateHolidayRequestCommandHandler(IHolidayRequestRepository holidayRequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository, IEmployeeRepository employeeRepository, IProjectHttpClient projectHttpClient)
        {
            _holidayRequestRepository = holidayRequestRepository;
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
            _employeeRepository = employeeRepository;
            _projectHttpClient = projectHttpClient;
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
            int wantedDays = (request.End - request.Start).Days;
            if(sender.DaysOff >= wantedDays)
            {
                sender.DaysOff -= wantedDays;
                await _employeeRepository.UpdateEmployeeAsync(sender, cancellationToken);
                domainEntity.Status = HolidayRequestStatus.Pending;
                var persistedHolidayRequest = await _holidayRequestRepository.CreateHolidayRequestAsync(domainEntity, cancellationToken);

                IEnumerable<Guid> teamLeadIds = await _projectHttpClient.GetTeamLeadsForEmployee(domainEntity.SenderId, cancellationToken);
                if (teamLeadIds.Any())
                {
                    foreach (var teamLeadId in teamLeadIds)
                    {
                        Core.Entities.HolidayRequestApprover holidayRequestApprover = new Core.Entities.HolidayRequestApprover();
                        holidayRequestApprover.Id = new Guid();
                        holidayRequestApprover.RequestId = persistedHolidayRequest.Id;
                        holidayRequestApprover.ApproverId = teamLeadId;
                        holidayRequestApprover.Status = HolidayRequestStatus.Pending;
                        var persistedHolidayRequestApprover = await _holidayRequestApproverRepository.CreateHolidayRequestApproverAsync(holidayRequestApprover, cancellationToken);
                    }
                }
                else
                {
                    IEnumerable<Core.Entities.Employee> managers = await _employeeRepository.GetAllManagersAsync(cancellationToken);
                    foreach (var manager in managers)
                    {
                        Core.Entities.HolidayRequestApprover holidayRequestApprover = new Core.Entities.HolidayRequestApprover();
                        holidayRequestApprover.Id = new Guid();
                        holidayRequestApprover.RequestId = persistedHolidayRequest.Id;
                        holidayRequestApprover.ApproverId = manager.Id;
                        holidayRequestApprover.Status = HolidayRequestStatus.Pending;
                        var persistedHolidayRequestApprover = await _holidayRequestApproverRepository.CreateHolidayRequestApproverAsync(holidayRequestApprover, cancellationToken);
                    }
                }

                return persistedHolidayRequest;
            }
            throw new NoAvailableDaysOffException();
        }

    }

    public record CreateHolidayRequestCommand(DateTime Start, DateTime End, HolidayRequestStatus Status, Guid SenderId) : IRequest<Core.Entities.HolidayRequest>;
}
