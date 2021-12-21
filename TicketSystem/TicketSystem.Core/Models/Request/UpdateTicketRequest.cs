using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Core.Models.Request
{
    public class UpdateTicketRequest
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }
    }
}
