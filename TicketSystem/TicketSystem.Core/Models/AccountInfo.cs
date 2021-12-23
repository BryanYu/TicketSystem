using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Models
{
    public class AccountInfo
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public RoleType Role { get; set; }
    }
}
