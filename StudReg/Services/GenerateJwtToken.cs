using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StudReg.Dtos;

public class JwtTokenGenerator
{
    public static string GenerateJwtToken(Guid userId, string username, ICollection<RoleDto> role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ggdghsdghghsdgvyvdsyvvvvvvvvvvvvvyygvdsytvdsydvsgsdvdysvdsyvchvchdvcdycudbdvdusbdcus")); // 🔐 Secret key
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512); // 🔑 Signing credentials

        // 🏷️ Create claims (user info stored in token)
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), 
            new Claim(JwtRegisteredClaimNames.Email, username),
            // new Claim(ClaimTypes.Role, ),
        };

        foreach (var item in role)
        {
            claims.Add(new Claim(ClaimTypes.Role, item.Name));
        }

        var token = new JwtSecurityToken(
            issuer: "your-app",  // 🔸 Token issuer
            audience: "your-audience", // 🔹 Token audience
            claims: claims, 
            expires: DateTime.UtcNow.AddHours(2), // ⏳ Token expiration
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token); // ✍️ Generate the token string
    }
}
