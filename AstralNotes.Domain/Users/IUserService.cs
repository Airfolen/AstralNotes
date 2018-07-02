using System.Threading.Tasks;
using AstralNotes.Database.Entities;

namespace AstralNotes.Domain.Users
{
    /// <summary>
    /// Интерфейс сервиса для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        Task<User> GetCurrentUserAsync();
    }
}