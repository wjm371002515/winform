using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.SmallTools
{
    // 参考网址:http://blog.csdn.net/irwin_chen/article/details/7430551
    // 1.定义委托  
    public delegate void DelReadStdOutput(string result);
    public delegate void DelReadErrOutput(string result);  

    public partial class FrmBaiduPan : BaseDock
    {
        // 2.定义委托事件  
        public event DelReadStdOutput ReadStdOutput;
        public event DelReadErrOutput ReadErrOutput;

        Process CmdProcess = null;

        public FrmBaiduPan()
        {
            InitializeComponent();

            //3.将相应函数注册到委托事件中  
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            ReadErrOutput += new DelReadErrOutput(ReadErrOutputAction);

            txtURL.Focus();
        }

        private void btnStartDosComand_Click(object sender, EventArgs e)
        {
            #region 检查URL
            if (txtURL.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning("请输入百度网盘下载地址");
                return;
            }

            if (txtURL.Text.Trim().Contains("pan.baidu.com"))
            {
                MessageDxUtil.ShowWarning("输入百度网盘地址不合法请检查");
                return;
            }

            if (ckPwd.Checked && txtPwd.Text.Length == 0)
            {
                MessageDxUtil.ShowWarning("输入百度网盘地址对应的密码");
                return;
            }
            if (ckPwd.Checked && txtPwd.Text.Length != 4)
            {
                MessageDxUtil.ShowWarning("输入百度网盘地址对应的密码长度不对");
                return;
            }



            #endregion



            // 启动进程执行相应命令,此例中以执行ping.exe为例  
            //RealAction(txtURL.Text.Trim());
        }

        #region 执行DOS命令
        private void RealAction(string command)
        {
            // 先注销掉上一个DOS命令
            if (CmdProcess != null)
            {
                CmdProcess.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                CmdProcess.ErrorDataReceived -= new DataReceivedEventHandler(p_ErrorDataReceived);
                CmdProcess.Close();
                CmdProcess.Dispose();
                CmdProcess = null;
            }

            CmdProcess = new Process();
         
            CmdProcess.StartInfo.FileName = "cmd.exe";           // 命令   

            CmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口  
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入  
            CmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出  
            CmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出 
            //CmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;  

            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            CmdProcess.EnableRaisingEvents = true;                      // 启用Exited事件  
            CmdProcess.Exited += new EventHandler(CmdProcess_Exited);   // 注册进程结束事件  

            CmdProcess.Start();
            CmdProcess.StandardInput.WriteLine(command);
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            // 如果打开注释，则以同步方式执行命令，此例子中用Exited事件异步执行。  
            // CmdProcess.WaitForExit();       
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                // 4. 异步调用，需要invoke  
                this.Invoke(ReadStdOutput, new object[] { e.Data });
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke(ReadErrOutput, new object[] { e.Data });
            }
        }

        private void ReadStdOutputAction(string result)
        {
            richResultShow.Text = richResultShow.Text + result + "\r\n";
            //richResultShow.VerticalScrollPosition =1000;
            richResultShow.Document.CaretPosition = richResultShow.Document.Range.End;
            richResultShow.ScrollToCaret();
            Application.DoEvents();
            System.Threading.Thread.Sleep(10);
        }

        private void ReadErrOutputAction(string result)
        {
            richResultShow.Text = richResultShow.Text + result + "\r\n";
            //richResultShow.VerticalScrollPosition = 1000;
            richResultShow.Document.CaretPosition = richResultShow.Document.Range.End;
            richResultShow.ScrollToCaret();
            Application.DoEvents();
            System.Threading.Thread.Sleep(10);
        }

        private void CmdProcess_Exited(object sender, EventArgs e)
        {
            // 执行结束后触发  
        }

        #endregion

        private void FrmDos_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 先注销掉上一个DOS命令
            if (CmdProcess != null)
            {
                CmdProcess.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived);
                CmdProcess.ErrorDataReceived -= new DataReceivedEventHandler(p_ErrorDataReceived);
                CmdProcess.Close();
                CmdProcess.Dispose();
                CmdProcess = null;
            }
        }

        /// <summary>
        /// 是否需要密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckPwd_CheckedChanged(object sender, EventArgs e)
        {
            // 勾选了
            if (ckPwd.Checked)
            {
                labelControl2.Enabled = true;
                txtPwd.Enabled = true;
            }
            else
            {
                labelControl2.Enabled = false;
                txtPwd.Enabled = false;
            }
        }
    }
}
