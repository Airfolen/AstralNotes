using System;
using AstralNotes.Database.Enums;

namespace AstralNotes.Domain.Notes.Models
{
    public class NoteInfo
    {
        public string Content { get; set; }
        
        public NoteCategory Category { get; set; }
        
        public Guid UserGuid { get; set; }
    }
}