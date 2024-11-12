using EmployeeService.Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Application.Queries.Employee
{
    public class GetDaysOffForEmployeesHandler : IRequestHandler<GetDaysOffForEmployeesQuery, GetDaysOffForEmployeesQueryResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        private readonly IHolidayRequestApproverRepository _holidayRequestApproverRepository;
        public GetDaysOffForEmployeesHandler(IEmployeeRepository employeeRepository, IHolidayRequestRepository holidayRequestRepository, IHolidayRequestApproverRepository holidayRequestApproverRepository)
        {
            _employeeRepository = employeeRepository;
            _holidayRequestRepository = holidayRequestRepository;
            _holidayRequestApproverRepository = holidayRequestApproverRepository;
        }
        public async Task<GetDaysOffForEmployeesQueryResponse> Handle(GetDaysOffForEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllEmployeesWithoutPagingAsync(cancellationToken);
            List<GetDaysOffForEmployeeReport> reports = new List<GetDaysOffForEmployeeReport>();
            foreach(var employee in employees)
            {
                var holidayRequestsForEmployee = await _holidayRequestRepository.GetAllHolidayRequestsBySenderIdAsync(employee.Id, cancellationToken);
                int realizedDays = 0;
                foreach(var holidayRequest in holidayRequestsForEmployee.Where(x => x.Status == Core.Enums.HolidayRequestStatus.Approved))
                {
                    realizedDays += (holidayRequest.End - holidayRequest.Start).Days;
                }
                GetDaysOffForEmployeeReport report = new GetDaysOffForEmployeeReport(employee, realizedDays, employee.DaysOff, holidayRequestsForEmployee.Where(x => x.Status == Core.Enums.HolidayRequestStatus.Pending).Count());
                reports.Add(report);
            }
            return new GetDaysOffForEmployeesQueryResponse(reports);
        }
    }


    public record GetDaysOffForEmployeesQuery() : IRequest<GetDaysOffForEmployeesQueryResponse>;

    public record GetDaysOffForEmployeesQueryResponse(IEnumerable<GetDaysOffForEmployeeReport> Reports);
    public record GetDaysOffForEmployeeReport(Core.Entities.Employee Employee, int realizedDays, int remainingDays, int pendingHolidayRequests);
}
