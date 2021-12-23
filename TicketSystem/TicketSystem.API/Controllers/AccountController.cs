using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicketSystem.API.ActionFilters;
using TicketSystem.Core.Models.Config;
using TicketSystem.Core.Models.Enums;
using TicketSystem.Core.Models.Request;
using TicketSystem.Core.Models.Response;
using TicketSystem.Core.Services;

namespace TicketSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TokenAuthorization]
    public class AccountController : BaseController
    {
        [HttpGet("")]
        [Authorize(Roles = "RD,QA")]
        public ActionResult GetAccountInfo()
        {
            return Ok(new BaseResponse<GetAccountInfoResponse>(ApiResponseCode.Success, new GetAccountInfoResponse
            {
                Account = base.Account,
                RoleType = base.Role.ToString()
            }));
        }
    }
}
