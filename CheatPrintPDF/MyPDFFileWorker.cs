using Patagames.Pdf;
using Patagames.Pdf.Net;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CheatPrintPDF
{
    public class MyPDFFileWorker
    {
        public MyPDFFileWorker()
        {

        }

        public Bitmap PdfToBitmap(PdfPage page)
        {
            using (var bmp = new PdfBitmap((int)page.Width, (int)page.Height, true))
            {
                bmp.FillRect(0, 0, (int)page.Width, (int)page.Height, FS_COLOR.White);
                page.Render(bmp, 0, 0, (int)page.Width, (int)page.Height,
                    Patagames.Pdf.Enums.PageRotate.Normal,
                    Patagames.Pdf.Enums.RenderFlags.FPDF_ANNOT);
                return new Bitmap(bmp.Image);
            }
        }

        public List<PagesResult> GetPDFPagesTypes(List<MyPDFFile> myPDFFiles)
        {
            List<PagesResult> Pages = new List<PagesResult>();

            PagesResult buffer;

            for (int i = 0; i < (int)PageSizeTypes.None; i++)
            {
                buffer = new PagesResult((PageSizeTypes)i);

                foreach (var pdfpage in myPDFFiles.ToList())
                {
                    if (myPDFFiles.Contains(pdfpage) && (int)pdfpage.SizeType == i)
                    {
                        buffer.Pages.Add(pdfpage.PageNumber);
                    }
                    else continue;
                }
                if (buffer.Pages.Count != 0)
                {
                    buffer.SetCopyPages();
                    Pages.Add(buffer);
                }
            }
            return Pages;
        }

        public async Task<bool> CheckIsColored(Bitmap myBitmap)
        {
            return await Task.Run(() =>
            {
                for (int k = 0; k < myBitmap.Height; k++)
                {
                    for (int n = 0; n < myBitmap.Width; n++)
                    {
                        var pcolor = myBitmap.GetPixel(n, k);
                        if ((pcolor.R != pcolor.B && pcolor.R != pcolor.G && pcolor.G != pcolor.B))
                        {
                            return true;
                        }
                    }
                }
                return false;
            });
        }

        public async Task<List<MyPDFFile>> GetPdfPages(string pathToFile)
        {
            List<MyPDFFile> pdfPages = new List<MyPDFFile>();
            using (var doc = PdfDocument.Load(pathToFile))
            {
                for (int i = 0; i < doc.Pages.Count; i++)
                {
                    MyPDFFile myPDFFile = new MyPDFFile(i + 1, doc.Pages[i].Width * 0.3528f, doc.Pages[i].Height * 0.3528f);
                    if (myPDFFile.SizeType == PageSizeTypes.A4 || myPDFFile.SizeType == PageSizeTypes.A3)
                    {
                        myPDFFile.isColored = await CheckIsColored(PdfToBitmap(doc.Pages[i]));
                        if (!myPDFFile.isColored) myPDFFile.SizeType = PageSizeTypes.A4xUncolored;
                    }
                    pdfPages.Add(myPDFFile);
                }
            }
            return pdfPages;
        }


    }
}
