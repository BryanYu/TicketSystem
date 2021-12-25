﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Models.Request
{
    public class CreateTicketRequest
    {
        public string Title { get; set; }

        public TicketType TicketType { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public int Severity { get; set; }

        public int Priority { get; set; }

    }
}
