using System.Threading.Tasks;
using AstralNotes.Database.Entities;

namespace AstralNotes.Domain.Users
{
    /// <summary>
    /// Интерфейс сервиса для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получение текущего пользователя
        /// <returns>Модель пользователя</returns>
        /// </summary>
        Task<User> GetCurrentUserAsync();
    }
}