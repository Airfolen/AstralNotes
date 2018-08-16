using System.Collections.Generic;
using System.IO;
using AstralNotes.Domain.Notes.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AstralNotes.Domain.Reports
{
    public class NotesReport
    {
        private Document Document { get; set; }
        private Font FontStyle { get; set; }
        private PdfPTable PdfPTable { get; set; }
        private PdfPCell PdfPCell { get; set; }
        private List<NoteModel> Notes { get; set; }
        private MemoryStream MemoryStream { get; set; }
        private const int NumberOfTableColumns = 1;
     
        public NotesReport(List<NoteModel> notes)
        {
            Notes = notes;
            MemoryStream = new MemoryStream();
            Document = new Document(PageSize.A4, 40, 40, 20, 20);
            
            PdfPTable = new PdfPTable(NumberOfTableColumns)
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
        }

        public byte[] GetPdfReport()
        {
           PdfWriter.GetInstance(Document, MemoryStream);
           Document.Open();

            ReportHeader();
            ReportBody();

            Document.Add(PdfPTable);
            Document.Close();

            return MemoryStream.ToArray();
        }

        private void ReportHeader()
        {
            FontStyle = FontFactory.GetFont("sylfaen", 18, Font.BOLD);

            PdfPCell = new PdfPCell(new Phrase("Your notes:", FontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                ExtraParagraphSpace = 0,
                Colspan = NumberOfTableColumns
            };
            PdfPTable.AddCell(PdfPCell);
            
            PdfPCell = new PdfPCell(new Phrase(" ", FontStyle))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = BaseColor.WHITE,
                ExtraParagraphSpace = 0,
                Colspan = NumberOfTableColumns
            };
            PdfPTable.AddCell(PdfPCell);
        }
        
        private void ReportBody()
        {
            #region Table

            FontStyle = FontFactory.GetFont(BaseFont.HELVETICA, 12, Font.NORMAL);

            var number = 1;

            foreach (var note in Notes)
            {
                PdfPCell = new PdfPCell(new Phrase("Note: " + number++, FontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.WHITE
                };
                PdfPTable.AddCell(PdfPCell);
                
                PdfPCell = new PdfPCell(new Phrase("Title: \n\n" + note.Title, FontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.WHITE,
                    Padding = 10
                };
                PdfPTable.AddCell(PdfPCell);
                
                PdfPCell = new PdfPCell(new Phrase("Content: \n\n" + note.Content, FontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.WHITE,
                    Padding = 20
                    
                };
                PdfPTable.AddCell(PdfPCell);
                
                PdfPCell = new PdfPCell(new Phrase("\n\n", FontStyle))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.WHITE,
                    ExtraParagraphSpace = 0,
                    Colspan = NumberOfTableColumns
                };
                PdfPTable.AddCell(PdfPCell);
            }
            #endregion
        }
    }
}