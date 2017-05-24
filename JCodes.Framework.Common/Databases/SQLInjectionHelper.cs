using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Databases
{
    /// <summary>
    /// 防止SQL注入问题的辅助类
    /// </summary>
    public class SQLInjectionHelper
    {
        /// <summary>
        /// 获取Post的数据
        /// </summary>
        public static bool ValidUrlPostData()
        {
            bool result = false;

            for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
            {
                result = HasInjectionData(HttpContext.Current.Request.Form[i].ToString());
                if (result)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, "检测出POST恶意数据: 【" + HttpContext.Current.Request.Form[i].ToString() + "】 URL: 【" + HttpContext.Current.Request.RawUrl + "】来源: 【" + HttpContext.Current.Request.UserHostAddress + "】", typeof(SQLInjectionHelper));
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取QueryString中的数据
        /// </summary>
        public static bool ValidUrlGetData()
        {
            bool result = false;

            for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
            {
                result = HasInjectionData(HttpContext.Current.Request.QueryString[i].ToString());
                if (result)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, "检测出GET恶意数据: 【" + HttpContext.Current.Request.QueryString[i].ToString() + "】 URL: 【" + HttpContext.Current.Request.RawUrl + "】来源: 【" + HttpContext.Current.Request.UserHostAddress + "】", typeof(SQLInjectionHelper));
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 验证是否存在注入代码(条件语句）
        /// </summary>
        /// <param name="inputData"></param>
        public static bool HasInjectionData(string inputData)
        {
            if (string.IsNullOrEmpty(inputData))
                return false;

            //里面定义恶意字符集合
            //验证inputData是否包含恶意集合
            if (Regex.IsMatch(inputData.ToLower(), GetRegexString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取正则表达式
        /// </summary>
        /// <returns></returns>
        private static string GetRegexString()
        {
            //构造SQL的注入关键字符
            string[] strBadChar =
            {
                "select\\s",
                "from\\s",
                "insert\\s",
                "delete\\s",
                "update\\s",
                "drop\\s",
                "truncate\\s",
                "exec\\s",
                "count\\(",
                "declare\\s",
				"asc\\(",
				"mid\\(",
				"\\schar\\(",
                "net user",
                "xp_cmdshell",
                "/add\\s",
                "exec master.dbo.xp_cmdshell",
                "net localgroup administrators"
            };

            //构造正则表达式
            string str_Regex = ".*(";
            for (int i = 0; i < strBadChar.Length - 1; i++)
            {
                str_Regex += strBadChar[i] + "|";
            }
            str_Regex += strBadChar[strBadChar.Length - 1] + ").*";

            return str_Regex;
        }


        /// <summary>
        /// 获取正则表达式(太严格的函数，暂时不用）
        /// </summary>
        /// <returns></returns>
        private static string GetRegexString2()
        {
            //构造SQL的注入关键字符
            string[] strBadChar =
        {
            "and"
            ,"exec"
            ,"insert"
            ,"select"
            ,"delete"
            ,"update"
            ,"count"
            ,"from"
            ,"drop"
            ,"asc"
            ,"char"
            ,"or"
            ,"%"
            ,";"
            ,":"
            ,"\'"
            ,"\""
            ,"-"
            ,"chr"
            ,"mid"
            ,"master"
            ,"truncate"
            ,"char"
            ,"declare"
            ,"SiteName"
            ,"net user"
            ,"xp_cmdshell"
            ,"/add"
            ,"exec master.dbo.xp_cmdshell"
            ,"net localgroup administrators"
        };

            //构造正则表达式
            string str_Regex = ".*(";
            for (int i = 0; i < strBadChar.Length - 1; i++)
            {
                str_Regex += strBadChar[i] + "|";
            }
            str_Regex += strBadChar[strBadChar.Length - 1] + ").*";

            return str_Regex;
        }

    }
}
