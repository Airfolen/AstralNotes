namespace AstralNotes.Utils.Password
{
    public interface IPasswordHashProvider
    {
        string Hash(string input);
    }
}