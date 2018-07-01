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
        private readonly int _descriptionMaxLenght = 90;

        public NoteService(NotesContext context, IMapper mapper, IAvatarService avatarService)
        {
            _context = context;
            _mapper = mapper;
            _avatarService = avatarService;
        }

        public async Task<Guid> Create(NoteInfo model, string userId)
        {
            var note = _mapper.Map<NoteInfo, Note>(model);

            var avatarFileGuid = await _avatarService.SaveAvatar(note.NoteGuid.ToString());

            note.FileGuid = avatarFileGuid;
            note.Description = model.Content.Length <= _descriptionMaxLenght
                ? model.Content
                : model.Content.Substring(0, _descriptionMaxLenght);
            
            note.UserId = userId;

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note.NoteGuid;
        }

        public async Task Remove(Guid noteGuid)
        {
            var note = await _context.Notes.FirstAsync(n => n.NoteGuid == noteGuid);
            
            _context.Notes.Remove(note);      
            await _context.SaveChangesAsync();
            
            await _avatarService.Remove(note.FileGuid);
        }

        public async Task<NoteModel> GetNote(Guid noteGuid, string userid)
        {
            var note = await _context.Notes.AsNoTracking()
                .FirstAsync(x => x.NoteGuid == noteGuid && x.UserId == userid);
            
            return _mapper.Map<Note, NoteModel>(note);
        }

        public async Task<List<NoteModel>> GetNotes(string search, string userid)
        {
            var result = _context.Notes.AsNoTracking().Where(x => x.UserId == userid);

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                result = result.Where(x => x.Content.ToLower().Contains(search));
            }

            return await result.ProjectTo<NoteModel>().ToListAsync();
        }
    }
}