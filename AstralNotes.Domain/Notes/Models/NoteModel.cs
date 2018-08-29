using System;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Avatars.Models;
using AstralNotes.Domain.Users.Models;

namespace AstralNotes.Domain.Notes.Models
{
    /// <summary>
    /// Выходная модель заметки
    /// </summary>
    public class NoteModel
    {
        public Guid NoteGuid { get; set; }
        
        public string Content { get; set; }
        
        public string Title { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public NoteCategory Category { get; set; }
        
        public Guid FileGuid { get; set; }
    }
}