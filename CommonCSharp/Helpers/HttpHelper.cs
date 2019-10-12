using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonCSharp.Helpers
{
    public class HttpHelper
    {
        public static string PostMulti(string url, Dictionary<string, string> param, byte[] body, int len, string cookies = "")
        {
            var postDataStr = convertDictionary(param);
            postDataStr += "&content=";

            if (body.Length != len)
            {
                var temp = new byte[len];
                Array.Copy(body, temp, len);
                body = temp;
            }

            return Post(url, Encoding.UTF8.GetBytes(postDataStr), body);
        }
        static string convertDictionary(Dictionary<string, string> param)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in param.Keys)
            {
                sb.Append(key + "=" + param[key] + "&");
            }
            sb.Remove(sb.Length - 1, 1);

            return WebUtility.UrlEncode(sb.ToString());
        }
        public static string Post(string url, Dictionary<string, string> param)
        {
            var postDataStr = convertDictionary(param);
            return Post(url, postDataStr);
        }
        public static string Post(string url, string postDataStr)
        {
            return Post(url, Encoding.UTF8.GetBytes(postDataStr));
        }

        public static string Post(string url, params byte[][] postData)
        {
            var bytesLength = postData?.Sum((bs) => bs.Length);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            //if (!string.IsNullOrEmpty(cookies))
            //    request.Headers.Add("Cookie", cookies);
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            request.ContentLength = bytesLength ?? 0;

            if (postData != null)
            {
                using (BinaryWriter writer = new BinaryWriter(request.GetRequestStream(), Encoding.UTF8))
                {
                    foreach (var data in postData)
                    {
                        if (data != null)
                        {
                            writer.Write(data);
                        }
                    }
                }
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding)))
                {
                    string retString = reader.ReadToEnd();
                    return retString;
                }
            }
        }

        public static string Get(string Url, string cookies = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            if (!string.IsNullOrEmpty(cookies))
                request.Headers.Add("Cookie", cookies);
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding)))
                {
                    string retString = reader.ReadToEnd();
                    return retString;
                }
            }
        }
    }
}
