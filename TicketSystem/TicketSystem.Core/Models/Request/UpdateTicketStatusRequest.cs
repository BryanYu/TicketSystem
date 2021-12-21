using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Models.Request
{
    public class UpdateTicketStatusRequest
    {
        public TicketStatus TicketStatus { get; set; }
    }
}
