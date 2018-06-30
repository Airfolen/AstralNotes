using System;
using AstralNotes.Database.Enums;

namespace AstralNotes.Domain.Users.Models
{
    public class UserModel
    {
        public Guid UserGuid { get; set; }
        
        public string Login { get; set; }
        
        public string FullName { get; set; }
        
        public Gender Gender { get; set; }
    }
}