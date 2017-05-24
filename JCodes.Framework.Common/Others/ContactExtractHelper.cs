using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 手机号码、电子邮件内容提取辅助类，可以提取放到逗号分隔的字符串或者List里面
    /// </summary>
    public class ContactExtractHelper
    {
        /// <summary>
        /// 提取字符串里面的有效手机号码
        /// </summary>
        /// <param name="mobileString">手机号码字符串</param>
        /// <param name="strSplit">返回项目的分隔符，默认为逗号</param>
        /// <returns></returns>
        public static string ExtractMobile(string mobileString, char strSplit =',' )
        {
            string mobileReg = @"(?<mobile>(13|15|18)\d{9})";
            string addresString = "";
            Regex reg = new Regex(mobileReg, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = reg.Matches(mobileString);
            foreach (Match mc in mcs)
            {
                addresString += mc.Groups["mobile"].Value + strSplit;
            }
            return addresString.Trim(strSplit);
        }

        /// <summary>
        /// 提取字符串里面的有效手机号码
        /// </summary>
        /// <param name="mobileString">手机号码字符串</param>
        /// <returns></returns>
        public static List<string> ExtractMobileList(string mobileString)
        {
            List<string> list = new List<string>();
            string mobileReg = @"(?<mobile>(13|15|18)\d{9})";
            Regex reg = new Regex(mobileReg, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = reg.Matches(mobileString);
            foreach (Match mc in mcs)
            {
                string mobile = mc.Groups["mobile"].Value;
                if (!list.Contains(mobile))
                {
                    list.Add(mobile);
                }
            }
            return list;
        }

        /// <summary>
        /// 提取字符串里面的有效邮件地址
        /// </summary>
        /// <param name="emailString">电子邮件字符串</param>
        /// <param name="strSplit">返回项目的分隔符，默认为逗号</param>
        /// <returns></returns>
        public static string ExtractEMail(string emailString, char strSplit = ',')
        {
            string mobileReg = @"(?<email>[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+)";
            string addresString = "";
            Regex reg = new Regex(mobileReg, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = reg.Matches(emailString);
            foreach (Match mc in mcs)
            {
                addresString += mc.Groups["email"].Value + strSplit;
            }
            return addresString.Trim(strSplit);
        }

        /// <summary>
        /// 提取字符串里面的有效邮件地址
        /// </summary>
        /// <param name="emailString">电子邮件字符串</param>
        /// <returns></returns>
        public static List<string> ExtractEMailList(string emailString)
        {
            List<string> list = new List<string>();
            string emailReg = @"(?<email>[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+)";
            Regex reg = new Regex(emailReg, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = reg.Matches(emailString);
            foreach (Match mc in mcs)
            {
                string email = mc.Groups["email"].Value;
                if (!list.Contains(email))
                {
                    list.Add(email);
                }
            }
            return list;
        }
    }
}
