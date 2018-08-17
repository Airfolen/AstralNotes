using System;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Avatars.Models;
using AstralNotes.Utils.DiceBearAvatars;
using AstralNotes.Utils.FileStore;
using Microsoft.EntityFrameworkCore;
using File = AstralNotes.Database.Entities.File;

namespace AstralNotes.Domain.Avatars
{
    /// <summary>
    /// Сервис для работы с DiceBear аватарами
    /// </summary>
    public class AvatarService : IAvatarService
    {
        readonly NotesContext _context;
        readonly IAvatarProvider _avatarProvider;
        readonly IFileStorage _fileStorage;

        public AvatarService(IAvatarProvider avatarProvider, NotesContext context, IFileStorage fileStorage)
        {
            _context = context;
            _avatarProvider = avatarProvider;
            _fileStorage = fileStorage;
        }
        
        /// <summary>
        /// Сохранение аватара
        /// <returns>Индетификатор аватара</returns>
        /// </summary>
        public async Task<Guid> SaveAvatar(string seed)
        {
            var content = await _avatarProvider.GetAsync(seed);
            var avatarFile = new File(".svg", content.Length);
            
            await _fileStorage.SaveAsync(content, avatarFile.FileGuid.ToString());
         
            _context.Files.Add(avatarFile);
            await _context.SaveChangesAsync();
            
            return avatarFile.FileGuid;
        }

        /// <summary>
        /// Получение аватара
        /// <param name="avatarGuid">Индетификатор аватара</param>
        /// <returns>Модель аватара</returns>
        /// </summary>
        public async Task<AvatarModel> GetAvatar(Guid avatarGuid)
        {
            var file = await _context.Files.AsNoTracking().FirstAsync(n => n.FileGuid == avatarGuid);
            var content = await _fileStorage.Get(file.FileGuid.ToString());

            return new AvatarModel(file.FileGuid, file.Extension, content);
        }

        /// <summary>
        /// Удаление аватара
        /// <param name="avatarGuid">Индетификатор аватра</param>
        /// <returns>Индетификатор аватра</returns>
        /// </summary>
        public async Task Remove(Guid avatarGuid)
        {
            var file = await _context.Files.FindAsync(avatarGuid);

            await _fileStorage.Remove(avatarGuid.ToString());

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
    }
}