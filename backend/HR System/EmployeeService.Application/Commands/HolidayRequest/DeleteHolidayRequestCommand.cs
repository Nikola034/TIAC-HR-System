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
        private readonly IHolidayRequestRepository _holidayrequestRepository;
        public DeleteHolidayRequestCommandHandler(IHolidayRequestRepository holidayrequestRepository)
        {
            _holidayrequestRepository = holidayrequestRepository;
        }
        public async Task<bool> Handle(DeleteHolidayRequestCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingHolidayRequest = await _holidayrequestRepository.GetHolidayRequestByIdAsync(domainEntity.Id);
            if (existingHolidayRequest is null)
            {
                throw new NotFoundException("Holiday request with that ID doesn't exist!");
            }
            var persistedHolidayRequest = await _holidayrequestRepository.DeleteHolidayRequestAsync(domainEntity.Id, cancellationToken);
            return persistedHolidayRequest;
        }

    }

    public record DeleteHolidayRequestCommand(Guid Id) : IRequest<bool>;
}
