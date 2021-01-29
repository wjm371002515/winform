using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JCodes.Framework.Common.Format;
using Newtonsoft.Json;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Framework;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;

namespace JCodes.Framework.WebUI.Controllers.Base
{
    public class CodeconversionController : RegisterControllers
    {

        /// <summary>
        /// 编码格式转换
        /// </summary>
        /// Refer: http://tool.chinaz.com/tools/unicode.aspx
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 代码转化
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxCalCodeConvert(InputClass model)
        {
            if (string.IsNullOrEmpty(model.DataType)) {
                return ToJsonContent(new ReturnResult() { ErrorCode = 100004, ErrorMessage = dicErrInfo["E100004"].ChineseName, LogLevel = dicErrInfo["E100004"].LogLevel });
            }

            if (string.IsNullOrEmpty(model.InputParam))
            {
                return ToJsonContent(new ReturnResult() { ErrorCode = 100005, ErrorMessage = dicErrInfo["E100005"].ChineseName, LogLevel = dicErrInfo["E100005"].LogLevel });
            }
           

            StringBuilder sb = new StringBuilder();
            switch (model.DataType)
            {
                case "ASCIIToUnicode":
                    #region  ASCIIToUnicode
                    for (int i = 0; i < model.InputParam.Length; i++)
                    {
                        sb.Append("&#" + ((int)model.InputParam[i]).ToString() + ";");
                    }
                    #endregion
                    break;
                case "UnicodeToASCII":
                    #region UnicodeToASCII
                    try
                    {
                        if (model.InputParam.Contains("&#"))
                        {
                            sb.Append(System.Text.RegularExpressions.Regex.Replace(
                            model.InputParam,
                            @"&#(?<Value>[a-zA-Z0-9]{5});",
                            m =>
                            {
                                return ((char)int.Parse(m.Groups["Value"].Value)).ToString();
                            }));
                        }
                        else {
                            string[] stringArray = model.InputParam.Split(' ');

                            if (stringArray.Length == 1)
                                sb.Append(model.InputParam);
                            else
                            {
                                for (int i = 0; i < stringArray.Length - 1; i++)
                                {
                                    int n;
                                    if (int.TryParse(stringArray[i], out n))
                                        sb.Append((char)int.Parse(stringArray[i]));
                                    else
                                        sb.Append(stringArray[i]);
                                }
                            }
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        // 系统日志
                        SystemLogInfo systemLogInfo1 = GetUserSystemInfo();
                        systemLogInfo1.LogLevel = dicErrInfo["E000031"].LogLevel;
                        systemLogInfo1.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
                        systemLogInfo1.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
                        systemLogInfo1.Remark = string.Format("Source:{0}, StackTrace:{1}, TargetSite:{2}, Message:{3}", ex.Source, ex.StackTrace, ex.TargetSite, ex.Message);
                        BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo1);

                        return ToJsonContent(new ReturnResult() { ErrorCode = 000031, ErrorMessage = dicErrInfo["E000031"].ChineseName, LogLevel = dicErrInfo["E000031"].LogLevel });
                    }
                    #endregion
                    break;
                case "UnicodeToChinese":
                    #region UnicodeToChinese
                    System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(model.InputParam, "\\\\u([\\w]{4})");

                    if (mc.Count == 0) {
                        sb.Append(model.InputParam);
                    }
                    else {
                        string a = model.InputParam.Replace("\\u", "");
                        char[] arr = new char[mc.Count];
                        for (int i = 0; i < arr.Length; i++)
                        {
                            arr[i] = (char)Convert.ToInt32(a.Substring(i * 4, 4), 16);
                        }
                        sb.Append(new string(arr));
                    }
                    #endregion
                    break;
                case "ChineseToUnicode":
                    #region ChineseToUnicode
                    if (!string.IsNullOrEmpty(model.InputParam))
                    {
                        for (int i = 0; i < model.InputParam.Length; i++)
                        {
                            //將中文轉為10進制整數，然後轉為16進制unicode
                            sb.Append("\\u" + ((int)model.InputParam[i]).ToString("x"));
                        }
                    }
                    #endregion
                    break;

                case "ChineseToUTF8":
                case "URLEncodeByutf-8":
                    #region ChineseToUTF8、URLEncodeByutf-8
                    sb.Append( HttpUtility.UrlEncode(model.InputParam, Encoding.UTF8));
                    #endregion
                    break;
                case "UTF8ToChinese":
                case "URLDecodeByutf-8":
                    #region UTF8ToChinese、URLDecodeByutf-8
                    sb.Append( HttpUtility.UrlDecode(model.InputParam, Encoding.UTF8));
                    #endregion
                    break;
                case "URLEncodeBygb2312":
                    #region URLEncodeBygb2312
                    sb.Append(HttpUtility.UrlEncode(model.InputParam, System.Text.Encoding.GetEncoding(936)));
                    #endregion
                    break;
                case "URLDecodeBygb2312":
                    #region URLDecodeBygb2312
                    sb.Append(HttpUtility.UrlDecode(model.InputParam, System.Text.Encoding.GetEncoding(936)));
                    #endregion
                    break;

                case "TimestampToString秒":
                    #region TimestampToString秒
                    // 时间戳是从 1970/1/1 8:00 开始
                    Int64 minTime = DateTimeHelper.GetMinDateTime().Ticks / 10000000 - 62135625600 + 28800;
                    Int64 maxTime = DateTimeHelper.GetMaxDateTime().Ticks / 10000000 - 62135625600;

                    // 判断输入参数是不是在这个范围内
                    if (Convert.ToInt64(model.InputParam) < minTime || Convert.ToInt64(model.InputParam) > maxTime)
                    {
                        return ToJsonContent(new ReturnResult() { ErrorCode = 000032, ErrorMessage = dicErrInfo["E000032"].ChineseName, LogLevel = dicErrInfo["E000032"].LogLevel });
                    }
                        
                    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970,1,1));
                    sb.Append(startTime.AddSeconds(double.Parse(model.InputParam)));
                    /*if (model.InputParam.Length == 10)        //精确到秒
                    {
                        
                    }*/
                    /*else if (model.InputParam.Length == 13)   //精确到毫秒
                    {
                        sb.Append(startTime.AddMilliseconds(double.Parse(model.InputParam)));
                    }*/
                    #endregion
                    break;
                 case "TimestampToString毫秒":
                    #region TimestampToString毫秒
                    // 时间戳是从 1970/1/1 8:00 开始
                    Int64 minTime2 = DateTimeHelper.GetMinDateTime().Ticks / 10000000 - 62135625600 + 28800;
                    Int64 maxTime2 = DateTimeHelper.GetMaxDateTime().Ticks / 10000000 - 62135625600;

