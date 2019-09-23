using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace CommonCSharp.Extend
{
    public static class SerializationExtend
    {
        #region 外部调用  
        /// <summary>  
        /// 将对象根据格式（XML/JSON）序列化成字符串结果  
        /// </summary>  
        /// <param name="o">目标对象</param>  
        /// <param name="format">输出格式</param>  
        /// <returns></returns>  
        public static string Serialize(this object o, SerializationFormat format)
        {
            if (format == SerializationFormat.Xml)
            {
                return SerializationExtend.XmlSerialize(o);
            }
            else
            {
                return SerializationExtend.JsonSerialize(o);
            }
        }

        /// <summary>  
        /// 将字符串根据格式（XML/JSON）反序列化成指定类型的对象  
        /// </summary>  
        /// <typeparam name="T">指定类型</typeparam>  
        /// <param name="s">目标字符串</param>  
        /// <param name="format">输入格式</param>  
        /// <returns></returns>  
        public static T Deserialize<T>(this string s, SerializationFormat format)
        {
            if (format == SerializationFormat.Xml)
            {
                return SerializationExtend.XmlDeserialize<T>(s);
            }
            else
            {
                return SerializationExtend.JsonDeserialize<T>(s);
            }
        }
        /// <summary>  
        /// 对象序列化成文件并保存  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="t">对象实体</param>  
        /// <param name="path">文件路径</param>  
        /// <param name="format">格式：json或 xml</param>  
        /// <returns></returns>  
        public static bool SerializableFile(this object t, string path, SerializationFormat format)
        {
            if (format == SerializationFormat.Xml)
            {
                return SerializationExtend.SerializableXML(t, path);
            }
            else
            {
                return SerializationExtend.SerializableJson(t, path);
            }
        }
        /// <summary>  
        /// 将文件反序列化成对象  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="path">文件路径</param>  
        /// <param name="format">格式： json 或 xml</param>  
        /// <returns></returns>  
        public static T DeserializeFile<T>(this string path, SerializationFormat format)
        {
            if (format == SerializationFormat.Xml)
            {
                return SerializationExtend.DeSerializableXML<T>(path);
            }
            else
            {
                return SerializationExtend.DeSerializableJson<T>(path);
            }
        }
        #endregion

        #region 对象的序列化与反序列化  
        /// <summary>  
        /// 将object对象序列化成XML  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        private static string XmlSerialize(object o)
        {
            XmlSerializer ser = new XmlSerializer(o.GetType());
            System.IO.MemoryStream mem = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            ser.Serialize(writer, o, ns);
            writer.Close();
            return Encoding.UTF8.GetString(mem.ToArray());
        }

        /// <summary>  
        /// 字符串反序列化成对象  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="s">XML值</param>  
        /// <returns></returns>  
        private static T XmlDeserialize<T>(string s)
        {
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.LoadXml(s);
                XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);
                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>  
        /// 对象序列化成Json字符串  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        private static string JsonSerialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
        /// <summary>  
        /// Json字符串反序列化成对象  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        private static T JsonDeserialize<T>(string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }
        #endregion

        #region 将对象保存成文件  
        /// <summary>  
        /// 将对象保存为json文件  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="t">对象</param>  
        /// <param name="path">保存路径</param>  
        /// <returns></returns>  
        private static bool SerializableJson(object t, string path)
        {
            bool bl = false;
            try
            {
                string strjson = JsonSerialize(t);
                System.IO.StreamWriter sw = System.IO.File.CreateText(path);
                sw.WriteLine(strjson);
                sw.Close();
            }
            catch
            {
                return bl;
            }
            return true;
        }

        /// <summary>  
        /// 将对象序列化成XML并保存到文件  
        /// </summary>  
        /// <returns></returns>  
        private static bool SerializableXML(object t, string path)
        {
            bool bl = false;
            FileStream fileStream = null;
            try
            {
                if (t != null)
                {
                    //创建xml格式器  
                    XmlSerializer xmls = new XmlSerializer(t.GetType());
                    //创建文件流  
                    fileStream = new FileStream(path, FileMode.Create);
                    //将对象序列化到流  
                    xmls.Serialize(fileStream, t);
                    bl = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }

            }

            return bl;
        }

        #endregion

        #region 将文件反序列化成对象  
        /// <summary>  
        /// 将JSON文件反序列化
        /// </summary>  
        /// <typeparam name="T">类型</typeparam>  
        /// <param name="path">json所存放的路径</param>  
        /// <returns>实体</returns>  
        private static T DeSerializableJson<T>(string path)
        {
            T t = default(T);
            try
            {
                FileInfo fi = new FileInfo(path);

                if (fi.Exists)
                {
                    StreamReader sr = new StreamReader(path, Encoding.UTF8);
                    String line;
                    StringBuilder strcontent = new StringBuilder();
                    while ((line = sr.ReadLine()) != null)
                    {
                        strcontent.Append(line.ToString());
                    }
                    t = SerializationExtend.JsonDeserialize<T>(strcontent.ToString());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return t;
        }
        
        /// <summary>  
        /// 将XML文件反序列化
        /// </summary>  
        /// <typeparam name="T">类型</typeparam>  
        /// <param name="path">XMl所存放的路径</param>  
        /// <returns>实体</returns>  
        private static T DeSerializableXML<T>(string path)
        {
            T t = default(T);
            FileStream fileStream = null;
            try
            {
                XmlSerializer xmls = new XmlSerializer(typeof(T));
                FileInfo fi = new FileInfo(path);

                if (fi.Exists)
                {
                    fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    t = (T)xmls.Deserialize(fileStream);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return t;
        }

        #endregion
    }

    public enum SerializationFormat
    {
        Xml,Json
    }

}
