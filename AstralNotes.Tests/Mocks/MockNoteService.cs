using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Notes.Models;
using Moq;

namespace AstralNotes.Tests.Mocks
{
    public static class MockNoteService
    {
        /// <summary>
        /// Заглушки для NoteService
        /// </summary>
        public static INoteService GetMock()
        {
            var repo = new MockRepository(MockBehavior.Default);
            var avatarMock = repo.Create<INoteService>();
            
            var notes = new List<NoteModel>
            {
                new NoteModel
                {
                    NoteGuid = new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27"),
                    Title = "SomeTitle1",
                    Content = "SomeContent1",
                    Category = NoteCategory.Diary
                    
                },
                new NoteModel
                {
                    NoteGuid = new Guid("39f82b8a-bbea-4305-aa03-cf4ccbbda7b8"),
                    Title = "SomeTitle2",
                    Content = "SomeContent2",
                    Category = NoteCategory.Diary
                },
                new NoteModel
                {
                    NoteGuid = new Guid("9438eab8-01f9-4540-973d-3cca2e845798"),
                    Title = "SomeTitle3",
                    Content = "UnusualContent",
                    Category = NoteCategory.Work
                }
            };
            
            avatarMock.Setup(x => x.Create(It.IsAny<NoteInfo>(), It.IsAny<string>())).ReturnsAsync(new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27"));
            avatarMock.Setup(x => x.Remove(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            avatarMock.Setup(x => x.GetNote(It.IsAny<Guid>())).ReturnsAsync( new NoteModel
            {
                NoteGuid = new Guid("05ad7880-815f-4245-ac4e-21e24a7afd27"),
                Title = "SomeTitle1",
                Content = "SomeContent1",
                Category = NoteCategory.Diary
                    
            });
            avatarMock.Setup(x => x.GetNotes(It.IsAny<string>(), It.IsAny<NoteCategory?>(), It.IsAny<string>())).ReturnsAsync(notes);
            
            return avatarMock.Object;
        }
    }
}