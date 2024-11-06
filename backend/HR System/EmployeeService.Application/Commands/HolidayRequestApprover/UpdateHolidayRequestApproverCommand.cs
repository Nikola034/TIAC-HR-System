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
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public UpdateHolidayRequestApproverCommandHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
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
            var persistedHolidayRequestApprover = await _holidayRequestApproverRepository.UpdateHolidayRequestApproverAsync(domainEntity, cancellationToken);
            return persistedHolidayRequestApprover;
        }

    }

    public record UpdateHolidayRequestApproverCommand(Guid RequestId, Guid ApproverId, HolidayRequestStatus Status) : IRequest<Core.Entities.HolidayRequestApprover>;
}
