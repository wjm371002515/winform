using JCodes.Framework.CommonControl.BaseUI;
using System;
using System.Threading;

namespace JCodes.Framework.AddIn.Test.ThreadSetUI
{
    public partial class FrmdelegateSetUI : BaseDock
    {
        public FrmdelegateSetUI()
        {
            InitializeComponent();
        }

        //第二步：定义线程的主体方法
        /// <summary>
        /// 线程的主体方法
        /// </summary>
        private void ThreadProcSafe()
        {
            //...执行线程任务

            //在线程中更新UI（通过控件的.Invoke方法）
            System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为1000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler((obj, e) => {
                Console.WriteLine(DateTime.Now.ToString("yyyyMMdd HHmmss:fff"));
                this.SetText(DateTime.Now.ToString("yyyyMMdd HHmmss:fff") + "This text was set safely.\r\n"); 
            });
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            //...执行线程其他任务
        }

        // 第一步：定义委托类型
        // 将text更新的界面控件的委托类型
        delegate void SetTextCallback(string text);
        private void btnStart_Click(object sender, EventArgs e)
        {
            var demoThread =
                new Thread(new ThreadStart(this.ThreadProcSafe));
            demoThread.Start();
        }

        //第三步：定义更新UI控件的方法
        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.richText.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.richText.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.richText.Disposing || this.richText.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(SetText);
                this.richText.Invoke(d, new object[] { text });
            }
            else
            {
                this.richText.Text += text;
            }
        }
    }
}
