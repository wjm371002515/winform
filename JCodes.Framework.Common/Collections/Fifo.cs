using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JCodes.Framework.Common.Collections
{
    /// <summary>
    /// 线程安全的先进先出队列
    /// </summary>
    /// <typeparam name="T">任意类型的参数</typeparam>
    public sealed class Fifo<T>
    {
        private Queue<T> queue = null;
        private int maxCount = int.MaxValue - 1;
        private AutoResetEvent writeLock = new AutoResetEvent(true);
        private AutoResetEvent readLock = new AutoResetEvent(true);
        private object thisObject = new object();
        private object objWrite = new object();
        private object objRead = new object();

        /// <summary>
        /// 默认最大值是int.MaxValue的实例
        /// </summary>
        public Fifo()
        {
            queue = new Queue<T>();
        }

        /// <summary>
        /// 指定队列大小的构造函数
        /// </summary>
        /// <param name="capacity"></param>
        public Fifo(int capacity)
        {
            queue = new Queue<T>(capacity);
        }

        /// <summary>
        /// 自定义最大值的实例
        /// </summary>
        /// <param name="MaxCount">大于一小于int.MaxValue</param>
        /// <param name="capacity">队列容量</param>
        public Fifo(int MaxCount, int capacity)
            : this(capacity)
        {
            if (MaxCount > 1 || MaxCount < int.MaxValue)
            {
                maxCount = MaxCount;
            }
        }

        /// <summary>
        /// 队列中目前存在个数
        /// </summary>
        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        /// <summary>
        /// 队列的最大容量
        /// </summary>
        public int MaxCount
        {
            get
            {
                return maxCount;
            }
        }

        /// <summary>
        /// 重新设置队列的最大容量
        /// </summary>
        /// <param name="MaxCount">大于1的整数</param>
        public void ResetMaxCount(int MaxCount)
        {
            if (MaxCount > 1 || MaxCount < int.MaxValue)
            {
                maxCount = MaxCount;
            }
        }

        /// <summary>
        /// 进队,将指定的对象值添加到队列的尾部
        /// </summary>
        /// <param name="obj">T 型的参数</param>
        public void Append(T obj)
        {
            lock (objWrite)
            {
                while (queue.Count >= maxCount)
                {
                    writeLock.WaitOne(Timeout.Infinite, false);
                }
                lock (thisObject)
                {
                    queue.Enqueue(obj);
                    readLock.Set();
                }
            }
        }

        /// <summary>
        /// 元素出队，即移除队列中开始的元素，
        /// 按先进先出（FIFO）的规则，从前向后移除元素。
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            lock (objRead)
            {
                while (queue.Count <= 0)
                {
                    readLock.WaitOne(Timeout.Infinite, false);
                }
                lock (thisObject)
                {
                    T obj = queue.Dequeue();
                    writeLock.Set();
                    return obj;
                }
            }
        }
    }
}
