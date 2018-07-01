using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// API для работы с заметками
    /// </summary>
    [Route("Note")]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        private readonly IUserService _userService;

        public NoteController(INoteService noteService, IUserService userService)
        {
            _noteService = noteService;
            _userService = userService;
        }
        
        /// <summary>
        /// Создание заметки
        /// </summary>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("Create")]
        public  IActionResult Create()
        {
            return View();
        }
        
        /// <summary>
        /// Создание заметки
        /// </summary>
        /// <param name="model">Входная модель заметки</param>
        [HttpPost]
        [Authorize]
        [Route("Create")]
        public async Task<IActionResult> Create(NoteInfo model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetCurrentUserAsync();
                await _noteService.Create(model, user.Id);
                
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="noteGuid">Идентификатор заметки</param>
        [HttpPost("Delete/{noteGuid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid noteGuid)
        {
            await _noteService.Remove(noteGuid);
            
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// Получение заметки
        /// </summary>
        [HttpGet]
        [HttpGet("{noteGuid}")]
        public async Task<IActionResult> GetNote()
        {
            return View();
        }
        
        /// <summary>
        /// Получение заметки
        /// </summary>
        /// <param name="noteGuid">Идентификатор заметки</param>
        /// <returns>Выходная модель заметки</returns>
        [HttpPost("{noteGuid}")]
        [Authorize]
        public async Task<IActionResult> GetNote([FromRoute] Guid noteGuid)
        {
            var user = await _userService.GetCurrentUserAsync();
            
            var note = await _noteService.GetNote(noteGuid, user.Id);
            return View(note);
        }

        /// <summary>
        /// Получение заметок
        /// </summary>
        /// <param name="search">Строка поиска по полю : Content</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Коллекция объектов отсортированная по полю : CreationDate</returns>
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<List<NoteShortModel>> GetNotes([FromQuery] string search, [FromRoute] string userId)
        {
            var result = _noteService.GetNotes(search, userId);
            
            return  result.Result.OrderBy(x => x.CreationDate).ToList();
        }
    }
}