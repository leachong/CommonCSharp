using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CommonCSharp.Extend
{
    public static class XmlNodeExtend
    {
        public static string GetValueString(this XmlNode node) => node?.InnerText;
        public static int GetValueInt32(this XmlNode node) => node == null ? 0 : node.InnerText.ToInt32();
        public static double GetValueDouble(this XmlNode node) => node == null ? 0 : node.InnerText.ToDouble();
        public static bool GetValueBoolean(this XmlNode node) => node == null ? false : node.InnerText.ToBoolean();
        public static T GetValueEnum<T>(this XmlNode node) => node == null ? 0.ToEnum<T>() : node.InnerText.ToEnum<T>();
    }
}
