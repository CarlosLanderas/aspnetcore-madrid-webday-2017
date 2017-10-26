using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlainConcepts.WebDay.API.Auth.Requests;
using PlainConcepts.WebDay.Infrastructure.DataContext;

namespace PlainConcepts.WebDay.API.Auth
{
    [Route("api/token")]
    public class AuthController : ControllerBase
    {
        private readonly WebDayDbContext _dbContext;

        public AuthController(WebDayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet, Route("get-claims")]
        [Authorize]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new {c.Type, c.Value});
            return Ok(claims);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Generate([FromBody] GenerateTokenRequest request)
        {
            
            var user = await _dbContext.Users.Include("_userRoles")
                .FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64)
                };

                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }

                // Create the JWT and write it to a string
                var jwt = new JwtSecurityToken(
                    issuer: "WebDay",
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SymmetricSecurityKey")), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = 50000
                };

                return Ok(response);
            }

            return BadRequest();
        }

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
