using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 文本日志记录辅助类
    /// </summary>
    public class LogTextHelper
    {
        static string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
        public static bool RecordLog = true;
        public static bool DebugLog = false;

        static LogTextHelper()
        {
            if (!Directory.Exists(LogFolder))
            {
                Directory.CreateDirectory(LogFolder);
            }
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public static void WriteLine(string message)
        {
            string temp = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]    ") + message + "\r\n\r\n";
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".log";
            try
            {
                if (RecordLog)
                {
                    File.AppendAllText(Path.Combine(LogFolder, fileName), temp, Encoding.GetEncoding("GB2312"));
                }
                if (DebugLog)
                {
                    Console.WriteLine(temp);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常信息</param>
        public static void WriteLine(string message, Exception ex)
        {
            string temp = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]    ") + message + "\r\n" 
                + ex.ToString() + "\r\n\r\n";
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".log";
            try
            {
                if (RecordLog)
                {
                    File.AppendAllText(Path.Combine(LogFolder, fileName), temp, Encoding.GetEncoding("GB2312"));
                }
                if (DebugLog)
                {
                    Console.WriteLine(temp);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 记录类名、消息等信息到日志文件
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="funName">全名</param>
        /// <param name="message">错误信息</param>
        public static void WriteLine(string className, string funName, string message)
        {
            WriteLine(string.Format("{0}：{1}\r\n{2}", className, funName, message));
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="ex">错误信息</param>
        public static void Debug(object ex)
        {
            WriteLine(ex.ToString());
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="ex">错误信息</param>  
        public static void Warn(object ex)
        {
            WriteLine(ex.ToString());
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="ex">错误信息</param>
        public static void Error(object ex)
        {
            WriteLine(ex.ToString());
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="ex">错误信息</param>
        public static void Info(object ex)
        {
            WriteLine(ex.ToString());
        }

        /// <summary>
        /// 记录信息和异常信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Debug(object message, Exception ex)
        {
            WriteLine(message.ToString(), ex);
        }

        /// <summary>
        /// 记录信息和异常信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Warn(object message, Exception ex)
        {
            WriteLine(message.ToString(), ex);
        }

        /// <summary>
        /// 记录信息和异常信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Error(object message, Exception ex)
        {
            WriteLine(message.ToString(), ex);
        }

        /// <summary>
        /// 记录信息和异常信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Info(object message, Exception ex)
        {
            WriteLine(message.ToString(), ex);
        }
    }
}
