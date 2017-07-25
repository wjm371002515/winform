using JCodes.Framework.CommonControl.BaseUI;
using System;
using System.Threading;

namespace JCodes.Framework.AddIn.Test.ThreadSetUI
{
    public partial class FrmsynccontextSetUI : BaseDock
    {
        //第一步：获取UI线程同步上下文（在窗体构造函数或FormLoad事件中）
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        SynchronizationContext m_SyncContext = null;

        public FrmsynccontextSetUI()
        {
            InitializeComponent();

            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext.Current;
        }

        //第二步：定义线程的主体方法
        /// <summary>
        /// 线程的主体方法
        /// </summary>
        private void ThreadProcSafePost()
        {
            //...执行线程任务

            //在线程中更新UI（通过UI线程同步上下文m_SyncContext）
            //在线程中更新UI（通过控件的.Invoke方法）
            System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为1000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler((obj, e) =>
            {
                Console.WriteLine(DateTime.Now.ToString("yyyyMMdd HHmmss:fff"));
                m_SyncContext.Post(SetTextSafePost, DateTime.Now.ToString("yyyyMMdd HHmmss:fff") + "This text was set safely by SynchronizationContext-Post.\r\n");
            });
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            //...执行线程其他任务
        }

        //第三步：定义更新UI控件的方法
        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void SetTextSafePost(object text)
        {
            this.richText.Text += text;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var demoThread = new Thread(new ThreadStart(this.ThreadProcSafePost));
            demoThread.Start();
        }

    }
}
