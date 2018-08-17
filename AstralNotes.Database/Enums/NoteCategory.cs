using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        [Description("Общая")]
        General = 0,
        
        /// <summary>
        /// Ежедневник
        /// </summary>
        [Display(Name = "Ежедневник")]
        [Description("Ежедневник")]
        Diary = 1,
        
        /// <summary>
        /// Работа
        /// </summary>
        [Display(Name = "Работа")]
        [Description("Работа")]
        Work = 2
    }
}