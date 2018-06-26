using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Notes.Models;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// API для работы с заметками
    /// </summary>
    [Route("Notes")]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        
        /// <summary>
        /// Создание заметки
        /// </summary>
        /// <param name="model">Входная модель пользователя</param>
        [HttpPost]
        //   [Authorize(Roles="Администратор")]
        public async Task<Guid> Create([FromBody]NoteInfo model)
        {
            return await _noteService.Create(model);
        }

        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="noteGuid">Идентификатор заметки</param>
        [HttpDelete("{noteGuid}")]
        //   [Authorize(Roles="Администратор")]
        public async Task Delete([FromRoute] Guid noteGuid)
        {
            await _noteService.Remove(noteGuid);
        }
    
        /// <summary>
        /// Получение заметки
        /// </summary>
        /// <param name="noteGuid">Идентификатор заметки</param>
        /// <param name="userGuid">Идентификатор пользователя</param>
        /// <returns>Выходная модель заметки</returns>
        [HttpGet("{noteGuid}/User/{userGuid}")]
        //  [Authorize(Roles="Администратор")]
        public async Task<NoteModel> GetUser([FromRoute] Guid noteGuid, [FromRoute] Guid userGuid)
        {
            return await _noteService.GetNote(noteGuid, userGuid);
        }

        /// <summary>
        /// Получение заметок
        /// </summary>
        /// <param name="search">Строка поиска по полю : Content</param>
        /// <param name="userGuid">Идентификатор пользователя</param>
        /// <returns>Коллекция объектов отсортированная по полю : CreationDate</returns>
        [HttpGet("User/{userGuid}")]
        //[Authorize(Roles="Администратор")]
        public async Task<List<NoteShortModel>> GetUsers([FromQuery] string search, [FromRoute] Guid userGuid)
        {
            var result = _noteService.GetNotes(search, userGuid);
            
            return  result.Result.OrderBy(x => x.CreationDate).ToList();
        }
    }
}