using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FullStackApi.Repositories
{
    public class TokenRepo: ITokenRepo
    {
        private readonly IConfiguration _configuration;
        public TokenRepo(IConfiguration configuration) { 
        _configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles) {

                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var cridentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
