using AstralNotes.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Database
{
    public class NotesContext : IdentityDbContext<User>
    {
        public DbSet<Note> Notes { get; set; }
        
        public DbSet<File> Files { get; set; }

        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}