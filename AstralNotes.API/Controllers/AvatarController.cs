using System;
using System.Threading.Tasks;
using AstralNotes.Domain.Avatars;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;

namespace AstralNotes.API.Controllers
{
    /// <summary>
    /// API для работы с аватарами для заметок
    /// </summary>
    [Route("Avaters")]
    public class AvatarController : Controller
    {
        private readonly IAvatarService _avatarService;

        public AvatarController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }
        
        /// <summary>
        /// Получение DiceBear аватара
        /// <param name="avatarGuid">Индетификатор аватара</param>
        /// <returns>Поток файла System.IO.Stream</returns>
        /// </summary>
        [HttpGet("{avatarGuid}")]
        public async Task<FileStreamResult> GetAvatar (Guid avatarGuid)
        {
            var avatar = await _avatarService.GetAvatar(avatarGuid);
            var fileType = MimeTypeMap.GetMimeType(avatar.Extension);
            
            return File(avatar.Content, fileType,  avatar.AvatarGuid + avatar.Extension);
        }
    }
}