using System.Collections.Generic;
using AstralNotes.Domain.Notes.Models;

namespace AstralNotes.API.ViewModels
{
    /// <summary>
    /// Модель для представления Home
    /// </summary>
    public class HomeView
    {
        public NoteFilter NoteFilter { get; set; }
        public IEnumerable<NoteModel> NoteModels { get; set; }
    }
}