using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonCSharp.Extend
{
    public static class StringExtend
    {
        public static T ToEnum<T>(this string str) => (T)Enum.Parse(typeof(T), str);
        public static double ToDouble(this string str) => Convert.ToDouble(str);
        public static int ToInt32(this string str) => Convert.ToInt32(str);
        public static bool ToBoolean(this string str)
        {
            try
            {
                return Convert.ToBoolean(str);
            }
            catch (FormatException fe)
            {
                try
                {
                    return Convert.ToBoolean(str.ToInt32());
                }
                catch (Exception)
                {
                    throw fe;
                }
            }
        }


        public static char GetPathSeparator()
        {
            return Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix ? '/' : '\\';
        }
        /// <summary>
        /// 路径字符串最后添加节点
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string AppendPathComponent(this string str, string pathComponent)
        {
            return Path.Combine(str, pathComponent);
        }
        /// <summary>
        /// 移除路径字符串最后的节点
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string RemoveLastPathComponent(this string str, char separator = ' ')
        {
            if (separator == ' ')
                separator = GetPathSeparator();

            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            
            if (str.Length >= 1)
            {
                if (str[str.Length - 1] == separator)
                {
                    str = str.Remove(str.Length - 1);
                }
            }
            var lastIndex = str.LastIndexOf(separator);
            if (lastIndex>-1)
            {
                str = str.Remove(lastIndex);

            }
            return str;
        }

        /// <summary>
        /// 去除路径字符串末尾的斜杠
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string RemoveFilePathSeparator(this string str, char separator = ' ')
        {
            if (separator == ' ')
                separator = GetPathSeparator();
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str[str.Length - 1] != separator)
            {
                return str;
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 添加路径字符串末尾的斜杠
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string AppendFilePathSeparator(this string str, char separator = ' ')
        {
            if (separator == ' ')
                separator = GetPathSeparator();
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str[str.Length - 1] != separator)
            {
                return str + separator;
            }
            return str;
        }

        /// <summary>
        /// 去除路径字符串开头的.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string RemoveDotForExtension(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str[0] == '.')
            {
                return str.Substring(1, str.Length - 1);
            }
            return str;
        }
        /// <summary>
        /// 添加路径字符串开头的.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string InsertDotForExtension(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str[0] == '.')
            {
                return str;
            }
            return '.' + str;
        }
        /// <summary>
        /// 将指定的所有字符替换为新的字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldChars"></param>
        /// <param name="newChar"></param>
        /// <returns></returns>
        public static string Replace(this string str, string oldChars, char newChar)
        {
            return Replace(str, oldChars.ToArray(), newChar);
        }
        /// <summary>
        /// 将指定的所有字符替换为新的字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldChars"></param>
        /// <param name="newChar"></param>
        /// <returns></returns>
        public static string Replace(this string str, char[] oldChars, char newChar)
        {
            for (int i = 0; oldChars != null && i < oldChars.Length; i++)
            {
                str = str.Replace(oldChars[i], newChar);
            }
            return str;
        }
    }
}
