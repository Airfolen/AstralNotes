using System.Linq;
using System.Threading.Tasks;
using AstralNotes.API.ViewModels;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
    /// <summary>
    /// Редирект на главную сраницу
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
        public async Task<IActionResult> Index(string search, NoteCategory category)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetCurrentUserAsync();

                var notes = await _noteService.GetNotes(search, user.Id);

                notes = notes.Where(note => note.Category == category).ToList();
                
                var viewModel = new HomeView
                {
                    NoteModels = notes,
                    NoteFilter = new NoteFilter(search, category)
                };
                
                return View(viewModel);
            }

            return View();
        }
    }
}