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
        public Task<(bool isPass, RoleType roleType)> IsAuthenticateAsync(string account, string password);

        public Task<string> GenerateTokenAsync(string account, RoleType roleType);

        public List<string> GetRolePermissions(RoleType roleType);

        public Task LogoutAsync(string account, string token);

    }
}
