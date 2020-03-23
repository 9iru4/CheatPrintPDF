using System.Collections.Generic;

namespace CheatPrintPDF
{
    public class MyPDFFile : EqualityComparer<MyPDFFile>
    {
        public int PageNumber { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool isColored { get; set; }
        public PageSizeTypes SizeType { get; set; }

        public MyPDFFile(int pageNumber, double width, double height)
        {
            PageNumber = pageNumber;
            Width = width;
            Height = height;
            SetPageSizeType();
            isColored = false;
        }

        public void SetPageSizeType()
        {
            var side = Width > Height ? Width : Height;

            if ((250 < Width && Width < 330 || 250 < Height && Height < 330) && (170 < Width && Width < 240 || 170 < Height && Height < 240))
            {
                //a4
                SizeType = PageSizeTypes.A4;
                return;
            }
            if ((250 < Width && Width < 330 || 250 < Height && Height < 330) && (370 < Width && Width < 460 || 370 < Height && Height < 460))
            {
                //a3
                SizeType = PageSizeTypes.A3;
                return;
            }
            if ((370 < Width && Width < 460 || 370 < Height && Height < 460) && (550 < Width && Width < 650 || 550 < Height && Height < 650))
            {
                //a2
                SizeType = PageSizeTypes.A2;
                return;
            }
            if ((550 < Width && Width < 650 || 550 < Height && Height < 650) && (800 < Width && Width < 900 || 800 < Height && Height < 900))
            {
                //a1
                SizeType = PageSizeTypes.A1;
                return;
            }
            if ((800 < Width && Width < 900 || 800 < Height && Height < 900) && (1150 < Width && Width < 1250 || 1150 < Height && Height < 1250))
            {
                //a0
                SizeType = PageSizeTypes.A0;
                return;
            }

            if ((250 < Width && Width < 330 || 250 < Height && Height < 330))
            {
                if (170 < side / 3 && side / 3 < 240)
                {
                    SizeType = PageSizeTypes.A4x3;
                    return;
                }
                if (170 < side / 4 && side / 4 < 240)
                {
                    SizeType = PageSizeTypes.A4x4;
                    return;
                }
                if (170 < side / 5 && side / 5 < 240)
                {
                    SizeType = PageSizeTypes.A4x5;
                    return;
                }
                if (170 < side / 6 && side / 6 < 240)
                {
                    SizeType = PageSizeTypes.A4x6;
                    return;
                }
                if (170 < side / 7 && side / 7 < 240)
                {
                    SizeType = PageSizeTypes.A4x7;
                    return;
                }
                if (170 < side / 8 && side / 8 < 240)
                {
                    SizeType = PageSizeTypes.A4x8;
                    return;
                }
                if (170 < side / 9 && side / 9 < 240)
                {
                    SizeType = PageSizeTypes.A4x9;
                    return;
                }
            }
            if ((370 < Width && Width < 460 || 370 < Height && Height < 460))
            {
                if (250 < side / 3 && side / 3 < 330)
                {
                    //a3x3
                    SizeType = PageSizeTypes.A3x3;
                    return;
                }
                if (250 < side / 4 && side / 4 < 330)
                {
                    //a3x4
                    SizeType = PageSizeTypes.A3x4;
                    return;
                }
                if (250 < side / 5 && side / 5 < 330)
                {
                    //a3x5
                    SizeType = PageSizeTypes.A3x5;
                    return;
                }
                if (250 < side / 6 && side / 6 < 330)
                {
                    //a3x6
                    SizeType = PageSizeTypes.A3x6;
                    return;
                }
                if (250 < side / 7 && side / 7 < 330)
                {
                    //a3x7
                    SizeType = PageSizeTypes.A3x7;
                    return;
                }
                if (250 < side / 8 && side / 8 < 330)
                {
                    //a3x6
                    SizeType = PageSizeTypes.A3x8;
                    return;
                }
                if (250 < side / 9 && side / 9 < 330)
                {
                    //a3x7
                    SizeType = PageSizeTypes.A3x9;
                    return;
                }
            }
            if ((550 < Width && Width < 650 || 550 < Height && Height < 650))
            {
                if (370 < side / 3 && side / 3 < 460)
                {
                    //a2x3
                    SizeType = PageSizeTypes.A2x3;
                    return;
                }
                if (370 < side / 4 && side / 4 < 460)
                {
                    //a2x4
                    SizeType = PageSizeTypes.A2x4;
                    return;
                }
                if (370 < side / 5 && side / 5 < 460)
                {
                    //a2x5
                    SizeType = PageSizeTypes.A2x5;
                    return;
                }
                if (370 < side / 6 && side / 6 < 460)
                {
                    //a2x6
                    SizeType = PageSizeTypes.A2x6;
                    return;
                }
                if (370 < side / 7 && side / 7 < 460)
                {
                    //a2x7
                    SizeType = PageSizeTypes.A2x7;
                    return;
                }
                if (370 < side / 8 && side / 8 < 460)
                {
                    //a2x8
                    SizeType = PageSizeTypes.A2x8;
                    return;
                }
                if (370 < side / 9 && side / 9 < 460)
                {
                    //a2x9
                    SizeType = PageSizeTypes.A2x9;
                    return;
                }
            }
            if ((800 < Width && Width < 900 || 800 < Height && Height < 900))
            {
                if (550 < side / 3 && side / 3 < 650)
                {
                    //a1x3
                    SizeType = PageSizeTypes.A1x3;
                    return;
                }
                if (550 < side / 4 && side / 4 < 650)
                {
                    //a1x4
                    SizeType = PageSizeTypes.A1x4;
                    return;
                }
                if (550 < side / 5 && side / 5 < 650)
                {
                    //a1x5
                    SizeType = PageSizeTypes.A1x5;
                    return;
                }
                if (550 < side / 6 && side / 6 < 650)
                {
                    //a1x6
                    SizeType = PageSizeTypes.A1x6;
                    return;
                }
                if (550 < side / 7 && side / 7 < 650)
                {
                    //a1x7
                    SizeType = PageSizeTypes.A1x7;
                    return;
                }
                if (550 < side / 8 && side / 8 < 650)
                {
                    //a1x8
                    SizeType = PageSizeTypes.A1x8;
                    return;
                }
                if (550 < side / 9 && side / 9 < 650)
                {
                    //a1x9
                    SizeType = PageSizeTypes.A1x9;
                    return;
                }
            }
            if ((1150 < Width && Width < 1250 || 1150 < Height && Height < 1250))
            {
                if (800 < side / 2 && side / 2 < 900)
                {
                    //a0x2
                    SizeType = PageSizeTypes.A0x2;
                    return;
                }
                if (800 < side / 3 && side / 3 < 900)
                {
                    //a0x3
                    SizeType = PageSizeTypes.A0x3;
                    return;
                }
                if (800 < side / 4 && side / 4 < 900)
                {
                    //a0x4
                    SizeType = PageSizeTypes.A0x4;
                    return;
                }
                if (800 < side / 5 && side / 5 < 900)
                {
                    //a0x5
                    SizeType = PageSizeTypes.A0x5;
                    return;
                }
                if (800 < side / 6 && side / 6 < 900)
                {
                    //a0x6
                    SizeType = PageSizeTypes.A0x6;
                    return;
                }
                if (800 < side / 7 && side / 7 < 900)
                {
                    //a0x7
                    SizeType = PageSizeTypes.A0x7;
                    return;
                }
                if (800 < side / 8 && side / 8 < 900)
                {
                    //a0x8
                    SizeType = PageSizeTypes.A0x8;
                    return;
                }
                if (800 < side / 9 && side / 9 < 900)
                {
                    //a0x9
                    SizeType = PageSizeTypes.A0x9;
                    return;
                }
            }
            SizeType = PageSizeTypes.None;
            return;
        }

        public override bool Equals(MyPDFFile x, MyPDFFile y)
        {
            if (x.PageNumber == y.PageNumber) return true;
            else return false;
        }

        public override int GetHashCode(MyPDFFile obj)
        {
            return (int)(obj.Width * obj.Height);
        }
    }
}
