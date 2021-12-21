using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Core.Models
{
    public class AccountInfo
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public RoleType Role { get; set; }
    }
}
