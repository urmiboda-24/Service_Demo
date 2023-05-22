using Microsoft.IdentityModel.Tokens;
using Service_Demo.Entites.Auth;
using Service_Demo.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Service_Demo.Auth
{
    public class JwtTokenHelper
    {
        public static string GenerateToken(JwtSetting jwtSetting, SessionDetailsViewModel session)
        {
            if (jwtSetting == null)
                return string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {

                new Claim(ClaimTypes.Name, session.FullName),

                new Claim(ClaimTypes.NameIdentifier, session.FullName),
                new Claim(ClaimTypes.Role, session.Role),

                new Claim("CustomClaimForUser", JsonSerializer.Serialize(session))
                }; // Additional Claims

            var token = new JwtSecurityToken(

            jwtSetting.Issuer,

            jwtSetting.Audience,

            claims,

            expires: DateTime.UtcNow.AddMinutes(120),

            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
