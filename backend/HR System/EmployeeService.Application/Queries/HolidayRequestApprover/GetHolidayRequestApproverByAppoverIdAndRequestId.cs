using Common.Exceptions;
using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequestApprover
{
    public class GetHolidayRequestsApproverByApproverIdAndRequestIdHandler : IRequestHandler<GetHolidayRequestsApproverByApproverIdAndRequestIdQuery, Core.Entities.HolidayRequestApprover>
    {

        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetHolidayRequestsApproverByApproverIdAndRequestIdHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<Core.Entities.HolidayRequestApprover> Handle(GetHolidayRequestsApproverByApproverIdAndRequestIdQuery request, CancellationToken cancellationToken)
        {
            var holidayRequestApprover = await _holidayRequestApproverRepository.GetHolidayRequestApproverByRequestIdAndApproverIdAsync(request.RequestId, request.ApproverId, cancellationToken);
            if (holidayRequestApprover is null)
            {
                throw new NotFoundException("HolidayRequestApprover with provided Ids doesn't exist!");
            }
            return holidayRequestApprover;
        }
    }


    public record GetHolidayRequestsApproverByApproverIdAndRequestIdQuery(Guid RequestId, Guid ApproverId) : IRequest<Core.Entities.HolidayRequestApprover>;
}
