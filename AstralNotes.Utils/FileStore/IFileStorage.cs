using System.IO;
using System.Threading.Tasks;

namespace AstralNotes.Utils.FileStore
{
    /// <summary>
    /// Интерфейс локального хранилища файлов
    /// </summary>
    public interface IFileStorage
    {
        Task SaveAsync(Stream content, string fileName);

        Task<Stream> Get(string fileName);

        Task Remove(string fileName);
    }
}