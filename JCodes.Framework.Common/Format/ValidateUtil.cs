using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JCodes.Framework.Common.Format
{
    /// <summary>
    /// 各种输入格式验证辅助类
    /// </summary>
    public class ValidateUtil
    {
        #region 正则表达式

        /// <summary>
        /// 电子邮件正则表达式
        /// </summary>
        public static readonly string EmailRegex = @"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$";
        // @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 

        /// <summary>
        /// 检测是否有中文字符正则表达式
        /// </summary>
        public static readonly string CHZNRegex = "[\u4e00-\u9fa5]";

        /// <summary>
        /// 检测用户名格式是否有效(只能是汉字、字母、下划线、数字)
        /// </summary>
        public static readonly string UserNameRegex = @"^([\u4e00-\u9fa5A-Za-z_0-9]{0,})$";

        /// <summary>
        /// 密码有效性正则表达式(仅包含字符数字下划线）6~16位
        /// </summary>
        public static readonly string PasswordCharNumberRegex = @"^[A-Za-z_0-9]{6,16}$";

        /// <summary>
        /// 密码有效性正则表达式（纯数字或者纯字母，不通过） 6~16位
        /// </summary>
        public static readonly string PasswordRegex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,16}$";

        /// <summary>
        /// INT类型数字正则表达式
        /// </summary>
        public static readonly string ValidIntRegex = @"^[1-9]\d*\.?[0]*$";

        /// <summary>
        /// 是否数字正则表达式
        /// </summary>
        public static readonly string NumericRegex = @"^[-]?\d+[.]?\d*$";

        /// <summary>
        /// 是否整数字正则表达式
        /// </summary>
        public static readonly string NumberRegex = @"^[0-9]+$";

        /// <summary>
        /// 是否整数正则表达式（可带带正负号）
        /// </summary>
        public static readonly string NumberSignRegex = @"^[+-]?[0-9]+$";
        
        /// <summary>
        /// 是否是浮点数正则表达式
        /// </summary>
        public static readonly string DecimalRegex = "^[0-9]+[.]?[0-9]+$";

        /// <summary>
        /// 是否是浮点数正则表达式(可带正负号)
        /// </summary>
        public static readonly string DecimalSignRegex = "^[+-]?[0-9]+[.]?[0-9]+$";//等价于^[+-]?\d+[.]?\d+$

        /// <summary>
        /// 固定电话正则表达式
        /// </summary>
        public static readonly string PhoneRegex = @"^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}$";
        
        /// <summary>
        /// 移动电话正则表达式
        /// </summary>
        public static readonly string MobileRegex = @"^(13|15|18)\d{9}$";

        /// <summary>
        /// 固定电话、移动电话正则表达式
        /// </summary>
        public static readonly string PhoneMobileRegex = @"^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}$|^(13|15|18)\d{9}$";

        /// <summary>
        /// 身份证15位正则表达式
        /// </summary>
        public static readonly string ID15Regex = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";

        /// <summary>
        /// 身份证18位正则表达式
        /// </summary>
        public static readonly string ID18Regex = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$";

        /// <summary>
        /// URL正则表达式
        /// </summary>
        public static readonly string UrlRegex = @"\b(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]*[-A-Za-z0-9+&@#/%=~_|]";

        /// <summary>
        /// IP正则表达式
        /// </summary>
        public static readonly string IPRegex = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

        /// <summary>
        /// Base64编码正则表达式。
        /// 大小写字母各26个，加上10个数字，和加号“+”，斜杠“/”，一共64个字符，等号“=”用来作为后缀用途
        /// </summary>
        public static readonly string Base64Regex = @"[A-Za-z0-9\+\/\=]";

        /// <summary>
        /// 是否为纯字符的正则表达式
        /// </summary>
        public static readonly string LetterRegex = @"^[A-Za-z]+$";

        /// <summary>
        /// GUID正则表达式
        /// </summary>
        public static readonly string GuidRegex = "[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}";

        #endregion

        #region 验证输入字符串是否与模式字符串匹配

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        #endregion

        #region 用户名密码格式

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string stringValue)
        {
            return Encoding.Default.GetBytes(stringValue).Length;
        }

        /// <summary>
        /// 检测用户名格式是否有效
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static bool IsValidUserName(string userName)
        {
            int userNameLength = GetStringLength(userName);
            if (userNameLength >= 4 && userNameLength <= 20 && Regex.IsMatch(userName, UserNameRegex))
            {   // 判断用户名的长度（4-20个字符）及内容（只能是汉字、字母、下划线、数字）是否合法
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 密码有效性（纯数字或者纯字母，不通过）
        /// </summary>
        /// <param name="password">密码字符串</param>
        /// <returns></returns>
        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, PasswordRegex);
        }
        #endregion

        #region 数字字符串检查

        /// <summary>
        /// int有效性
        /// </summary>
        static public bool IsValidInt(string val)
        {
            return Regex.IsMatch(val, ValidIntRegex);
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(string inputData)
        {
            Regex RegNumeric = new Regex(NumericRegex);
            Match m = RegNumeric.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否整数字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Regex RegNumber = new Regex(NumberRegex);
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否整数字符串（带正负号）
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Regex RegNumberSign = new Regex(NumberSignRegex);
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Regex RegDecimal = new Regex(DecimalRegex);
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Regex RegDecimalSign = new Regex(DecimalSignRegex); 
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 中文检测

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        public static bool IsHasCHZN(string inputData)
        {
            Regex RegCHZN = new Regex(CHZNRegex);
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        /// <summary> 
        /// 检测含有中文字符串的实际长度 
        /// </summary> 
        /// <param name="inputData">字符串</param> 
        public static int GetCHZNLength(string inputData)
        {
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] bytes = n.GetBytes(inputData);

            int length = 0; // l 为字符串之实际长度 
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                if (bytes[i] == 63) //判断是否为汉字或全脚符号 
                {
                    length++;
                }
                length++;
            }
            return length;

        }

        #endregion

        #region 常用格式

        /// <summary>
        /// 验证输入字母  "^[A-Za-z]+$"
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public bool IsLetter(string inputData)
        {
            return Regex.IsMatch(inputData, LetterRegex);
        } 

        /// <summary>
        /// 验证身份证是否合法  15 和  18位两种
        /// </summary>
        /// <param name="idCard">要验证的身份证</param>
        public static bool IsIdCard(string idCard)
        {
            if (string.IsNullOrEmpty(idCard))
            {
                return false;
            }

            if (idCard.Length == 15)
            {
                return Regex.IsMatch(idCard, ID15Regex);
            }
            else if (idCard.Length == 18)
            {
                return Regex.IsMatch(idCard, ID18Regex, RegexOptions.IgnoreCase);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是邮件地址
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Regex RegEmail = new Regex(EmailRegex);
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 邮编有效性
        /// </summary>
        public static bool IsValidZip(string zip)
        {
            Regex rx = new Regex(@"^\d{6}$", RegexOptions.None);
            Match m = rx.Match(zip);
            return m.Success;
        }

        /// <summary>
        /// 固定电话有效性
        /// </summary>
        public static bool IsValidPhone(string phone)
        {
            Regex rx = new Regex(PhoneRegex, RegexOptions.None);
            Match m = rx.Match(phone);
            return m.Success;
        }

        /// <summary>
        /// 手机有效性
        /// </summary>
        public static bool IsValidMobile(string mobile)
        {
            Regex rx = new Regex(MobileRegex, RegexOptions.None);
            Match m = rx.Match(mobile);
            return m.Success;
        }

        /// <summary>
        /// 电话有效性（固话和手机 ）
        /// </summary>
        public static bool IsValidPhoneAndMobile(string number)
        {
            Regex rx = new Regex(PhoneMobileRegex, RegexOptions.None);
            Match m = rx.Match(number);
            return m.Success;
        }

        /// <summary>
        /// Url有效性
        /// </summary>
        public static bool IsValidURL(string url)
        {
            return Regex.IsMatch(url, UrlRegex);
        }

        /// <summary>
        /// IP有效性
        /// </summary>
        public static bool IsValidIP(string ip)
        {
            return Regex.IsMatch(ip, IPRegex);
        }

        /// <summary>
        /// domain 有效性
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns></returns>
        public static bool IsValidDomain(string host)
        {
            Regex r = new Regex(@"^\d+$");
            if (host.IndexOf(".") == -1)
            {
                return false;
            }
            return r.IsMatch(host.Replace(".", string.Empty)) ? false : true;
        }

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        public static bool IsBase64String(string str)
        {
            return Regex.IsMatch(str, Base64Regex);
        }

        /// <summary>
        /// 验证字符串是否是GUID
        /// </summary>
        /// <param name="guid">字符串</param>
        /// <returns></returns>
        public static bool IsGuid(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                return false;

            return Regex.IsMatch(guid, GuidRegex, RegexOptions.IgnoreCase);
        }

        #endregion

        #region 日期检查

        /// <summary>
        /// 判断输入的字符是否为日期
        /// </summary>
        public static bool IsDate(string strValue)
        {
            return Regex.IsMatch(strValue, @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
        }

        /// <summary>
        /// 判断输入的字符是否为日期,如2004-07-12 14:25|||1900-01-01 00:00|||9999-12-31 23:59
        /// </summary>
        public static bool IsDateHourMinute(string strValue)
        {
            return Regex.IsMatch(strValue, @"^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
        }

        #endregion

        #region 其他

        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string CheckMathLength(string inputData, int maxLength)
        {
            if (inputData != null && inputData != string.Empty)
            {
                inputData = inputData.Trim();
                if (inputData.Length > maxLength)//按最大长度截取字符串
                {
                    inputData = inputData.Substring(0, maxLength);
                }
            }
            return inputData;
        }

        /// <summary>
        /// 转换成 HTML code
        /// </summary>
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }
        /// <summary>
        ///解析html成 普通文本
        /// </summary>
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        /// <summary>
        /// 判断是否是图片
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static bool IsImageFile(string extension)
        {
            List<string> imageExList = new List<string>() { ".bmp", ".jpg", ".gif", ".png", ".jpeg", ".tif" };
            return imageExList.Contains(extension.ToLower());
        }

        #endregion
    }
}
