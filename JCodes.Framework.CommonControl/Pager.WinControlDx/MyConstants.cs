using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using JCodes.Framework.Common;

namespace JCodes.Framework.CommonControl
{
    public class MyConstants
    {
        private static string m_ConfigFile = "";

        /// <summary>
        /// 默认的配置文件路径
        /// </summary>
        public static string ConfigFile
        {
            get
            {
                if (string.IsNullOrEmpty(m_ConfigFile))
                {
                    string searchPattern = "*.exe.config";
                    string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, searchPattern, SearchOption.TopDirectoryOnly);
                    foreach (string filePath in files)
                    {
                        if (!filePath.Contains(".vshost"))
                        {
                            m_ConfigFile = filePath;
                            break;
                        }
                    }
                }
                return m_ConfigFile;
            }
            set
            {
                m_ConfigFile = value;
            }
        }

        private static string m_License = "";

        /// <summary>
        /// 授权用户的授权码
        /// </summary>
        public static string License
        {
            get
            {
                if (string.IsNullOrEmpty(m_License))
                {
                    AppConfig config = new AppConfig(MyConstants.ConfigFile);
                    string SecurityLicense = config.AppConfigGet("PagerLicense");
                    return SecurityLicense;
                }
                else
                {
                    return m_License;
                }
            }
            set { MyConstants.m_License = value; }
        }
    }

    public class LicenseCheckResult
    {
        /// <summary>
        /// 是否验证通过
        /// </summary>
        public bool IsValided = false;

        /// <summary>
        /// 注册的用户名称
        /// </summary>
        public string Username = "";

        /// <summary>
        /// 注册的公司名称
        /// </summary>
        public string CompanyName = "";

        /// <summary>
        /// 是否显示授权信息
        /// </summary>
        public bool DisplayCopyright = true;
    }

    public class LicenseTool
    {
        /// <summary>
        /// 检查用户的授权码
        /// </summary>
        /// <returns></returns>
        public static LicenseCheckResult CheckLicense()
        {
            LicenseCheckResult result = new LicenseCheckResult();
            string license = MyConstants.License;
            if (!string.IsNullOrEmpty(license))
            {
                try
                {
                    string decodeLicense = Base64Util.Decrypt(MD5Util.RemoveMD5Profix(license));
                    string[] strArray = decodeLicense.Split('|');
                    if (strArray.Length >= 4)
                    {
                        string componentType = strArray[0];
                        if (componentType.ToLower() == "whc.pager")
                        {
                            result.IsValided = true;
                        }
                        result.Username = strArray[1];
                        result.CompanyName = strArray[2];
                        try
                        {
                            result.DisplayCopyright = Convert.ToBoolean(strArray[3]);
                        }
                        catch
                        {
                            result.DisplayCopyright = true;
                        }

                        return result;

                        #region 设置显示内容
                        //string displayText = string.Format("该组件已授权给：");
                        //if (!string.IsNullOrEmpty(LicenseResult.CompanyName))
                        //{
                        //    displayText += string.Format("{0}", LicenseResult.CompanyName);
                        //}
                        //if (!string.IsNullOrEmpty(LicenseResult.Username))
                        //{
                        //    displayText += string.Format(" ({0})", LicenseResult.Username);
                        //}
                        //this.tssLink.Text = displayText;
                        //this.tssLink.Visible = LicenseResult.DisplayCopyright; 
                        #endregion
                    }
                }
                catch
                {
                }
            }

            return result;
        }
    }
}
