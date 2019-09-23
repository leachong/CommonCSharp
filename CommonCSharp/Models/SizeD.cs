using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Models
{
#pragma warning disable CS0659 // “SizeD”重写 Object.Equals(object o) 但不重写 Object.GetHashCode()
#pragma warning disable CS0661 // “SizeD”定义运算符 == 或运算符 !=，但不重写 Object.GetHashCode()
    public struct SizeD
#pragma warning restore CS0661 // “SizeD”定义运算符 == 或运算符 !=，但不重写 Object.GetHashCode()
#pragma warning restore CS0659 // “SizeD”重写 Object.Equals(object o) 但不重写 Object.GetHashCode()
    {
        public static SizeD Empty { get; } = new SizeD(0,0);
        public bool IsEmpty => Empty == this;

        public double Width { get; set; }
        public double Height { get; set; }

        public SizeD(SizeD size)
        {
            Width = size.Width;
            Height = size.Height;
        }
        public SizeD(PointD pt)
        {
            Width = pt.X;
            Height = pt.Y;
        }
        public SizeD(float width, float height)
        {
            Width = width;
            Height = height;
        }
        public SizeD(double width, double height)
        {
            Width = width;
            Height = height;
        }

        
        public static SizeD Add(SizeD sz1, SizeD sz2)
        {
            sz1 += sz2;
            return sz1;
        }
        public static SizeD Subtract(SizeD sz1, SizeD sz2)
        {
            sz1 -= sz2;
            return sz1;
        }
        public override bool Equals(object obj)
        {
            var sz2 = (SizeD)obj;
            return sz2 == this;
        }
        public override string ToString()
        {
            return string.Format("{{0},{1}}", Width, Height);
        }
        public PointD ToPointD()
        {
            return new PointD(Width, Height);
        }

        public static SizeD operator +(SizeD sz1, SizeD sz2)
        {
            sz1.Width += sz2.Width;
            sz1.Height += sz2.Height;
            return sz1;
        }
        public static SizeD operator -(SizeD sz1, SizeD sz2)
        {
            sz1.Width -= sz2.Width;
            sz1.Height -= sz2.Height;
            return sz1;
        }
        public static bool operator ==(SizeD sz1, SizeD sz2)
        {
            return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
        }
        public static bool operator !=(SizeD sz1, SizeD sz2)
        {
            return sz1.Width != sz2.Width || sz1.Height != sz2.Height;
        }
        
        public static explicit operator PointD(SizeD size)
        {
            return new PointD(size.Width, size.Height);
        }
    }
}
