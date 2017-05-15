using System.Threading;

namespace JCodes.Framework.Common.Threading
{
    /// <summary>
    /// WorkItem的储存指定的WaitCallback和用户状态对象，如当前的ExecutionContext对象一样。
    /// </summary>
    public sealed class WorkItem
    {
        private WaitCallback _Callback;
        private object _State;
        private ExecutionContext _Context;

        /// <summary>
        /// 构造函数
        /// </summary>
        internal WorkItem(WaitCallback callback, object state, ExecutionContext context)
        {
            _Callback = callback;
            _State = state;
            _Context = context;
        }


        #region 属性
        /// <summary>
        /// 线程池线程要执行的一个回调方法
        /// </summary>
        public WaitCallback Callback { get { return _Callback; } }

        /// <summary>
        /// 调方法要使用的一个对象，其中包含的信息
        /// </summary>
        public object State { get { return _State; } }

        /// <summary>
        /// 当前线程的执行上下文对象
        /// </summary>
        public ExecutionContext Context { get { return _Context; } }

        #endregion
    }
}
