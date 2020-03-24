using System.Collections.Generic;

namespace CheatPrintPDF
{
    /// <summary>
    /// Типы страниц
    /// </summary>
    public enum PageSizeTypes
    {
        //_BW - черно-белый
        A4, A4_BW, A4x3, A4x4, A4x5, A4x6, A4x7, A4x8, A4x9, A3, A3_BW, A3x3, A3x4, A3x5, A3x6, A3x7, A3x8, A3x9, A2, A2x3, A2x4, A2x5, A2x6, A2x7, A2x8, A2x9, A1, A1x3, A1x4, A1x5, A1x6, A1x7, A1x8, A1x9, A0, A0x2, A0x3, A0x4, A0x5, A0x6, A0x7, A0x8, A0x9, None
    }

    /// <summary>
    /// Класс описывающий сгруппированные страницы по формату
    /// </summary>
    public class PagesResult
    {
        /// <summary>
        /// Тип страниц
        /// </summary>
        PageSizeTypes PagesType { get; set; }

        /// <summary>
        /// Номера страниц
        /// </summary>
        public List<int> Pages { get; set; }

        /// <summary>
        /// Номера страниц для копирования
        /// </summary>
        public string CopyPages { get; set; }

        /// <summary>
        /// Тип страниц для отображения
        /// </summary>
        public string PagesTypeText { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="pageSizeTypes">Тип страниц</param>
        public PagesResult(PageSizeTypes pageSizeTypes)
        {
            PagesType = pageSizeTypes;
            Pages = new List<int>();
        }

        /// <summary>
        /// Метод устанавливающий текст для копирования и страницы
        /// </summary>
        public void SetCopyPagesAndText()
        {
            if (PageSizeTypes.A4_BW == PagesType)
            {
                PagesTypeText = "А4(чб)";
            }
            else if (PageSizeTypes.A4 == PagesType)
            {
                PagesTypeText = "А4(ц)";
            }
            else if (PageSizeTypes.A3_BW == PagesType)
            {
                PagesTypeText = "A3(чб)";
            }
            else if (PageSizeTypes.A3 == PagesType)
            {
                PagesTypeText = "A3(ц)";
            }
            else
            {
                PagesTypeText = PagesType.ToString();
            }
            //Алгоритм сокращения идущих подряд страниц до вида 1-5
            bool skip = false;
            for (int i = 0; i < Pages.Count; i++)
            {
                if (!skip)
                    CopyPages += Pages[i];
                if (i + 1 < Pages.Count && Pages[i + 1] - 1 == Pages[i])
                {
                    skip = true;
                }
                else if (i + 1 >= Pages.Count)
                {
                    if (skip)
                    {
                        CopyPages += "-" + Pages[i] + ";";
                        skip = false;
                    }
                    else
                    {
                        CopyPages += ";";
                    }
                }
                else
                {
                    if (skip)
                    {
                        CopyPages += "-" + Pages[i] + ",";
                        skip = false;
                    }
                    else
                    {
                        CopyPages += ",";
                    }
                }
            }
        }
    }
}
