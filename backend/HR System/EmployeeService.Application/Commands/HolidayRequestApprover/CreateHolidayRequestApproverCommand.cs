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
            var existingEmployee = await _holidayRequestApproverRepository.GetHolidayRequestApproverByIdAsync(domainEntity.Id);
            if (existingEmployee is not null)
            {
                throw new EmployeeAlreadyExistException();
            }
            domainEntity.Id = new Guid();
            var persistedEmployee = await _holidayRequestApproverRepository.CreateHolidayRequestApproverAsync(domainEntity, cancellationToken);
            return persistedEmployee;
        }

    }

    public record CreateHolidayRequestApproverCommand(Guid ApproverId, Guid RequestId, HolidayRequestStatus Status) : IRequest<Core.Entities.HolidayRequestApprover>;
}
