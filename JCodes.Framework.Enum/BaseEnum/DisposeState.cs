
namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary>
    /// 实现IDisposable接口的对象状态
    /// </summary>
    public enum DisposeState
    {
        /// <summary>
        /// 完全初始化对象
        /// </summary>
        None = 0,

        /// <summary>
        /// 对象正在尝试释放
        /// </summary>
        Disposing = 1,

        /// <summary>
        ///对象处理完毕
        /// </summary>
        Disposed = 2
    }
}
