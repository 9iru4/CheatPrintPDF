using System.Collections.Generic;

namespace CheatPrintPDF
{

    public enum PageSizeTypes
    {
        A4, A4xUncolored, A4x3, A4x4, A4x5, A4x6, A4x7, A4x8, A4x9, A3, A3xUncolored, A3x3, A3x4, A3x5, A3x6, A3x7, A3x8, A3x9, A2, A2x3, A2x4, A2x5, A2x6, A2x7, A2x8, A2x9, A1, A1x3, A1x4, A1x5, A1x6, A1x7, A1x8, A1x9, A0, A0x2, A0x3, A0x4, A0x5, A0x6, A0x7, A0x8, A0x9, None
    }

    public class PagesResult
    {
        public PageSizeTypes PagesType { get; set; }

        public List<int> Pages { get; set; }

        public string CopyPages { get; set; }

        public PagesResult(PageSizeTypes pageSizeTypes)
        {
            PagesType = pageSizeTypes;
            Pages = new List<int>();
        }

        public void SetCopyPages()
        {
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
