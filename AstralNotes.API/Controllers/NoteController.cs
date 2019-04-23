using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Domain.Users;
using iTextSharp.text;
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
        readonly INoteService _noteService;
        readonly IUserService _userService;

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
        
        /// <summary>
        /// Получение заметок
        /// </summary>
        /// <returns>Выходная модель заметки</returns>
        [HttpGet("GetNotes")]
        [AllowAnonymous]
        public async Task<List<NoteModel>> GetNotes()
        {
            var user = await _userService.GetCurrentUserAsync();
            var notes = await _noteService.GetNotes(string.Empty, NoteCategory.General, "ec435307-7707-4710-a872-f20d63af350e");
            
            return notes;
        }
        
        /// <summary>
        /// Создание заметки, используя телефон
        /// </summary>
        /// <param name="model">Входная модель заметки</param>
        [HttpPost]
        [Route("CreateWithMobile")]
        public async Task<NoteModel> CreateWithMobile([FromBody] NoteInfo model)
        {
            var noteGuid = await _noteService.Create(model, "ec435307-7707-4710-a872-f20d63af350e");
            
            return new NoteModel
            {
                NoteGuid =  noteGuid,
                Title = model.Title,
                Content = model.Content,
                Category = model.Category,
                CreationDate = DateTime.Now
            };
        }


        /// <summary>
        /// Обновление заметки, используя телефон
        /// </summary>
        /// <param name="noteGuid">Индетификатор заметки</param>
        /// <param name="model">Входная модель заметки</param>
        [HttpPut]
        [Route("UpdateWithMobile/{noteGuid}")]
        public async Task<NoteModel> UpdateWithMobile([FromRoute] Guid noteGuid, [FromBody] NoteInfo model)
        {
            await _noteService.Update(noteGuid, model, "ec435307-7707-4710-a872-f20d63af350e");
            
            return new NoteModel
            {
                NoteGuid =  noteGuid,
                Title = model.Title,
                Content = model.Content,
                Category = model.Category,
                CreationDate = DateTime.Now
            };
        }
        
        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="noteGuid">Идентификатор заметки</param>
        [HttpDelete("RemoveWithMobile/{noteGuid}")]
        public async Task<NoteModel> RemoveWithMobile(Guid noteGuid)
        {
            return await _noteService.RemoveWithMobile(noteGuid);
        }
    }
}