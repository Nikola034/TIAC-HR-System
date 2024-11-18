using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequest
{
    public class GetAllHolidayRequestsToApproveHandler : IRequestHandler<GetAllHolidayRequestsToApprovesQuery, GetAllHolidayRequestsToApproveQueryResponse>
    { 
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetAllHolidayRequestsToApproveHandler(IHolidayRequestRepository holidayRequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<GetAllHolidayRequestsToApproveQueryResponse> Handle(GetAllHolidayRequestsToApprovesQuery request, CancellationToken cancellationToken)
        {
            var approvers = await _holidayRequestApproverRepository.GetHolidayRequestApproversByApproverIdAsync(request.approverId, cancellationToken);
            List<Guid> holidayRequestsToApproveIds = new List<Guid>();
            foreach (var approver in approvers)
            {
                holidayRequestsToApproveIds.Add(approver.RequestId);
            }
            List<Core.Entities.HolidayRequest> holidayRequests = new List<Core.Entities.HolidayRequest>();
            foreach (var id in holidayRequestsToApproveIds)
            {
               holidayRequests.Add(await _holidayRequestRepository.GetHolidayRequestByIdAsync(id, cancellationToken));
            }

            return new GetAllHolidayRequestsToApproveQueryResponse(holidayRequests);
        }
    }

    public record GetAllHolidayRequestsToApprovesQuery(Guid approverId) : IRequest<GetAllHolidayRequestsToApproveQueryResponse>;
    public record GetAllHolidayRequestsToApproveQueryResponse(IEnumerable<Core.Entities.HolidayRequest> HolidayRequests);
}
