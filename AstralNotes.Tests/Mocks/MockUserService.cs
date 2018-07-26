using AstralNotes.Database.Entities;
using AstralNotes.Domain.Users;
using Moq;

namespace AstralNotes.Tests.Mocks
{
    /// <summary>
    /// Заглушки для UserService
    /// </summary>
    public static class MockUserService
    {
        public static IUserService GetMock()
        {
            var repo = new MockRepository(MockBehavior.Default);
            var fileStorageMock = repo.Create<IUserService>();
        
            fileStorageMock.Setup(x =>  x.GetCurrentUserAsync()).ReturnsAsync(new User(){Id = null});

            return fileStorageMock.Object;
        }
    }
}