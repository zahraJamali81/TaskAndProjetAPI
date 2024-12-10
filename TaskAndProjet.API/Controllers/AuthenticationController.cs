using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskAndProjet.API.Models;

namespace TaskAndProjet.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }

        private readonly IConfiguration _configuration;
        private readonly Context _context;

        public AuthenticationController(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate([FromBody] AuthenticationRequestBody authRequestBody)
        {
            if (authRequestBody == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = ValidateUserCredentials(authRequestBody.UserName, authRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
            );
            var signingCredential = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256
            );
            var claimsForToken = new List<Claim>();
            
            claimsForToken.Add(new Claim("userId", user.Id.ToString()));
            claimsForToken.Add(new Claim("userId", user.FullName.ToString()));
            

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredential
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        //private UserModels ValidateUserCredentials(string? userName, string? password)
        //{

        //    return new UserModels(1, userName??"", "Zahra", "Jamali");

        //}


   

        private UserModels? ValidateUserCredentials(string? userName, string? password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _context.Users
                        .FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (user != null)
            {
                return new UserModels(user.Id, user.UserName, user.FirstName, user.LastName);
            }

            return null;
        }

    }
}
