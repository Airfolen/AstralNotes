using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AstralNotes.Utils.JwtOptions;
using AstralNotes.Utils.Tokens.Models;
using Microsoft.IdentityModel.Tokens;

namespace AstralNotes.Utils.Tokens
{
    public class JwtTokenService : IJwtTokenService
    {
        public Token CreateToken(ClaimsIdentity identity)
        {
            var token = new JwtSecurityToken(
                issuer: AuthenticationOptions.Issuer,
                audience: AuthenticationOptions.Audience,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(AuthenticationOptions.LifeTime)),
                signingCredentials: new SigningCredentials(AuthenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            return new Token()
            {
                Value = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = DateTime.UtcNow.Add(TimeSpan.FromDays(AuthenticationOptions.LifeTime))
            };
        }
    }
}