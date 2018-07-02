using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AstralNotes.Database.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }
    }
}