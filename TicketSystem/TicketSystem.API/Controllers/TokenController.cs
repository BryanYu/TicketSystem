using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicketSystem.Core.Models;
using TicketSystem.Core.Models.Enums;
using TicketSystem.Core.Models.Response;
using TicketSystem.Core.Services;

namespace TicketSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IAuthenticationService _authenticationService;

        public TokenController(IOptions<JwtConfig> jwtConfig, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public ActionResult GenerateTokenAsync([FromBody] GenerateTokenRequest request)
        {
            var (isPass, role) = _authenticationService.IsAuthenticate(request.Account, request.Password);
            if (!isPass)
            {
                return Unauthorized(new BaseResponse<object>
                {
                    Code = ApiResponseCode.UnAuthorized,
                    Message = "UnAuthorized"
                });
            }

            var token = _authenticationService.GenerateToken(request.Account, role);
            return Ok(new BaseResponse<string>
            {
                Code = ApiResponseCode.Success,
                Message = "",
                Data = token
            });
        }
    }
}
