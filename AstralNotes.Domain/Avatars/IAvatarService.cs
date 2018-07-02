using System;
using System.Threading.Tasks;
using AstralNotes.Domain.Avatars.Models;

namespace AstralNotes.Domain.Avatars
{
    public interface IAvatarService
    {
        Task<Guid> SaveAvatar(string seed);
        
        Task<AvatarModel> GetAvatar(Guid avatarGuid);
        
        Task Remove(Guid avatarGuid);
    }
}