using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequestApprover
{
    public class GetAllHolidayRequestApproversByIdHandler : IRequestHandler<GetAllHolidayRequestsApproversByIdQuery, Core.Entities.HolidayRequestApprover>
    {

        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetAllHolidayRequestApproversByIdHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<Core.Entities.HolidayRequestApprover> Handle(GetAllHolidayRequestsApproversByIdQuery request, CancellationToken cancellationToken)
        {
            return await _holidayRequestApproverRepository.GetHolidayRequestApproverByIdAsync(request.Id, cancellationToken);
        }
    }


    public record GetAllHolidayRequestsApproversByIdQuery(Guid Id) : IRequest<Core.Entities.HolidayRequestApprover>;
}
