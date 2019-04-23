using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Database.Entities;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Avatars;
using AstralNotes.Domain.Notes.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Domain.Notes
{
    /// <summary>
    /// Сервис для работы с заметками
    /// </summary>
    public class NoteService : INoteService
    {
        readonly NotesContext _databaseContext;
        readonly IMapper _mapper;
        readonly IAvatarService _avatarService;

        public NoteService(NotesContext databaseContext, IMapper mapper, IAvatarService avatarService)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _avatarService = avatarService;
        }

        /// <inheritdoc/>
        public async Task<Guid> Create(NoteInfo model, string userId)
        {
            var note = _mapper.Map<NoteInfo, Note>(model);

            var avatarFileGuid = await _avatarService.SaveAvatar(note.NoteGuid.ToString());

            note.FileGuid = avatarFileGuid;
            
            note.UserId = userId;

            _databaseContext.Notes.Add(note);
            await _databaseContext.SaveChangesAsync();

            return note.NoteGuid;
        }

        public async Task Update(Guid noteGuid, NoteInfo model, string userId)
        {
            var note = await _databaseContext.Notes.SingleAsync(a => a.NoteGuid == noteGuid);
            _mapper.Map(model, note);
            await _databaseContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task Remove(Guid noteGuid)
        {
            var note = await _databaseContext.Notes.FirstAsync(n => n.NoteGuid == noteGuid);
            
            _databaseContext.Notes.Remove(note);      
            await _databaseContext.SaveChangesAsync();
            
            await _avatarService.Remove(note.FileGuid);
        }

        public async Task<NoteModel> RemoveWithMobile(Guid noteGuid)
        {
            var note = await _databaseContext.Notes.FirstAsync(n => n.NoteGuid == noteGuid);
            
            _databaseContext.Notes.Remove(note);      
            await _databaseContext.SaveChangesAsync();
            
            await _avatarService.Remove(note.FileGuid);
            
            return _mapper.Map<Note, NoteModel>(note);
        }

        /// <inheritdoc/>
        public async Task<NoteModel> GetNote(Guid noteGuid)
        {
            var note = await _databaseContext.Notes.AsNoTracking()
                .FirstAsync(x => x.NoteGuid == noteGuid);
            
            return _mapper.Map<Note, NoteModel>(note);
        }

        /// <inheritdoc/>
        public async Task<List<NoteModel>> GetNotes(string search, NoteCategory? noteCategory, string userId)
        {
            var result = _databaseContext.Notes.AsNoTracking().Where(x => x.UserId == userId);

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                result = result.Where(x => x.Content.ToLower().Contains(search));
            }
            
            if (noteCategory != null)
            {
                result = result.Where(note => note.Category == noteCategory);
            }
            
            return await result.ProjectTo<NoteModel>().ToListAsync();
        }
    }
}