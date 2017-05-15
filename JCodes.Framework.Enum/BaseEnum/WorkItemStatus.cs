
namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary>
    /// WorkItem的对象执行状态
    /// </summary>
    public enum WorkItemStatus
    {
        /// <summary>
        /// 项目执行完毕
        /// </summary>
        Completed,

        /// <summary>
        /// 项目是目前在执行队列
        /// </summary>
        Queued,

        /// <summary>
        /// 该项目目前正在执行
        /// </summary>
        Executing,

        /// <summary>
        /// 项目被中止
        /// </summary>
        Aborted
    }
}
