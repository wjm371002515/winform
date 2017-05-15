using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JCodes.Framework.Common.Threading
{
    /// <summary>
    /// 线程池辅助操作类
    /// </summary>
    public class ThreadPoolHelper
    {
        /// <summary>
        /// 方法委托
        /// </summary>
        public delegate void WaitCallbackNew();

        /// <summary>
        /// （集合）通知等待线程，事件已经发生。这个类不能被继承。
        /// </summary>
        private static List<WaitHandle> AutoResetEvents;

        /// <summary>
        /// 线程池的实际WaitCallback委托对象。
        /// </summary>
        /// <param name="state"></param>
        static void Callback(object state)
        {
            WaitCallbackHelper wcbh = (state as WaitCallbackHelper);
            wcbh.Callback();
            (wcbh.WaitHandle as AutoResetEvent).Set();
        }

        /// <summary>
        /// 把执行方法放到队列中。
        /// 当线程池线程变为可用的时候，方法执行。
        /// </summary>
        /// <param name="callback">委托对象</param>
        public static bool QueueUserWorkItem(WaitCallbackNew callback)
        {
            WaitCallbackHelper wcbh = new WaitCallbackHelper();
            wcbh.Callback = callback;
            wcbh.WaitHandle = new AutoResetEvent(false);
            if (AutoResetEvents == null)
            {
                AutoResetEvents = new List<WaitHandle>();
            }
            AutoResetEvents.Add(wcbh.WaitHandle);

            return ThreadPool.QueueUserWorkItem(new WaitCallback(Callback), wcbh);
        }

        /// <summary>
        /// 把执行方法放到队列中。
        /// 当线程池线程变为可用的时候，方法执行。
        /// </summary>
        /// <param name="proc">委托对象数组</param>
        /// <returns></returns>
        public static bool QueueUserWorkItems(params WaitCallbackNew[] proc)
        {
            bool result = true;
            foreach (WaitCallbackNew tp in proc)
            {
                result &= QueueUserWorkItem(tp);
            }
            return result;
        }

        /// <summary>
        ///等待指定数组中所有元素收到信号
        /// </summary>
        public static bool WaitAll()
        {
            return WaitHandle.WaitAll(AutoResetEvents.ToArray());
        }

        /// <summary>
        ///等待指定数组中任何一个元素收到信号
        /// </summary>
        /// <returns>满足等待的对象数组索引</returns>
        public static int WaitAny()
        {
            return WaitHandle.WaitAny(AutoResetEvents.ToArray());
        }

        /// <summary>
        /// 一个回调一个WaitHandle容器
        /// </summary>
        class WaitCallbackHelper
        {
            public WaitCallbackNew Callback
            {
                get;
                set;
            }

            public WaitHandle WaitHandle
            {
                get;
                set;
            }
        }
    }
}
