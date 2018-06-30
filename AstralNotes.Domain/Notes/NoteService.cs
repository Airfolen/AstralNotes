using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Database.Entities;
using AstralNotes.Domain.Avatars;
using AstralNotes.Domain.Notes.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Domain.Notes
{
    public class NoteService : INoteService
    {
        private readonly NotesContext _context;
        private readonly IMapper _mapper;
        private readonly IAvatarService _avatarService;

        public NoteService(NotesContext context, IMapper mapper, IAvatarService avatarService)
        {
            _context = context;
            _mapper = mapper;
            _avatarService = avatarService;
        }

        public async Task<Guid> Create(NoteInfo model)
        {
            var note = _mapper.Map<NoteInfo, Note>(model);

            var avatarFileGuid = await _avatarService.SaveAvatar(
                _context.Users.FirstAsync(x => x.UserGuid == model.UserGuid).Result.Gender.ToString(),
                note.NoteGuid.ToString());

            note.FileGuid = avatarFileGuid;
            
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note.NoteGuid;
        }

        public async Task Remove(Guid noteGuid)
        {
            var note = await _context.Notes.FirstAsync(n => n.NoteGuid == noteGuid);
            
            await _avatarService.Remove(note.FileGuid);
            
            _context.Notes.Remove(note);      
            await _context.SaveChangesAsync();
        }

        public async Task<NoteModel> GetNote(Guid noteGuid, Guid userGuid)
        {
            var note = await _context.Notes.AsNoTracking().Include(x => x.User)
                .FirstAsync(x => x.NoteGuid == noteGuid && x.User.UserGuid == userGuid);
            
            return _mapper.Map<Note, NoteModel>(note);
        }

        public async Task<List<NoteShortModel>> GetNotes(string search, Guid userGuid)
        {
            var result = _context.Notes.AsNoTracking().Include(x => x.User)
                .Where(x => x.User.UserGuid == userGuid);

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                result = result.Where(x => x.Content.ToLower().Contains(search));
            }

            return await result.ProjectTo<NoteShortModel>().ToListAsync();
        }
    }
}