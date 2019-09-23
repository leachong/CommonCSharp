using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Extend
{
    public static class Int32Extend
    {
        public static T ToEnum<T>(this Int32 i) => (T)Enum.ToObject(typeof(T), i);

        /// <summary>
        /// 是否奇数
        /// </summary>
        /// <param name="i"></param>
        /// <returns>true:奇数；false：偶数</returns>
        public static bool IsOdd(this Int32 i) => (i & 1) == 1;

        public static Int32 NextIndex(this Int32 i, Int32 max)
        {
            i++;
            if (i >= max)
                i = 0;
            return i;
        }
    }
}
