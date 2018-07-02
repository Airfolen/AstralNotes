using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralNotes.Domain.Notes.Models;

namespace AstralNotes.Domain.Notes
{
    /// <summary>
    /// Интерфейс сервиса для работы с заметками
    /// </summary>
    public interface INoteService
    {
        Task<Guid> Create(NoteInfo model, string userId);
        
        Task Remove(Guid noteGuid);
        
        Task<NoteModel> GetNote(Guid noteGuid, string userId);

        Task<List<NoteModel>> GetNotes(string search, string userId);
    }
}