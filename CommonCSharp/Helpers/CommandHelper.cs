using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace CommonCSharp.Helpers
{
    public class CommandHelper
    {
        public static void SaveXml(Dictionary<string, string> dic, string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));

            var root = doc.CreateElement("Command");

            if (dic!=null)
            {
                foreach (var item in dic)
                {
                    var node = doc.CreateElement(item.Key);
                    node.InnerText = item.Value;
                    root.AppendChild(node);
                }
            }

            doc.AppendChild(root);
            StreamWriter sw = new StreamWriter(fileName, false, new UTF8Encoding(false));
            doc.Save(sw);
            sw.Close();
        }
        public static Dictionary<string, string> LoadXml(string fileName)
        {
            var result = new Dictionary<string, string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            var rootNode = doc.SelectSingleNode("/Command");

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                result.Add(node.Name, node.InnerText);
            }

            return result;
        }
    }
}
