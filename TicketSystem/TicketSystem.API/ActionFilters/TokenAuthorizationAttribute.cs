using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using TicketSystem.Core.Models;
using TicketSystem.Core.Models.Enums;
using TicketSystem.Core.Models.Response;

namespace TicketSystem.API.ActionFilters
{
    public class TokenAuthorizationAttribute : TypeFilterAttribute
    {
        public TokenAuthorizationAttribute() : base(typeof(TokenAuthorizationFilter))
        {

        }
    }

    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IMemoryCache _memoryCache;

        public TokenAuthorizationFilter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new BaseResponse<object>(ApiResponseCode.UnAuthorized, null));
                context.HttpContext.Response.StatusCode = 401;
            }

            var claims = context.HttpContext.User.Claims;
            if (claims == null)
            {
                context.Result = new JsonResult(new BaseResponse<object>(ApiResponseCode.UnAuthorized, null));
                context.HttpContext.Response.StatusCode = 401;
            }
            var account = claims.FirstOrDefault(item => item.Type == ClaimTypes.Name);
            var role = claims.FirstOrDefault(item => item.Type == ClaimTypes.Role);
            var token = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty)
                .Trim();

            if (account == null || role == null)
            {
                context.Result = new JsonResult(new BaseResponse<object>(ApiResponseCode.UnAuthorized, null));
                context.HttpContext.Response.StatusCode = 401;
            }

            var tokenDict = this._memoryCache.Get<Dictionary<string, List<string>>>(Constant.Token);
            if (!tokenDict.ContainsKey(account.Value))
            {
                context.Result = new JsonResult(new BaseResponse<object>(ApiResponseCode.UnAuthorized, null));
                context.HttpContext.Response.StatusCode = 401;
            }

            if (tokenDict[account.Value].All(item => item != token))
            {
                context.Result = new JsonResult(new BaseResponse<object>(ApiResponseCode.UnAuthorized, null));
                context.HttpContext.Response.StatusCode = 401;
            }

        }
    }
}
