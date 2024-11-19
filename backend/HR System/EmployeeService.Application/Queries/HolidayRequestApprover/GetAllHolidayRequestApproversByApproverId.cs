using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByApproverIdHandler : IRequestHandler<GetAllHolidayRequestsApproversByApproverIdQuery, IEnumerable<GetAllHolidayRequestsApproversByApproverIdQueryResponse>>
    {
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetAllHolidayRequestApproversByApproverIdHandler(IHolidayRequestRepository holidayRequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<IEnumerable<GetAllHolidayRequestsApproversByApproverIdQueryResponse>> Handle(GetAllHolidayRequestsApproversByApproverIdQuery request, CancellationToken cancellationToken)
        {
            var approvers = await _holidayRequestApproverRepository.GetHolidayRequestApproversByApproverIdAsync(request.ApproverId, cancellationToken);
            List<GetAllHolidayRequestsApproversByApproverIdQueryResponse> responses = new List<GetAllHolidayRequestsApproversByApproverIdQueryResponse>();
            foreach (var approver in approvers)
            {
                var holidayRequest = await _holidayRequestRepository.GetHolidayRequestByIdAsync(approver.RequestId, cancellationToken);
                responses.Add(new GetAllHolidayRequestsApproversByApproverIdQueryResponse
                (
                   approver.Id,
                   approver.RequestId,
                   holidayRequest.Sender.Name,
                   holidayRequest.Sender.Surname,
                   holidayRequest.Start,
                   holidayRequest.End,
                   holidayRequest.Status
                ));
            }
            return responses.OrderBy(x => x.Start);
        }
    }


    public record GetAllHolidayRequestsApproversByApproverIdQuery(Guid ApproverId) : IRequest<IEnumerable<GetAllHolidayRequestsApproversByApproverIdQueryResponse>>;
    public record GetAllHolidayRequestsApproversByApproverIdQueryResponse(Guid Id, Guid RequestId, string SenderName, string SenderSurname, DateTime Start, DateTime End, HolidayRequestStatus Status);
}
