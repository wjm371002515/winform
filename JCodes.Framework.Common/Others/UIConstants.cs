using System;
using System.Collections.Generic;
using System.Text;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 常用软件字符串
    /// </summary>
    public class UIConstants
    {
        /// <summary>
        /// 过期时间
        /// </summary>
        public static string ApplicationExpiredDate = "12/29/2009";
        /// <summary>
        /// 软件版本
        /// </summary>
        public static string SoftwareVersion = "3.0";
        /// <summary>
        /// 软件产品名称
        /// </summary>
        public static string SoftwareProductName = "OrderWaterEnterprise";
        /// <summary>
        /// 软件注册表键
        /// </summary>
        public static string SoftwareRegistryKey = "SOFTWARE\\Microsoft\\OrderWaterEnterprise\\" + SoftwareVersion;
        /// <summary>
        /// 软件的试用期
        /// </summary>
        public static int SoftwareProbationDay = 20;

        /// <summary>
        /// 独立存储位置
        /// </summary>
        public static string IsolatedStorage = "UserNameDir\\OrderWaterEnterprise.txt";
        /// <summary>
        /// 独立存储目录名称
        /// </summary>
        public static string IsolatedStorageDirectoryName = "UserNameDir";
        /// <summary>
        /// 独立存储加密钥
        /// </summary>
        public static string IsolatedStorageEncryptKey = "12345678";

        /// <summary>
        /// 注册加密公钥
        /// </summary>
        public static string PublicKey = @"<RSAKeyValue><Modulus>mtDtu679/0quhftVyOc6/cBov/i534Dkh3AB8RwrpC9Vq2RIFB3uvjRUuaAEPR8vMcijQjVzqLZgMM7jFKclzbh21rWTM+YlOeraKz5FPCC7rSLnv6Tfbzia9VI/r5cfM8ogVMuUKCZeU+PTEmVviasCl8nPYyqOQchlf/MftMM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        /// <summary>
        /// Web验证地址
        /// </summary>
        public static string WebRegisterURL = "http://www.iqidi.com/WebRegister.aspx";

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="expiredDate">过期时间</param>
        /// <param name="version">软件版本</param>
        /// <param name="name">软件名称</param>
        /// <param name="publicKey">公钥字符串</param>
        public static void SetValue(string expiredDate, string version, string name, string publicKey)
        {
            UIConstants.ApplicationExpiredDate = expiredDate;
            UIConstants.SoftwareVersion = version;
            UIConstants.SoftwareProductName = name;
            UIConstants.SoftwareRegistryKey = "SOFTWARE\\Microsoft\\" + name + "\\" + version;
            UIConstants.IsolatedStorage = "UserNameDir\\" + name + ".txt";
            UIConstants.PublicKey = publicKey;
        }
    }
}
