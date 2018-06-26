using System;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Users.Models;

namespace AstralNotes.Domain.Notes.Models
{
    public class NoteModel
    {
        public Guid NoteGuid { get; set; }
        
        public string Content { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public NoteCategory Category { get; set; }

        public UserModel User { get; set; }
    }
}