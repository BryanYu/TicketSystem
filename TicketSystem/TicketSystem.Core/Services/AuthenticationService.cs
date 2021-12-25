using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TicketSystem.Core.Models;
using TicketSystem.Core.Models.Config;
using TicketSystem.Core.Models.Enums;

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

        public async Task<(bool isPass, RoleType roleType)> IsAuthenticateAsync(string account, string password)
        {
            var data = _memoryCache.Get<List<AccountInfo>>(Constant.Account);
            var user = data.FirstOrDefault(item => item.Account == account);
            if (user != null)
            {
                var comparePassword = ComputePassword(password, user.Salt);
                if (user.Password == comparePassword)
                {
                    return (true, user.Role);
                }
            }
            return (false, RoleType.Default);
        }

        public async Task<string> GenerateTokenAsync(string account, RoleType roleType)
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
                Issuer = this._jwtConfig.Issuer,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            var tokenDict = this._memoryCache.Get<Dictionary<string, List<string>>>(Constant.Token);
            if (tokenDict == null)
            {
                tokenDict = new Dictionary<string, List<string>>();
                tokenDict[account] = new List<string>
                {
                    token
                };
                this._memoryCache.Set(Constant.Token, tokenDict);
            }
            else 
            {
                if (tokenDict.ContainsKey(account))
                {
                    tokenDict[account].Add(token);
                }
                else
                {
                    tokenDict[account] = new List<string> { token };
                }
            }
            return token;
        }

        public Task LogoutAsync(string account, string token)
        {
            var tokenDict = this._memoryCache.Get<Dictionary<string, List<string>>>(Constant.Token);
            if (tokenDict != null && tokenDict.ContainsKey(account))
            {
                tokenDict[account].Remove(token);
            }

            return Task.CompletedTask;
        }

        public List<string> GetRolePermissions(RoleType roleType)
        {
            var permissions = _memoryCache.Get<Dictionary<RoleType, List<string>>>(Constant.Permissions);
            return permissions[roleType];
        }

        private string ComputePassword(string password, string salt)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(string.Concat(password, salt));
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        
    }
}
