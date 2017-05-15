using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.Entity;

namespace JCodes.Framework.CommonControl.Framework
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public bool bLogin = false; //判断用户是否登录

        public Login()
        {
            InitializeComponent();

            this.txtUserName.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region 检查验证
            if (this.txtUserName.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请输入帐号");
                this.txtUserName.Focus();
                return;
            }
            #endregion

            try
            {
                string ip = NetworkUtil.GetLocalIP();
                string macAddr = HardwareInfoHelper.GetMacAddress();
                string loginName = this.txtUserName.Text.Trim();

                string identity = BLLFactory<User>.Instance.VerifyUser(loginName, this.txtPassword.Text, "Security", ip, macAddr);
                if (!string.IsNullOrEmpty(identity))
                {
                    if (BLLFactory<User>.Instance.UserIsAdmin(loginName))
                    {
                        UserInfo info = BLLFactory<User>.Instance.GetUserByName(loginName);                        
                        Portal.gc.UserInfo = info; //赋值给全局变量“管理用户”                        
                        Portal.gc.RoleList = BLLFactory<Role>.Instance.GetRolesByUser(info.ID);//用户的角色集合

                        bLogin = true;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageDxUtil.ShowWarning("该用户没有管理员权限");
                        return;
                    }
                }
                else
                {
                    MessageDxUtil.ShowWarning("用户名或密码错误或被禁止登陆");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError(ex.Message);
            }
        }


        private void labelControl1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Application.ExitThread();
        }

        private void Login_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPassword.Focus();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void txtRole_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); 
            //Application.ExitThread();
        }
    }
}