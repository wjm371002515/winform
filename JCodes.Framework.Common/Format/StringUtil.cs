using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.VisualBasic;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 字符串大小写等相关操作辅助类
    /// </summary>
    public class StringUtil
    {
        private StringUtil()
        {
        }

        /// <summary>
        /// 转换为Camel字符串格式，去掉字符之间的空格以及起始"_"符号
        /// </summary>
        /// <param name="name">待转换字符串</param>
        /// <returns></returns>
        public static string ToCamel(string name)
        {
            string clone = name.TrimStart('_');
            clone = RemoveSpaces(ToProperCase(clone));
            return String.Format("{0}{1}", Char.ToLower(clone[0]),
                                 clone.Substring(1, clone.Length - 1));
        }

        /// <summary>
        /// 转换为Capital格式显示，去掉字符之间的空格以及起始"_"符号
        /// </summary>
        /// <param name="name">待转换字符串</param>
        /// <returns></returns>
        public static string ToCapit(String name)
        {
            string clone = name.TrimStart('_');
            return RemoveSpaces(ToProperCase(clone));
        }

        /// <summary>
        /// 移除字符串最后的一个字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveFinalChar(string s)
        {
            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            return s;
        }

        /// <summary>
        /// 移除字符串中最后的一个逗号
        /// </summary>
        /// <param name="s">操作的字符串</param>
        /// <returns></returns>
        public static string RemoveFinalComma(string s)
        {
            if (s.Trim().Length > 0)
            {
                int c = s.LastIndexOf(",");
                if (c > 0)
                {
                    s = s.Substring(0, s.Length - (s.Length - c));
                }
            }
            return s;
        }

        /// <summary>
        /// 移除字符间的空格
        /// </summary>
        /// <param name="s">操作的字符串</param>
        /// <returns></returns>
        public static string RemoveSpaces(string s)
        {
            s = s.Trim();
            s = s.Replace(" ", "");
            return s;
        }

        /// <summary>
        /// 将字符串转换为合适的大小写
        /// </summary>
        /// <param name="s">操作的字符串</param>
        /// <returns></returns>
        public static string ToProperCase(string s)
        {
            string revised = "";
            if (s.Length > 0)
            {
                if (s.IndexOf(" ") > 0)
                {
                    revised = Strings.StrConv(s, VbStrConv.ProperCase, 1033);
                }
                else
                {
                    string firstLetter = s.Substring(0, 1).ToUpper(new CultureInfo("en-US"));
                    revised = firstLetter + s.Substring(1, s.Length - 1);
                }
            }
            return revised;
        }

        /// <summary>
        /// 清除字符间的空格，并转换为合适的大小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToTrimmedProperCase(string s)
        {
            return RemoveSpaces(ToProperCase(s));
        }

        /// <summary>
        /// 转换对象为字符串表示
        /// </summary>
        /// <param name="o">操作对象</param>
        /// <returns></returns>
        public static string ToString(Object o)
        {
            Type t = o.GetType();
            PropertyInfo[] pi = t.GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.Append("Properties for: " + o.GetType().Name + Environment.NewLine);
            foreach (PropertyInfo i in pi)
            {
                try
                {
                    sb.Append("\t" + i.Name + "(" + i.PropertyType.ToString() + "): ");
                    if (null != i.GetValue(o, null))
                    {
                        sb.Append(i.GetValue(o, null).ToString());
                    }
                }
                catch
                {  }
                sb.Append(Environment.NewLine);
            }

            FieldInfo[] fi = t.GetFields();
            foreach (FieldInfo i in fi)
            {
                try
                {
                    sb.Append("\t" + i.Name + "(" + i.FieldType.ToString() + "): ");
                    if (null != i.GetValue(o))
                    {
                        sb.Append(i.GetValue(o).ToString());
                    }
                }
                catch
                {  }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 在字符串中，指定开始字符和结束字符，提取中间的内容
        /// </summary>
        /// <param name="content">待操作字符串</param>
        /// <param name="start">开始字符</param>
        /// <param name="end">结束字符</param>
        /// <returns></returns>
        public static ArrayList ExtractInnerContent(string content, string start, string end)
        {
            int sindex = -1, eindex = -1;
            int msindex = -1, meindex = -1;
            int span = 0;

            ArrayList al = new ArrayList();

            sindex = content.IndexOf(start);
            msindex = sindex + start.Length;
            eindex = content.IndexOf(end, msindex);
            span = eindex - msindex;

            if (sindex >= 0 && eindex > sindex)
            {
                al.Add(content.Substring(msindex, span));
            }

            while (sindex >= 0 && eindex > 0)
            {
                sindex = content.IndexOf(start, eindex);
                if (sindex > 0)
                {
                    eindex = content.IndexOf(end, sindex);
                    msindex = sindex + start.Length;
                    span = eindex - msindex;

                    if (msindex > 0 && eindex > 0)
                    {
                        al.Add(content.Substring(msindex, span));
                    }
                }
            }

            return al;
        }

        /// <summary>
        /// 在字符串中，指定开始字符和结束字符，提取非中间的数据
        /// </summary>
        /// <param name="content">待操作的字符</param>
        /// <param name="start">开始字符</param>
        /// <param name="end">结束字符</param>
        /// <returns></returns>
        public static ArrayList ExtractOuterContent(string content, string start, string end)
        {
            int sindex = -1, eindex = -1;

            ArrayList al = new ArrayList();

            sindex = content.IndexOf(start);
            eindex = content.IndexOf(end);
            if (sindex >= 0 && eindex > sindex)
            {
                al.Add(content.Substring(sindex, eindex + end.Length - sindex));
            }

            while (sindex >= 0 && eindex > 0)
            {
                sindex = content.IndexOf(start, eindex);
                if (sindex > 0)
                {
                    eindex = content.IndexOf(end, sindex);
                    if (sindex > 0 && eindex > 0)
                    {
                        al.Add(content.Substring(sindex, eindex + end.Length - sindex));
                    }
                }
            }

            return al;
        }

        /// <summary>
        /// 去除指定字符串前缀的算法
        /// </summary>
        /// <param name="content">待除去特定字符串的内容</param>
        /// <param name="prefixString">特定字符串列表(以逗号,分号,空格等标识)</param>
        /// <returns></returns>
        public static string RemovePrefix(string content, string prefixString)
        {
            if (string.IsNullOrEmpty(prefixString) || prefixString.Trim() == string.Empty)
            {
                return content;
            }

            char[] splitChars = new char[] {',', ';', ' '};            
            string strReturn = content;
            prefixString = prefixString.Trim(splitChars); //过滤前后多余的分隔符号,否则容易出错

            string[] suffixArray = prefixString.Split(splitChars);
            foreach (string suffix in suffixArray)
            {
                int sindex = strReturn.IndexOf(suffix, StringComparison.OrdinalIgnoreCase);// 非大小写敏感
                if (sindex == 0)
                {
                    strReturn = strReturn.Substring(suffix.Length);
                    break; //匹配一次就应该出来
                }
            }

            return strReturn;
        }
    }
}