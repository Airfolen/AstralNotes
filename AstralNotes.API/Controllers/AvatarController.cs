using System;
using System.Threading.Tasks;
using AstralNotes.Domain.Avatars;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;

namespace AstralNotes.API.Controllers
{
    /// <inheritdoc />
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

       
        [HttpPost("{seed}")]
        public async Task<Guid> Create([FromRoute] Guid seed)
        {
            return await _avatarService.SaveAvatar(seed.ToString());
        }

       
        [HttpDelete("{avatarGuid}")]
        public async Task Delete(Guid avatarGuid)
        {
            await _avatarService.Remove(avatarGuid);
        }
    
      
        [HttpGet("{avatarGuid}")]
        public async Task<FileStreamResult> GetAvatar (Guid avatarGuid)
        {
            var avatar = await _avatarService.GetAvatar(avatarGuid);
            var fileType = MimeTypeMap.GetMimeType(avatar.Extension);
            return File(avatar.Content, fileType,  avatar.AvatarGuid + avatar.Extension);
        }
    }
}