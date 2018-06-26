namespace AstralNotes.Utils.Password
{
    public interface IPasswordHasher
    {
        string Hash(string password); 
        bool VerifyHashedPassword(string pass1, string pass2);
    }
}