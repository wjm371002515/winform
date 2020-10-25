using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.Common.Web
{
    /// <summary>
    /// Web 页面中实现弹出提示信息、窗口定位、模态对话框及窗口等等辅助操作。
    /// </summary>
    public class JavascriptHelper
    {
        #region 提示信息及窗口操作

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="control">Web控件</param>
        /// <param name="message">提示信息</param>
        public static void ShowConfirm(WebControl control, string message)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            control.Attributes.Add("onclick", "return confirm('" + EncodeJS(message) + "');");
        }

        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="control">当前请求的page</param>
        /// <param name="message">提示消息内容</param>
        public static void Alerts(Control control, string message)
        {
            control.Page.RegisterStartupScript("", string.Format(
                "<script>javascript:alert(\"{0}\");</script>", EncodeJS(message)));
        }

        /// <summary>
        /// 弹出提示信息并关闭窗口
        /// </summary>
        /// <param name="control">当前请求的page</param>
        /// <param name="message">提示消息内容</param>
        public static void AlertAndClose(Control control, string message)
        {
            control.Page.RegisterStartupScript("", string.Format(
                "<script>javascript:alert(\"{0}\");window.close();</script>", EncodeJS(message)));
        }

        /// <summary>
        /// 对Js操作的内容进行编码，避免出现错误脚本提示
        /// </summary>
        /// <param name="text">操作的内容</param>
        /// <returns></returns>
        public static string EncodeJS(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return text.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\'", "\\\'").Replace("\"", "\\\"");
        }

        /// <summary>
        /// 定位到指定的页面
        /// </summary>
        /// <param name="GoPage">目标页面</param>
        public static void GoTo(string GoPage)
        {
            HttpContext.Current.Response.Redirect(GoPage);
        }

        /// <summary>
        /// 使用js跳转到指定的页面
        /// </summary>
        /// <param name="control">当前请求的page</param>
        /// <param name="page">页面路径</param>
        public static void Location(Control control, string page)
        {
            string js = "<script language='JavaScript'>";
            js += "top.location='" + page + "'";
            js += "</script>";
            control.Page.RegisterStartupScript("", js);
        }

        /// <summary>
        /// 弹出提示信息，并定位到指定的页面
        /// </summary>
        /// <param name="control">当前请求的page</param>
        /// <param name="page">定位到指定的页面</param>
        /// <param name="message">提示信息内容</param>
        public static void AlertAndLocation(Control control, string page, string message)
        {
            string js = "<script language='JavaScript'>";
            js += "alert('" + message + "');";
            js += "top.location='" + page + "'";
            js += "</script>";
            control.Page.RegisterStartupScript("", js);
        }

        /// <summary>
        /// 弹出提示信息，并定位到指定的页面（指定显示的框架）
        /// </summary>
        /// <param name="control">当前请求的page</param>
        /// <param name="page">定位到指定的页面</param>
        /// <param name="message">提示信息内容</param>
        /// <param name="target">页面显示的框架</param>
        public static void AlertAndLocation(Control control, string page, string message, string target)
        {
            string js = "<script language='JavaScript'>";
            js += "alert('" + message + "');";
            js += ";window.target='" + target + "'";
            js += ";window.location='" + page + "'";
            js += "</script>";
            control.Page.RegisterStartupScript("", js);
        }

        /// <summary>
        /// 弹出提示信息，并将父窗口定位到指定页面，然后关闭子窗口
        /// </summary>
        /// <param name="control">当前请求的page</param>
        /// <param name="page">定位到指定的页面</param>
        /// <param name="message">提示信息内容</param>
        public static void AlertAndLocationOpener(Control control, string page, string message)
        {
            string js = "<script language='JavaScript'>";
            js += "alert('" + message + "');";
            js += ";window.opener.location='" + page + "'";
            js += ";window.close();";
            js += "</script>";
            control.Page.RegisterStartupScript("", js);
        }

        /// <summary>
        /// 弹出提示，并将父窗口定位到指定页面，然后关闭Popup窗口（需要父窗口引用popup.js和popupclass.js）
        /// </summary>
        /// <param name="control">当前请求的page</param>
        /// <param name="page">定位到指定的页面</param>
        /// <param name="message">提示信息内容</param>
        public static void AlertAndLocationPopWin(Control control, string page, string message)
        {
            string js = "<script language='JavaScript'>";
            js += "alert('" + message + "');";
            js += ";parent.location='" + page + "'";
            js += ";parent.ClosePop();";
            js += "</script>";
            control.Page.RegisterStartupScript("", js);
        }

        /// <summary>
        /// 关闭弹出窗口，并返回指定的值
        /// </summary>
        /// <param name="control">当前请求的page</param>
        /// <param name="returnValue">返回指定的值</param>
        public static void CloseWin(Control control, string returnValue)
        {
            string js = "<script language='JavaScript'>";
            js += "window.parent.returnValue='" + returnValue + "';";
            js += "window.close();";
            js += "</script>";
            control.Page.RegisterStartupScript("", js);
        }

        /// <summary>
        /// 返回历史页面
        /// </summary>
        public static void BackHistory(int value)
        {
            string js = @"<Script language='JavaScript'>history.go({0});</Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 注册脚本块
        /// </summary>
        public static void RegisterScriptBlock(Page page, string scriptString)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "scriptblock", "<script type='text/javascript'>" + scriptString + "</script>");
        } 

        #endregion

        #region 模式对话框及窗体操作

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
        }

        /// <summary>
        /// 打开模式对话框
        /// </summary>
        /// <param name="webFormUrl">链接地址</param>
        /// <param name="features"></param>
        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 打开模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="features"></param>
        /// <returns></returns>
        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            string js = @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
            #endregion
        }

        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            #region
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

            HttpContext.Current.Response.Write(js);
            #endregion
        }

        #endregion

        #region 数据转换

        /// <summary>
        /// 是否是数值
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool IsNumerical(string strValue)
        {
            return Regex.IsMatch(strValue, @"^[0-9]*$");
        }

        /// <summary>
        /// 是否浮点数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns>bool</returns>
        public static bool IsFloat(string strValue)
        {
            return Regex.IsMatch(strValue, @"^(-?\d+)(\.\d+)?$");
        }

        /// <summary>
        /// 过滤输入的前后空格和分号字符
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string ConvertString(string strValue)
        {
            return strValue.Trim().Replace("'", "''");
        }

        /// <summary>
        /// 时间类型字符串转化为[yyyy-MM-dd]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DateStringToString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return DateTimeHelper.GetServerDate();

            try
            {
                return DateTime.Parse(str).ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(JavascriptHelper));
                return DateTimeHelper.GetServerDate();
            }
        }

        /// <summary>
        /// 获取可控日期的(yyyy-MM-dd)内容，如果时间为空，则返回空字符
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetShortDate(DateTime? date)
        {
            string result = "";
            if (date.HasValue)
            {
                result = date.Value.ToString("yyyy-MM-dd");
            }
            return result;
        }

        /// <summary>
        /// 安全转换字符串为日期格式，不正确则返回默认时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime SafeConvertDate(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(JavascriptHelper));
                    return Convert.ToDateTime("1970-1-1");
                }
            }
            else
            {
                return Convert.ToDateTime("1970-1-1");
            }
        }

        /// <summary>
        /// 安全转换字符串为可控日期格式，不正确则空值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? SafeConvertDate2(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(JavascriptHelper));
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 安全转换字符串为数值格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal SafeConvertDecimal(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Convert.ToDecimal(value);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(JavascriptHelper));
                    return 0M;
                }
            }
            else
            {
                return 0M;
            }
        }

        /// <summary>
        /// 安全转换字符串为数值格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int SafeConvertInt32(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(JavascriptHelper));
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
