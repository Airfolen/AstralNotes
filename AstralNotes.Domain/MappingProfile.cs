using AstralNotes.Database.Entities;
using AstralNotes.Domain.Avatars.Models;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Domain.Users.Models;
using AutoMapper;

namespace AstralNotes.Domain
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserInfo, User>();
            CreateMap<User, UserModel>();
            
            CreateMap<NoteInfo, Note>();
            CreateMap<Note, NoteModel>();
        }
    }
}