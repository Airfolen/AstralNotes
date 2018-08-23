using System;
using System.Threading.Tasks;
using AstralNotes.Domain.Avatars.Models;

namespace AstralNotes.Domain.Avatars
{
    /// <summary>
    /// Интерфейс для работы с DiceBear аватарами
    /// </summary>
    public interface IAvatarService
    {
        /// <summary>
        /// Сохранение аватара
        /// <returns>Индетификатор аватара</returns>
        /// </summary>
        Task<Guid> SaveAvatar(string seed);
        
        /// <summary>
        /// Получение аватара
        /// <param name="avatarGuid">Индетификатор аватара</param>
        /// <returns>Модель аватара</returns>
        /// </summary>
        Task<AvatarModel> GetAvatar(Guid avatarGuid);
        
        /// <summary>
        /// Удаление аватара
        /// <param name="avatarGuid">Индетификатор аватра</param>
        /// <returns>Индетификатор аватра</returns>
        /// </summary>
        Task Remove(Guid avatarGuid);
    }
}