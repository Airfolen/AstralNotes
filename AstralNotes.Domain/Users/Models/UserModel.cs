using System;

namespace AstralNotes.Domain.Users.Models
{
    public class UserModel
    {
        public Guid UserGuid { get; set; }
        
        public string Login { get; set; }
        
        public string FullName { get; set; }
    }
}