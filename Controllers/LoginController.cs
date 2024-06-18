using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiAuth.Data;
using WebApiAuth.Models;

namespace WebApiAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")] 

        public ActionResult Login(TblUser user)
        {
            var email = _context.TblUsers.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if(email== null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
                else
                {
                    user.UserMessage = "Login Successfully";
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId",user.ID.ToString()),
                    new Claim("DisplayName", user.Name),
                    new Claim("UserName", user.Name),
                    new Claim("Email",user.Email)
                };

                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var SignIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: SignIn);
                    user.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(user);
                }
        }  
    }
}
