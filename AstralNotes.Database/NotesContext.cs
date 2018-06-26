using AstralNotes.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Database
{
    public class NotesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Note> Notes { get; set; }

        public NotesContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}