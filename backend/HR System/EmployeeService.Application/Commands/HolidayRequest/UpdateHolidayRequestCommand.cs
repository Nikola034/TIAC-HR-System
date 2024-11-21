using Common.Exceptions;
using EmployeeService.Application.Common.Mappers;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Enums;
using EmployeeService.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Commands.HolidayRequest
{
    public class UpdateHolidayRequestCommandHandler : IRequestHandler<UpdateHolidayRequestCommand, Core.Entities.HolidayRequest>
    {
        private readonly IHolidayRequestRepository _holidayrequestRepository;

        public UpdateHolidayRequestCommandHandler(IHolidayRequestRepository holidayrequestRepository)
        {
            _holidayrequestRepository = holidayrequestRepository;
        }
        public async Task<Core.Entities.HolidayRequest> Handle(UpdateHolidayRequestCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingHolidayRequest = await _holidayrequestRepository.GetHolidayRequestByIdAsync(domainEntity.Id, cancellationToken);
            if (existingHolidayRequest is null)
            {
                throw new NotFoundException("Holiday request with that ID doesn't exist!");
            }
            domainEntity.Sender = existingHolidayRequest.Sender;
            var persistedHolidayRequest = await _holidayrequestRepository.UpdateHolidayRequestAsync(domainEntity, cancellationToken);
            if(persistedHolidayRequest.Status == HolidayRequestStatus.Approved)
            {
                string requestCreatedMessage = $"Your holiday request from {persistedHolidayRequest.Start.Date.ToString("d")} to {persistedHolidayRequest.End.Date.ToString("d")} is APPROVED";
                if (SseConnectionManager.UserConnections.TryGetValue(persistedHolidayRequest.SenderId.ToString(), out var userChannel))
                {
                    await userChannel.Writer.WriteAsync(requestCreatedMessage, cancellationToken);
                }
            }
            return persistedHolidayRequest;
        }

    }

    public record UpdateHolidayRequestCommand(Guid Id, DateTime Start, DateTime End, HolidayRequestStatus Status) : IRequest<Core.Entities.HolidayRequest>;
}
