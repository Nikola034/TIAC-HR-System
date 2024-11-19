using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequest
{
    public class GetAllHolidayRequestsBySenderIdHandler : IRequestHandler<GetAllHolidayRequestsBySenderIdQuery, GetAllHolidayRequestsBySenderIdQueryResponse>
    {

        private readonly IHolidayRequestRepository _holidayRequestRepository;
        public GetAllHolidayRequestsBySenderIdHandler(IHolidayRequestRepository holidayRequestRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
        }
        public async Task<GetAllHolidayRequestsBySenderIdQueryResponse> Handle(GetAllHolidayRequestsBySenderIdQuery request, CancellationToken cancellationToken)
        {
            var holidayRequests = await _holidayRequestRepository.GetAllHolidayRequestsBySenderIdAsync(request.senderId, request.page, request.items, cancellationToken);
            var totalPages = (int)Math.Ceiling((double)(await _holidayRequestRepository.GetAllHolidayRequestsAsync(-1, -1, cancellationToken)).Where(x => x.SenderId == request.senderId).Count() / request.items);
            return new GetAllHolidayRequestsBySenderIdQueryResponse(holidayRequests, request.page, totalPages, request.items);
        }
    }

    public record GetAllHolidayRequestsBySenderIdQuery(Guid senderId, int page, int items) : IRequest<GetAllHolidayRequestsBySenderIdQueryResponse>;
    public record GetAllHolidayRequestsBySenderIdQueryResponse(IEnumerable<Core.Entities.HolidayRequest> HolidayRequests, int Page, int TotalPages, int Items);
}
