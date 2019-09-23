using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Extend
{
    public static class CharExtend
    {
        public static bool IsNumber(this char c) => c >= '0' && c <= '9';
    }
}
