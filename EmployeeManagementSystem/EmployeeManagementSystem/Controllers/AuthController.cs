using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("token")]
        public ActionResult<string> GetToken()
        {
            string securityKey = "this_is_our_super_long_key_for_token_validation_project_2019_06_28$smesk.in";
            var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var signingCredentials = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            claims.Add(new Claim("Our_Custom_Claim", "Our cutom calue"));
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                //  notBefore:DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(10),
                signingCredentials: signingCredentials,
                claims: claims
                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}