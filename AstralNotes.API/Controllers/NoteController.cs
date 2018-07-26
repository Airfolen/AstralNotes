using System;
using System.Threading.Tasks;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
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
        [Route("Create")]
        public IActionResult Create()
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
        [HttpGet("Remove/{noteGuid}")]
        [Authorize]
        public async Task<IActionResult> Remove(Guid noteGuid)
        {
            await _noteService.Remove(noteGuid);
            
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// Получение заметки
        /// </summary>
        /// <param name="noteGuid">Идентификатор заметки</param>
        /// <returns>Выходная модель заметки</returns>
        [HttpGet("{noteGuid}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] Guid noteGuid)
        {
            var note = await _noteService.GetNote(noteGuid);
            
            return View(note);
        }
    }
}