using System.IO;
using System.Threading.Tasks;

namespace AstralNotes.Utils.DiceBearAvatars
{
    /// <summary>
    /// Интерфейс провайдера для работы с DiceBear Avatars
    /// </summary>
    public interface IAvatarProvider
    {
        /// <summary>
        /// Запрос на создание
        /// <returns></returns>
        Task<Stream> GetAsync(string gender, string seed);
    }
}