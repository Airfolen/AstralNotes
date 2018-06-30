using System.ComponentModel.DataAnnotations;

namespace AstralNotes.Domain.Authentication.Models
{
    /// <summary>
    /// Модель аутентификации
    /// </summary>
    public class SignInModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
         
        public string ReturnUrl { get; set; }
    }
}
