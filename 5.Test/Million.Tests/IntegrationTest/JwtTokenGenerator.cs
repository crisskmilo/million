using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtTokenGenerator
{
    public static string GenerateToken(string secretKey, string username)
    {
        var key = Encoding.UTF8.GetBytes(secretKey);

        var claims = new[]
        {
            new Claim("unique_name", username),
            new Claim("nameid", "1"),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
