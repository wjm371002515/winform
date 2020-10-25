using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JCodes.Framework.Common.Threading
{
    /// <summary>
    /// 提供一个队列的线程处理服务
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class QueueServer<T> : DisposableObject
    {
        private Thread thread = null;
        private Queue<T> queue = new Queue<T>();
        private bool isBackground = false;
        private bool disposed = false;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public QueueServer()
        {
        }

        #region  属性

        /// <summary>
        /// 是否是背景线程
        /// </summary>
        public bool IsBackground
        {
            get
            {
                return this.isBackground;
            }
            set
            {
                this.isBackground = true;
                if ((this.thread != null) && (this.thread.IsAlive))
                {
                    this.thread.IsBackground = this.isBackground;
                }
            }
        }

        /// <summary>
        /// 执行队列
        /// </summary>
        public T[] Items
        {
            get
            {
                lock (this.queue)
                {
                    return this.queue.ToArray();
                }
            }
        }

        /// <summary>
        /// 队列数量
        /// </summary>
        public int QueueCount
        {
            get
            {
                lock (this.queue)
                {
                    return this.queue.Count;
                }
            }
        }

        #endregion

        #region  公共方法

        /// <summary>
        /// 将对象加到队列结尾
        /// </summary>
        /// <param name="item"></param>
        public void EnqueueItem(T item)
        {
            lock (this.queue)
            {
                this.queue.Enqueue(item);
            }
            if ((this.thread == null) || !(this.thread.IsAlive))
            {
                this.CreateThread();
                this.thread.Start();
            }
        }

        /// <summary>
        /// 清除队列
        /// </summary>
        public void ClearItems()
        {
            lock (this.queue)
            {
                this.queue.Clear();
            }
        }

        #endregion

        #region 线程处理

        /// <summary>
        /// 创建线程
        /// </summary>
        private void CreateThread()
        {
            this.thread = new Thread(new ThreadStart(this.ThreadProc));
            this.thread.IsBackground = this.isBackground;
        }

        /// <summary>
        /// 线程处理
        /// </summary>
        private void ThreadProc()
        {
            T item = default(T);
            while (true)
            {
                lock (this.queue)
                {
                    if (this.queue.Count > 0)
                    {
                        item = this.queue.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }
                try
                {
                    this.OnProcessItem(item);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(QueueServer<T>));
                }
            }
        }

        /// <summary>
        /// 处理单个元素
        /// </summary>
        /// <param name="item">元素项目</param>
        protected virtual void OnProcessItem(T item)
        {
            if (ProcessItem != null)
            {
                ProcessItem(item);
            }
        }

        /// <summary>
        /// 处理函数
        /// </summary>
        public event Action<T> ProcessItem;

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    this.ClearItems();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
