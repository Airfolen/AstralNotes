namespace AstralNotes.Utils.Password
{
    public interface IPasswordValidator
    {
        bool Validate(string password);
    }
}