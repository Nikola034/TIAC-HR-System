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
    public class CreateHolidayRequestApproverCommandHandler : IRequestHandler<CreateHolidayRequestApproverCommand, Core.Entities.HolidayRequestApprover>
    {
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public CreateHolidayRequestApproverCommandHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<Core.Entities.HolidayRequestApprover> Handle(CreateHolidayRequestApproverCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingHolidayRequestApprover = await _holidayRequestApproverRepository.GetHolidayRequestApproverByIdAsync(domainEntity.Id, cancellationToken);
            if (existingHolidayRequestApprover is not null)
            {
                throw new EmployeeAlreadyExistException();
            }
            domainEntity.Id = new Guid();
            var persistedHolidayRequestApprover = await _holidayRequestApproverRepository.CreateHolidayRequestApproverAsync(domainEntity, cancellationToken);
            return persistedHolidayRequestApprover;
        }

    }

    public record CreateHolidayRequestApproverCommand(Guid ApproverId, Guid RequestId, HolidayRequestStatus Status) : IRequest<Core.Entities.HolidayRequestApprover>;
}
