using System.IO;
using System.Threading.Tasks;

namespace AstralNotes.Utils.FileStore
{
    /// <summary>
    /// Локальное хранилище файлов
    /// </summary>
    public class LocalFileStorage : IFileStorage
    {
        readonly LocalFileStorageСharacteristics _сharacteristics;

        public LocalFileStorage(LocalFileStorageСharacteristics сharacteristics)
        {
            _сharacteristics = сharacteristics;
            Directory.CreateDirectory(сharacteristics.RootPath);
        }
       
        /// <inheritdoc/>
        public async Task SaveAsync(Stream content, string fileName)
        {
            if (content != null && fileName != null)
            {
                using (var fileStream = new FileStream(Path.Combine(_сharacteristics.RootPath, fileName), FileMode.Create))
                {
                    await content.CopyToAsync(fileStream);
                }
            }
        }

        /// <inheritdoc/>
        public async Task<Stream> Get(string fileName)
        {
            return new FileStream(Path.Combine(_сharacteristics.RootPath, fileName), FileMode.Open);
        }

        /// <inheritdoc/>
        public async Task Remove(string fileName)
        {
            File.Delete(Path.Combine(_сharacteristics.RootPath, fileName));
        }
    }
}