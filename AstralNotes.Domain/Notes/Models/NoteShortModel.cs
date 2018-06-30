using System;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Avatars.Models;

namespace AstralNotes.Domain.Notes.Models
{
    public class NoteShortModel
    {
        public Guid NoteGuid { get; set; }
        
        public string Content { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public Guid FileGuid { get; set; }
        
        public NoteCategory Category { get; set; }
    }
}