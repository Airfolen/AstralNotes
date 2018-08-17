using System.Collections.Generic;
using AstralNotes.Domain.Notes.Models;

namespace AstralNotes.Domain.Reports
{
    /// <summary>
    /// Сервис для преобразования заметок в pdf документ
    /// </summary>
    public interface INoteConverterService
    {
        /// <summary>
        /// Получение заметок в pdf документе
        /// </summary>
        /// <returns>Массив байт</returns>
        byte[] GetPdfDocument(List<NoteModel> notes);
    }
}