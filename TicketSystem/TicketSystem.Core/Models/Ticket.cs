using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public TicketType TicketType { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }
        
        public int Severity { get; set; }

        public int Priority { get; set; }

        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset UpdateDate { get; set; }
    }
}
