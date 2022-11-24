using Microsoft.IdentityModel.Tokens;
using Project_PetShop.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_PetShop.Auth.AuthRepository
{
    public class TokenService : ITokenService
    {
        public string GetToken(string key, string issuer, string audience, Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.userName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var sign = new SigningCredentials(skey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: sign);

            var tHandler = new JwtSecurityTokenHandler();
            var sToken = tHandler.WriteToken(token);
            return sToken;
        }
    }
}
