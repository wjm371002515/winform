using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;
using System.Web;

namespace JCodes.Framework.Common.Web
{
    /// <summary>
    /// 管理文档服务器类，提供文件上传、下载、删除等功能。
    /// 不过注意，由于IIS操作限制，可能一些后缀名的文件不支持访问。
    /// </summary>
    [Serializable()]
    public class FileServerManage
    {
        private CredentialCache myCredentialCache = null;
        private string URL = string.Empty;
        private string UserName = string.Empty;
        private string Password = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileServerManage()
        {
            //FileServer: http://192.168.101.82:8009
            this.URL = ConfigurationSettings.AppSettings["FileServer"];
            this.UserName = ConfigurationSettings.AppSettings["FileServerUser"];
            this.Password = ConfigurationSettings.AppSettings["FileServerPass"];

            if (this.URL.Length > 0)
            {
                if (this.URL.Substring(this.URL.Length - 1, 1) != "/")
                {
                    this.URL = this.URL + "/";
                }
            }

            this.myCredentialCache = new CredentialCache();
            this.myCredentialCache.Add(new Uri(URL), "NTLM", new NetworkCredential(UserName, Password));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">指定URL地址</param>
        /// <param name="username">指定用户名</param>
        /// <param name="password">指定密码</param>
        public FileServerManage(string url, string username, string password)
        {
            if (url.Length > 0)
            {
                if (url.Substring(url.Length - 1, 1) != "/")
                {
                    url = url + "/";
                }
            }

            this.URL = url;
            this.UserName = username;
            this.Password = password;

            this.myCredentialCache = new CredentialCache();
            this.myCredentialCache.Add(new Uri(URL), "NTLM", new NetworkCredential(UserName, Password));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="inputStream">流对象</param>
        /// <param name="fileName">保存文件名,可包含文件夹(test/test.txt)</param>
        /// <returns>bool[true:成功,false:失败]</returns>
        public bool UploadFile(Stream inputStream, string fileName)
        {
            if (inputStream == null || string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            WebClient client = new WebClient();
            client.Credentials = this.myCredentialCache;
            int length = (int)inputStream.Length;
            byte[] buffer = new byte[length];
            inputStream.Read(buffer, 0, length);

            try
            {
                string urlFile = GetUrlFile(this.URL, fileName);
                using (Stream stream = client.OpenWrite(urlFile, "PUT"))
                {
                    stream.Write(buffer, 0, length);
                }
            }
            catch (WebException ex)
            {
                LogTextHelper.Error(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据Url和FileName组合为合理的Url文件路径
        /// </summary>
        /// <param name="url">服务器Url基础地址</param>
        /// <param name="fileName">文件名称，可包含文件夹(test/test.sql)</param>
        /// <returns></returns>
        private string GetUrlFile(string url, string fileName)
        {
            string urlFile = url;
            if (url.EndsWith("/"))
            {
                urlFile += fileName.TrimStart('/');
            }
            else if (fileName.StartsWith("/"))
            {
                urlFile += fileName;
            }
            else
            {
                urlFile += string.Format("/{0}", fileName);
            }
            return urlFile;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileUrl">上传地址</param>
        /// <param name="fileName">上传文件名称,可包含文件夹(test/test.txt)</param>
        /// <returns>bool[true:成功,false:失败]</returns>
        public bool UploadFile(string fileUrl, string fileName)
        {
            if (string.IsNullOrEmpty(fileUrl) || string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            WebClient client = new WebClient();//在System.Net的空间命名下
            client.Credentials = this.myCredentialCache;

            int length = 0;
            byte[] buffer = null;
            using (Stream inputStream = client.OpenRead(fileUrl))
            {
                length = (int)inputStream.Length;
                buffer = new byte[length];
                inputStream.Read(buffer, 0, length);
            }

            try
            {
                string urlFile = GetUrlFile(this.URL, fileName);
                using (Stream stream = client.OpenWrite(urlFile, "PUT"))
                {
                    stream.Write(buffer, 0, length);
                }
            }
            catch (WebException exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名称,可包含文件夹(test/test.txt)</param>
        /// <returns>bool[true:成功,false:失败]</returns>
        public bool DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !this.IsFileExist(fileName))
            {
                return false;
            }

            string urlFile = GetUrlFile(this.URL, fileName);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlFile);
            request.Credentials = this.myCredentialCache;
            request.Method = "DELETE";

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException exception)
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="fileName">文件名称,可包含文件夹(test/test.txt)</param>
        /// <returns>bool[true:存在,false:否]</returns>
        public bool IsFileExist(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            bool result = false;

            string urlFile = GetUrlFile(this.URL, fileName);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlFile);
            request.Credentials = this.myCredentialCache;
            request.Method = "Get";

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = true;
                }
            }
            catch (WebException exception)
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }
        
        /// <summary>
        /// 通过HttpResponse方式读取文件，Web开发才可以使用
        /// </summary>
        /// <param name="newFileName">新文件名称,可包含文件夹(test/test.txt)</param>
        /// <param name="oldFileName">原文件名称</param>
        /// <returns></returns>
        public string ReadFile(string newFileName, string oldFileName)
        {
            if (string.IsNullOrEmpty(newFileName) || string.IsNullOrEmpty(oldFileName))
                return string.Empty;

            if (!this.IsFileExist(newFileName))
                return "文件不存在";

            try
            {
                string urlFile = GetUrlFile(this.URL, newFileName);
                WebClient client = new WebClient();
                client.Credentials = this.myCredentialCache;

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.BinaryWrite(client.DownloadData(urlFile));

                response.Charset = "GB2312";
                response.ContentEncoding = System.Text.Encoding.UTF8;
                oldFileName = HttpUtility.UrlEncode(oldFileName, Encoding.UTF8);
                string httpHeader = "attachment;filename=" + oldFileName;
                response.AppendHeader("Content-Disposition", httpHeader);
                response.Flush();
                response.End();

                return string.Empty;
            }
            catch (WebException ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// 读取服务器文件到字节数据中
        /// </summary>
        /// <param name="fileName">文件名称,可包含文件夹(test/test.txt)</param>
        /// <returns></returns>
        public byte[] ReadFileBytes(string fileName)
        {
            string urlFile = GetUrlFile(this.URL, fileName);
            WebClient client = new WebClient();
            client.Credentials = this.myCredentialCache;

            return client.DownloadData(urlFile);
        }
 
    }
}
