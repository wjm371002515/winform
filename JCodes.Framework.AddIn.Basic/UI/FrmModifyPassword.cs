using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Device;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Network;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.Basic
{
    public partial class FrmModifyPassword : BaseDock
    {
        public FrmModifyPassword()
        {
            InitializeComponent();
        }

        private void FrmModifyPassword_Load(object sender, EventArgs e)
        {
            this.txtLogin.Text = this.LoginUserInfo.LoginName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (this.txtRePassword.Text != this.txtPassword.Text)
            {
                MessageDxUtil.ShowTips("两个新密码的输入不一致");
                this.txtRePassword.Focus();
                return;
            }
            #endregion

            try
            {
                string ip = NetworkUtil.GetLocalIP();
                string mac = NetworkUtil.GetMacAddress();
                Boolean result = BLLFactory<User>.Instance.ModifyPassword(this.LoginUserInfo.Name, this.txtPassword.Text, Portal.gc.SYSTEMTYPEID, ip, mac);

                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageDxUtil.ShowTips("密码修改成功");
                }
                else
                {
                    MessageDxUtil.ShowWarning("用户密码资料不正确，请核对");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmModifyPassword));
                MessageDxUtil.ShowError(ex.Message);
            }
        }
    }
}
