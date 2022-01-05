using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ResortProjectAPI.IServices;

namespace ResortProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IStaffSV _staff;
        private readonly IConfiguration _config;
        public AuthController(IStaffSV staff, IConfiguration config)
        {
            _staff = staff;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Model is invalid: " + ModelState.Values);
            var staff = await _staff.GetByID(model.Username);
            if (staff == null) return NotFound("Staff not exist");
            return staff.Password != model.Password ? BadRequest("Password was wrong") : Ok(new { token = GenerateJSONWebToken(model, staff.RoleID, staff.Name) });
        }

        //Generate JWT
        [NonAction]
        private string GenerateJSONWebToken(LoginModel model, string role, string name)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:JWT_Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                new Claim(ClaimTypes.Role,role.ToUpper()),
                new Claim("name",name),
                new Claim("role",role.ToUpper())
            };
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
