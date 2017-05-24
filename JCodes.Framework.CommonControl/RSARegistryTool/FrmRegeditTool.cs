using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using Microsoft.Win32;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Device;
using JCodes.Framework.Common.Encrypt;

namespace JCodes.Framework.CommonControl.RSARegistryTool
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class FrmRegeditTool : Form
    {
        #region 属性变量
        private Label label3;
        private TextBox txtMachineCode;
        private Label label4;
        private TextBox txtRegisterCode;
        private Button btnOK;
        private Container components = null;
        private Button btnValidateCode;
        private Button btnGenerateNewKey;
        private TextBox txtExpiredDate;
        private Label label5;
        private Button btnGetMachineCode;
        private Button btnReg;
        private Button btnUnreg;
        private Button btnGetExpiredDate;
        
        #endregion

        public FrmRegeditTool()
		{
			InitializeComponent();

            Init_View();
		}

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void Init_View()
        {
            txtMachineCode.Text = HardwareInfoHelper.GetCPUId();
            txtExpiredDate.Text = DateTime.Now.AddYears(1).ToString("yyyyMMdd");
        }

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegeditTool));
            this.label3 = new System.Windows.Forms.Label();
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRegisterCode = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnValidateCode = new System.Windows.Forms.Button();
            this.btnGenerateNewKey = new System.Windows.Forms.Button();
            this.txtExpiredDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGetMachineCode = new System.Windows.Forms.Button();
            this.btnGetExpiredDate = new System.Windows.Forms.Button();
            this.btnReg = new System.Windows.Forms.Button();
            this.btnUnreg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "机器码";
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMachineCode.Location = new System.Drawing.Point(119, 21);
            this.txtMachineCode.Multiline = true;
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(704, 29);
            this.txtMachineCode.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "软件注册码";
            // 
            // txtRegisterCode
            // 
            this.txtRegisterCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegisterCode.Location = new System.Drawing.Point(119, 94);
            this.txtRegisterCode.Multiline = true;
            this.txtRegisterCode.Name = "txtRegisterCode";
            this.txtRegisterCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRegisterCode.Size = new System.Drawing.Size(704, 276);
            this.txtRegisterCode.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(275, 393);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 42);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "生成注册码";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnValidateCode
            // 
            this.btnValidateCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnValidateCode.Location = new System.Drawing.Point(391, 393);
            this.btnValidateCode.Name = "btnValidateCode";
            this.btnValidateCode.Size = new System.Drawing.Size(77, 42);
            this.btnValidateCode.TabIndex = 6;
            this.btnValidateCode.Text = "验证";
            this.btnValidateCode.UseVisualStyleBackColor = true;
            this.btnValidateCode.Click += new System.EventHandler(this.btnValidateCode_Click);
            // 
            // btnGenerateNewKey
            // 
            this.btnGenerateNewKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerateNewKey.Location = new System.Drawing.Point(474, 393);
            this.btnGenerateNewKey.Name = "btnGenerateNewKey";
            this.btnGenerateNewKey.Size = new System.Drawing.Size(143, 42);
            this.btnGenerateNewKey.TabIndex = 7;
            this.btnGenerateNewKey.Text = "生成新公钥私钥";
            this.btnGenerateNewKey.UseVisualStyleBackColor = true;
            this.btnGenerateNewKey.Click += new System.EventHandler(this.btnGenerateNewKey_Click);
            // 
            // txtExpiredDate
            // 
            this.txtExpiredDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpiredDate.Location = new System.Drawing.Point(119, 58);
            this.txtExpiredDate.Name = "txtExpiredDate";
            this.txtExpiredDate.Size = new System.Drawing.Size(704, 28);
            this.txtExpiredDate.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(19, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "有效日期";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGetMachineCode
            // 
            this.btnGetMachineCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetMachineCode.Location = new System.Drawing.Point(12, 393);
            this.btnGetMachineCode.Name = "btnGetMachineCode";
            this.btnGetMachineCode.Size = new System.Drawing.Size(123, 42);
            this.btnGetMachineCode.TabIndex = 4;
            this.btnGetMachineCode.Text = "获取机器码";
            this.btnGetMachineCode.Click += new System.EventHandler(this.btnGetMachineCode_Click);
            // 
            // btnGetExpiredDate
            // 
            this.btnGetExpiredDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetExpiredDate.Location = new System.Drawing.Point(141, 393);
            this.btnGetExpiredDate.Name = "btnGetExpiredDate";
            this.btnGetExpiredDate.Size = new System.Drawing.Size(128, 42);
            this.btnGetExpiredDate.TabIndex = 4;
            this.btnGetExpiredDate.Text = "获取有效日期";
            this.btnGetExpiredDate.Click += new System.EventHandler(this.btnGetExpiredDate_Click);
            // 
            // btnReg
            // 
            this.btnReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReg.Location = new System.Drawing.Point(623, 393);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(77, 42);
            this.btnReg.TabIndex = 6;
            this.btnReg.Text = "注册";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // btnUnreg
            // 
            this.btnUnreg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnreg.Location = new System.Drawing.Point(706, 393);
            this.btnUnreg.Name = "btnUnreg";
            this.btnUnreg.Size = new System.Drawing.Size(77, 42);
            this.btnUnreg.TabIndex = 6;
            this.btnUnreg.Text = "注销";
            this.btnUnreg.UseVisualStyleBackColor = true;
            this.btnUnreg.Click += new System.EventHandler(this.btnUnreg_Click);
            // 
            // FrmRegeditTool
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(10, 21);
            this.ClientSize = new System.Drawing.Size(891, 468);
            this.Controls.Add(this.txtExpiredDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGenerateNewKey);
            this.Controls.Add(this.btnUnreg);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.btnValidateCode);
            this.Controls.Add(this.btnGetExpiredDate);
            this.Controls.Add(this.btnGetMachineCode);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRegisterCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegeditTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件注册码生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		//加密
		private void btnOK_Click(object sender, EventArgs e)
		{
            this.txtRegisterCode.Text = RSASecurityHelper.GetRegistrationCode();
		}

        private string GetMachineCode()
        {
            return HardwareInfoHelper.GetCPUId();
        }

		//验证
		private void btnValidateCode_Click(object sender, EventArgs e)
		{
            AppConfig config = new AppConfig();
            string LicensePath = config.AppConfigGet("LicensePath");
            string machineCode = GetMachineCode();
            string regCode = txtRegisterCode.Text.Trim();//FileUtil.FileToString(LicensePath);
            Int32 passed = -1;
            passed = RSASecurityHelper.CheckRegistrationCode(regCode);
            
			if(passed == 0)
			{
				MessageBox.Show("验证成功", "提示",  MessageBoxButtons.OK); 
			}
			else
			{
				MessageBox.Show("验证不成功", "提示",  MessageBoxButtons.OK); 
			}
		}

        private void btnGenerateNewKey_Click(object sender, EventArgs e)
        {
            FrmKeyPair dlg = new FrmKeyPair();
            dlg.ShowDialog();
        }

        private void btnGetMachineCode_Click(object sender, EventArgs e)
        {
            txtMachineCode.Text = HardwareInfoHelper.GetCPUId();
        }

        private void btnGetExpiredDate_Click(object sender, EventArgs e)
        {
            string nowDate = Data.getSysDate();
            txtExpiredDate.Text = Convert.ToDateTime(nowDate).AddYears(1).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReg_Click(object sender, EventArgs e)
        {
            AppConfig config = new AppConfig();
            string LicensePath = config.AppConfigGet("LicensePath");
            string machineCode = GetMachineCode();
            string regCode = txtRegisterCode.Text.Trim(); //FileUtil.FileToString(LicensePath);
            Int32 passed = -1;
            passed = RSASecurityHelper.CheckRegistrationCode(regCode);

            if (passed == 0)
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
                    reg.SetValue("SysDate", Data.getSysDate());
                    reg.SetValue("regCode", regCode);
                }

                // 写入lic 文件
                FileUtil.WriteText(LicensePath, txtRegisterCode.Text.Trim(), Encoding.Default);
                MessageDxUtil.ShowTips("祝贺您，注册成功");
                Close();
                Application.Exit();
            }
            else
            {
                MessageBox.Show("注册失败", "提示", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnreg_Click(object sender, EventArgs e)
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
                reg.SetValue("SysDate", Data.getSysDate());
                reg.SetValue("regCode", string.Empty);
            }
            AppConfig config = new AppConfig();
            string LicensePath = config.AppConfigGet("LicensePath");
            FileUtil.DeleteFile(LicensePath);
            MessageDxUtil.ShowTips("祝贺您，注销成功");
            Close();
            Application.Exit();
        }
	}
}
