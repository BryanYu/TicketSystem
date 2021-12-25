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
    public class TokenController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public TokenController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ActionResult> GenerateTokenAsync([FromBody] GenerateTokenRequest request)
        {
            var (isPass, role) = await _authenticationService.IsAuthenticateAsync(request.Account, request.Password);
            if (!isPass)
            {
                return Unauthorized(new BaseResponse<object>(ApiResponseCode.UnAuthorized, null));
            }
            var token = await _authenticationService.GenerateTokenAsync(request.Account, role);
            return Ok(new BaseResponse<string>(ApiResponseCode.Success, token));
        }

        [HttpPost("Logout")]
        [Authorize]
        [TokenAuthorization]
        public async Task<ActionResult> LogoutAsync()
        {
            var authorization = this.Request.Headers.Authorization;
            var token = authorization.ToString().Replace("Bearer", string.Empty).Trim();
            await _authenticationService.LogoutAsync(base.Account, token);
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, null));
        }
        
    }
}
