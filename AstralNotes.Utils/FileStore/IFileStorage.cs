using System.IO;
using System.Threading.Tasks;

namespace AstralNotes.Utils.FileStore
{
    /// <summary>
    /// Интерфейс локального хранилища файлов
    /// </summary>
    public interface IFileStorage
    {
        /// <summary>
        /// Сохранение содержимого файла в локальном хранилище
        /// <param name="content">Cодержимое файла</param>
        /// <param name="fileName">Имя файла</param>
        /// </summary>
        Task SaveAsync(Stream content, string fileName);

        /// <summary>
        /// Получение содержимого файла из локального хранилища
        /// <param name="fileName">Имя файла</param>
        /// <returns>Cодержимое файла</returns>
        /// </summary>
        Task<Stream> Get(string fileName);

        /// <summary>
        /// Удаление содержимого файла из локального хранилища
        /// <param name="fileName">Имя файла</param>
        /// </summary>
        Task Remove(string fileName);
    }
}