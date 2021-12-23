using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        public ActionResult GenerateTokenAsync([FromBody] GenerateTokenRequest request)
        {
            var (isPass, role) = _authenticationService.IsAuthenticate(request.Account, request.Password);
            if (!isPass)
            {
                return Unauthorized(new BaseResponse<object>(ApiResponseCode.UnAuthorized, null));
            }
            var token = _authenticationService.GenerateToken(request.Account, role);
            return Ok(new BaseResponse<string>(ApiResponseCode.Success, token));
        }

        
    }
}
