using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByRequestIdHandler : IRequestHandler<GetAllHolidayRequestsApproversByRequestIdQuery, IEnumerable<Core.Entities.HolidayRequestApprover>>
    {

        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetAllHolidayRequestApproversByRequestIdHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<IEnumerable<Core.Entities.HolidayRequestApprover>> Handle(GetAllHolidayRequestsApproversByRequestIdQuery request, CancellationToken cancellationToken)
        {
            return await _holidayRequestApproverRepository.GetHolidayRequestApproversByRequestIdAsync(request.RequestId, cancellationToken);
        }
    }


    public record GetAllHolidayRequestsApproversByRequestIdQuery(Guid RequestId) : IRequest<IEnumerable<Core.Entities.HolidayRequestApprover>>;
}
