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

namespace EmployeeService.Application.Commands.HolidayRequest
{
    public class CreateHolidayRequestCommandHandler : IRequestHandler<CreateHolidayRequestCommand, Core.Entities.HolidayRequest>
    {
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        public CreateHolidayRequestCommandHandler(IHolidayRequestRepository holidayRequestRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
        }
        public async Task<Core.Entities.HolidayRequest> Handle(CreateHolidayRequestCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingEmployee = await _holidayRequestRepository.GetHolidayRequestByIdAsync(domainEntity.Id);
            if (existingEmployee is not null)
            {
                throw new EmployeeAlreadyExistException();
            }
            domainEntity.Id = new Guid();
            var persistedEmployee = await _holidayRequestRepository.CreateHolidayRequestAsync(domainEntity, cancellationToken);
            return persistedEmployee;
        }

    }

    public record CreateHolidayRequestCommand(DateTime Start, DateTime End, HolidayRequestStatus Status) : IRequest<Core.Entities.HolidayRequest>;
}
