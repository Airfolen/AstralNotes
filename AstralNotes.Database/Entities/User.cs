using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralNotes.Database.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserGuid { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Salt { get; set; }

        [Required]
        [MaxLength(20)]
        public string FullName { get; set; }

        #region Навигационные свойства

        public List<Note> Notes { get; set; }

        #endregion
        
        public User()
        {
            UserGuid = Guid.NewGuid();
        }
    }
}