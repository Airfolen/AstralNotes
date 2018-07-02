using System.Threading.Tasks;
using AstralNotes.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AstralNotes.Domain.Users
{
    /// <summary>
    /// Сервис для управления пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        /// <summary>
        /// Получение текущего пользователя
        /// <returns>Модель пользователя</returns>
        /// </summary>
        public async Task<User> GetCurrentUserAsync() => await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
    }
}
