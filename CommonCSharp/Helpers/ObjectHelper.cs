using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Helpers
{
    public class ObjectHelper
    {
        public static void ExchangeValue<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}
