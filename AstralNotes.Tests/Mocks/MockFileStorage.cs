using System.IO;
using System.Threading.Tasks;
using AstralNotes.Utils.FileStore;
using Moq;

namespace AstralNotes.Tests.Mocks
{
    /// <summary>
    /// Заглушки для FileStorage
    /// </summary>
    public static class MockFileStorage
    {
        public static IFileStorage GetMock()
        {
            var repo = new MockRepository(MockBehavior.Default);
            var fileStorageMock = repo.Create<IFileStorage>();
            
            fileStorageMock.Setup(x =>  x.Get(It.IsAny<string>())).ReturnsAsync(new MemoryStream());
            fileStorageMock.Setup(x => x.SaveAsync(It.IsAny<Stream>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            fileStorageMock.Setup(x => x.Remove(It.IsAny<string>())).Returns(Task.CompletedTask);

            return fileStorageMock.Object;
        }
    }
}