using EmployeeService.Application.Common.Repositories;
using EmployeeService.Application.Queries.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequest
{
    public class GetAllHolidayRequestsHandler : IRequestHandler<GetAllHolidayRequestsQuery, GetAllHolidayRequestsQueryResponse>
    {

        private readonly IHolidayRequestRepository _holidayRequestRepository;
        public GetAllHolidayRequestsHandler(IHolidayRequestRepository holidayRequestRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
        }
        public async Task<GetAllHolidayRequestsQueryResponse> Handle(GetAllHolidayRequestsQuery request, CancellationToken cancellationToken)
        {
            var holidayRequests = await _holidayRequestRepository.GetAllHolidayRequestsAsync(request.page, request.items, cancellationToken);
            var totalPages = await _holidayRequestRepository.GetTotalPagesAsync(request.page, request.items, cancellationToken);
            return new GetAllHolidayRequestsQueryResponse(holidayRequests, request.page, totalPages, request.items);
        }
    }

    public record GetAllHolidayRequestsQuery(int page, int items) : IRequest<GetAllHolidayRequestsQueryResponse>;
    public record GetAllHolidayRequestsQueryResponse(IEnumerable<Core.Entities.HolidayRequest> HolidayRequests, int Page, int TotalPages, int ItemsPerPage);
}
