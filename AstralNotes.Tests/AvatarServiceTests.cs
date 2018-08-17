using System;
using System.Collections.Generic;
using AstralNotes.Database;
using AstralNotes.Database.Entities;
using AstralNotes.Domain.Avatars;
using AstralNotes.Domain.Avatars.Models;
using AstralNotes.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MockFileStorage = AstralNotes.Tests.Mocks.MockFileStorage;

namespace AstralNotes.Tests
{
    public class AvatarServiceTests
    {
        readonly NotesContext _context;
        readonly AvatarService _avatarService;
        
        public AvatarServiceTests()
        {
            var db = new DbContextOptionsBuilder<NotesContext>();
            db.UseInMemoryDatabase();
            _context = new NotesContext(db.Options);
            
            InitializeTestAvatars();
            _context.SaveChangesAsync();

            _avatarService = new AvatarService(MockAvatarProvider.GetMock(), _context,
                MockFileStorage.GetMock());
        }
        
        [Fact]
        public async void CheckAvatarServiceOnCreateMethod()
        {
            //Arrange && Act
            var fileGiud = await _avatarService.SaveAvatar("SomeSeed");
            var file = await _context.Files.FirstAsync(x => x.FileGuid == fileGiud);
            
            //Assert
            Assert.NotNull(file);
            Assert.IsType<File>(file);
        }
        
        [Fact]
        public async void CheckAvatarServiceOnGetMethod()
        {
            //Arrange
            var fileGuid = new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27");
            
            //Act
            var avatarModel = await _avatarService.GetAvatar(fileGuid);
            
            //Assert
            Assert.NotNull(avatarModel);
            Assert.IsType<AvatarModel>(avatarModel);
            Assert.Equal("svg", avatarModel.Extension);
        }
        
        [Fact]
        public async void CheckAvatarServiceOnRemoveMethod()
        {
            //Arrange
            var fileGuid = new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27");
            
            //Act
            await _avatarService.Remove(fileGuid);
            
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _context.Files.FirstAsync(x => x.FileGuid == fileGuid));
        }
        
        private async void InitializeTestAvatars()
        {
            var notes = new List<File>
            {
                new File
                {
                    FileGuid = new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27"),
                    Extension = "svg"
                },
                new File
                {
                    FileGuid = new Guid("39f82b8a-bbea-4305-aa03-cf4ccbbda7b8"),
                    Extension = "png"
                }
            };
           
            
            foreach (var note in notes)
            {
                if (!await _context.Files.AnyAsync(a => a.FileGuid == note.FileGuid))
                    await _context.Files.AddAsync(note);
            }
        }
    }
}