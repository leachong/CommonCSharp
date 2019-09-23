using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Extend
{
    public static class EnumerableExtend
    {
        public static int Contains<TSource>(this IEnumerable<TSource> source, TSource value, Func<TSource, TSource, bool> comparer)
        {
            int index = 0;
            foreach (var item in source)
            {
                if (comparer(item,value))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}
