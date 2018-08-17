using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AstralNotes.Utils.Documents
{
    public class TextSharpDocument : ITextSharpDocument
    {
        Document Document { get; set; }
        PdfPTable PdfPTable { get; set; }
        PdfPCell PdfPCell { get; set; }
        
        public Document CreateDocument(Rectangle pageSize, int margingLeft, int margingRight, int margingTop, 
            int margingBottom)
        {
            Document = new Document(pageSize, margingLeft, margingRight, margingTop, margingBottom);

            return Document;
        }

        public PdfPTable CreateTable(int numberOfTableColumns, float widthPercentage, int horizontalAlignment)
        {
            PdfPTable = new PdfPTable(numberOfTableColumns)
            {
                WidthPercentage = widthPercentage,
                HorizontalAlignment = horizontalAlignment
            };

            return PdfPTable;
        }
        
        public PdfPCell CreateCell(string text, Font fontStyle, BaseColor backgroundColor, float padding = 0, 
            int border = Rectangle.BOX, int horizontalAlignment = Element.ALIGN_CENTER,
            int verticalAlignment = Element.ALIGN_MIDDLE, int extraParagraphSpace = 0, int colspan = 0)
        {
            PdfPCell = new PdfPCell(new Phrase(text, fontStyle))
            {
                Padding = padding,
                Border =  border,
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment,
                BackgroundColor = backgroundColor,
                ExtraParagraphSpace = extraParagraphSpace,
                Colspan = colspan
            };

            return PdfPCell;
        }
    }
}