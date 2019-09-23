using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Models
{
#pragma warning disable CS0659 // “PointD”重写 Object.Equals(object o) 但不重写 Object.GetHashCode()
#pragma warning disable CS0661 // “PointD”定义运算符 == 或运算符 !=，但不重写 Object.GetHashCode()
    public class PointD
#pragma warning restore CS0661 // “PointD”定义运算符 == 或运算符 !=，但不重写 Object.GetHashCode()
#pragma warning restore CS0659 // “PointD”重写 Object.Equals(object o) 但不重写 Object.GetHashCode()
    {
        public static PointD Empty { get; } = new PointD(0, 0);
        public bool IsEmpty => Empty == this;
        public double X { get; set; }
        public double Y { get; set; }

        public PointD(SizeD size)
        {
            X = size.Width;
            Y = size.Height;
        }
        public PointD(PointD pt)
        {
            X = pt.X;
            Y = pt.Y;
        }
        public PointD(float x, float y)
        {
            X = x;
            Y = y;
        }
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static PointD Add(PointD sz1, PointD sz2)
        {
            sz1 += sz2;
            return sz1;
        }
        public static PointD Subtract(PointD sz1, PointD sz2)
        {
            sz1 -= sz2;
            return sz1;
        }
        public override bool Equals(object obj)
        {
            var sz2 = (PointD)obj;
            return sz2 == this;
        }
        public override string ToString()
        {
            return string.Format("{{0},{1}}", X, Y);
        }
        public SizeD ToSizeD()
        {
            return new SizeD(X, Y);
        }

        public static PointD operator +(PointD sz1, PointD sz2)
        {
            sz1.X += sz2.X;
            sz1.Y += sz2.Y;
            return sz1;
        }
        public static PointD operator -(PointD sz1, PointD sz2)
        {
            sz1.X -= sz2.X;
            sz1.Y -= sz2.Y;
            return sz1;
        }
        public static bool operator ==(PointD sz1, PointD sz2)
        {
            return sz1.X == sz2.X && sz1.Y == sz2.Y;
        }
        public static bool operator !=(PointD sz1, PointD sz2)
        {
            return sz1.X != sz2.X || sz1.Y != sz2.Y;
        }

        public static explicit operator SizeD(PointD pt)
        {
            return new SizeD(pt.X, pt.Y);
        }
    }
}
