using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AstralNotes.Utils.Documents
{
    /// <summary>
    /// Класс для работы с документами iTextSharp
    /// </summary>
    /// <returns>Массив байт</returns>
    public interface ITextSharpDocument
    {
        /// <summary>
        /// Создание документа 
        /// </summary>
        /// <returns>Документ iTextSharp</returns>
        Document CreateDocument(Rectangle pageSize, int margingLeft, int margingRight, int margingTop, int margingBottom);
        
        /// <summary>
        /// Создание таблицы в документе 
        /// </summary>
        /// <returns>Таблица iTextSharp</returns>
        PdfPTable CreateTable(int numberOfTableColumns, float widthPercentage, int horizontalAlignment);

        /// <summary>
        /// Создание ячейки для таблицы
        /// </summary>
        /// <returns>Ячейка таблицы iTextSharp</returns>
        PdfPCell CreateCell(string text, Font fontStyle, BaseColor backgroundColor, float padding = 0,
            int border = Rectangle.BOX, int horizontalAlignment = Element.ALIGN_CENTER,
            int verticalAlignment = Element.ALIGN_MIDDLE, int extraParagraphSpace = 0, int colspan = 0);
    }
}