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
    /// <summary>
    /// Сервис для работы с заметками
    /// </summary>
    public class NoteService : INoteService
    {
        private readonly NotesContext _context;
        private readonly IMapper _mapper;
        private readonly IAvatarService _avatarService;
        private const int DescriptionMaxLenght = 90;

        public NoteService(NotesContext context, IMapper mapper, IAvatarService avatarService)
        {
            _context = context;
            _mapper = mapper;
            _avatarService = avatarService;
        }

        /// <summary>
        /// Создание заметки
        /// <param name="model">Входная модель заметки</param>
        /// <param name="userId">Индетификатор пользователя</param>
        /// <returns>Индетификатор заметки</returns>
        /// </summary>
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

        /// <summary>
        /// Удаление заметки
        /// <param name="noteGuid">Индетификатор заметки</param>
        /// </summary>
        public async Task Remove(Guid noteGuid)
        {
            var note = await _context.Notes.FirstAsync(n => n.NoteGuid == noteGuid);
            
            _context.Notes.Remove(note);      
            await _context.SaveChangesAsync();
            
            await _avatarService.Remove(note.FileGuid);
        }

        /// <summary>
        /// Получение заметки для конкретного  пользователя
        /// <param name="noteGuid">Индетификатор заметки</param>
        /// <param name="userId">Индетификатор пользователя</param>
        /// <returns>Выходная модель заметки</returns>
        /// </summary>
        public async Task<NoteModel> GetNote(Guid noteGuid)
        {
            var note = await _context.Notes.AsNoTracking()
                .FirstAsync(x => x.NoteGuid == noteGuid);
            
            return _mapper.Map<Note, NoteModel>(note);
        }

        /// <summary>
        /// Получение всех заметок для конкретного  пользователя
        /// <param name="search">Строка для поиска по содержимому заметки</param>
        /// <param name="userId">Индетификатор пользователя</param>
        /// <returns>Список выходных моделей заметок</returns>
        /// </summary>
        public async Task<List<NoteModel>> GetNotes(string search, string userId)
        {
            var result = _context.Notes.AsNoTracking().Where(x => x.UserId == userId);

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                result = result.Where(x => x.Content.ToLower().Contains(search));
            }

            return await result.ProjectTo<NoteModel>().ToListAsync();
        }
    }
}