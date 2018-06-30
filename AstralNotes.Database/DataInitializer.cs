//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AstralNotes.Database.Entities;
//using AstralNotes.Database.Enums;
//using AstralNotes.Utils.Password;
//using Microsoft.EntityFrameworkCore;
//
//namespace AstralNotes.Database
//{
//    public class DataInitializer : IDataInitializer
//    {
//        private readonly NotesContext _context;
//        private readonly IPasswordHasher _passwordHasher;
//
//        /// <inheritdoc />
//        public DataInitializer(NotesContext context, IPasswordHasher passwordHasher)
//        {
//            _context = context;
//            _passwordHasher = passwordHasher;
//        }
//
//        /// <summary>
//        /// Инициализация данных
//        /// </summary>
//        public async Task Initialize()
//        {
//            await SeedUsers();
//
//            await _context.SaveChangesAsync();
//        }
//
//        /// <summary>
//        /// Добавление пользователей
//        /// </summary>
//        private async Task SeedUsers()
//        {
//            var userSalt = "supersalt";
//            var users = new List<User>
//            {
//                new User
//                {
//                    FullName = "Peter Petrov",
//                    Login = "User1",
//                    Salt = userSalt,
//                    PasswordHash = _passwordHasher.Hash("User1" + userSalt),
//                    Gender = Gender.Male
//                },
//                new User
//                {
//                    FullName = "Ivan Ivanov",
//                    Login = "User2",
//                    Salt = userSalt,
//                    PasswordHash = _passwordHasher.Hash("User2" + userSalt),
//                    Gender = Gender.Male
//                },
//                new User
//                {
//                    FullName = "David Davidich",
//                    Login = "User3",
//                    Salt = userSalt,
//                    PasswordHash = _passwordHasher.Hash("User3" + userSalt),
//                    Gender = Gender.Female
//                }
//            };
//            
//            foreach (var user in users)
//            {
//                if (!await _context.Users.AnyAsync(a => a.Login == user.Login))
//                    _context.Users.Add(user);
//            }
//        }
//    }
//}