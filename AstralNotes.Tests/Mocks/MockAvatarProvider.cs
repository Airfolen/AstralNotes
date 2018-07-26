using System.IO;
using AstralNotes.Utils.DiceBearAvatars;
using Moq;

namespace AstralNotes.Tests.Mocks
{
    public static class MockAvatarProvider
    {
        /// <summary>
        /// Заглушки для AvatarProvider
        /// </summary>
        public static IAvatarProvider GetMock()
        {
            var repo = new MockRepository(MockBehavior.Default);
            var avatarMock = repo.Create<IAvatarProvider>();
            
            avatarMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new MemoryStream());

            return avatarMock.Object;
        }
    }
}