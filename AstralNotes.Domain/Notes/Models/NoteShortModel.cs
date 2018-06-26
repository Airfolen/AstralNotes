using System;
using AstralNotes.Database.Enums;

namespace AstralNotes.Domain.Notes.Models
{
    public class NoteShortModel
    {
        public Guid NoteGuid { get; set; }
        
        public string Content { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public NoteCategory Category { get; set; }
    }
}