using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.HolidayRequest
{
    public class GetHolidayRequestByIdHandler : IRequestHandler<GetHolidayRequestByIdQuery, Core.Entities.HolidayRequest>
    {

        private readonly IHolidayRequestRepository _holidayRequestRepository;
        public GetHolidayRequestByIdHandler(IHolidayRequestRepository holidayRequestRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
        }
        public async Task<Core.Entities.HolidayRequest> Handle(GetHolidayRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _holidayRequestRepository.GetHolidayRequestByIdAsync(request.Id, cancellationToken);
            return employee;
        }
    }


    public record GetHolidayRequestByIdQuery(Guid Id) : IRequest<Core.Entities.HolidayRequest>;
}
