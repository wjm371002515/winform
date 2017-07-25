using System;
using Microsoft.Win32;
using System.Windows.Forms;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 注册表操作辅助类，通过默认指定注册表的前缀路径，减少调用复杂性。
    /// </summary>
    public sealed class RegistryHelper
    {
        #region 基础操作（读取和保存）
        private static string Software_Key = @"Software\DeepLand\OrderWater";

        /// <summary>
        /// 获取注册表项的值。如果该键不存在，则返回空字符串。
        /// </summary>
        /// <param name="key">注册表键</param>
        /// <returns>指定键返回的值</returns>
        public static string GetValue(string key)
        {
            return GetValue(Software_Key, key);
        }

        /// <summary>
        /// 获取注册表项的值。如果该键不存在，则返回空字符串。
        /// </summary>
        /// <param name="softwareKey">注册表键的前缀路径</param>
        /// <param name="key">注册表键</param>
        /// <returns>指定键返回的值</returns>
        public static string GetValue(string softwareKey, string key)
        {
            return GetValue(softwareKey, key, Registry.CurrentUser);
        }

        /// <summary>
        /// 获取注册表项的值。如果该键不存在，则返回空字符串。
        /// </summary>
        /// <param name="softwareKey">注册表键的前缀路径</param>
        /// <param name="key">注册表键</param>
        /// <param name="rootRegistry">开始的根节点（Registry.CurrentUser或者Registry.LocalMachine等）</param>
        /// <returns>指定键返回的值</returns>
        public static string GetValue(string softwareKey, string key, RegistryKey rootRegistry)
        {
            if (null == key)
            {
                throw new ArgumentNullException(Const.parameter);
            }

            string strRet = string.Empty;
            try
            {
                RegistryKey regKey = rootRegistry.OpenSubKey(softwareKey);
                strRet = regKey.GetValue(key).ToString();
            }
            catch (Exception ex)
            {
                strRet = "";
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RegistryHelper));
            }
            return strRet;
        }

        /// <summary>
        /// 保存键值到注册表
        /// </summary>
        /// <param name="key">注册表键</param>
        /// <param name="value">键的值内容</param>
        /// <returns>如果保存成功返回true，否则为false</returns>
        public static bool SaveValue(string key, string value)
        {
            return SaveValue(Software_Key, key, value);
        }

        /// <summary>
        /// 保存键值到注册表
        /// </summary>
        /// <param name="softwareKey">注册表键的前缀路径</param>
        /// <param name="key">注册表键</param>
        /// <param name="value">键的值内容</param>
        /// <returns>如果保存成功返回true，否则为false</returns>
        public static bool SaveValue(string softwareKey, string key, string value)
        {
            return SaveValue(softwareKey, key, value, Registry.CurrentUser);
        }

        /// <summary>
        /// 保存键值到注册表
        /// </summary>
        /// <param name="softwareKey">注册表键的前缀路径</param>
        /// <param name="key">注册表键</param>
        /// <param name="value">键的值内容</param>
        /// <param name="rootRegistry">开始的根节点（Registry.CurrentUser或者Registry.LocalMachine等）</param>
        /// <returns>如果保存成功返回true，否则为false</returns>
        public static bool SaveValue(string softwareKey, string key, string value, RegistryKey rootRegistry)
        {
            if (null == key)
            {
                throw new ArgumentNullException(Const.parameter1);
            }

            if (null == value)
            {
                throw new ArgumentNullException(Const.parameter2);
            }

            RegistryKey reg;
            reg = rootRegistry.OpenSubKey(softwareKey, true);

            if (null == reg)
            {
                reg = rootRegistry.CreateSubKey(softwareKey);
            }
            reg.SetValue(key, value);

            return true;
        }

        /// <summary>
        /// 每次程序运行时候,检查用户是否注册(如果第一次, 那么写入第一次运行时间)
        /// </summary>
        /// <returns>如果用户已经注册, 那么返回True, 否则为False</returns>
        public static bool CheckRegister()
        {
            // 保存注册码的信息
            string regCode = string.Empty;              //注册码
            string userName = string.Empty;             // 注册用户
            string company = string.Empty;              // 注册公司
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(UIConstants.SoftwareRegistryKey, true);

            // 首先判断注册表中是否存在regCode 注册码的信息，如果没有在从lic文件中读取文件，如果2个都不存在则验证不通过
            if (null == reg)
            {
                AppConfig config = Cache.Instance["AppConfig"] as AppConfig;
                if (config == null)
                {
                    config = new AppConfig();
                    Cache.Instance["AppConfig"] = config;
                }

                string LicensePath = config.AppConfigGet("LicensePath");
                if (FileUtil.IsExistFile(LicensePath))
                {
                    string[] tmpstr = FileUtil.FileToString(LicensePath).Split(Convert.ToChar(Const.VerticalLine));

                    if (tmpstr.Length == 3)
                    {
                        regCode = tmpstr[0];
                        userName = tmpstr[1];
                        company = tmpstr[2];
                    }
                }
            }

            if (reg.GetValue("regCode") != null)
            {
                // 获取验证码
                regCode = reg.GetValue("regCode").ToString();
                userName = reg.GetValue("UserName").ToString();
                company = reg.GetValue("Company").ToString();
            }

            // 再去配置表的数据
            if (string.Equals(regCode, string.Empty))
            {
                AppConfig config = Cache.Instance["AppConfig"] as AppConfig;
                if (config == null)
                {
                    config = new AppConfig();
                    Cache.Instance["AppConfig"] = config;
                }

                string LicensePath = config.AppConfigGet("LicensePath");
                if (FileUtil.IsExistFile(LicensePath))
                {
                    string[] tmpstr = FileUtil.FileToString(LicensePath).Split(Convert.ToChar(Const.VerticalLine));

                    if (tmpstr.Length == 3)
                    {
                        regCode = tmpstr[0];
                        userName = tmpstr[1];
                        company = tmpstr[2];
                    }
                }
            }

            // 2个都没有获取到注册码信息则认为没有注册过
            if (string.Equals(regCode, string.Empty))
            {
                return false;
            }

            Int32 passed = RSASecurityHelper.CheckRegistrationCode(regCode,userName, company);
            if (passed == 0)
            { return true; }
            else
            {
                return false;
            }

        }
        #endregion

        #region 自动启动程序设置

        /// <summary>
        /// 检查是否随系统启动
        /// </summary>
        /// <returns></returns>
        public static bool CheckStartWithWindows()
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if (regkey != null && (string)regkey.GetValue(Application.ProductName, "null", RegistryValueOptions.None) != "null")
            {
                Registry.CurrentUser.Flush();
                return true;
            }

            Registry.CurrentUser.Flush();
            return false;
        }

        /// <summary>
        /// 设置随系统启动
        /// </summary>
        /// <param name="startWin"></param>
        public static void SetStartWithWindows(bool startWin)
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (regkey != null)
            {
                if (startWin)
                {
                    regkey.SetValue(Application.ProductName, Application.ExecutablePath, RegistryValueKind.String);
                }
                else
                {
                    regkey.DeleteValue(Application.ProductName, false);
                }

                Registry.CurrentUser.Flush();
            }
        }

        #endregion
    }
}