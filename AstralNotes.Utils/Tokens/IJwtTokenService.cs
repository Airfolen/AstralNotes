using System.Security.Claims;
using AstralNotes.Utils.Tokens.Models;

namespace AstralNotes.Utils.Tokens
{
    public interface IJwtTokenService
    {
        Token CreateToken(ClaimsIdentity identity);
    }
}