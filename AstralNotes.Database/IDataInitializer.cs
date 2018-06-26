using System.Threading.Tasks;

namespace AstralNotes.Database
{
    public interface IDataInitializer
    {
        Task Initialize();
    }
}