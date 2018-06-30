using System.ComponentModel.DataAnnotations;
using AstralNotes.Database.Enums;

namespace AstralNotes.Domain.Users.Models
{
    public class UserInfo
    {  
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
        
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
    }
}