
namespace JCodes.Framework.jCodesenum.BaseEnum
{
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
}
