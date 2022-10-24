using IronPdf;

namespace NetCoreMvc.Services;

public class PdfGenerator : IPdfGenerator
{
    public byte[] GeneratePdf(string html)
    {
        var ironPdfRender = new ChromePdfRenderer();
        using var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html);

        return pdfDoc.Stream.ToArray();
    }
}
