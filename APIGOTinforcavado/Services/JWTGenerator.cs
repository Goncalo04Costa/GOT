using Microsoft.IdentityModel.Tokens;
using Shared.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGOTinforcavado.Services
{
    public class JwtGenerator
    {
        private readonly string _key;

        public JwtGenerator(IConfiguration configuration)
        {
            _key = configuration["Jwt:SecretKey"];
        }

        public string GenerateJwtToken(Utilizador utilizador)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, utilizador.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
