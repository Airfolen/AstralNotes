using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralNotes.Database.Entities
{
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FileGuid { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Extension { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public long Size { get; set; }

        public File()
        {
            FileGuid = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        
        public File(string extension, long size)
        {
            FileGuid = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Extension = extension;
            Size = size;
        }
    }
}