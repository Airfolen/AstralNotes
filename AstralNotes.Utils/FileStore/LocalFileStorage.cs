using System.IO;
using System.Threading.Tasks;

namespace AstralNotes.Utils.FileStore
{
    /// <summary>
    /// Локальное хранилище файлов
    /// </summary>
    public class LocalFileStorage : IFileStorage
    {
        private readonly LocalFileStorageСharacteristics _сharacteristics;

        public LocalFileStorage(LocalFileStorageСharacteristics сharacteristics)
        {
            _сharacteristics = сharacteristics;
            Directory.CreateDirectory(сharacteristics.RootPath);
        }
        
        /// <summary>
        /// Сохранение содержимого файла в локальном хранилище
        /// <param name="content">Cодержимое файла</param>
        /// <param name="fileName">Имя файла</param>
        /// </summary>
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

        /// <summary>
        /// Получение содержимого файла из локального хранилища
        /// <param name="fileName">Имя файла</param>
        /// <returns>Cодержимое файла</returns>
        /// </summary>
        public async Task<Stream> Get(string fileName)
        {
            return new FileStream(Path.Combine(_сharacteristics.RootPath, fileName), FileMode.Open);
        }

        /// <summary>
        /// Удаление содержимого файла из локального хранилища
        /// <param name="fileName">Имя файла</param>
        /// </summary>
        public async Task Remove(string fileName)
        {
            File.Delete(Path.Combine(_сharacteristics.RootPath, fileName));
        }
    }
}