using CommonCSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonCSharp.Helpers
{
    public class HttpHelper
    {
        //文件数据模板
        static string fileFormdataTemplate =
            "\r\n--{2}" +
            "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
            //"\r\nContent-Type: application/octet-stream" +
            "\r\n\r\n";
        //文本数据模板
        static string dataFormdataTemplate =
            "\r\n--{2}" +
            "\r\nContent-Disposition: form-data; name=\"{0}\"" +
            "\r\n\r\n{1}";

        public static string PostFile(string url, Dictionary<string, string> param, string fileKey, byte[] fileData)
        {
            List<FormItemModel> lstForm = new List<FormItemModel>();
            foreach (var key in param.Keys)
            {
                lstForm.Add(new FormItemModel()
                {
                    Key = key,
                    Value = param[key]
                });
            }
            lstForm.Add(new FormItemModel()
            {
                Key = fileKey,
                Value = "",
                FileName = fileKey,
                FileBytes = fileData
            });
            return PostForm(url, lstForm);
        }
        static string convertDictionary(Dictionary<string, string> param)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in param.Keys)
            {
                sb.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(param[key]) + "&");
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
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

        public static string Post(string url, byte[] postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            //if (!string.IsNullOrEmpty(cookies))
            //    request.Headers.Add("Cookie", cookies);
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            request.ContentLength = postData.Length;

            if (postData != null)
            {
                using (var s = request.GetRequestStream())
                {
                    s.Write(postData, 0, postData.Length);
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
        //2.提交表单主逻辑实现
        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formItems">Post表单内容</param>
        /// <param name="cookieContainer"></param>
        /// <param name="timeOut">默认20秒</param>
        /// <param name="encoding">响应内容的编码类型（默认utf-8）</param>
        /// <returns></returns>
        public static string PostForm(string url, List<FormItemModel> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 20000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            #region 初始化请求对象
            request.Method = "POST";
            request.Timeout = timeOut;
            request.Accept = "*/*";
            request.KeepAlive = true;
            request.UserAgent = "python-requests/2.22.0";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            //request.Connection = "keep-alive";
            request.Expect = "";
            //request.Headers.Add("Connection", "keep-alive");
            //request.Headers.Remove("Expect");
            if (!string.IsNullOrEmpty(refererUrl))
                request.Referer = refererUrl;
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;
            #endregion

            string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);

            var lstData = GetFormData(formItems, boundary);
            //请求流
            request.ContentLength = lstData.Sum((arr) => arr.Length);

            //直接写入流
            using (Stream requestStream = request.GetRequestStream())
            {
                foreach (var bytes in lstData)
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
            }
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        static List<byte[]> GetFormData(List<FormItemModel> formItems, string boundary)
        {
            List<byte[]> lstData = new List<byte[]>();
            bool firstLine = true;
            foreach (var item in formItems)
            {
                string formdata = null;
                if (item.IsFile)
                {
                    //上传文件
                    formdata = string.Format(fileFormdataTemplate, item.Key, item.FileName, boundary);
                }
                else
                {
                    //上传文本
                    formdata = string.Format(dataFormdataTemplate, item.Key, item.Value, boundary);
                }

                //统一处理
                byte[] formdataBytes = null;
                //第一行不需要换行
                if (firstLine)
                {
                    formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    firstLine = false;
                }
                else
                {
                    formdataBytes = Encoding.UTF8.GetBytes(formdata);
                }
                lstData.Add(formdataBytes);
                if (item.FileBytes != null && item.FileBytes.Length > 0)
                {
                    lstData.Add(item.FileBytes);
                }
            }
            return lstData;
        }
        static string http_build_query(Dictionary<string, string> dict = null)
        {
            if (dict == null)
            {
                return "";
            }
            var builder = new UriBuilder();
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var item in dict.Keys)
            {
                query[item] = dict[item];
            }
            return query.ToString().Trim('?');
        }
        static string http_build_query(IEnumerable<KeyValuePair<string, string>> dict = null)
        {
            if (dict == null)
            {
                return "";
            }
            var builder = new UriBuilder();
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var kv in dict)
            {
                query[kv.Key] = kv.Value;
            }
            return query.ToString().Trim('?');
        }

        public static string MD5Encrypt(string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }
        public static string Sign(Dictionary<string,string> dic, string secret)
        {
            var lstParams = dic.Select(kv => kv);
            lstParams.OrderBy(kv => kv.Key);

            var strParams = http_build_query(lstParams);
            var md5 = MD5Encrypt(strParams + secret);
            return md5;
        }
    }
}

