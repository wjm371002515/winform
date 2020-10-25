using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Network;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.Framework
{
    public partial class FrmException : Form
    {
        #region 全局变量
        Exception _bugInfo;
        #endregion

        #region 构造函数
        /// <summary>
        /// Bug发送窗口
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        public FrmException(Exception bugInfo)
        {
            InitializeComponent();
            _bugInfo = bugInfo;
            this.txtBugInfo.Text = bugInfo.Message;
            lblErrorCode.Text = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Bug发送窗口
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        /// <param name="errorCode">错误号</param>
        public FrmException(Exception bugInfo, string errorCode)
        {
            InitializeComponent();
            _bugInfo = bugInfo;
            this.txtBugInfo.Text = bugInfo.Message;
            lblErrorCode.Text = errorCode;
        }
        #endregion

        #region 公开静态方法
        /// <summary>
        /// 提示Bug
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        /// <param name="errorCode">错误号</param>
        public static void ShowBug(Exception bugInfo, string errorCode)
        {
            new FrmException(bugInfo, errorCode).ShowDialog();
        }

        /// <summary>
        /// 提示Bug
        /// </summary>
        /// <param name="bugInfo">Bug信息</param>
        public static void ShowBug(Exception bugInfo)
        {
            ShowBug(bugInfo, Guid.NewGuid().ToString());
        }
        #endregion

        private void btnDetailsInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("异常详细信息：" + _bugInfo.Message + "\r\n跟踪：" + _bugInfo.StackTrace);
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // 发送错误报告 
            if (chkSendBug.Checked)
            {
                this.DialogResult = DialogResult.None;
                if (this.txtContentInfo.Text == null || this.txtContentInfo.Text.Trim().Length < 10)
                {
                    MessageDxUtil.ShowTips("您反馈内容太短(少于10个字符），请输入详细一些内容，谢谢。");
                    this.txtContentInfo.Focus();
                    return;
                }

                if (SendEmail())
                {
                    DialogResult = DialogResult.OK;
                    MessageDxUtil.ShowTips("反馈成功，感谢你的本产品的支持");
                }
                else
                {
                    DialogResult = DialogResult.No;
                    MessageDxUtil.ShowError("反馈失败");
                }
            }

            // 重启程序
            if (chkReboot.Checked)
            {
                string strAppFileName = Process.GetCurrentProcess().MainModule.FileName;
                Process myNewProcess = new Process();
                myNewProcess.StartInfo.FileName = strAppFileName;
                myNewProcess.StartInfo.Arguments = Const.Restart;
                myNewProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                myNewProcess.Start();
                Application.ExitThread();  
            }

            this.Close();
        }

        private bool SendEmail()
        {
            Thread.Sleep(Const.SLEEP_TIME);
            EmailHelper email = new EmailHelper("smtp.163.com", "codeany@163.com", "abc123");
            email.Subject = string.Format("jCodes 框架奔溃反馈 ");
            email.Body = "这个问题是如何出现的:"+txtContentInfo.Text+"<br />异常详细信息：" + _bugInfo.Message + "<br />跟踪日志：<br />" + _bugInfo.StackTrace + "<br />";
            email.IsHtml = true;
            email.From = "codeany@163.com";
            email.FromName = "jCodes 官方邮箱";
            email.AddRecipient("codeany@163.com");
            email.RecipientName = "jCodes 官方邮箱";

            // 把日志打包以附件的形式发送到邮箱
            if (chkCanHelp.Checked)
            {
                AppConfig config = Cache.Instance["AppConfig"] as AppConfig;
                if (config == null)
                {
                    config = new AppConfig();
                    Cache.Instance["AppConfig"] = config;
                }	
                string LicensePath = config.AppConfigGet("LicensePath");
                if (FileUtil.IsExistFile(LicensePath))
                {
                    string[] tmpstr = FileUtil.FileToString(LicensePath).Split(Convert.ToChar(Const.VerticalLine));

                    if (tmpstr.Length == 3)
                    {
                        email.Body += "<br /> 注册用户:" + tmpstr[1] + "<br />注册公司:" + tmpstr[2];
                    }
                }
            }
            bool success = false;

            try
            {
                success = email.SendEmail();
                return success;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmException));
                MessageDxUtil.ShowError(ex.Message);
                success = false;
                return success;
            }
        }

    }
}
