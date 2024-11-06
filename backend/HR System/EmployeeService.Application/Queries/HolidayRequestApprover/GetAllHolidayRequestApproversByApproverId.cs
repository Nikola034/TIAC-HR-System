using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByApproverIdHandler : IRequestHandler<GetAllHolidayRequestsApproversByApproverIdQuery, IEnumerable<Core.Entities.HolidayRequestApprover>>
    {

        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetAllHolidayRequestApproversByApproverIdHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<IEnumerable<Core.Entities.HolidayRequestApprover>> Handle(GetAllHolidayRequestsApproversByApproverIdQuery request, CancellationToken cancellationToken)
        {
            return await _holidayRequestApproverRepository.GetHolidayRequestApproversByApproverIdAsync(request.ApproverId, cancellationToken);
        }
    }


    public record GetAllHolidayRequestsApproversByApproverIdQuery(Guid ApproverId) : IRequest<IEnumerable<Core.Entities.HolidayRequestApprover>>;
}
