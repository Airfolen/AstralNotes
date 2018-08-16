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
        private readonly INoteService _noteService;
        private readonly IUserService _userService;

        public HomeController(INoteService noteService, IUserService userService)
        {
            _noteService = noteService;
            _userService = userService;
        }
        
        /// <summary>
        /// Получение заметок на главной странице
        /// <param name="search">Параметр для поиска по содержимому заметок</param>
        /// <param name="category">Параметр для фильтрации заметок</param>
        /// </summary>
        public async Task<IActionResult> Index(string search, string category = "Все")
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetCurrentUserAsync();
                var notes = await _noteService.GetNotes(search, user.Id);

                notes = SortByCategory(notes, category);
                
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

                var notesReport = new NotesReport(notes);
                var bytes = notesReport.GetPdfReport();

                return File(bytes, "application/pdf", "Notes.pdf");
            }

            return null;
        }
        
        private List<NoteModel> SortByCategory(List<NoteModel> notes, string sortNoteBy)
        {
            switch (sortNoteBy)
            {
                case "Общая":
                {
                    notes = notes.Where(note => note.Category == NoteCategory.General).ToList();
                    break;
                }
                case "Ежедневник":
                {
                    notes = notes.Where(note => note.Category == NoteCategory.Diary).ToList();
                    break;
                }
                case "Работа":
                {
                    notes = notes.Where(note => note.Category == NoteCategory.Work).ToList();
                    break;
                }
            }
            
            return notes;
        }
    }
}