using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using PaysisReconAPI.DatabaseContext;
using PaysisReconAPI.Model;
using PaysisReconAPI.Service;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace PaysisReconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AccountService AccService;
        private readonly IConfiguration _config;
        public AuthController(IDataDbContext db, IConfiguration config) 
        {
            AccService = new AccountService(db);
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin request)
        {
            if (IsValidInput(request.UserName))
            {
                ResponseStatus ResponseStatus =  AccService.AuthenticateUser(request.UserName, request.Password, "", "");

                if (ResponseStatus.strcode.Equals("00"))
                {
                    var jwtSettings = _config.GetSection("JwtSettings");
                    var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);
                    
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, request.UserName)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
                        Issuer = jwtSettings["Issuer"],
                        Audience = jwtSettings["Audience"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { message = "Login successful", accessToken = tokenString });
                }
                else
                {
                    return Unauthorized(new { message = ResponseStatus.strmessage ?? "Invalid credentials" });
                }
            }
            else
            {
                return BadRequest(new { message = "Invalid input. Only letters, numbers, @, ., and _ are allowed." });
            }
        }

        public static bool IsValidInput(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            // ^ start, $ end → only allowed characters
            return Regex.IsMatch(username, @"^[a-zA-Z0-9@._]+$");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // In a stateless JWT setup, logout is mainly handled by the client 
            // deleting the accessToken. 
            // If you later implement HttpOnly refresh cookies, delete them here:
            // Response.Cookies.Delete("refreshToken");

            return Ok(new { message = "Logged out successfully" });
        }

    }
}
