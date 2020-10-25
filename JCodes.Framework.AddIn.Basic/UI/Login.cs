using System;
using System.Windows.Forms;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Network;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Device;
using DevExpress.XtraEditors;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Basic
{
    public partial class Login : XtraForm
    {
        public bool bLogin = false; //判断用户是否登录

        public Login()
        {
            InitializeComponent();

            this.txtUserName.Focus();

            if (Portal.gc._waitBeforeLogin != null)
            {
                Portal.gc._waitBeforeLogin.Close(); Portal.gc._waitBeforeLogin = null;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region 检查验证
            if (this.txtUserName.Text.Length == 0)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "请输入帐号", typeof(Login));
                MessageDxUtil.ShowError("请输入帐号");
                this.txtUserName.Focus();
                return;
            }

            if (this.txtPassword.Text.Length == 0)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "请输入密码", typeof(Login));
                MessageDxUtil.ShowError("请输入密码");
                this.txtPassword.Focus();
                return;
            }
            #endregion

            try
            {
                string loginName = this.txtUserName.Text.Trim();
                string ip = NetworkUtil.GetLocalIP();
                string macAddr = HardwareInfoHelper.GetMacAddress();
                string identity = BLLFactory<User>.Instance.VerifyUser(loginName, this.txtPassword.Text, Portal.gc.SYSTEMTYPEID, ip, macAddr);
                if (!string.IsNullOrEmpty(identity))
                {
                    UserInfo info = BLLFactory<User>.Instance.GetUserByName(loginName);
                    
                    // 20191207 wjm 新增判断超级管理员 系统配置参数为1
                    // 20171109 wjm 不应该直接去判断这个Name的值，不合理 删除其逻辑判断
                    if (info != null && BLLFactory<Sysparameter>.Instance.UserIsSuperAdmin(loginName))
                    {
                        Portal.gc.UserInfo = info;
                        Portal.gc.IsSuperAdmin = true; 
                        bLogin = true;
                        this.DialogResult = DialogResult.OK;
                    }
                    else if (info != null && BLLFactory<Role>.Instance.UserHasRole(info.Id))
                    {
                        Portal.gc.UserInfo = info;
                        Portal.gc.IsSuperAdmin = false;
                        bLogin = true;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("该用户({0})没有管理员权限", loginName), typeof(Login));
                        MessageDxUtil.ShowError(string.Format("该用户({0})没有管理员权限", loginName));
                        txtUserName.Focus();
                        return;
                    }
                }
                else
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "用户名或密码错误或被禁止登陆", typeof(Login));
                    MessageDxUtil.ShowError("用户名或密码错误或被禁止登陆");
                    txtUserName.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Login));
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); 
        }
    }
}