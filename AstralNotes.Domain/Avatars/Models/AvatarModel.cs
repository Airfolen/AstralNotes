using System;
using System.IO;

namespace AstralNotes.Domain.Avatars.Models
{
    public class AvatarModel
    {
        public Guid AvatarGuid { get; set; }
        
        public string Extension { get; set; }

        public Stream Content { get; set; }
        
        public AvatarModel(Guid avatarGuid, string extension, Stream content)
        {
            AvatarGuid = avatarGuid;
            Extension = extension;
            Content = content;
        }
    }
}