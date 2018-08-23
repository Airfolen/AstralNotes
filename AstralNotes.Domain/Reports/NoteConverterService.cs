using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Utils.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AstralNotes.Domain.Reports
{
    /// <summary>
    /// Сервис для преобразования заметок в pdf документ
    /// </summary>
    public class NoteConverterService : INoteConverterService
    {
        readonly ITextSharpDocument _textSharpDocument;
        Font _fontStyle;
        List<NoteModel> _notes;
        readonly MemoryStream _memoryStream;
        const int NumberOfTableColumns = 1;
     
        public NoteConverterService(ITextSharpDocument textSharpDocument)
        {
            _textSharpDocument = textSharpDocument;
            _memoryStream = new MemoryStream();
        }

        /// <inheritdoc/>
        public Task<byte[]> GetPdfDocumentAsync(List<NoteModel> notes)
        {
            return Task.Run(() =>
            {
                _notes = notes;
                var document = _textSharpDocument.CreateDocument(PageSize.A4, 40, 40, 20, 20);
                var table = _textSharpDocument.CreateTable(NumberOfTableColumns, 100, Element.ALIGN_CENTER);
            
                PdfWriter.GetInstance(document, _memoryStream);
                document.Open();

                DocumentHeader(table);
                DocumentBody(table);

                document.Add(table);
                document.Close();
                
                return _memoryStream.ToArray();
            });
        }

        void DocumentHeader(PdfPTable table)
        {
            _fontStyle = FontFactory.GetFont("sylfaen", 18, Font.BOLD);

            table.AddCell(_textSharpDocument.CreateCell("Your notes:", _fontStyle, BaseColor.LIGHT_GRAY,
                colspan: NumberOfTableColumns));
            table.AddCell(_textSharpDocument.CreateCell(" ", _fontStyle, BaseColor.WHITE,
                    colspan: NumberOfTableColumns, border: 0));
        }

       void DocumentBody(PdfPTable table)
        {
            _fontStyle = FontFactory.GetFont(BaseFont.HELVETICA, 12, Font.NORMAL);
            var number = 1;

            foreach (var note in _notes)
            {
                table.AddCell(_textSharpDocument.CreateCell("Note: " + number++, _fontStyle, BaseColor.WHITE));
                table.AddCell(_textSharpDocument.CreateCell("Title: \n\n" + note.Title, _fontStyle, BaseColor.WHITE, 10));
                table.AddCell(_textSharpDocument.CreateCell("Content: \n\n" + note.Content, _fontStyle, BaseColor.WHITE, 20));
                table.AddCell(_textSharpDocument.CreateCell("\n\n", _fontStyle, BaseColor.WHITE, border: 0,
                    colspan: NumberOfTableColumns));
            }
        }
    }
}