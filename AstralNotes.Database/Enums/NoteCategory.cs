using System.ComponentModel.DataAnnotations;

namespace AstralNotes.Database.Enums
{
    /// <summary>
    /// Категория заметки
    /// </summary>
    public enum NoteCategory
    {
        /// <summary>
        /// Общая
        /// </summary>
        [Display(Name = "Общая")]
        General = 0,
        
        /// <summary>
        /// Ежедневник
        /// </summary>
        [Display(Name = "Ежедневник")]
        Diary = 1,
        
        /// <summary>
        /// Работа
        /// </summary>
        [Display(Name = "Работа")]
        Work = 2
    }
}