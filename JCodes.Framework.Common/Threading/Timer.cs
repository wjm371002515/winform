using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Threading;

namespace JCodes.Framework.Common.Threading
{
    /// <summary>
    /// 定期执行某些任务的定时器辅助类
    /// </summary>
    public class Timer
    {
        #region 事件或属性

        /// <summary>
        /// 按定时器周期定期引发的事件
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// 定时器任务间隔（毫秒）
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// 指示是否在方法开始的时候，启动定时器Elapsed事件一次。默认为false。
        /// </summary>
        public bool RunOnStart { get; set; }

        #endregion

        #region 字段定义

        /// <summary>
        ///此定时器用于执行 指定间隔的任务
        /// </summary>
        private readonly System.Threading.Timer _taskTimer;

        /// <summary>
        /// 指示定时器是运行还是停止
        /// </summary>
        private volatile bool _running;

        /// <summary>
        /// 指示是正在执行任务还是_taskTimer处于睡眠模式。
        /// 当停止定时器时候，此字段用于等待执行任务。
        /// </summary>
        private volatile bool _performingTasks;

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建一个定时器
        /// </summary>
        /// <param name="period">定时器间隔 (毫秒)</param>
        public Timer(int period)
            : this(period, false)
        {

        }

        /// <summary>
        /// 创建一个定时器
        /// </summary>
        /// <param name="period">定时器间隔 (毫秒)</param>
        /// <param name="runOnStart">指示是否在方法开始的时候，启动定时器Elapsed事件一次</param>
        public Timer(int period, bool runOnStart)
        {
            Period = period;
            RunOnStart = runOnStart;
            _taskTimer = new System.Threading.Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 启动定时器
        /// </summary>
        public void Start()
        {
            _running = true;
            _taskTimer.Change(RunOnStart ? 0 : Period, Timeout.Infinite);
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void Stop()
        {
            lock (_taskTimer)
            {
                _running = false;
                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        /// <summary>
        /// 等待定时器停止
        /// </summary>
        public void WaitToStop()
        {
            lock (_taskTimer)
            {
                while (_performingTasks)
                {
                    Monitor.Wait(_taskTimer);
                }
            }
        }

        /// <summary>
        /// 定时器处理
        /// </summary>
        /// <param name="state">不使用参数</param>
        private void TimerCallBack(object state)
        {
            lock (_taskTimer)
            {
                if (!_running || _performingTasks)
                {
                    return;
                }

                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _performingTasks = true;
            }

            try
            {
                if (Elapsed != null)
                {
                    Elapsed(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Timer));
            }
            finally
            {
                lock (_taskTimer)
                {
                    _performingTasks = false;
                    if (_running)
                    {
                        _taskTimer.Change(Period, Timeout.Infinite);
                    }

                    Monitor.Pulse(_taskTimer);
                }
            }
        }

        #endregion
    }
}
