using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Device;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Encrypt;
using System.Text;
using System.Drawing;

namespace JCodes.Framework.AddIn.Basic
{
    /// <summary>
    /// RegDlg 的摘要说明。
    /// </summary>
    public class RegDlg : BaseDock
    {
        private DevExpress.XtraEditors.TextEdit txtMachineCode;
        private DevExpress.XtraEditors.LabelControl lblMachineCode;
        private DevExpress.XtraEditors.MemoEdit txtSerialNumber;
        private DevExpress.XtraEditors.LabelControl lblSerialNumber;
        private Container components = null;
        private DevExpress.XtraEditors.SimpleButton btRegister;
        private DevExpress.XtraEditors.SimpleButton btnGetSerial;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.LabelControl lblCompany;
        private DevExpress.XtraEditors.TextEdit txtCompany;
        private DevExpress.XtraEditors.SimpleButton btnCopy;

        public RegDlg()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegDlg));
            this.txtMachineCode = new DevExpress.XtraEditors.TextEdit();
            this.lblMachineCode = new DevExpress.XtraEditors.LabelControl();
            this.txtSerialNumber = new DevExpress.XtraEditors.MemoEdit();
            this.lblSerialNumber = new DevExpress.XtraEditors.LabelControl();
            this.btRegister = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetSerial = new DevExpress.XtraEditors.SimpleButton();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.lblCompany = new DevExpress.XtraEditors.LabelControl();
            this.txtCompany = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMachineCode.Location = new System.Drawing.Point(104, 15);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Properties.ReadOnly = true;
            this.txtMachineCode.Size = new System.Drawing.Size(456, 28);
            this.txtMachineCode.TabIndex = 1;
            // 
            // lblMachineCode
            // 
            this.lblMachineCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMachineCode.Location = new System.Drawing.Point(44, 18);
            this.lblMachineCode.Name = "lblMachineCode";
            this.lblMachineCode.Size = new System.Drawing.Size(54, 22);
            this.lblMachineCode.TabIndex = 0;
            this.lblMachineCode.Text = "机器码";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSerialNumber.Location = new System.Drawing.Point(104, 167);
            this.txtSerialNumber.MinimumSize = new System.Drawing.Size(286, 100);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(518, 100);
            this.txtSerialNumber.TabIndex = 1;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSerialNumber.Location = new System.Drawing.Point(44, 167);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(54, 22);
            this.lblSerialNumber.TabIndex = 0;
            this.lblSerialNumber.Text = "序列号";
            // 
            // btRegister
            // 
            this.btRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRegister.Location = new System.Drawing.Point(399, 300);
            this.btRegister.Name = "btRegister";
            this.btRegister.Size = new System.Drawing.Size(96, 25);
            this.btRegister.TabIndex = 2;
            this.btRegister.Text = "注册";
            this.btRegister.Click += new System.EventHandler(this.btRegister_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(530, 15);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(55, 29);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "复制";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnGetSerial
            // 
            this.btnGetSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetSerial.Location = new System.Drawing.Point(517, 300);
            this.btnGetSerial.Name = "btnGetSerial";
            this.btnGetSerial.Size = new System.Drawing.Size(96, 25);
            this.btnGetSerial.TabIndex = 2;
            this.btnGetSerial.Text = "申请序列号";
            this.btnGetSerial.Click += new System.EventHandler(this.btnGetSerial_Click);
            // 
            // lblUserName
            // 
            this.lblUserName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUserName.Location = new System.Drawing.Point(26, 70);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(72, 22);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "注册用户";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(104, 67);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(518, 28);
            this.txtUserName.TabIndex = 1;
            // 
            // lblCompany
            // 
            this.lblCompany.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCompany.Location = new System.Drawing.Point(26, 119);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(72, 22);
            this.lblCompany.TabIndex = 0;
            this.lblCompany.Text = "注册公司";
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.Location = new System.Drawing.Point(104, 116);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(518, 28);
            this.txtCompany.TabIndex = 1;
            // 
            // RegDlg
            // 
            this.ClientSize = new System.Drawing.Size(643, 336);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnGetSerial);
            this.Controls.Add(this.btRegister);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.lblMachineCode);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.lblSerialNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(419, 300);
            this.Name = "RegDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "注册";
            this.Load += new System.EventHandler(this.RegDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void btRegister_Click(object sender, EventArgs e)
        {
            string regCode = txtSerialNumber.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string company = txtCompany.Text.Trim();

            // 校验文本框信息是否完整
            if (!CheckInput()) return;

            Int32 passed = -1;
            passed = RSASecurityHelper.CheckRegistrationCode(regCode, userName, company);

            if (passed == 0)
            {
                MessageDxUtil.ShowTips("祝贺您，注册成功\r\n请重启登录之后才会生效！");
            }
            else
            {
                MessageDxUtil.ShowError("注册失败");
            }
        }

        private bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region 校验文本框内容
            if (txtUserName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblUserName.Text.Replace(Const.MsgCheckSign, string.Empty));
                lblUserName.Appearance.ForeColor = Color.Red;
                this.txtUserName.Focus();
                result = false;
            }

            if (txtCompany.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblCompany.Text.Replace(Const.MsgCheckSign, string.Empty));
                lblCompany.Appearance.ForeColor = Color.Red;
                this.txtCompany.Focus();
                result = false;
            }

            if (txtSerialNumber.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblSerialNumber.Text.Replace(Const.MsgCheckSign, string.Empty));
                lblSerialNumber.Appearance.ForeColor = Color.Red;
                this.txtSerialNumber.Focus();
                result = false;
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 保存用户输入的序列号到注册表当中
        /// </summary>
        /// <param name="mySerail"></param>
        private void SaverInformationToRegedit(string mySerail)
        {
            RegistryKey reg;
            string regkey = UIConstants.SoftwareRegistryKey;
            reg = Registry.CurrentUser.OpenSubKey(regkey, true);
            if (null == reg)
            {
                reg = Registry.CurrentUser.CreateSubKey(regkey);
            }
            if (null != reg)
            {
                reg.SetValue("productName", UIConstants.SoftwareProductName);
                reg.SetValue("version", UIConstants.SoftwareVersion);
                reg.SetValue("SerialNumber", mySerail);
            }
        }

        private void RegDlg_Load(object sender, EventArgs e)
        {
            txtMachineCode.Text = HardwareInfoHelper.GetCPUId();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(this.txtMachineCode.Text);
            MessageDxUtil.ShowTips(Const.CopyOkMsg);
        }

        /// <summary>
        /// 申请序列号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetSerial_Click(object sender, EventArgs e)
        {
            MessageDxUtil.ShowTips(string.Format("请发送邮箱到{0}申请试用序列号, 邮箱标题【XXX申请jCodes 应用程序序列号】，邮件正文填写申请理由", Portal.gc.config.AppConfigGet("EMail")));
        }
    }
}