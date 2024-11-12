using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversHandler : IRequestHandler<GetAllHolidayRequestsApproversQuery, IEnumerable<Core.Entities.HolidayRequestApprover>>
    {

        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetAllHolidayRequestApproversHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<IEnumerable<Core.Entities.HolidayRequestApprover>> Handle(GetAllHolidayRequestsApproversQuery request, CancellationToken cancellationToken)
        {
            return await _holidayRequestApproverRepository.GetAllHolidayRequestApproversAsync(request.page, cancellationToken);
        }
    }


    public record GetAllHolidayRequestsApproversQuery(int page) : IRequest<IEnumerable<Core.Entities.HolidayRequestApprover>>;
}
