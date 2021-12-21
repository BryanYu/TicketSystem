using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public RoleType Role
        {
            get
            {
                if (this.User.Identity != null && this.User.Identity.IsAuthenticated)
                {
                    var claim = this.User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Role);
                    var roleType = Enum.Parse<RoleType>(claim.Value);
                    return roleType;
                }
                return RoleType.Default;
            }
        }

        public string Account
        {
            get
            {
                if (this.User.Identity != null && this.User.Identity.IsAuthenticated)
                {
                    var claim = this.User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name);
                    
                    return claim.Value;
                }

                return string.Empty;
            }
        }
    }
}
