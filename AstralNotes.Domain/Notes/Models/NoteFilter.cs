namespace AstralNotes.Domain.Notes.Models
{
    public class NoteFilter
    {
        public string Search { get; }
        
        public NoteFilter(string search)
        {
            Search = search;
        }
    }
}