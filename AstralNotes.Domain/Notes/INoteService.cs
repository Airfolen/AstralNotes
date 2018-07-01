﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralNotes.Domain.Notes.Models;

namespace AstralNotes.Domain.Notes
{
    public interface INoteService
    {
        Task<Guid> Create(NoteInfo model, string userId);
        
        Task Remove(Guid noteGuid);
        
        Task<NoteModel> GetNote(Guid noteGuid, string userid);

        Task<List<NoteShortModel>> GetNotes(string search, string userid);
    }
}