using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Database.Entities;
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

        public NoteService(NotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Create(NoteInfo model)
        {
            var note = _mapper.Map<NoteInfo, Note>(model);
            
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note.NoteGuid;
        }

        public async Task Remove(Guid noteGuid)
        {
            var student = await _context.Notes.FirstAsync(n => n.NoteGuid == noteGuid);
            
            _context.Notes.Remove(student);      
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
            var result = _context.Notes.AsNoTracking().Include(x => x.User).Where(x => x.User.UserGuid == userGuid);

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                result = result.Where(x => x.Content.ToLower().Contains(search));
            }

            return await result.ProjectTo<NoteShortModel>().ToListAsync();
        }
    }
}