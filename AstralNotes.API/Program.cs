using AstralNotes.Database;
using AstralNotes.Utils.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AstralNotes.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).MigrateDatabase<NotesContext>()
                .Run();
            //.SetUpWithService<IDataInitializer>(x => x.Initialize().Wait())
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}