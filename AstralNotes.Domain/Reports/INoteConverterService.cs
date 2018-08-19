using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<byte[]> GetPdfDocumentAsync(List<NoteModel> notes);
    }
}