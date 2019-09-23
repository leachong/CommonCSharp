using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CommonCSharp.Extend
{
    public static class EnumExtend
    {
        /// <summary>
        /// 获取枚举变量的整型值
        /// </summary>
        /// <param name="f">枚举变量</param>
        /// <returns>枚举变量的int值</returns>
        public static int ToInt32(this Enum f)
        {
            return Convert.ToInt32(f);
        }

        /// <summary>
        /// 获取枚举变量的内置描述信息
        /// </summary>
        /// <param name="f">枚举变量</param>
        /// <returns>枚举值的描述信息</returns>
        public static string Description(this Enum f)
        {
            string des = f.ToString();

            des = GetDes(f, des);

            return des;
        }

        private static string GetDes(Enum f, string des)
        {
            var fieldInfo = f.GetType().GetField(des);
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                des = attributes[0].Description;
            }
            else
            {
                des = "Unknown";
            }

            return des;
        }
    }
    
}
