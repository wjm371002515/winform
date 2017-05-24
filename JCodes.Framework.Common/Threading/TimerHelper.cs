using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JCodes.Framework.Common.Threading
{
    /// <summary>
    /// 定时器辅助类，可指定运行间隔、延迟启动时间等操作。
    /// </summary>
    [Serializable]
    public class TimerHelper : System.ComponentModel.Component
    {
        private System.Threading.Timer timer;
        private long timerInterval;
        private TimerState timerState;

        /// <summary>
        /// 定时器执行操作的函数原型
        /// </summary>
        public delegate void TimerExecution();

        /// <summary>
        /// 定时器执行时调用的操作
        /// </summary>
        public event TimerExecution Execute;

        /// <summary>
        /// 创建一个指定时间间隔的定时器，并在指定的延迟后开始启动。（默认间隔为100毫秒）
        /// </summary>
        public TimerHelper()
        {
            timerInterval = 100;
            timerState = TimerState.Stopped;
            timer = new System.Threading.Timer(new TimerCallback(Tick), null, Timeout.Infinite, timerInterval);
        }


        /// <summary>
        /// 创建一个指定时间间隔的定时器，并在指定的延迟后开始启动。
        /// </summary>
        /// <param name="interval">定时器执行操作的间隔时间（毫秒）</param>
        /// <param name="startDelay">指定的延迟时间（毫秒）</param>
        public TimerHelper(long interval, int startDelay)
        {
            timerInterval = interval;
            timerState = (startDelay == Timeout.Infinite) ? TimerState.Stopped : TimerState.Running;
            timer = new System.Threading.Timer(new TimerCallback(Tick), null, startDelay, interval);
        }

        /// <summary>
        /// 创建一个指定时间间隔的定时器
        /// </summary>
        /// <param name="interval">定时器执行操作的间隔时间（毫秒）</param>
        /// <param name="start">是否启动</param>
        public TimerHelper(long interval, bool start)
        {
            timerInterval = interval;
            timerState = (!start) ? TimerState.Stopped : TimerState.Running;
            timer = new System.Threading.Timer(new TimerCallback(Tick), null, 0, interval);
        }

        /// <summary>
        /// 启动定时器并指定延迟时间（毫秒）
        /// </summary>
        /// <param name="delayBeforeStart">指定延迟时间（毫秒）</param>
        public void Start(int delayBeforeStart)
        {
            timerState = TimerState.Running;
            timer.Change(delayBeforeStart, timerInterval);
        }

        /// <summary>
        /// 立即启动定时器
        /// </summary>
        public void Start()
        {
            timerState = TimerState.Running;
            timer.Change(0, timerInterval);
        }

        /// <summary>
        /// 暂停定时器
        /// 注意：运行中的线程不会被停止
        /// </summary>
        public void Pause()
        {
            timerState = TimerState.Paused;
            timer.Change(Timeout.Infinite, timerInterval);
        }

        /// <summary>
        /// 停止定时器
        /// 注意：运行中的线程不会被停止
        /// </summary>
        public void Stop()
        {
            timerState = TimerState.Stopped;
            timer.Change(Timeout.Infinite, timerInterval);
        }

        /// <summary>
        /// 定时器的处理时间
        /// </summary>
        /// <param name="obj"></param>
        public void Tick(object obj)
        {
            if (timerState == TimerState.Running && Execute != null)
            {
                lock (this)
                {
                    Execute();
                }
            }
        }

        /// <summary>
        /// 定时器的状态
        /// </summary>
        public TimerState State
        {
            get
            {
                return timerState;
            }
        }

        /// <summary>
        /// 获取或设置定时器的运行间隔
        /// </summary>
        public long Interval
        {
            get
            {
                return timerInterval;
            }
            set
            {
                timer.Change(((timerState == TimerState.Running) ? value : Timeout.Infinite), value);
            }
        }
    }
}
