using EmployeeService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Entities
{
    public class HolidayRequestApprover
    {
        public Guid Id { get; set; }
        public Guid ApproverId { get; set; }
        public Guid RequestId { get; set; }
        public HolidayRequestStatus Status { get; set; }
    }
}
