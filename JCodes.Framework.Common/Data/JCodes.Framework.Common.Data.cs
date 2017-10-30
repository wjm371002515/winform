using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Office;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace JCodes.Framework.Common
{
    public class Data
    {
        /// <summary>
        /// 获取日期
        /// 先从接口上 http://www.jcodes.cn/index.php/ExtInterface/getSysDate 取时间，如果接口异常或者取不到数据
        /// 在从注册表中 SoftwareRegistryKey 获取SysDate 日期
        /// 如果注册表中不存在则取本机系统时间
        /// </summary>
        /// <returns></returns>
        public static string getSysDate()
        {
            string nowDate = string.Empty;
            try
            {
                WebClient MyWebClient = new WebClient();
                //MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData("http://www.jcodes.cn/index.php/ExtInterface/getSysDate"); //从指定网站下载
                nowDate = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句     

                if (!Regex.IsMatch(nowDate, @"^(\d{4}-\d{1,2}-\d{1,2})$"))
                { 
                    throw new Exception("获取日期参数格式不正确");
                }
            }
            catch
            {
                // 获取注册表的日期
                string regkey = UIConstants.SoftwareRegistryKey;
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(regkey, true);

                if (null != reg && reg.GetValue("SysDate") != null)
                {
                    nowDate = reg.GetValue("SysDate").ToString();
                }
                else
                {
                    nowDate = DateTimeHelper.GetServerDate();
                }
            }
            return nowDate;
        }
    }
}
