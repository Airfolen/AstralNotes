using System.Collections.Generic;
using System.Threading.Tasks;
using AstralNotes.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Database
{
    public class DataInitializer : IDataInitializer
    {
        private readonly NotesContext _context;
        private readonly UserManager<User> _userManager;

       public DataInitializer(NotesContext context,  UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Инициализация данных
        /// </summary>
        public async Task Initialize()
        {
            await SeedUsers();

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавление пользователей
        /// </summary>
        private async Task SeedUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    FullName = "Peter Petrov",
                    UserName = "User1"
                },
                new User
                {
                    FullName = "Ivan Ivanov",
                    UserName = "User2"
                },
                new User
                {
                    FullName = "David Davidich",
                    UserName = "User3"
                }
            };
            
            foreach (var user in users)
            {
                if (!await _context.Users.AnyAsync(a => a.UserName == user.UserName))
                    await _userManager.CreateAsync(user, user.UserName);
            }
        }
    }
}