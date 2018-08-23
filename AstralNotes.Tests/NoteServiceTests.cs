using System;
using System.Collections.Generic;
using AstralNotes.Database;
using AstralNotes.Database.Entities;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Domain.Users.Models;
using AstralNotes.Tests.Mocks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AstralNotes.Tests
{
    public class NoteServiceTests
    {
        readonly NotesContext _context;
        readonly NoteService _noteService;
        
        public NoteServiceTests()
        {
            var db = new DbContextOptionsBuilder<NotesContext>();
            db.UseInMemoryDatabase();
            _context = new NotesContext(db.Options);
            
            InitializeTestNotes();
            _context.SaveChangesAsync();

            InitializeAutoMapper();
            
            _noteService = new  NoteService(_context, Mapper.Instance, MockAvatarService.GetMock());
        }

        [Fact]
        public async void CanCreateNote()
        {
            //Arrange
            var noteInfo = new NoteInfo {Content = "SomeContent", Title = "Title", Category = NoteCategory.Diary};

            //Act
            var noteGuid = await _noteService.Create(noteInfo, null);

            var resultNote = await _context.Notes.FirstAsync(x => x.NoteGuid == noteGuid);
            
            //Assert
            Assert.NotNull(resultNote);
            Assert.IsType<Note>(resultNote);
            Assert.Equal(noteInfo.Content, resultNote.Content);
            Assert.Equal(noteInfo.Title, resultNote.Title);
            Assert.Equal(noteInfo.Category, resultNote.Category);
        }
        
        [Fact]
        public async void GiveExceptionWithNullModelAtCreation()
        {
            //Arrange && Act && Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _noteService.Create(null, null));
        }
        
        [Fact]
        public async void CanRemoveNote()
        {
            //Arrange 
            var noteGuid = new Guid("39f82b8a-bbea-4305-aa03-cf4ccbbda7b8");
            
            // Act
            await _noteService.Remove(noteGuid);
            
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _context.Notes.FirstAsync(x => x.NoteGuid == noteGuid));
        }
        
        [Fact]
        public async void CanGiveNote()
        {
            //Arrange 
            var noteGuid = new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27");
            
            // Act
            var note = await _noteService.GetNote(noteGuid);
            
            //Assert
            Assert.NotNull(note);
            Assert.IsType<NoteModel>(note);
            Assert.Equal("SomeContent1" , note.Content);
        } 
        
        [Fact]
        public async void GiveExceptionWithWrongGuidAtGetting()
        {
            //Arrange && Act
            var noteGuid = new Guid("00cb1b5b-d059-4b5d-91d0-c904c3f6dc7a");
            
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _noteService.GetNote(noteGuid));
        }
        
        [Fact]
        public async void CanGetNotes()
        {
            //Arrange && Act
            var notes = await _noteService.GetNotes("SomeContent", null, null);
            
            //Assert
            Assert.NotNull(notes);
            Assert.IsType<List<NoteModel>>(notes);
            Assert.Contains(notes, model => model.Content.Contains("SomeContent"));
            Assert.Contains(notes, model => model.Title.Contains("SomeTitle"));
            Assert.Equal(notes.Count, 4);
        }
        
        [Fact]
        public async void CanAddEntity()
        {
            //Arrange
            var note = new Note
            {
                NoteGuid = new Guid("05a3537a-a12f-4d65-976e-1ff1fdb727c2"),
                Title = "SomeTitle",
                Content = "SomeContent"
            };

            //Act
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();

            var foundNote = await _context.Notes.FirstOrDefaultAsync(x => x.NoteGuid == note.NoteGuid);
            
            //Assert
            Assert.NotNull(foundNote);
            Assert.Equal(note.Content, foundNote.Content);
        }
        
        [Fact]
        public async void CanGetEntity()
        {
            //Arrange && Act
            var foundNote =
                await _context.Notes.FirstAsync(x => x.NoteGuid == new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27"));
            
            //Assert
            Assert.NotNull(foundNote);
            Assert.IsType(typeof(Note), foundNote);
        }
        [Fact]
        public async void CanRemoveEntity()
        {
            //Arrange & Act
            var note =
                await _context.Notes.FirstAsync(x => x.NoteGuid == new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27"));
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _context.Notes.FirstAsync(x =>
                x.NoteGuid == new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27")));
        }

        async void InitializeTestNotes()
        {
            var notes = new List<Note>
            {
                new Note
                {
                    NoteGuid = new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27"),
                    Title = "SomeTitle1",
                    Content = "SomeContent1",
                    Category = NoteCategory.Diary
                    
                },
                new Note
                {
                    NoteGuid = new Guid("39f82b8a-bbea-4305-aa03-cf4ccbbda7b8"),
                    Title = "SomeTitle2",
                    Content = "SomeContent2",
                    Category = NoteCategory.Diary
                },
                new Note
                {
                    NoteGuid = new Guid("9438eab8-01f9-4540-973d-3cca2e845798"),
                    Title = "SomeTitle3",
                    Content = "UnusualContent",
                    Category = NoteCategory.Work
                }
            };
           
            
            foreach (var note in notes)
            {
                if (!await _context.Notes.AnyAsync(a => a.NoteGuid == note.NoteGuid))
                    await _context.Notes.AddAsync(note);
            }
        }

        void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserInfo, User>();
                cfg.CreateMap<NoteInfo, Note>();
                cfg.CreateMap<Note, NoteModel>();
            });
        }
    }
}