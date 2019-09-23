using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Helpers
{
    public class ColorHelper
    {
        public static byte[] Cmyk2Rgb(byte[] cmyk100, byte[] rgb255 = null)
        {
            var c = cmyk100[0] / 100.0;
            var m = cmyk100[1] / 100.0;
            var y = cmyk100[2] / 100.0;
            var k = cmyk100[3] / 100.0;

            double r, g, b;
            r = 1 - Math.Min(1, c * (1 - k) + k);
            g = 1 - Math.Min(1, m * (1 - k) + k);
            b = 1 - Math.Min(1, y * (1 - k) + k);

            rgb255 = rgb255 ?? new byte[3];
            rgb255[0] = (byte)Math.Round(r * 255);
            rgb255[1] = (byte)Math.Round(g * 255);
            rgb255[2] = (byte)Math.Round(b * 255);

            return rgb255;
        }
        public static byte[] Rgb2Cmyk(byte[] rgb255, byte[] cmyk100 = null)
        {
            var r = rgb255[0] / 255.0;
            var g = rgb255[1] / 255.0;
            var b = rgb255[2] / 255.0;

            double y, m, c, k;
            k = Math.Min(Math.Min(1 - r, 1 - g), 1 - b);
            if ((1 - k) == 0)
            {
                c = 0;
                m = 0;
                y = 0;
            }
            else
            {
                c = (1 - r - k) / (1 - k);
                m = (1 - g - k) / (1 - k);
                y = (1 - b - k) / (1 - k);
            }

            cmyk100 = cmyk100 ?? new byte[4];
            cmyk100[0] = (byte)Math.Round(c * 100);
            cmyk100[1] = (byte)Math.Round(m * 100);
            cmyk100[2] = (byte)Math.Round(y * 100);
            cmyk100[3] = (byte)Math.Round(k * 100);

            return cmyk100;
        }
    }
}
