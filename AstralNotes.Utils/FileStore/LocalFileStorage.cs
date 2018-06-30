using System.IO;
using System.Threading.Tasks;

namespace AstralNotes.Utils.FileStore
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly LocalFileStorageOptions _options;

        public LocalFileStorage(LocalFileStorageOptions options)
        {
            _options = options;
            Directory.CreateDirectory(options.RootPath);
        }
        
        public async Task SaveAsync(Stream content, string fileName)
        {
            if (content != null && fileName != null)
            {
                using (var fileStream = new FileStream(Path.Combine(_options.RootPath, fileName), FileMode.Create))
                {
                    await content.CopyToAsync(fileStream);
                }
            }
        }

        public async Task<Stream> Get(string fileName)
        {
            return new FileStream(Path.Combine(_options.RootPath, fileName), FileMode.Open);
        }

        public async Task Remove(string fileName)
        {
            File.Delete(Path.Combine(_options.RootPath, fileName));
        }
    }
}