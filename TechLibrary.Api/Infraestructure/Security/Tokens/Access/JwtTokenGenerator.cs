using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infraestructure.Security.Tokens.Access;

public class JwtTokenGenerator
{
    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        };
        
        var tokenDescription = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = new ClaimsIdentity(),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescription);
        
        return tokenHandler.WriteToken(securityToken);
    }

    private static SymmetricSecurityKey SecurityKey()
    {
        var signingKey = "91zn3tk2iAZ9Z260H89UcRSvoGuULkyV";

        var symmetricKey = Encoding.UTF8.GetBytes(signingKey);
        
        return new SymmetricSecurityKey(symmetricKey);
    }
}