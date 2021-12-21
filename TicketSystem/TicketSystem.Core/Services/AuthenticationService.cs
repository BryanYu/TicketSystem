using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TicketSystem.Core.Models;

namespace TicketSystem.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationService(IMemoryCache memoryCache, IOptions<JwtConfig> jwtConfig)
        {
            _memoryCache = memoryCache;
            _jwtConfig = jwtConfig.Value;
        }

        public (bool isPass, RoleType roleType) IsAuthenticate(string account, string password)
        {
            var data = _memoryCache.Get<List<AccountInfo>>(Constant.Account);
            var user = data.FirstOrDefault(item => item.Account == account && item.Password == password);
            if (user == null)
            {
                return (false, RoleType.Default);
            }

            return (true, user.Role);

        }

        public string GenerateToken(string account, RoleType roleType)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtConfig.Secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account),
                    new Claim(ClaimTypes.Role, roleType.ToString(), nameof(RoleType))
                }),
                Expires = DateTime.UtcNow.Add(new TimeSpan(_jwtConfig.ExpiredDays, 0, 0)),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
