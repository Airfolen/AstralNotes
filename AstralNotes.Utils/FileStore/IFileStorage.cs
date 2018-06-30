using System.IO;
using System.Threading.Tasks;

namespace AstralNotes.Utils.FileStore
{
    public interface IFileStorage
    {
        Task SaveAsync(Stream content, string fileName);

        Task<Stream> Get(string fileName);

        Task Remove(string fileName);
    }
}