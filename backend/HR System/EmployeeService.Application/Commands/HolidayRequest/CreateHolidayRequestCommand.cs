using Core.Exceptions;
using EmployeeService.Application.Common.Mappers;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Enums;
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
        public CreateHolidayRequestCommandHandler(IHolidayRequestRepository holidayRequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<Core.Entities.HolidayRequest> Handle(CreateHolidayRequestCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingHolidayRequest = await _holidayRequestRepository.GetHolidayRequestByIdAsync(domainEntity.Id, cancellationToken);
            if (existingHolidayRequest is not null)
            {
                throw new EmployeeAlreadyExistException();
            }
            domainEntity.Id = new Guid();
            // find ALL TEAM LEADERS or MANAGER
            // var approvers = new List<approver>();
            /* if(!userProjects.empty){
             *      for(var p in projects){
             *          if(p.TeamLeadId is not null){
             *              Core.Entities.HolidayRequestApprover holidayRequestApprover = new Core.Entities.HolidayRequestApprover(); 
             *              holidayRequestApprover.RequestId = domainEntity.Id;
             *              holidayRequestApprover.ApproverId = projects.TeamLeadId;
             *              holidayRequestApprover.Status = Pending;
             *              approvers.add(holidayRequestApprover);
                        }          
             *     }
             *   
             * }
             * if(!approvers.empty){
             *     foreach(var a in approvers){
             *      await _holidayRequestApproverRepository.CreateHolidayRequestApproverAsync(a);
             *     }
             * }
             * else{
             *  Core.Entities.HolidayRequestApprover holidayRequestApprover = new Core.Entities.HolidayRequestApprover(); 
             *  holidayRequestApprover.RequestId = domainEntity.Id;
             *  holidayRequestApprover.ApproverId = ManagerId
             *  holidayRequestApprover.Status = Pending;
             * }
            */
            var persistedHolidayRequest = await _holidayRequestRepository.CreateHolidayRequestAsync(domainEntity, cancellationToken);
            return persistedHolidayRequest;
        }

    }

    public record CreateHolidayRequestCommand(DateTime Start, DateTime End, HolidayRequestStatus Status, Guid SenderId) : IRequest<Core.Entities.HolidayRequest>;
}
