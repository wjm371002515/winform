using DevExpress.LookAndFeel;
using JCodes.Framework.AddIn;
using JCodes.Framework.AddIn.Other;
using JCodes.Framework.AddIn.UI.Basic;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Device;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Network;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestSecurityMix_WCF_WIN
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        //登录用户具有的功能字典集合
        public Dictionary<string, FunctionInfo> functionDict = new Dictionary<string, FunctionInfo>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //获取所有权限管理系统的用户，并在下拉列表中展示
            List<UserInfo> userList = BLLFactory<User>.Instance.GetAll();
            this.txtLogin.Properties.BeginUpdate();
            this.txtLogin.Properties.Items.Clear();
            foreach (UserInfo info in userList)
            {
                this.txtLogin.Properties.Items.Add(info.Name);
            }
            this.txtLogin.Properties.EndUpdate();
        }

        private void btnSecurity_Click(object sender, EventArgs e)
        {
            //代码设置授权码
            //WHC.Security.MyConstants.License = "397cV0hDLlNlY3VybXR5fOS8jeWNjuiBqnzlua-lt57niLHlkK-o_6rmioDmnK-mnInpmZDlhbzlj7h8RmFsc2Uv";

            //独立启动权限管理系统，只需一行代码即可
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");

            Login dlg = new Login();
            dlg.StartPosition = FormStartPosition.CenterScreen;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (dlg.bLogin)
                {
                    MainForm MainDialog = new MainForm();
                    Portal.gc.MainDialog = MainDialog;
                    MainDialog.ShowDialog();
                }
            }
            dlg.Dispose();
        }

        private void btnTestLogin_Click(object sender, EventArgs e)
        {
            if (this.txtLogin.Text.Trim() == "")
            {
                MessageDxUtil.ShowTips("请输入帐号");
                this.txtLogin.Focus();
                return;
            }

            try
            {
                //判断用户是否登录成功
                string loginName = this.txtLogin.Text.Trim();
                string ip = NetworkUtil.GetLocalIP();
                string macAddr = HardwareInfoHelper.GetMacAddress();
                string systemType = "WareMis";
                string identity = BLLFactory<User>.Instance.VerifyUser(loginName, this.txtPassword.Text, systemType, ip, macAddr);
                if (!string.IsNullOrEmpty(identity))
                {
                    UserInfo info = BLLFactory<User>.Instance.GetUserByName(loginName);
                    if (info != null)
                    {
                        //获取该登陆用户的权限集合
                        List<FunctionInfo> list = BLLFactory<Functions>.Instance.GetFunctionsByUser(info.ID, systemType);
                        if (list != null && list.Count > 0)
                        {
                            foreach (FunctionInfo functionInfo in list)
                            {
                                if (!functionDict.ContainsKey(functionInfo.ControlID))
                                {
                                    functionDict.Add(functionInfo.ControlID, functionInfo);
                                }
                            }
                        }

                        //进一步判断用户角色
                        if (BLLFactory<User>.Instance.UserIsAdmin(loginName))
                        {
                            MessageDxUtil.ShowTips(string.Format("用户【{0}】身份验证正确", loginName));
                        }
                        else
                        {
                            MessageDxUtil.ShowWarning("该用户没有管理员权限");
                            return;
                        }
                    }
                }
                else
                {
                    MessageDxUtil.ShowWarning("用户名或密码错误");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Form1));
                MessageDxUtil.ShowError(ex.Message); 
            }
        }

        private void txtLogin_KeyUp(object sender, KeyEventArgs e)
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
                btnTestLogin_Click(null, null);
            }
        }
    }
}
