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
    public class GetHolidayRequestApproverByIdHandler : IRequestHandler<GetHolidayRequestApproverByIdQuery, Core.Entities.HolidayRequestApprover>
    {

        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetHolidayRequestApproverByIdHandler(IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<Core.Entities.HolidayRequestApprover> Handle(GetHolidayRequestApproverByIdQuery request, CancellationToken cancellationToken)
        {
            var holidayRequestApprover = await _holidayRequestApproverRepository.GetHolidayRequestApproverByIdAsync(request.Id, cancellationToken);
            if (holidayRequestApprover is null)
            {
                throw new NotFoundException("HolidayRequestApprover with provided Id doesn't exist!");
            }
            return holidayRequestApprover;
        }
    }


    public record GetHolidayRequestApproverByIdQuery(Guid Id) : IRequest<Core.Entities.HolidayRequestApprover>;
}
