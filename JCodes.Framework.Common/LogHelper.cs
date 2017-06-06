using System;
using System.Configuration;
using JCodes.Framework.jCodesenum.BaseEnum;
using System.Text;
using System.Data.Common;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace JCodes.Framework.Common
{
    public class LogHelper
    {
        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="ex">异常类</param>
        /// <param name="t">窗体的类型(如typeof("frmMain"))</param>
        public static void WriteLog(LogLevel logLevel, Exception ex, Type t)
        {
            // 获取app.config 中配置的日志级别
            string configSetlogLevel = ConfigurationManager.AppSettings["SYSTEM_LOG_LEVEL"];
            int getSetlogLevel = 0;
            // 如果未配置日志界别，默认 为
            if (string.IsNullOrEmpty(configSetlogLevel)) //一般性错误
                getSetlogLevel = (int)LogLevel.LOG_LEVEL_ERR;
            else
                getSetlogLevel = Convert.ToInt32(configSetlogLevel);

            // 如果配置的日志级别大于传进来的日志级别 则不记录日志
            if (getSetlogLevel < (int)logLevel)
                return;

            log4net.ILog log = log4net.LogManager.GetLogger(t);

            // 把系统配置的日志界别转化为log4net 的日志级别
            switch (logLevel)
            {
                case LogLevel.LOG_LEVEL_EMERG:
                case LogLevel.LOG_LEVEL_ALERT:
                case LogLevel.LOG_LEVEL_CRIT: log.Fatal("jCodes 系统致命日志", ex); break;
                case LogLevel.LOG_LEVEL_ERR: log.Error("jCodes 系统错误日志", ex); break;
                case LogLevel.LOG_LEVEL_WARN: log.Warn("jCodes 系统警告日志", ex); break;
                case LogLevel.LOG_LEVEL_NOTICE:
                case LogLevel.LOG_LEVEL_INFO: log.Info("jCodes 系统提示日志", ex); break;
                case LogLevel.LOG_LEVEL_DEBUG:
                case LogLevel.LOG_LEVEL_SQL: log.Debug("jCodes 系统调试日志", ex); break;
            }
        }

        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="str">日志信息</param>
        /// <param name="t">窗体的类型(如typeof("frmMain"))</param>
        public static void WriteLog(LogLevel logLevel, string str, Type t)
        {
            // 获取app.config 中配置的日志级别
            string configSetlogLevel = ConfigurationManager.AppSettings["SYSTEM_LOG_LEVEL"];
            int getSetlogLevel = 0;
            // 如果未配置日志界别，默认 为
            if (string.IsNullOrEmpty(configSetlogLevel)) //一般性错误
                getSetlogLevel = (int)LogLevel.LOG_LEVEL_ERR;
            else
                getSetlogLevel = Convert.ToInt32(configSetlogLevel);

            // 如果配置的日志级别大于传进来的日志级别 则不记录日志
            if (getSetlogLevel < (int)logLevel)
                return;

            log4net.ILog log = log4net.LogManager.GetLogger(t);

            // 把系统配置的日志界别转化为log4net 的日志级别
            switch (logLevel)
            {
                case LogLevel.LOG_LEVEL_EMERG:
                case LogLevel.LOG_LEVEL_ALERT:
                case LogLevel.LOG_LEVEL_CRIT: log.Fatal(str); break;
                case LogLevel.LOG_LEVEL_ERR: log.Error(str); break;
                case LogLevel.LOG_LEVEL_WARN: log.Warn(str); break;
                case LogLevel.LOG_LEVEL_NOTICE:
                case LogLevel.LOG_LEVEL_INFO: log.Info(str); break;
                case LogLevel.LOG_LEVEL_DEBUG:
                case LogLevel.LOG_LEVEL_SQL: log.Debug(str); break;
            }
        }

        /// <summary>
        /// 根据 SqlParameter 得到起对应的sql
        /// </summary>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static string DbParameterToString(params DbParameter[] cmdParms)
        {
            if (cmdParms == null || cmdParms.Length == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("  Sql参数: ");
            for (Int32 i = 0; i < cmdParms.Length; i++)
            {
                sb.Append(cmdParms[i] + "=" + cmdParms[i] + " ,");
            }

            return sb.ToString();
        }
    }
}
