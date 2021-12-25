using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Models.Response
{
    public class GetAccountInfoResponse
    {
        public string Account { get; set; }

        public string RoleType { get; set; }

        public List<PermissionType> Permissions { get; set; }

        public List<TicketType> TicketTypes { get; set; }


    }
}
