using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.API.ActionFilters;
using TicketSystem.Core.Models.Enums;
using TicketSystem.Core.Models.Response;
using TicketSystem.Core.Services;

namespace TicketSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TokenAuthorization]
    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITicketService _ticketService;

        public AccountController(IAuthenticationService authenticationService, ITicketService ticketService)
        {
            _authenticationService = authenticationService;
            _ticketService = ticketService;
        }
        [HttpGet("")]
        [Authorize(Roles = "RD,QA,PM")]
        public async Task<ActionResult> GetAccountInfo()
        {
            var permissions = await _authenticationService.GetRolePermissionsAsync(base.Role);
            var ticketTypes = await _ticketService.GetTicketTypeAsync(base.Role);
            return Ok(new BaseResponse<GetAccountInfoResponse>(ApiResponseCode.Success, new GetAccountInfoResponse
            {
                Account = base.Account,
                RoleType = base.Role.ToString(),
                Permissions = permissions,
                TicketTypes = ticketTypes
            }));
        }
    }
}
