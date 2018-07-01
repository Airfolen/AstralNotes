using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using AstralNotes.API.Migrations;
using AstralNotes.Database.Entities;
using AstralNotes.Domain.Avatars;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;

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
        /// Получение заметок
        /// </summary>
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userService.GetCurrentUserAsync();

                var notes = await _noteService.GetNotes(null, user.Result.Id);
                
                return View(notes);
            }

            return View();
        }
    }
}