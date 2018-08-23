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
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Domain.Notes
{
    /// <summary>
    /// Сервис для работы с заметками
    /// </summary>
    public class NoteService : INoteService
    {
        readonly NotesContext _context;
        readonly IMapper _mapper;
        readonly IAvatarService _avatarService;
        const int DescriptionMaxLenght = 90;

        public NoteService(NotesContext context, IMapper mapper, IAvatarService avatarService)
        {
            _context = context;
            _mapper = mapper;
            _avatarService = avatarService;
        }

        /// <inheritdoc/>
        public async Task<Guid> Create(NoteInfo model, string userId)
        {
            var note = _mapper.Map<NoteInfo, Note>(model);

            var avatarFileGuid = await _avatarService.SaveAvatar(note.NoteGuid.ToString());

            note.FileGuid = avatarFileGuid;
            note.Description = model.Content.Length <= DescriptionMaxLenght
                ? model.Content
                : model.Content.Substring(0, DescriptionMaxLenght);
            
            note.UserId = userId;

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note.NoteGuid;
        }

        /// <inheritdoc/>
        public async Task Remove(Guid noteGuid)
        {
            var note = await _context.Notes.FirstAsync(n => n.NoteGuid == noteGuid);
            
            _context.Notes.Remove(note);      
            await _context.SaveChangesAsync();
            
            await _avatarService.Remove(note.FileGuid);
        }

        /// <inheritdoc/>
        public async Task<NoteModel> GetNote(Guid noteGuid)
        {
            var note = await _context.Notes.AsNoTracking()
                .FirstAsync(x => x.NoteGuid == noteGuid);
            
            return _mapper.Map<Note, NoteModel>(note);
        }

        /// <inheritdoc/>
        public async Task<List<NoteModel>> GetNotes(string search, NoteCategory? noteCategory, string userId)
        {
            var result = _context.Notes.AsNoTracking().Where(x => x.UserId == userId);

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