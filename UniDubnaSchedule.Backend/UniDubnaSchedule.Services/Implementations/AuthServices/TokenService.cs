using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.Services.Implementations.AuthServices;

public static class TokenService
{
    public static string CreateToken(User user, IConfiguration configuration)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var securityKey = configuration.GetSection("JWT:SecurityKey").Value;
        var lifeTimeInDays = configuration.GetSection("JWT:LifeTimeInDays").Value;

        if (securityKey is null || lifeTimeInDays is null)
            throw new Exception("Security key or lifetime not configured in appsettings.json");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: cred,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(int.Parse(lifeTimeInDays))
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}