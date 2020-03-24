using Patagames.Pdf;
using Patagames.Pdf.Net;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CheatPrintPDF
{
    /// <summary>
    /// Класс описывающий фукционал для работы с пдф файлом
    /// </summary>
    public class MyPDFFileWorker
    {
        /// <summary>
        /// Базовай конструктор
        /// </summary>
        public MyPDFFileWorker()
        {

        }

        /// <summary>
        /// Преобразование страницы в изображение
        /// </summary>
        /// <param name="page">Страница</param>
        /// <returns>Изображение</returns>
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

        /// <summary>
        /// Метод получающий сгруппированные страницы по типу
        /// </summary>
        /// <param name="myPDFPages">Страницы</param>
        /// <returns>Сгруппированные страницы</returns>
        public List<PagesResult> GetPDFPagesTypes(List<MyPDFPage> myPDFPages)
        {
            List<PagesResult> Pages = new List<PagesResult>();

            PagesResult buffer;

            for (int i = 0; i < (int)PageSizeTypes.None; i++)
            {
                buffer = new PagesResult((PageSizeTypes)i);

                foreach (var pdfpage in myPDFPages.ToList())
                {
                    if (myPDFPages.Contains(pdfpage) && (int)pdfpage.SizeType == i)
                    {
                        buffer.Pages.Add(pdfpage.PageNumber);
                    }
                    else continue;
                }
                if (buffer.Pages.Count != 0)
                {
                    buffer.SetCopyPagesAndText();
                    Pages.Add(buffer);
                }
            }
            return Pages;
        }

        /// <summary>
        /// Проверка страницы на цветность
        /// </summary>
        /// <param name="myBitmap">Изображене</param>
        /// <returns>Цветная ли страница</returns>
        public async Task<bool> CheckIsColored(Bitmap myBitmap)
        {
            return await Task.Run(() =>
            {
                for (int k = 0; k < myBitmap.Height; k++)
                {
                    for (int n = 0; n < myBitmap.Width; n++)
                    {
                        var pcolor = myBitmap.GetPixel(n, k);
                        if ((pcolor.R == pcolor.B && pcolor.R == pcolor.G && pcolor.G == pcolor.B))
                        {
                            continue;
                        }
                        else
                        {
                            myBitmap.Dispose();
                            return true;
                        }
                    }
                }
                myBitmap.Dispose();
                return false;
            });
        }

        /// <summary>
        /// Метод получения страниц из пдф файла
        /// </summary>
        /// <param name="pathToFile">Путь к файлу пдф</param>
        /// <returns>Страницы</returns>
        public async Task<List<MyPDFPage>> GetPdfPages(string pathToFile)
        {
            List<MyPDFPage> pdfPages = new List<MyPDFPage>();
            using (var doc = PdfDocument.Load(pathToFile))
            {
                for (int i = 0; i < doc.Pages.Count; i++)
                {
                    MyPDFPage myPDFFile = new MyPDFPage(i + 1, doc.Pages[i].Width * 0.3528f, doc.Pages[i].Height * 0.3528f);
                    if (myPDFFile.SizeType == PageSizeTypes.A3)
                    {
                        myPDFFile.isColored = await CheckIsColored(PdfToBitmap(doc.Pages[i]));
                        if (!myPDFFile.isColored)
                        {
                            myPDFFile.SizeType = PageSizeTypes.A3_BW;
                        }
                    }
                    if (myPDFFile.SizeType == PageSizeTypes.A4)
                    {
                        myPDFFile.isColored = await CheckIsColored(PdfToBitmap(doc.Pages[i]));
                        if (!myPDFFile.isColored)
                        {
                            myPDFFile.SizeType = PageSizeTypes.A4_BW;
                        }
                    }
                    pdfPages.Add(myPDFFile);
                }
            }
            return pdfPages;
        }
    }
}
