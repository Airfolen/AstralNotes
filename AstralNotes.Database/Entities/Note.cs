using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AstralNotes.Database.Enums;

namespace AstralNotes.Database.Entities
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid NoteGuid { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        public string Title { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public NoteCategory Category { get; set; }
        
        [ForeignKey("File")]
        public Guid FileGuid { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get;set; }        
       
        
        #region Навигационные свойства
    
        public virtual User User{ get; set; }
        
        public virtual File File { get; set; }
        
        #endregion
        
        public Note()
        {
            NoteGuid  = Guid.NewGuid();    
            CreationDate = DateTime.UtcNow;
        }
    }
}