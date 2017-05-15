using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Threading;

namespace JCodes.Framework.Common.Threading
{
    /// <summary>
    /// 线程池中的可以取消执行操作的线程池辅助类
    /// </summary>
    public static class AbortableThreadPool
    {        
        private static readonly LinkedList<WorkItem> CallbacksList = new LinkedList<WorkItem>();
        private static readonly Dictionary<WorkItem, Thread> ThreadList = new Dictionary<WorkItem, Thread>();

        /// <summary>
        /// 把执行操作放到队列中。当线程池的线程可用的时候，方法执行。
        /// </summary>
        /// <param name="callback">一个代表将要执行方法的WaitCallback对象</param>
        public static WorkItem QueueUserWorkItem(WaitCallback callback)
        {
            return QueueUserWorkItem(callback, null);
        }

        /// <summary>
        /// 把执行操作放到队列中，并指定了一个对象，它包含将要执行方法的数据。
        /// 当线程池的线程可用的时候，方法执行。
        /// </summary>
        /// <param name="callback">一个代表将要执行方法的WaitCallback对象</param>
        /// <param name="state">一个对象，它包含将要执行方法的数据</param>
        public static WorkItem QueueUserWorkItem(WaitCallback callback, object state)
        {
            WorkItem item = new WorkItem(callback, state, ExecutionContext.Capture());
            lock (CallbacksList)
            {
                CallbacksList.AddLast(item);
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(HandleItem));
            return item;
        }

        /// <summary>
        /// 处理队列中的线程池工作项目。
        /// </summary>
        /// <param name="ignored">The ignored.</param>
        private static void HandleItem(object ignored)
        {
            WorkItem item = null;
            try
            {
                lock (CallbacksList)
                {
                    if (CallbacksList.Count > 0)
                    {
                        item = CallbacksList.First.Value;
                        CallbacksList.RemoveFirst();
                    }

                    if (item == null)
                    {
                        return;
                    }
                    ThreadList.Add(item, Thread.CurrentThread);

                }
                ExecutionContext.Run(item.Context, delegate { item.Callback(item.State); }, null);
            }
            finally
            {
                lock (CallbacksList)
                {
                    if (item != null)
                    {
                        ThreadList.Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// 取消指定的队列中的工作项。
        /// </summary>
        /// <param name="item">线程池中取消的项目</param>
        /// <param name="allowAbort">如果设置为<see langword="true"/>则允许终止线程</param>
        /// <returns>项目队列的状态</returns>
        public static WorkItemStatus Cancel(WorkItem item, bool allowAbort)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            lock (CallbacksList)
            {
                LinkedListNode<WorkItem> node = CallbacksList.Find(item);

                if (node != null)
                {
                    CallbacksList.Remove(node);
                    return WorkItemStatus.Queued;
                }
                else if (ThreadList.ContainsKey(item))
                {
                    if (allowAbort)
                    {
                        ThreadList[item].Abort();
                        ThreadList.Remove(item);
                        return WorkItemStatus.Aborted;
                    }
                    else
                    {
                        return WorkItemStatus.Executing;
                    }
                }
                else
                {
                    return WorkItemStatus.Completed;
                }
            }
        }

        /// <summary>
        /// 获取指定队列中工作项的状态
        /// </summary>
        /// <param name="item">线程池中工作项</param>
        /// <returns>工作项的状态</returns>
        public static WorkItemStatus GetStatus(WorkItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            lock (CallbacksList)
            {
                LinkedListNode<WorkItem> node = CallbacksList.Find(item);

                if (node != null)
                {
                    return WorkItemStatus.Queued;
                }
                else if (ThreadList.ContainsKey(item))
                {
                    return WorkItemStatus.Executing;
                }
                else
                {
                    return WorkItemStatus.Completed;
                }
            }
        }

        /// <summary>
        /// 取消所有任务
        /// </summary>
        /// <param name="allowAbort">线程是否终止</param>
        public static void CancelAll(bool allowAbort)
        {
            lock (CallbacksList)
            {
                CallbacksList.Clear();
                if (allowAbort)
                {
                    foreach (Thread t in ThreadList.Values)
                    {
                        t.Abort();
                    }
                    ThreadList.Clear();
                }
            }
        }

        /// <summary>
        /// 类似Thread.Join,等待AbortableThreadPool执行完成
        /// </summary>
        public static void Join()
        {
            foreach (Thread thread in ThreadList.Values)
            {
                thread.Join();
            }
        }

        /// <summary>
        /// 类似Thread.Join,等待AbortableThreadPool执行完成
        /// </summary>
        /// <param name="millisecondsTimeout">等待的毫秒数</param>
        /// <returns></returns>
        public static  bool Join(int millisecondsTimeout)
        {
            foreach (Thread thread in ThreadList.Values)
            {
                if (!thread.Join(millisecondsTimeout))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 类似Thread.Join,等待AbortableThreadPool执行完成
        /// </summary>
        /// <param name="timeout">等待的时间范围</param>
        /// <returns></returns>
        public static bool Join(TimeSpan timeout)
        {
            foreach (Thread thread in ThreadList.Values)
            {
                if (!thread.Join(timeout))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 在队列中，还未执行处理的数量
        /// </summary>
        public static int QueueCount
        {
            get
            {
                lock (CallbacksList)
                {
                    return CallbacksList.Count;
                }
            }
        }

        /// <summary>
        /// 在执行中的线程数量
        /// </summary>
        public static int WorkingCount
        {
            get
            {
                lock (ThreadList)
                {
                    return ThreadList.Count;
                }
            }
        }

    }
}
