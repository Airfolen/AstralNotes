using AstralNotes.Domain.Users.Models;
using AstralNotes.Utils.Tokens.Models;

namespace AstralNotes.Domain.Authentication.Models
{
    public class AuthenticationResult
    {
        public Token Token { get; set; }

        public UserModel User { get; set; }
    }
}
