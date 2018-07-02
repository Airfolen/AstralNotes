using System.ComponentModel.DataAnnotations;

namespace AstralNotes.Domain.Users.Models
{
    /// <summary>
    /// Входная модель пользователя
    /// </summary>
    public class UserInfo
    {  
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }
 
        [Required]
        [Display(Name = "Имя пользователя")]
        public string FullName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}