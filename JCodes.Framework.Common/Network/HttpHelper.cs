using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Web;
using System.Collections.Specialized;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Network
{
    /// <summary>
    /// 获取网页数据辅助类库。
    /// </summary>
    public class HttpHelper
    {
        #region 私有变量
        private CookieContainer cc;
        private string contentType = "application/x-www-form-urlencoded";
        private string accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
        private string userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
        
        private Encoding encoding = Encoding.GetEncoding("utf-8");
        private int delay = 1000;
        private int maxTry = 3;
        private int currentTry = 0;
        #endregion

        #region 属性

        /// <summary>
        /// 内容类型，默认为"application/x-www-form-urlencoded"
        /// </summary>
        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        /// <summary>
        /// Accept值，默认支持各种类型
        /// </summary>
        public string Accept
        {
            get { return accept; }
            set { accept = value; }
        }

        /// <summary>
        /// UserAgent，默认支持Mozilla/MSIE等
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }  

        /// <summary>
        /// Cookie容器
        /// </summary>
        public CookieContainer CookieContainer
        {
            get
            {
                return cc;
            }
        }

        /// <summary>
        /// 获取网页源码时使用的编码
        /// </summary>
        /// <value></value>
        public Encoding Encoding
        {
            get
            {
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }

        /// <summary>
        /// 网络延时
        /// </summary>
        public int NetworkDelay
        {
            get
            {
                Random r = new Random();
                return (r.Next(delay / 1000, delay / 1000 * 2))*1000;
            }
            set
            {
                delay = value;
            }
        }

        /// <summary>
        /// 最大尝试次数
        /// </summary>
        public int MaxTry
        {
            get
            {
                return maxTry;
            }
            set
            {
                maxTry = value;
            }
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public HttpHelper()
        {
            cc = new CookieContainer();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cc">指定CookieContainer的值</param>
        public HttpHelper(CookieContainer cc)
        {
            this.cc = cc;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contentType">内容类型</param>
        /// <param name="accept">Accept类型</param>
        /// <param name="userAgent">UserAgent内容</param>
        public HttpHelper(string contentType, string accept, string userAgent)
        {
            this.contentType = contentType;
            this.accept = accept;
            this.userAgent = userAgent;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cc">指定CookieContainer的值</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="accept">Accept类型</param>
        /// <param name="userAgent">UserAgent内容</param>
        public HttpHelper(CookieContainer cc, string contentType, string accept, string userAgent)
        {
            this.cc = cc;
            this.contentType = contentType;
            this.accept = accept;
            this.userAgent = userAgent;
        }

        #endregion

        #region 公共方法
                        
        /// <summary>
        /// 获取指定页面的HTML代码
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="cookieContainer">Cookie集合</param>
        /// <param name="postData">回发的数据</param>
        /// <param name="isPost">是否以post方式发送请求</param>
        /// <returns></returns>
        public string GetHtml(string url, CookieContainer cookieContainer, string postData, bool isPost)
        {
            return GetHtml(url, cookieContainer, postData, isPost, url);
        }

        /// <summary>
        /// 获取指定页面的HTML代码
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="cookieContainer">Cookie集合对象</param>
        /// <param name="postData">回发的数据</param>
        /// <param name="isPost">是否以post方式发送请求</param>
        /// <param name="referer">页面引用</param>
        /// <returns></returns>
        public string GetHtml(string url, CookieContainer cookieContainer, string postData, bool isPost, string referer)
        {
            if (string.IsNullOrEmpty(postData))
            {
                CookieCollection Cookie = new CookieCollection();
                return GetHtml(url, cookieContainer, referer);
            }

            //Thread.Sleep(NetworkDelay);
            currentTry++;
            try
            {
                byte[] byteRequest = Encoding.GetBytes(postData);

                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Referer = referer;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = isPost ? "POST" : "GET";
                httpWebRequest.ContentLength = byteRequest.Length;

                httpWebRequest.AllowAutoRedirect = false;

                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();

                HttpWebResponse httpWebResponse;
                try
                {
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    //redirectURL = httpWebResponse.Headers["Location"];// Get redirected uri
                }
                catch (WebException ex)
                {
                    httpWebResponse = (HttpWebResponse)ex.Response;
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(HttpHelper));
                }
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                currentTry = 0;
                return html;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(HttpHelper));
                if (currentTry <= maxTry)
                {
                    GetHtml(url, cookieContainer, postData, isPost);
                }

                currentTry = 0;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取指定页面的HTML代码
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="cookieContainer">Cookie集合</param>
        /// <param name="reference">页面引用</param>
        /// <returns></returns>
        public string GetHtml(string url, CookieContainer cookieContainer, string reference)
        {
            //Thread.Sleep(NetworkDelay);
            currentTry++;

            try
            {
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Referer = reference;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";

                HttpWebResponse httpWebResponse;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                currentTry = 0;
                return html;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(HttpHelper));
                if (currentTry <= maxTry)
                {
                    GetHtml(url, cookieContainer, reference);
                }

                currentTry = 0;
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取指定页面的HTML代码
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <returns></returns>
        public string GetHtml(string url)
        {
            return GetHtml(url, cc, url);
        }

        /// <summary>
        /// 获取指定页面的HTML代码
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="reference">页面引用</param>
        /// <returns></returns>
        public string GetHtml(string url, string reference)
        {
            return GetHtml(url, cc, reference);
        }

        /// <summary>
        /// 获取指定页面的HTML代码
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="postData">回发的数据</param>
        /// <param name="isPost">是否以post方式发送请求</param>
        /// <returns></returns>
        public string GetHtml(string url, string postData, bool isPost)
        {
            string redirectUrl = "";
            return GetHtml(url, cc, postData, isPost, redirectUrl);
        }
                        
        /// <summary>
        /// 获取指定页面的Stream
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="cookieContainer">Cookie集合对象</param>
        /// <returns></returns>
        public Stream GetStream(string url, ref string fileName, CookieContainer cookieContainer)
        {
            return GetStream(url, ref fileName, cookieContainer, url);
        }

        /// <summary>
        /// 获取指定页面的Stream
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="cookieContainer">Cookie对象</param>
        /// <param name="reference">页面引用</param>
        public Stream GetStream(string url, ref string fileName, CookieContainer cookieContainer, string reference)
        {
            //Thread.Sleep(delay);

            currentTry++;
            try
            {
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Referer = reference;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";

                HttpWebResponse httpWebResponse;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                fileName = httpWebResponse.Headers["Content-Disposition"] != null ?
                    httpWebResponse.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                    httpWebResponse.Headers["Location"] != null ? Path.GetFileName(httpWebResponse.Headers["Location"]) :
                    Path.GetFileName(url).Contains("?") || Path.GetFileName(url).Contains("=") ?
                    Path.GetFileName(httpWebResponse.ResponseUri.ToString()) : "";

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetFileName(fileName);
                }
                else
                {
                    fileName = "UnKnowFileName";//默认文件名称
                }

                currentTry = 0;
                return responseStream;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(HttpHelper));
                if (currentTry <= maxTry)
                {
                    CookieCollection cookie = new CookieCollection();
                    GetHtml(url, cookieContainer, url);
                }

                currentTry = 0;
                return null;
            }
        }

        /// <summary>
        /// 提交文件流到服务地址
        /// </summary>
        /// <param name="url">指定页面的路径</param>
        /// <param name="files">提交的文件列表</param>
        /// <param name="nvc">其他内容（名称-值键值）</param>
        /// <param name="cookieContainer">Cookie对象</param>
        /// <param name="reference">页面引用</param>
        /// <returns></returns>
        public string PostStream(string url, string[] files, NameValueCollection nvc = null, CookieContainer cookieContainer = null, string reference = null)
        {
            string boundary = "----------------------------" +
            DateTime.Now.Ticks.ToString("x");

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            webRequest.CookieContainer = cookieContainer;
            webRequest.Referer = reference;

            Stream memStream = new System.IO.MemoryStream();
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +  boundary + "\r\n");            
            string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            if (nvc != null)
            {
                foreach (string key in nvc.Keys)
                {
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = Encoding.GetBytes(formitem);
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
            for (int i = 0; i < files.Length; i++)
            {
                //string header = string.Format(headerTemplate, "file" + i, files[i]);
                string header = string.Format(headerTemplate, "uplTheFile", files[i]);
                byte[] headerbytes = Encoding.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);

                FileStream fileStream = new FileStream(files[i], FileMode.Open,
                FileAccess.Read);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
                
                memStream.Write(boundarybytes, 0, boundarybytes.Length);
                fileStream.Close();
            }

            webRequest.ContentLength = memStream.Length;
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();
            }

            string result = null;
            using (WebResponse webResponse2 = webRequest.GetResponse())
            {
                using (Stream stream2 = webResponse2.GetResponseStream())
                {
                    using (StreamReader reader2 = new StreamReader(stream2))
                    {
                        result = reader2.ReadToEnd();
                    }
                }
            }
            webRequest = null;

            return result;
        }

        /// <summary>
        /// 根据Cookie字符串获取Cookie的集合
        /// </summary>
        /// <param name="cookieString">Cookie字符串</param>
        /// <returns></returns>
        public CookieCollection GetCookieCollection(string cookieString)
        {
            CookieCollection cc = new CookieCollection();
            //string cookieString = "SID=ARRGy4M1QVBtTU-ymi8bL6X8mVkctYbSbyDgdH8inu48rh_7FFxHE6MKYwqBFAJqlplUxq7hnBK5eqoh3E54jqk=;Domain=.google.com;Path=/,LSID=AaMBTixN1MqutGovVSOejyb8mVkctYbSbyDgdH8inu48rh_7FFxHE6MKYwqBFAJqlhCe_QqxLg00W5OZejb_UeQ=;Domain=www.google.com;Path=/accounts";
            Regex re = new Regex("([^;,]+)=([^;,]+);Domain=([^;,]+);Path=([^;,]+)", RegexOptions.IgnoreCase);
            foreach (Match m in re.Matches(cookieString))
            {
                //name,   value,   path,   domain   
                Cookie c = new Cookie(m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[3].Value);
                cc.Add(c);
            }
            return cc;
        }

        /// <summary>
        /// 获取HTML页面内容指定隐藏域Key的Value内容
        /// </summary>
        /// <param name="html">待操作的HTML页面内容</param>
        /// <param name="key">隐藏域的名称</param>
        /// <returns></returns>
        public string GetHiddenKeyValue(string html, string key)
        {
            string result = "";
            string sRegex = string.Format("<input\\s*type=\"hidden\".*?name=\"{0}\".*?\\s*value=[\"|'](?<value>.*?)[\"|'^/]", key);
            Regex re = new Regex(sRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            Match mc = re.Match(html);
            if (mc.Success)
            {
                result = mc.Groups[1].Value;
            }
            return result;
        }

        /// <summary>
        /// 获取网页的编码格式
        /// </summary>
        /// <param name="url">网页地址</param>
        /// <returns></returns>
        public string GetEncoding(string url)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 20000;
                request.AllowAutoRedirect = false;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
                {
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                    }
                    
                    string html = reader.ReadToEnd();
                    Regex reg_charset = new Regex(@"charset\b\s*=\s*(?<charset>[^""]*)");
                    if (reg_charset.IsMatch(html))
                    {
                        return reg_charset.Match(html).Groups["charset"].Value;
                    }
                    else if (response.CharacterSet != string.Empty)
                    {
                        return response.CharacterSet;
                    }
                    else
                    {
                        return Encoding.Default.BodyName;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(HttpHelper));
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (reader != null)
                    reader.Close();
                if (request != null)
                    request = null;
            }
            return Encoding.Default.BodyName;
        }

        /// <summary>
        /// 判断URL是否有效
        /// </summary>
        /// <param name="url">待判断的URL，可以是网页以及图片链接等</param>
        /// <returns>200为正确，其余为大致网页错误代码</returns>
        public int GetUrlError(string url)
        {
            int num = 200;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                ServicePointManager.Expect100Continue = false;
                ((HttpWebResponse)request.GetResponse()).Close();
            }
            catch (WebException ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(HttpHelper));
                if (ex.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (ex.Message.IndexOf("500 ") > 0)
                {
                    return 500;
                }
                if (ex.Message.IndexOf("401 ") > 0)
                {
                    return 401;
                }
                if (ex.Message.IndexOf("404") > 0)
                {
                    num = 404;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(HttpHelper));
                num = 401;
            }
            return num;
        }

        /// <summary>
        /// 移除Html标记
        /// </summary>
        public string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="inputData">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        #endregion
    }
}
