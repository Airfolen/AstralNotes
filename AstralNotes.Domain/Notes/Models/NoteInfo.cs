using System.ComponentModel.DataAnnotations;
using AstralNotes.Database.Enums;

namespace AstralNotes.Domain.Notes.Models
{
    /// <summary>
    /// Входная модель заметки
    /// </summary>
    public class NoteInfo
    {
        [Required]
        [Display(Name = "Содержимое")]
        public string Content { get; set; }
        
        [Display(Name = "Заголовок")]
        [MaxLength(40)]
        public string Title { get; set; }
        
        [Required]
        [Display(Name = "Категория")]
        public NoteCategory Category { get; set; }
    }
}