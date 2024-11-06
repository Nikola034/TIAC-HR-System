using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequestApprover
{
    public class GetAllHolidayRequestsApproversByApproverIdAndRequestIdHandler : IRequestHandler<GetAllHolidayRequestsApproversByApproverIdAndRequestIdQuery, Core.Entities.HolidayRequestApprover>
    {

        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetAllHolidayRequestsApproversByApproverIdAndRequestIdHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<Core.Entities.HolidayRequestApprover> Handle(GetAllHolidayRequestsApproversByApproverIdAndRequestIdQuery request, CancellationToken cancellationToken)
        {
            return await _holidayRequestApproverRepository.GetHolidayRequestApproverByRequestIdAndApproverIdAsync(request.RequestId, request.ApproverId, cancellationToken);
        }
    }


    public record GetAllHolidayRequestsApproversByApproverIdAndRequestIdQuery(Guid RequestId, Guid ApproverId) : IRequest<Core.Entities.HolidayRequestApprover>;
}
