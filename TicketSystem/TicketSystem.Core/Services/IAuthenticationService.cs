using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Services
{
    public interface IAuthenticationService
    {
        public (bool isPass, RoleType roleType) IsAuthenticate(string account, string password);

        string GenerateToken(string account, RoleType roleType);

        
    }
}
