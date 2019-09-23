using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Models
{
#pragma warning disable CS0659 // “RectangleD”重写 Object.Equals(object o) 但不重写 Object.GetHashCode()
#pragma warning disable CS0661 // “RectangleD”定义运算符 == 或运算符 !=，但不重写 Object.GetHashCode()
    /// <summary>
    /// 原点x,y左上角
    /// </summary>
    public struct RectangleD
#pragma warning restore CS0661 // “RectangleD”定义运算符 == 或运算符 !=，但不重写 Object.GetHashCode()
#pragma warning restore CS0659 // “RectangleD”重写 Object.Equals(object o) 但不重写 Object.GetHashCode()
    {
        public static RectangleD Empty { get; } = new RectangleD(0, 0, 0, 0);
        public bool IsEmpty => this == Empty;

        public double Right { get; private set; }

        public double Top { get; private set; }

        public double Left { get; private set; }
        public double Bottom { get; private set; }

        public double Height
        {
            get
            {
                return Bottom - Top;
            }
            set
            {
                Bottom = Top + value;
            }
        }

        public double Width
        {
            get
            {
                return Right - Left;
            }
            set
            {
                Right = Left + value;
            }
        }

        public double Y {
            get
            {
                return Top;
            }
            set
            {
                Top = value;
            }
        }

        public double X
        {
            get
            {
                return Left;
            }
            set
            {
                Left = value;
            }
        }
        public SizeD Size
        {
            get
            {
                return new SizeD(Width, Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public PointD Location
        {
            get
            {
                return new PointD(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }


        public RectangleD(PointD location, SizeD size)
        {
            Left = 0;
            Right = 0;
            Top = 0;
            Bottom = 0;

            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }
        public RectangleD(float x, float y, float width, float height)
        {
            Left = 0;
            Right = 0;
            Top = 0;
            Bottom = 0;

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public RectangleD(double x, double y, double width, double height)
        {
            Left = 0;
            Right = 0;
            Top = 0;
            Bottom = 0;

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        
        public static RectangleD FromLTRB(float left, float top, float right, float bottom)
        {
            var result = new RectangleD();
            result.Left = left;
            result.Top = top;
            result.Right = right;
            result.Bottom = bottom;
            return result;
        }
        public static RectangleD FromLTRB(double left, double top, double right, double bottom)
        {
            var result = new RectangleD();
            result.Left = left;
            result.Top = top;
            result.Right = right;
            result.Bottom = bottom;
            return result;
        }
        /*
        public static PdfRectangle Inflate(PdfRectangle rect, double x, double y);
        //
        // 摘要:
        //     Returns a BitMiracle.Docotic.Pdf.PdfRectangle structure that represents the intersection
        //     of two rectangles. If there is no intersection, an empty BitMiracle.Docotic.Pdf.PdfRectangle
        //     is returned.
        //
        // 参数:
        //   a:
        //     First rectangle to intersect.
        //
        //   b:
        //     Second rectangle to intersect.
        //
        // 返回结果:
        //     A BitMiracle.Docotic.Pdf.PdfRectangle structure that represents the intersection
        //     of two rectangles. If there is no intersection, an empty BitMiracle.Docotic.Pdf.PdfRectangle
        //     is returned.
        public static PdfRectangle Intersect(PdfRectangle a, PdfRectangle b);
        //
        // 摘要:
        //     Creates the smallest possible third rectangle that can contain both of two rectangles
        //     that form a union.
        //
        // 参数:
        //   a:
        //     First rectangle to union.
        //
        //   b:
        //     Second rectangle to union.
        //
        // 返回结果:
        //     A third BitMiracle.Docotic.Pdf.PdfRectangle structure that contains both of the
        //     two rectangles that form the union.
        public static PdfRectangle Union(PdfRectangle a, PdfRectangle b);
        //
        // 摘要:
        //     Determines if the specified point is contained within this BitMiracle.Docotic.Pdf.PdfRectangle
        //     structure.
        //
        // 参数:
        //   x:
        //     The x-coordinate of the point to test.
        //
        //   y:
        //     The y-coordinate of the point to test.
        //
        // 返回结果:
        //     This method returns true if the point defined by x and y is contained within
        //     this BitMiracle.Docotic.Pdf.PdfRectangle structure; otherwise false.
        public bool Contains(double x, double y);
        //
        // 摘要:
        //     Determines if the specified point is contained within this BitMiracle.Docotic.Pdf.PdfRectangle
        //     structure.
        //
        // 参数:
        //   pt:
        //     The BitMiracle.Docotic.Pdf.PdfPoint to test.
        //
        // 返回结果:
        //     This method returns true if the point represented by the pt parameter is contained
        //     within this BitMiracle.Docotic.Pdf.PdfRectangle structure; otherwise false.
        public bool Contains(PdfPoint pt);
        //
        // 摘要:
        //     Determines if the specified point is contained within this BitMiracle.Docotic.Pdf.PdfRectangle
        //     structure.
        //
        // 参数:
        //   pt:
        //     The System.Drawing.PointF to test.
        //
        // 返回结果:
        //     This method returns true if the point represented by the pt parameter is contained
        //     within this BitMiracle.Docotic.Pdf.PdfRectangle structure; otherwise false.
        public bool Contains(PointF pt);
        //
        // 摘要:
        //     Determines if the rectangular region represented by rect is entirely contained
        //     within this BitMiracle.Docotic.Pdf.PdfRectangle structure.
        //
        // 参数:
        //   rect:
        //     The BitMiracle.Docotic.Pdf.PdfRectangle to test.
        //
        // 返回结果:
        //     This method returns true if the rectangular region represented by rect is entirely
        //     contained within the rectangular region represented by this BitMiracle.Docotic.Pdf.PdfRectangle;
        //     otherwise false.
        public bool Contains(PdfRectangle rect);
        //
        // 摘要:
        //     Determines if the rectangular region represented by rect is entirely contained
        //     within this BitMiracle.Docotic.Pdf.PdfRectangle structure.
        //
        // 参数:
        //   rect:
        //     The System.Drawing.RectangleF to test.
        //
        // 返回结果:
        //     This method returns true if the rectangular region represented by rect is entirely
        //     contained within the rectangular region represented by this BitMiracle.Docotic.Pdf.PdfRectangle;
        //     otherwise false.
        public bool Contains(RectangleF rect);
        //
        // 摘要:
        //     Determines if the specified point is contained within this BitMiracle.Docotic.Pdf.PdfRectangle
        //     structure.
        //
        // 参数:
        //   x:
        //     The x-coordinate of the point to test.
        //
        //   y:
        //     The y-coordinate of the point to test.
        //
        // 返回结果:
        //     This method returns true if the point defined by x and y is contained within
        //     this BitMiracle.Docotic.Pdf.PdfRectangle structure; otherwise false.
        public bool Contains(float x, float y);
        //
        // 摘要:
        //     Gets the hash code for this BitMiracle.Docotic.Pdf.PdfRectangle structure.
        //
        // 返回结果:
        //     The hash code for this BitMiracle.Docotic.Pdf.PdfRectangle.
        public override int GetHashCode();
        //
        // 摘要:
        //     Inflates this BitMiracle.Docotic.Pdf.PdfRectangle by the specified amount.
        //
        // 参数:
        //   x:
        //     The amount to inflate this BitMiracle.Docotic.Pdf.PdfRectangle structure horizontally.
        //
        //   y:
        //     The amount to inflate this BitMiracle.Docotic.Pdf.PdfRectangle structure vertically.
        public void Inflate(double x, double y);
        //
        // 摘要:
        //     Inflates this BitMiracle.Docotic.Pdf.PdfRectangle by the specified amount.
        //
        // 参数:
        //   x:
        //     The amount to inflate this BitMiracle.Docotic.Pdf.PdfRectangle structure horizontally.
        //
        //   y:
        //     The amount to inflate this BitMiracle.Docotic.Pdf.PdfRectangle structure vertically.
        public void Inflate(float x, float y);
        //
        // 摘要:
        //     Inflates this BitMiracle.Docotic.Pdf.PdfRectangle by the specified amount.
        //
        // 参数:
        //   size:
        //     The amount to inflate this rectangle.
        public void Inflate(PdfSize size);
        //
        // 摘要:
        //     Inflates this BitMiracle.Docotic.Pdf.PdfRectangle by the specified amount.
        //
        // 参数:
        //   size:
        //     The amount to inflate this rectangle.
        public void Inflate(SizeF size);
        //
        // 摘要:
        //     Replaces this BitMiracle.Docotic.Pdf.PdfRectangle structure with the intersection
        //     of itself and the specified BitMiracle.Docotic.Pdf.PdfRectangle structure.
        //
        // 参数:
        //   rect:
        //     The rectangle to intersect.
        public void Intersect(PdfRectangle rect);
        //
        // 摘要:
        //     Replaces this BitMiracle.Docotic.Pdf.PdfRectangle structure with the intersection
        //     of itself and the specified System.Drawing.RectangleF structure.
        //
        // 参数:
        //   rect:
        //     The rectangle to intersect.
        public void Intersect(RectangleF rect);
        //
        // 摘要:
        //     Determines if this rectangle intersects with rect.
        //
        // 参数:
        //   rect:
        //     The rectangle to test.
        //
        // 返回结果:
        //     This method returns true if there is any intersection.
        public bool IntersectsWith(PdfRectangle rect);
        //
        // 摘要:
        //     Determines if this rectangle intersects with rect.
        //
        // 参数:
        //   rect:
        //     The rectangle to test.
        //
        // 返回结果:
        //     This method returns true if there is any intersection.
        public bool IntersectsWith(RectangleF rect);
        //
        // 摘要:
        //     Adjusts the location of this rectangle by the specified amount.
        //
        // 参数:
        //   pos:
        //     The amount to offset the location.
        public void Offset(PointF pos);
        //
        // 摘要:
        //     Adjusts the location of this rectangle by the specified amount.
        //
        // 参数:
        //   x:
        //     The amount to offset the location horizontally.
        //
        //   y:
        //     The amount to offset the location vertically.
        public void Offset(double x, double y);
        //
        // 摘要:
        //     Adjusts the location of this rectangle by the specified amount.
        //
        // 参数:
        //   x:
        //     The amount to offset the location horizontally.
        //
        //   y:
        //     The amount to offset the location vertically.
        public void Offset(float x, float y);
        //
        // 摘要:
        //     Adjusts the location of this rectangle by the specified amount.
        //
        // 参数:
        //   pos:
        //     The amount to offset the location.
        public void Offset(PdfPoint pos);
        */

        public override bool Equals(object obj)
        {
            var sz2 = (RectangleD)obj;
            return sz2 == this;
        }
        public override string ToString()
        {
            return string.Format("{{0},{1},{2},{3}}", X, Y, Width, Height);
        }

        public static bool operator ==(RectangleD left, RectangleD right)
        {
            return left.Width == right.Width && left.Height == right.Height && left.X == right.X && left.Y == right.Y;
        }
        public static bool operator !=(RectangleD left, RectangleD right)
        {
            return left.Width != right.Width || left.Height != right.Height || left.X != right.X || left.Y != right.Y;
        }
        
    }
}
