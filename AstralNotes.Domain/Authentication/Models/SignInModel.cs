namespace AstralNotes.Domain.Authentication.Models
{
    /// <summary>
    /// Модель аутентификации
    /// </summary>
    public class SignInModel
    {
        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}
