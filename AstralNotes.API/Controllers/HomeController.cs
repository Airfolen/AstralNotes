using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.API.ViewModels;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Domain.Reports;
using AstralNotes.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
    /// <summary>
    /// API для взаимодействия с пользователем на главной странице
    /// </summary>
    public class HomeController : Controller
    {
        readonly INoteService _noteService;
        readonly IUserService _userService;
        readonly INoteConverterService _noteConverterService;

        public HomeController(INoteService noteService, IUserService userService, INoteConverterService noteConverterService)
        {
            _noteService = noteService;
            _userService = userService;
            _noteConverterService = noteConverterService;
        }
        
        /// <summary>
        /// Получение заметок на главной странице
        /// <param name="search">Параметр для поиска по содержимому заметок</param>
        /// <param name="category">Параметр для фильтрации заметок</param>
        /// </summary>
        public async Task<IActionResult> Index(string search, string category)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetCurrentUserAsync();
                var notes = await _noteService.GetNotes(search, user.Id);

                if(Enum.TryParse(category, out NoteCategory noteCategory))
                {
                    notes = notes.Where(note => note.Category == noteCategory).ToList();
                }
                
                var viewModel = new HomeView
                {
                    NoteModels = notes,
                    NoteFilter = new NoteFilter(search, category)
                };
                
                return View(viewModel);
            }

            return RedirectToAction("Login", "Account");
        }
        
        /// <summary>
        /// Получение заметок в pdf
        /// </summary>
        /// <returns>Бинарный файл</returns>
        [HttpGet("Report")]
        public async Task<FileResult> GetPdfReport()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetCurrentUserAsync();
                var notes = await _noteService.GetNotes(null, user.Id);

                var bytes = await _noteConverterService.GetPdfDocumentAsync(notes);

                return File(bytes, "application/pdf", "Notes.pdf");
            }

            return null;
        }
    }
}