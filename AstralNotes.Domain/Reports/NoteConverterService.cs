using System.Collections.Generic;
using System.IO;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Utils.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AstralNotes.Domain.Reports
{
    public class NoteConverterService : INoteConverterService
    {
        readonly ITextSharpDocument _textSharpDocument;
        Font FontStyle { get; set; }
        List<NoteModel> Notes { get; set; }
        MemoryStream MemoryStream { get; set; }
        const int NumberOfTableColumns = 1;
     
        public NoteConverterService(ITextSharpDocument textSharpDocument)
        {
            _textSharpDocument = textSharpDocument;
            MemoryStream = new MemoryStream();
        }

        public byte[] GetPdfDocument(List<NoteModel> notes)
        {
            Notes = notes;
            var document = _textSharpDocument.CreateDocument(PageSize.A4, 40, 40, 20, 20);
            var table = _textSharpDocument.CreateTable(NumberOfTableColumns, 100, Element.ALIGN_CENTER);
            
            PdfWriter.GetInstance(document, MemoryStream);
            document.Open();

            DocumentHeader(table);
            DocumentBody(table);

            document.Add(table);
            document.Close();

            return MemoryStream.ToArray();
        }

        private void DocumentHeader(PdfPTable table)
        {
            FontStyle = FontFactory.GetFont("sylfaen", 18, Font.BOLD);

            table.AddCell(_textSharpDocument.CreateCell("Your notes:", FontStyle, BaseColor.LIGHT_GRAY,
                colspan: NumberOfTableColumns));
            table.AddCell(_textSharpDocument.CreateCell(" ", FontStyle, BaseColor.WHITE,
                    colspan: NumberOfTableColumns, border: 0));
        }
        
        private void DocumentBody(PdfPTable table)
        {
            FontStyle = FontFactory.GetFont(BaseFont.HELVETICA, 12, Font.NORMAL);
            var number = 1;

            foreach (var note in Notes)
            {
                table.AddCell(_textSharpDocument.CreateCell("Note: " + number++, FontStyle, BaseColor.WHITE));
                table.AddCell(_textSharpDocument.CreateCell("Title: \n\n" + note.Title, FontStyle, BaseColor.WHITE, 10));
                table.AddCell(_textSharpDocument.CreateCell("Content: \n\n" + note.Content, FontStyle, BaseColor.WHITE, 20));
                table.AddCell(_textSharpDocument.CreateCell("\n\n", FontStyle, BaseColor.WHITE, border: 0,
                    colspan: NumberOfTableColumns));
            }
        }
    }
}