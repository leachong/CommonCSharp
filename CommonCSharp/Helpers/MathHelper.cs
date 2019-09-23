using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Helpers
{
    public class MathHelper
    {
        #region 单位转换
        public static double PointToMillimeter(double pt) => pt / 72.0 * 25.4;
        public static double InchToMillimeter(double inch) => inch * 25.4;

        public static double MillimeterToPoint(double mm) => mm / 25.4 * 72.0;
        public static double InchToPoint(double inch) => inch * 72.0;

        public static double PointToInch(double pt) => pt / 72.0;
        public static double MillimeterToInch(double mm) => mm / 25.4;
        #endregion

        #region 尺寸比例计算

        /// <summary>
        /// 计算缩放比例
        /// 按比例缩放 & 能显示全部内容 & 留空白
        /// </summary>
        /// <param name="srcWidth"></param>
        /// <param name="srcHeight"></param>
        /// <param name="dstWidth"></param>
        /// <param name="dstHeight"></param>
        /// <returns></returns>
        public static double CalcScaleRadio(double srcWidth, double srcHeight, double dstWidth, double dstHeight)
        {
            double result;
            var radioSrc = srcWidth / srcHeight;
            var radioDst = dstWidth / dstHeight;
            if (radioSrc > radioDst)
            {
                result = dstWidth / srcWidth;
            }
            else
            {
                result = dstHeight / srcHeight;
            }
            return result;
        }

        #endregion



    }
}
