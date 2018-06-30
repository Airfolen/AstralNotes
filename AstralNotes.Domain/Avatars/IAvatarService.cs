using System;
using System.IO;
using System.Threading.Tasks;
using AstralNotes.Domain.Avatars.Models;

namespace AstralNotes.Domain.Avatars
{
    public interface IAvatarService
    {
        Task<Guid> SaveAvatar(string gender, string seed);
        
        Task<AvatarModel> GetAvatar(Guid avatarGuid);
        
        Task Remove(Guid avatarGuid);
    }
}