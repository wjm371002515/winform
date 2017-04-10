using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

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
                case LogLevel.LOG_TYPE_ALERT:
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
                case LogLevel.LOG_TYPE_ALERT:
                case LogLevel.LOG_LEVEL_CRIT: log.Fatal(str); break;
                case LogLevel.LOG_LEVEL_ERR: log.Error(str); break;
                case LogLevel.LOG_LEVEL_WARN: log.Warn(str); break;
                case LogLevel.LOG_LEVEL_NOTICE:
                case LogLevel.LOG_LEVEL_INFO: log.Info(str); break;
                case LogLevel.LOG_LEVEL_DEBUG:
                case LogLevel.LOG_LEVEL_SQL: log.Debug(str); break;
            }
        }
    }

    #region 定义日志级别的枚举类型
    public enum LogLevel
    {
        /// <summary>
        /// 严重错误，导致系统崩溃无法使用 1
        /// </summary>
        LOG_LEVEL_EMERG = 1,
        
        /// <summary>
        /// 警戒性错误， 必须被立即修改的错误 2
        /// </summary>
        LOG_TYPE_ALERT = 2,
        
        /// <summary>
        /// 临界值错误， 超过临界值的错误 3
        /// </summary>
        LOG_LEVEL_CRIT = 3,

        /// <summary>
        /// 一般性错误 4
        /// </summary>
        LOG_LEVEL_ERR = 4,

        /// <summary>
        /// 警告性错误， 需要发出警告的错误 5
        /// </summary>
        LOG_LEVEL_WARN = 5,

        /// <summary>
        /// 通知，程序可以运行但是还不够完美的错误 6
        /// </summary>
        LOG_LEVEL_NOTICE = 6,

        /// <summary>
        /// 信息，程序输出信息 7
        /// </summary>
        LOG_LEVEL_INFO = 7,

        /// <summary>
        /// 调试，用于调试信息 8
        /// </summary>
        LOG_LEVEL_DEBUG = 8,

        /// <summary>
        /// SQL语句，该级别只在调试模式开启时有效 9
        /// </summary>
        LOG_LEVEL_SQL = 9,
    }

    #endregion
}
