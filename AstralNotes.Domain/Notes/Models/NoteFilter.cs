using AstralNotes.Database.Enums;

namespace AstralNotes.Domain.Notes.Models
{
    /// <summary>
    /// Модель с параметрами для фильтрации заметок
    /// </summary>
    public class NoteFilter
    {
        public string Search { get; }
        public string Category { get; }
        
        public NoteFilter(string search, string category)
        {
            Search = search;
            Category = category;
        }
    }
}