                    // 判断输入参数是不是在这个范围内
                    if (Convert.ToInt64(model.InputParam) < minTime2 || Convert.ToInt64(model.InputParam) > maxTime2)
                    {
                        return ToJsonContent(new ReturnResult() { ErrorCode = 000032, ErrorMessage = dicErrInfo["E000032"].ChineseName, LogLevel = dicErrInfo["E000032"].LogLevel });
                    }

                    DateTime startTime2 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970,1,1));
                    /*if (model.InputParam.Length == 10)        //精确到秒
                    {
                        sb.Append(startTime.AddSeconds(double.Parse(model.InputParam)));
                    }
                    else if (model.InputParam.Length == 13)   //精确到毫秒
                    {  
                     * sb.Append(startTime2.AddMilliseconds(double.Parse(model.InputParam)));
                    }*/
                    sb.Append(startTime2.AddMilliseconds(double.Parse(model.InputParam))); 
                    #endregion
                    break;
                 case "DateToTimestamp秒":
                    #region DateToTimestamp秒
                    sb.Append((ConvertHelper.ToDateTime(model.InputParam, DateTime.Now) - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalSeconds);
                    #endregion
                    break;
                 case "DateToTimestamp毫秒":
                    #region DateToTimestamp毫秒
                    sb.Append((ConvertHelper.ToDateTime(model.InputParam, DateTime.Now) - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds);
                    #endregion
                    break;
                 case "AsciiToNativeBy不转换字母和数字":
                    #region AsciiToNativeBy不转换字母和数字
                    int code;
                    char[] chars = model.InputParam.ToCharArray();
                    for (int i = 0; i < chars.Length; i++)
                    {
                        char c = chars[i];
                        if (c > 255)
                        {
                            sb.Append("\\u");
                            code = (c >> 8);
                            string tmp = code.ToString("X");
                            if (tmp.Length == 1) 
                                sb.Append("0");
                            sb.Append(tmp);
                            code = (c & 0xFF);
                            tmp = code.ToString("X");
                            if (tmp.Length == 1) 
                                sb.Append("0");
                            sb.Append(tmp);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }
                    #endregion 
                    break;
                 case "AsciiToNativeBy转换字母和数字":
                    #region AsciiToNativeBy转换字母和数字
                    int code2;
                    char[] chars2 = model.InputParam.ToCharArray();
                    for (int i = 0; i < chars2.Length; i++)
                    {
                        char c = chars2[i];
                        sb.Append("\\u");
                        code2 = (c >> 8);
                        string tmp = code2.ToString("X");
                        if (tmp.Length == 1)
                            sb.Append("0");
                        sb.Append(tmp);
                        code2 = (c & 0xFF);
                        tmp = code2.ToString("X");
                        if (tmp.Length == 1)
                            sb.Append("0");
                        sb.Append(tmp);
                    }
                    #endregion
                    break;
                 case "NativeToAsciiBy不转换字母和数字":
                    #region NativeToAsciiBy不转换字母和数字
                    if (!string.IsNullOrEmpty(model.InputParam))
                    {
                        string[] strlist = model.InputParam.Replace("\\", "").Split('u');
                        try
                        {
                            for (int i = 1; i < strlist.Length; i++)
                            {
                                //将unicode字符转为10进制整数，然后转为char中文字符
                                // 如果出现大于4 则判断一下最后
                                if (strlist[i].Length > 4) {
                                    sb.Append((char)int.Parse(strlist[i].Substring(0, 4), System.Globalization.NumberStyles.HexNumber));
                                    sb.Append(strlist[i].Substring(4, strlist[i].Length - 4));
                                }
                                else
                                    sb.Append((char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber));
                            }
                        }
                        catch (FormatException ex)
                        {
                            // 系统日志
                            SystemLogInfo systemLogInfo1 = GetUserSystemInfo();
                            systemLogInfo1.LogLevel = dicErrInfo["E000031"].LogLevel;
                            systemLogInfo1.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
                            systemLogInfo1.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
                            systemLogInfo1.Remark = string.Format("Source:{0}, StackTrace:{1}, TargetSite:{2}, Message:{3}", ex.Source, ex.StackTrace, ex.TargetSite, ex.Message);
                            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo1);

                            return ToJsonContent(new ReturnResult() { ErrorCode = 000031, ErrorMessage = dicErrInfo["E000031"].ChineseName, LogLevel = dicErrInfo["E000031"].LogLevel });
                        }
                    }
                    #endregion
                    break;
                 case "NativeToAsciiBy转换字母和数字":
                    #region NativeToAsciiBy转换字母和数字
                    if (!string.IsNullOrEmpty(model.InputParam))
                    {
                        string[] strlist = model.InputParam.Replace("\\", "").Split('u');
                        try
                        {
                            for (int i = 1; i < strlist.Length; i++)
                            {
                               //将unicode字符转为10进制整数，然后转为char中文字符
                               sb.Append((char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber));
                            }
                        }
                        catch (FormatException ex)
                        {
                            // 系统日志
                            SystemLogInfo systemLogInfo1 = GetUserSystemInfo();
                            systemLogInfo1.LogLevel = dicErrInfo["E000031"].LogLevel;
                            systemLogInfo1.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
                            systemLogInfo1.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
                            systemLogInfo1.Remark = string.Format("Source:{0}, StackTrace:{1}, TargetSite:{2}, Message:{3}", ex.Source, ex.StackTrace, ex.TargetSite, ex.Message);
                            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo1);

                            return ToJsonContent(new ReturnResult() { ErrorCode = 000031, ErrorMessage = dicErrInfo["E000031"].ChineseName, LogLevel = dicErrInfo["E000031"].LogLevel });
                        }
                    }
                    #endregion
                    break;
                 case "HexEncodeByutf-8":
                    // https://www.cnblogs.com/wxbug/p/6991445.html
                    #region HexEncodeByutf-8
                    byte[] bytes = Encoding.UTF8.GetBytes(model.InputParam);
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        sb.Append(string.Format("%{0:X}", bytes[i]));
                    }
                    #endregion
                    break;
                 case "HexDecodeByutf-8":
                    #region HexDecodeByutf-8
                    model.InputParam = model.InputParam.Replace(",", "");
                    model.InputParam = model.InputParam.Replace("\n", "");
                    model.InputParam = model.InputParam.Replace("\\", "");
                    model.InputParam = model.InputParam.Replace(" ", "");
                    model.InputParam = model.InputParam.Replace("%", "");
                   // 需要将 hex 转换成 byte 数组。
                    byte[] bytes3 = new byte[model.InputParam.Length / 2];
                    for (int i = 0; i < bytes3.Length; i++)
                    {
                       try
                       {
                           // 每两个字符是一个 byte。
                           bytes3[i] = byte.Parse(model.InputParam.Substring(i * 2, 2),
                           System.Globalization.NumberStyles.HexNumber);
                       }
                       catch (Exception ex)
                       {
                           // 系统日志
                           SystemLogInfo systemLogInfo1 = GetUserSystemInfo();
                           systemLogInfo1.LogLevel = dicErrInfo["E000031"].LogLevel;
                           systemLogInfo1.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
                           systemLogInfo1.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
                           systemLogInfo1.Remark = string.Format("Source:{0}, StackTrace:{1}, TargetSite:{2}, Message:{3}", ex.Source, ex.StackTrace, ex.TargetSite, ex.Message);
                           BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo1);

                           return ToJsonContent(new ReturnResult() { ErrorCode = 000031, ErrorMessage = dicErrInfo["E000031"].ChineseName, LogLevel = dicErrInfo["E000031"].LogLevel });
                       }
                    }
                    sb.Append(Encoding.UTF8.GetString(bytes3));
                    #endregion
                    break;
                 case "HexEncodeBygb2312":
                    #region HexEncodeBygb2312
                    byte[] bytes2 = Encoding.GetEncoding("GB2312").GetBytes(model.InputParam);
                    for (int i = 0; i < bytes2.Length; i++)
                    {
                       sb.Append(string.Format("%{0:X}", bytes2[i]));
                    }
                    #endregion
                    break;
                 case "HexDecodeBygb2312":
                    #region URLDecodeBygb2312
                    model.InputParam = model.InputParam.Replace(",", "");
                    model.InputParam = model.InputParam.Replace("\n", "");
                    model.InputParam = model.InputParam.Replace("\\", "");
                    model.InputParam = model.InputParam.Replace(" ", "");
                    model.InputParam = model.InputParam.Replace("%", "");
                   
                   // 需要将 hex 转换成 byte 数组。
                    byte[] bytes4 = new byte[model.InputParam.Length / 2];
                    for (int i = 0; i < bytes4.Length; i++)
                    {
                       try
                       {
                           // 每两个字符是一个 byte。
                           bytes4[i] = byte.Parse(model.InputParam.Substring(i * 2, 2),
                           System.Globalization.NumberStyles.HexNumber);
                       }
                       catch (Exception ex)
                       {
                           // 系统日志
                           SystemLogInfo systemLogInfo1 = GetUserSystemInfo();
                           systemLogInfo1.LogLevel = dicErrInfo["E000031"].LogLevel;
                           systemLogInfo1.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
                           systemLogInfo1.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
                           systemLogInfo1.Remark = string.Format("Source:{0}, StackTrace:{1}, TargetSite:{2}, Message:{3}", ex.Source, ex.StackTrace, ex.TargetSite, ex.Message);
                           BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo1);

                           return ToJsonContent(new ReturnResult() { ErrorCode = 000031, ErrorMessage = dicErrInfo["E000031"].ChineseName, LogLevel = dicErrInfo["E000031"].LogLevel });
                       }
                    }
                    sb.Append(Encoding.GetEncoding("GB2312").GetString(bytes4));
                    #endregion
                    break;
                 case "HtmlEncode":
                    #region HtmlEncode
                    sb.Append(HttpUtility.HtmlEncode(model.InputParam));
                    #endregion
                    break;
                 case "HtmlDecode":
                    #region HtmlDecode
                    sb.Append(HttpUtility.HtmlDecode(model.InputParam));
                     #endregion
                    break;
            }

            if (sb.Length == 0)
                sb.Append(model.InputParam);

            // 系统日志
            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_INFO;
            systemLogInfo.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            systemLogInfo.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            systemLogInfo.Remark = string.Format("格式转化 数据类型:{0} 输入参数{1} 输入时间戳{2} 结果为:{3}", model.DataType, model.InputParam, model.TimeStamp, sb.ToString());
            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);

            return ToJsonContent(new ReturnResult() { ErrorCode = 0, ErrorMessage = sb.ToString(), LogLevel = (short)LogLevel.LOG_LEVEL_INFO });
        }
    }
}
