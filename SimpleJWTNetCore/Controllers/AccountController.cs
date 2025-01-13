using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleJWTNetCore.Application.Infrastracture.Account;
using SimpleJWTNetCore.Domain.Account;
using SimpleJWTNetCore.Domain.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleJWTNetCore.UI.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccount _iAccount;
        private readonly IConfiguration _configuration;
        public AccountController(IAccount iAccount, IConfiguration configuration)
        {
            _iAccount = iAccount;
            _configuration = configuration;
        }

        #region Authenticate
        private string GenerateJWTToken(JWTGenerateViewModel model)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, model.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            
            foreach (var role in model.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            JWTGenerateViewModel jwtModel = _iAccount.UserAuthenticate(model);
            if (jwtModel.IsAuth)
            {
                var token = GenerateJWTToken(jwtModel);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddHours(12),
                };
                Response.Cookies.Append("Token", token, cookieOptions);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized(new { Error = "You are not authorized" });
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            return Ok(new { Message = "Logout successful" });
        }

        [HttpGet("UserLoggedInOrNot")]
        public IActionResult UserLoggedInOrNot()
        {
            string token = Request.Cookies["Token"];
            if (!token.IsNullOrEmpty())
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _configuration["JwtSettings:Issuer"],
                        ValidAudience = _configuration["JwtSettings:Audience"],
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userName = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

                    return Ok(new { IsAuthenticated = true, UserName = userName });
                }
                catch (SecurityTokenExpiredException)
                {
                    return Unauthorized(new { Error = "Token has expired" });
                }
                catch (Exception)
                {
                    return Unauthorized(new { Error = "Invalid token" });
                }
            }
            else
            {
                return Unauthorized(new { Error = "No token found" });
            }
        }
        #endregion

        #region Account

        [Authorize(Roles = "Admin")]
        [HttpGet("SystemUser")]
        public IEnumerable<SystemUser> GetAllUsers()
        {
            return _iAccount.GetAllUsers();
        }
        #endregion

    }
}
