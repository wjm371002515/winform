using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Net;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.Common.Network
{
    /// <summary>
    /// IE代理设置辅助类
    /// </summary>
    public class ProxyHelper
    {
        #region Variable

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lPBuffer, int lpdwBufferLength);
        #endregion

        #region IE代理设置

        /// <summary>
        /// 让IE支持WAP
        /// </summary>
        public static void SetIESupportWap()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(
                            @"Software\Classes\MIME\Database\Content Type\text/vnd.wap.wml",
                            true);

            if (rk == null)
            {
                rk = Registry.CurrentUser.CreateSubKey(
                            @"Software\Classes\MIME\Database\Content Type\text/vnd.wap.wml");
            }

            //设置代理可用 
            rk.SetValue("CLSID", "{25336920-03F9-11cf-8FD0-00AA00686F13}");

            rk.Close();
            Reflush();
        }

        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="ProxyServer">代理服务器</param>
        /// <param name="EnableProxy">设置代理可用</param>
        /// <returns></returns>
        public static string SetIEProxy(string ProxyServer, int EnableProxy)
        {
            string result ="";
            //打开注册表键 
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                        @"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                        true);

            //设置代理可用 
            rk.SetValue("ProxyEnable", EnableProxy);

            if (!ProxyServer.Equals("") && EnableProxy == 1)
            {
                //设置代理IP和端口 
                rk.SetValue("ProxyServer", ProxyServer);
                rk.SetValue("ProxyEnable", 1);
                result = "设置代理成功！";
            }

            if (EnableProxy == 0)
            {
                //设置代理IP和端口 
                rk.SetValue("ProxyEnable", 0);
                result = "取消代理成功！";
            }

            rk.Close();
            Reflush();
            return result;
        }

        /// <summary>
        /// 提交改变
        /// </summary>
        private static void Reflush()
        {
            InternetSetOption(IntPtr.Zero, Const.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, Const.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        } 
        
        #endregion

        #region 其他操作

        /// <summary>
        /// 测试代理配置
        /// </summary>
        /// <param name="setting">代理信息</param>
        /// <param name="te">测试信息</param>
        public static bool TestProxy(ProxySettingEntity setting, TestEntity te)
        {
            if (setting == null)
                return false;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(te.TestUrl);
            WebProxy proxy = WebProxy.GetDefaultProxy();
            if (setting.Ip != null && setting.Ip != "" && setting.Port != 0)
            {
                proxy.Address = new Uri("http://" + setting.Ip + ":" + setting.Port + "/");

                if (!string.IsNullOrEmpty(setting.UserName) &&
                    !string.IsNullOrEmpty(setting.Password))
                {
                    proxy.Credentials = new NetworkCredential(setting.UserName, setting.Password);
                }
            }
            webRequest.Proxy = proxy;

            webRequest.Method = "Get";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";
            WebResponse response = webRequest.GetResponse();
            string html = GetHtmlString(response, Encoding.GetEncoding(te.TestWebEncoding));
            response.Close();

            if (html.Contains(te.TestWebTitle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 代理设置
        /// </summary>
        /// <param name="request">Web请求</param>
        /// <param name="Proxy">代理设置</param>
        public static void SetProxySetting(WebRequest request, ProxySettingEntity Proxy)
        {
            if (Proxy == null)
                return;
            WebProxy webProxy = WebProxy.GetDefaultProxy();

            if (Proxy.Ip != null && Proxy.Ip != "" && Proxy.Port != 0)
            {
                webProxy.Address = new Uri("http://" + Proxy.Ip + ":" + Proxy.Port + "/");

                if (!string.IsNullOrEmpty(Proxy.UserName) &&
                    !string.IsNullOrEmpty(Proxy.Password))
                {
                    webProxy.Credentials = new NetworkCredential(Proxy.UserName, Proxy.Password);
                }
            }
            request.Proxy = webProxy;
        }

        private static string GetHtmlString(WebResponse response, Encoding encoding)
        {
            try
            {
                StreamReader stream = new StreamReader(response.GetResponseStream(), encoding);
                string code = stream.ReadToEnd();
                stream.Close();
                return code;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ProxyHelper));
                throw ex;
            }
        }

        #endregion
    }

    /// <summary>
    /// 测试信息
    /// </summary>
    [Serializable]
    public class TestEntity
    {
        private string _testUrl = "http://www.baidu.com";
        private string _testWebTitle = "百度一下";
        private string _testWebEncoding = "GB2312";

        /// <summary>
        /// 测试网站地址
        /// </summary>
        public string TestUrl
        {
            get { return _testUrl; }
            set { _testUrl = value; }
        }


        /// <summary>
        /// 测试网站Title
        /// </summary>
        public string TestWebTitle
        {
            get { return _testWebTitle; }
            set { _testWebTitle = value; }
        }


        /// <summary>
        /// Web编码
        /// </summary>
        public string TestWebEncoding
        {
            get { return _testWebEncoding; }
            set { _testWebEncoding = value; }
        }
    }

    /// <summary>
    /// 代理设置
    /// </summary>
    [Serializable]
    public class ProxySettingEntity
    {
        private int _id;
        private string ip;
        private int port;
        private string userName;
        private string password;
        private int proxyType;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 代理服务器IP
        /// </summary>
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// 代理服务器端口
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// 代理用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 代理密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 代理类型
        /// </summary>
        public int ProxyType
        {
            get { return proxyType; }
            set { proxyType = value; }
        }
    }
}
