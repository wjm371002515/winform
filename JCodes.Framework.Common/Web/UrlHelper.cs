using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace JCodes.Framework.Common.Web
{
    /// <summary>
    /// URL的操作辅助类，包括BASE64加密URL，增加参数，更新参数等。
    /// </summary>
    public class UrlHelper
    {
        private static System.Text.Encoding encoding = System.Text.Encoding.UTF8;

        #region 加解码URL字符串

        /// <summary>
        /// URL的64位编码URL
        /// </summary>
        /// <param name="sourthUrl">未编码的URL</param>
        /// <returns></returns>
        public static string Base64Encrypt(string sourthUrl)
        {
            string eurl = HttpUtility.UrlEncode(sourthUrl);
            eurl = Convert.ToBase64String(encoding.GetBytes(eurl));
            return eurl;
        }

        /// <summary>
        /// URL的64位解码URL
        /// </summary>
        /// <param name="eStr">编码后的URL</param>
        /// <returns></returns>
        public static string Base64Decrypt(string eStr)
        {
            if (!IsBase64(eStr))
            {
                return eStr;
            }
            byte[] buffer = Convert.FromBase64String(eStr);
            string sourthUrl = encoding.GetString(buffer);
            sourthUrl = HttpUtility.UrlDecode(sourthUrl);
            return sourthUrl;
        }

        /// <summary>
        /// 是否是Base64字符串
        /// </summary>
        /// <param name="eStr">检测URL字符串</param>
        /// <returns></returns>
        public static bool IsBase64(string eStr)
        {
            if ((eStr.Length % 4) != 0)
            {
                return false;
            }
            if (!Regex.IsMatch(eStr, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        } 

        #endregion

        #region URL后面的参数操作

        /// <summary>
        /// 添加URL参数
        /// </summary>
        /// <param name="url">URL字符串</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="value">参数值</param>
        public static string AddParam(string url, string paramName, string value)
        {
            Uri uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Query))
            {
                string eval = HttpContext.Current.Server.UrlEncode(value);
                return string.Concat(url, "?" + paramName + "=" + eval);
            }
            else
            {
                string eval = HttpContext.Current.Server.UrlEncode(value);
                return string.Concat(url, "&" + paramName + "=" + eval);
            }
        }

        /// <summary>
        /// 更新URL参数
        /// </summary>
        /// <param name="url">URL字符串</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="value">参数值</param>
        public static string UpdateParam(string url, string paramName, string value)
        {
            string keyWord = paramName + "=";
            int index = url.IndexOf(keyWord) + keyWord.Length;
            int index1 = url.IndexOf("&", index);
            if (index1 == -1)
            {
                url = url.Remove(index, url.Length - index);
                url = string.Concat(url, value);
                return url;
            }
            url = url.Remove(index, index1 - index);
            url = url.Insert(index, value);
            return url;
        } 
        #endregion

        /// <summary>
        /// 获取网站的根路径。
        /// 如http://localhost:5232/ 或者http://localhost/TestWebCommons 
        /// </summary>
        /// <returns>网站的根路径</returns>
        public static string GetSiteRoot()
        {
            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = "";
            else
                port = ":" + port;

            string siteRoot = protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + HttpContext.Current.Request.ApplicationPath;
            return siteRoot;
        }

        /// <summary>
        /// 获取相对的站点的URL。（如/TestWebCommons/Scripts/Javascript/UI.js ）
        /// </summary>
        /// <remarks>
        /// 使用这种方法获得相对网站的网址。
        /// 如资源文件：1。JAVASCRIPT  2。图片  3。xml文件
        /// </remarks>
        /// <param name="url">"~/Scripts/Javascript/UI.js"</param>
        /// <returns>相对的站点的URL。</returns>
        public static string GetRelativeSiteUrl(string url)
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            // Get proper application path ending with "/"
            if (!applicationPath.EndsWith("/"))
            {
                applicationPath += "/";
            }

            // Remove the "~/" from the url since we are using application path.
            if (!string.IsNullOrEmpty(url) && url.StartsWith("~/"))
            {
                url = url.Substring(2, url.Length - 2);
            }
            return applicationPath + url;
        }

        /// <summary>
        /// 根据给出的相对地址获取网站绝对地址
        /// </summary>
        /// <param name="localPath">相对地址</param>
        /// <returns>绝对地址</returns>
        public static string GetWebPath(string localPath)
        {
            string path = HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            string thisLocalPath;
            //如果不是根目录就加上"/" 根目录自己会加"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }

            if (localPath.StartsWith("~/"))
            {
                thisLocalPath = localPath.Substring(2);
            }
            else
            {
                return localPath;
            }
            return thisPath + thisLocalPath;
        }

        /// <summary>
        ///  获取网站绝对地址
        /// </summary>
        /// <returns></returns>
        public static string GetWebPath()
        {
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            //如果不是根目录就加上"/" 根目录自己会加"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }
            return thisPath;
        }

        /// <summary>
        /// 根据相对路径或绝对路径获取绝对路径
        /// </summary>
        /// <param name="localPath">相对路径或绝对路径</param>
        /// <returns>绝对路径</returns>
        public static string GetFilePath(string localPath)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(localPath, @"([A-Za-z]):\\([\S]*)"))
            {
                return localPath;
            }
            else
            {
                return System.Web.HttpContext.Current.Server.MapPath(localPath);
            }
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            try
            {
                return System.Web.HttpContext.Current.Request.Url.ToString();
            }
            catch { }

            return "";
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;
        }

        /// <summary>
        /// 返回请求的文件的名称。
        /// </summary>
        /// <param name="rawUrl">原始URL。</param>
        /// <param name="includeExtension">是否包含后缀名</param>
        public static string GetRequestedFileName(string rawUrl, bool includeExtension)
        {
            string file = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);

            if (includeExtension)
                return file;

            return file.Substring(0, file.IndexOf("."));
        }
    }
}
