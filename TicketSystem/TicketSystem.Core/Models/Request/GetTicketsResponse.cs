using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Models.Request
{
    public class GetTicketsResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string TicketTypeName { get; set; }

        public TicketType TicketType { get; set; }

        public string TicketStatusName { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public int Severity { get; set; }

        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

        public long CreateDate { get; set; }

        public long UpdateDate { get; set; }
    }
}
