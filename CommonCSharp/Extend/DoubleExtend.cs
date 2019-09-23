using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Extend
{
    public static class DoubleExtend
    {
        const double _offset = 0.001;

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>1:大于；0:等于；-1:小于</returns>
        public static int Compare2(this double a, double b, double precision = _offset)
        {
            var result = 0;
            var o = a - b;
            if (o > precision)
            {
                result = 1;
            }
            else if (o < precision)
            {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 比较，是否大于
        /// </summary>
        /// <returns></returns>
        public static bool Greater2(this double a, double b, double precision = _offset) => a - b > precision;
        /// <summary>
        /// 比较，是否大于
        /// </summary>
        /// <returns></returns>
        public static bool Less2(this double a, double b, double precision = _offset) => b - a > precision;
        /// <summary>
        /// 判断两个double是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equals2(this double a, double b, double precision = _offset) => Math.Abs(a - b) < precision;

        /// <summary>
        /// 向上取整，如果相等，如0.999999999 返回1
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int ToNearestUp(this double v, double precision = _offset) => Equals2(v, (int)v+1, precision) ? (int)v+1 : (int)v;
    }
}
