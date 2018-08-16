using System;
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
        private const int NumberOfTableColumns = 3;
     
        public NotesReport(List<NoteModel> notes)
        {
            Notes = notes;
            MemoryStream = new MemoryStream();
        }

        public byte[] GetPdfReport()
        {
            Document = new Document(PageSize.A4, 40, 40, 20, 20);
            PdfPTable = new PdfPTable(NumberOfTableColumns)
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_CENTER
            };


//            var sylfaenpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\sylfaen.ttf";
//            var sylfaen = BaseFont.CreateFont(sylfaenpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
//            _fontStyle = new Font(sylfaen, 14, Font.NORMAL, BaseColor.BLUE);
            
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
            FontStyle = FontFactory.GetFont("Arial", 18, Font.BOLD);

            PdfPCell = new PdfPCell(new Phrase("Your notes:", FontStyle))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = BaseColor.WHITE,
                ExtraParagraphSpace = 0,
                Colspan = NumberOfTableColumns
            };
            PdfPTable.AddCell(PdfPCell);
            
            PdfPTable.CompleteRow();
            
            PdfPCell = new PdfPCell(new Phrase(" ", FontStyle))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = BaseColor.WHITE,
                ExtraParagraphSpace = 0,
                Colspan = NumberOfTableColumns
            };
            PdfPTable.AddCell(PdfPCell);
            
            PdfPTable.CompleteRow();
        }
        
        private void ReportBody()
        {
            #region Table Header

            var tableColumns = new String[] {"Number", "Title", "Content"};
            
            FontStyle = FontFactory.GetFont("Arial", 12, Font.BOLD);

            foreach (var tableColumn in tableColumns)
            {
                PdfPCell = new PdfPCell(new Phrase(tableColumn, FontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.LIGHT_GRAY
                };
                PdfPTable.AddCell(PdfPCell);
            }

            #endregion

            #region Table Body

            FontStyle = FontFactory.GetFont("Arial", 12, Font.NORMAL);

            var number = 1;

            foreach (var note in Notes)
            {
                PdfPCell = new PdfPCell(new Phrase(number++.ToString(), FontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.WHITE
                };
                PdfPTable.AddCell(PdfPCell);
                
                PdfPCell = new PdfPCell(new Phrase(note.Title ?? "―――――", FontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.WHITE
                };
                PdfPTable.AddCell(PdfPCell);
                
                PdfPCell = new PdfPCell(new Phrase(note.Content, FontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.WHITE
                };
                PdfPTable.AddCell(PdfPCell);
            }

            #endregion
        }
    }
}