using System;
using System.ComponentModel.DataAnnotations;
using AstralNotes.Database.Enums;

namespace AstralNotes.Domain.Notes.Models
{
    public class NoteInfo
    {
        [Required]
        [Display(Name = "Содержимое")]
        public string Content { get; set; }
        
        [Required]
        [Display(Name = "Категория")]
        public NoteCategory Category { get; set; }
    }
}