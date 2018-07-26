using System;
using System.IO;
using System.Threading.Tasks;
using AstralNotes.Domain.Avatars;
using AstralNotes.Domain.Avatars.Models;
using Moq;

namespace AstralNotes.Tests.Mocks
{
    public static class MockAvatarService
    {
        /// <summary>
        /// Заглушки для AvatarService
        /// </summary>
        public static IAvatarService GetMock()
        {
            var repo = new MockRepository(MockBehavior.Default);
            var avatarMock = repo.Create<IAvatarService>();
            
            avatarMock.Setup(x => x.SaveAvatar(It.IsAny<string>())).ReturnsAsync(new Guid("15d141e5-eb43-48d7-b4b6-25109167e46c"));
            avatarMock.Setup(x => x.GetAvatar(new Guid("05a3537a-a12f-4d65-976e-1ff1fdb727c2"))).ReturnsAsync(
                new AvatarModel(new Guid("05a3537a-a12f-4d65-976e-1ff1fdb727c2"), "txt", new MemoryStream()));
            avatarMock.Setup(x => x.Remove(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            return avatarMock.Object;
        }
    }
}