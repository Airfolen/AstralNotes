using AstralNotes.Database.Enums;

namespace AstralNotes.Domain.Users.Models
{
    public class UserInfo
    {  
        public string FullName { get; set; }
        
        public Gender Gender { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}