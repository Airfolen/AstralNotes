using AstralNotes.Database.Entities;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Domain.Users.Models;
using AutoMapper;

namespace AstralNotes.Domain
{
    public class IdentityMappingProfile: Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<UserInfo, User>();
            CreateMap<User, UserModel>();
            
            CreateMap<NoteInfo, Note>();
            CreateMap<Note, NoteModel>();
            CreateMap<Note, NoteShortModel>();
        }
    }
}