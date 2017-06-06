using JCodes.Framework.Common;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Network;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Basic
{
    public partial class FrmFeeBack : BaseForm
    {
        private int maxTry = 3;
        private int currentTry = 0;

        public FrmFeeBack()
        {
            InitializeComponent();
        }

        private void FrmFeeBack_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.txtAdvise.Dispose();//显式关闭空间，防止错误出现
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 检查地址
            this.DialogResult = DialogResult.None;
            if (this.txtAdvise.Text == null || this.txtAdvise.Text.Trim().Length < 10)
            {
                MessageDxUtil.ShowTips("您的建议太短(少于10个字符），请输入详细一些内容，谢谢。");
                this.txtAdvise.Focus();
                return;
            }
            else if (this.txtEmail.Text.Length == 0 || !ValidateUtil.IsEmail(this.txtEmail.Text))
            {
                MessageDxUtil.ShowTips("请输入邮件地址，以便我们能够快速联系到您。");
                this.txtEmail.Focus();
                return;
            }
            #endregion

            SendEmail();
            this.DialogResult = DialogResult.OK;
            MessageDxUtil.ShowTips("谢谢您的建议！");
        }

        private void SendEmail()
        {
            Thread.Sleep(100);
            currentTry++;

            EmailHelper email = new EmailHelper("smtp.163.com", "wuhuacong2013@163.com", "123abc");
            email.Subject = string.Format("来自【{0}】对Winform开发框架的建议", this.txtEmail.Text);
            email.Body = this.txtAdvise.Text;//支持嵌入图片的内容发送
            email.IsHtml = true;
            email.From = "wuhuacong2013@163.com";
            email.FromName = "wuhuacong2013";
            email.AddRecipient("6966254@qq.com");
            //email.AddAttachment(System.IO.Path.Combine(Application.StartupPath, "cityroad.jpg")); 

            try
            {
                bool success = email.SendEmail();
                if (success)
                {
                    currentTry = 0;
                }
                else if (currentTry < maxTry)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, email.ErrorMessage, typeof(FrmFeeBack));
                    SendEmail();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmFeeBack));
                MessageDxUtil.ShowError(ex.Message);
                if (currentTry <= maxTry)
                {
                    SendEmail();
                }
                currentTry = 0;
            }  
        }
    }
}
