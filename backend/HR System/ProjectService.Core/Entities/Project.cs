using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid? TeamLeadId { get; set; }
    }
}
