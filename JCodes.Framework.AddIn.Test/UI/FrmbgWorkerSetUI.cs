using JCodes.Framework.CommonControl.BaseUI;
using System;
using System.ComponentModel;
using System.Threading;

namespace JCodes.Framework.AddIn.Test
{
    public partial class FrmbgWorkerSetUI : BaseDock
    {
        //第一步：定义BackgroundWorker对象，并注册事件（执行线程主体、执行UI更新事件）
        private BackgroundWorker backgroundWorker1 = null;

        public FrmbgWorkerSetUI()
        {
            InitializeComponent();

            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            //设置报告进度更新
            backgroundWorker1.WorkerReportsProgress = true;
            //注册线程主体方法
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //注册更新UI方法
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            //backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
        }

        //第二步：定义执行线程主体事件
        //线程主体方法
        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //在线程中更新UI（通过ReportProgress方法）
            //...执行线程任务


            for (Int32 i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                backgroundWorker1.ReportProgress(i, DateTime.Now.ToString("yyyyMMdd HHmmss:fff") + "This text was set safely by BackgroundWorker.\r\n");
            }


            //...执行线程其他任务
        }

        //第三步：定义执行UI更新事件
        //UI更新方法
        public void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.richText.Text += e.UserState.ToString();
        }

        //之后，启动线程
        //启动backgroundWorker
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
        }

    }
}
