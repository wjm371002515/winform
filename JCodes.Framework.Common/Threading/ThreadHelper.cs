using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;

namespace JCodes.Framework.Common.Threading
{
    /// <summary>
    /// 线程操作辅助类
    /// </summary>
    public class ThreadHelper
    {
        private static CultureInfo mainCulture;

        /// <summary>
        /// 线程名称，最长不超过10个字符！
        /// </summary>
        /// <param name="name">线程名称</param>
        public static void SetThreadName(string name)
        {
            if (Thread.CurrentThread.Name != null)
            {
                return;
            }

            Thread.CurrentThread.Name = "SVNM_" + name.PadRight(10);
            if (mainCulture != null)
            {
                Thread.CurrentThread.CurrentUICulture = mainCulture;
            }
        }

        /// <summary>
        /// 设置线程优先级
        /// </summary>
        /// <param name="priority">线程优先级</param>
        public static void SetThreadPriority(ThreadPriority priority)
        {
            if (Thread.CurrentThread.Priority != priority)
            {
                Thread.CurrentThread.Priority = priority;
            }
        }

        /// <summary>
        /// 设置主线程的UI Culture
        /// </summary>
        /// <param name="cultureName"></param>
        public static void SetMainThreadUICulture(string cultureName)
        {
            try
            {
                LogTextHelper.Info(string.Format("UICulture = {0}", cultureName));

                var culture = new CultureInfo(cultureName);
                mainCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(string.Format("Error setting UICulture: {0}", cultureName), ex);
            }
        }

        /// <summary>
        /// 把执行方法放到队列中，并指定了一个对象，它包含使用该方法的数据。
        /// 当线程池线程变为可用的时候，方法执行。
        /// </summary>
        /// <param name="callBack">工作项(WaitCallback)对象</param>
        /// <param name="threadName">线程名称，最长不超过10个字符！</param>
        /// <param name="priority">线程优先级</param>
        public static bool Queue(WaitCallback callBack, string threadName, ThreadPriority priority)
        {
            return Queue(callBack, threadName, null, priority);
        }

        /// <summary>
        /// 把执行方法放到队列中，并指定了一个对象，它包含使用该方法的数据。
        /// 当线程池线程变为可用的时候，方法执行。
        /// </summary>
        /// <param name="callBack">工作项(WaitCallback)对象</param>
        /// <param name="threadName">线程名称，最长不超过10个字符！</param>
        /// <param name="state">执行方法的数据</param>
        /// <param name="priority">线程优先级</param>
        public static bool Queue(WaitCallback callBack, string threadName, object state, ThreadPriority priority)
        {
            WaitCallback start = delegate(object _state)
            {
                SetThreadName(threadName);
                SetThreadPriority(priority);
                callBack(_state);
            };

            return ThreadPool.QueueUserWorkItem(start, state);
        }

        /// <summary>
        /// 线程休眠一段毫秒时间
        /// </summary>
        /// <param name="millisecondsTimeout">一段毫秒时间</param>
        public static void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        /// <summary>
        /// 线程休眠一段时间
        /// </summary>
        /// <param name="timeOut"></param>
        public static void Sleep(TimeSpan timeOut)
        {
            Thread.Sleep(timeOut);
        }
    }
}
