using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequest
{
    public class GetAllHolidayRequestsHandler : IRequestHandler<GetAllHolidayRequestsQuery, IEnumerable<Core.Entities.HolidayRequest>>
    {

        private readonly IHolidayRequestRepository _holidayRequestRepository;
        public GetAllHolidayRequestsHandler(IHolidayRequestRepository holidayRequestRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
        }
        public async Task<IEnumerable<Core.Entities.HolidayRequest>> Handle(GetAllHolidayRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _holidayRequestRepository.GetAllHolidayRequestsAsync(request.page, cancellationToken);
        }
    }


    public record GetAllHolidayRequestsQuery(int page) : IRequest<IEnumerable<Core.Entities.HolidayRequest>>;
}
