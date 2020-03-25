using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TallComponents.PDF.Rasterizer;

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
        public Image PdfToBitmap(string pathToFile, int pageNumber)
        {
            Document pdfDocument = new Document(new FileStream(pathToFile, FileMode.Open, FileAccess.Read));
            Page pdfPage = pdfDocument.Pages[pageNumber];
            // create a bitmap to draw to and a graphics object
            Bitmap bitmap = new Bitmap((int)pdfPage.Width, (int)pdfPage.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                //draw the image from the first page to the graphics object, which is connected to the bitmap object
                pdfPage.Draw(graphics);
                return bitmap;
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

            for (int i = 0; i < (int)PageSizeTypes.None + 1; i++)
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
        public async Task<bool> CheckIsColored(Image image)
        {
            return await Task.Run(() =>
            {
                using (Bitmap myBitmap = new Bitmap(image))
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
                }
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
            using (var doc = PdfReader.Open(pathToFile))
            {
                for (int i = 0; i < doc.Pages.Count; i++)
                {
                    MyPDFPage myPDFFile = new MyPDFPage(i + 1, doc.Pages[i].Width * 0.3528f, doc.Pages[i].Height * 0.3528f);
                    if (myPDFFile.SizeType == PageSizeTypes.A3)
                    {
                        myPDFFile.isColored = await CheckIsColored(PdfToBitmap(pathToFile, i));
                        if (!myPDFFile.isColored)
                        {
                            myPDFFile.SizeType = PageSizeTypes.A3_BW;
                        }
                    }
                    if (myPDFFile.SizeType == PageSizeTypes.A4)
                    {
                        myPDFFile.isColored = await CheckIsColored(PdfToBitmap(pathToFile, i));
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
