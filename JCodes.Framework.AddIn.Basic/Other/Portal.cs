using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.Framework;
using Microsoft.Win32;
using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl.PlugInInterface;
using JCodes.Framework.CommonControl.Pager;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Device;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Other;
using DevExpress.Utils;
using JCodes.Framework.Common.Files;

namespace JCodes.Framework.AddIn.Basic
{
    public class Portal
    {
        public static GlobalControl gc = new GlobalControl();
    }

    public class GlobalControl
    {
        public MainForm MainDialog;                                         // 主窗体对话框(对应的是MainForm)
        public string SystemType = "071bafed-4634-4083-bb34-86dda58edfc4";  //系统类型
        public string AppUnit = string.Empty;                               //单位名称
        public string AppName = string.Empty;                               //程序名称
        public string AppWholeName = string.Empty;                          //单位名称+程序名称
        public bool Registed { get; set;}                                   // 判断是否注册了   
        public bool EnableRegister = false;                                 //设置一个开关，确定是否要求注册后才能使用软件
        public WaitDialogForm _waitBeforeLogin = null;                      // 登录等待窗口
        public AppConfig config = new AppConfig();                                 // config 全局变量 只一次读取config文件提高效率

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 转换框架通用的用户基础信息，方便框架使用
        /// </summary>
        /// <param name="info">权限系统定义的用户信息</param>
        /// <returns></returns>
        public LoginUserInfo ConvertToLoginUser(UserInfo info)
        {
            LoginUserInfo loginInfo = new LoginUserInfo();
            loginInfo.ID = info.ID;
            loginInfo.Name = info.Name;
            loginInfo.FullName = info.FullName;
            loginInfo.IdentityCard = info.IdentityCard;
            loginInfo.MobilePhone = info.MobilePhone;
            loginInfo.QQ = info.QQ;
            loginInfo.Email = info.Email;
            loginInfo.Gender = info.Gender;

            loginInfo.DeptId = info.Dept_ID;
            loginInfo.CompanyId = info.Company_ID;
            return loginInfo;
        }

        /// <summary>
        /// 看用户是否具有某个功能
        /// </summary>
        /// <param name="controlID"></param>
        /// <returns></returns>
        public bool HasFunction(string controlID)
        {
            bool result = false;
            var cacheDict = Cache.Instance["FunctionDict"] as Dictionary<string, string>;
            if (string.IsNullOrEmpty(controlID))
            {
                result = true;
            }
            else if (cacheDict != null && cacheDict.ContainsKey(controlID))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 如果用户没有注册，提示用户注册
        /// </summary>
        public void ShowRegDlg()
        {
            RegDlg myRegdlg = new RegDlg();
            myRegdlg.StartPosition = FormStartPosition.CenterScreen;
            myRegdlg.TopMost = true;
            myRegdlg.Hide();
            myRegdlg.Show();
            myRegdlg.BringToFront();
        }

        /// <summary>
        /// 每次程序运行时候,检查用户是否注册(如果第一次, 那么写入第一次运行时间)
        /// </summary>
        /// <returns>如果用户已经注册, 那么返回True, 否则为False</returns>
        public bool CheckRegister()
        {
            // 先获取用户的注册码进行比较
            string serialNumber = string.Empty; //注册码
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(UIConstants.SoftwareRegistryKey, true);
            if (null != reg)
            {
                serialNumber = (string)reg.GetValue("SerialNumber");
                Portal.gc.Registed = Portal.gc.Register(serialNumber);
            }

            return Portal.gc.Registed;
        }

        /// <summary>
        /// 调用非对称加密方式对序列号进行验证
        /// </summary>
        /// <param name="serialNumber">正确的序列号</param>
        /// <returns>如果成功返回True，否则为False</returns>
        public bool Register(String serialNumber)
        {
            string hardNumber = HardwareInfoHelper.GetCPUId();
            return RSASecurityHelper.Validate(hardNumber, serialNumber);
        }

        #region 基本操作函数
        /// <summary>
        /// Opens the help document.
        /// </summary>
        public void Help()
        {
            try
            {
                const string helpfile = Const.HelpFile;
                Process.Start(helpfile);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GlobalControl));
                MessageDxUtil.ShowError(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 显示当前用户信息 TODO
        /// </summary>
        /*public void CurrentUserInfo() {
            FrmEditUser dlg = new FrmEditUser();
            var loginUserInfo = Cache.Instance["LoginUserInfo"] as LoginUserInfo;
            dlg.ID = loginUserInfo.ID.ToString();
            dlg.StartPosition = FormStartPosition.CenterScreen;
            dlg.ShowDialog();
        }*/
        #endregion

        /// <summary>
        /// 根据机构分类获取对应的图形序号
        /// </summary>
        /// <param name="category">机构分类</param>
        /// <returns></returns>
        public int GetImageIndex(string category)
        {
            int index = 0;
            if (category == OUCategoryEnum.公司.ToString())
            {
                index = 1;
            }
            else if (category == OUCategoryEnum.部门.ToString())
            {
                index = 2;
            }
            else if (category == OUCategoryEnum.工作组.ToString())
            {
                index = 3;
            }
            return index;
        }

        /// <summary>
        /// 判断当前用户具有某个角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public bool UserInRole(string roleName)
        {
            var roleList = Cache.Instance["RoleList"] as List<RoleInfo>;

            bool result = false;
            if (roleList != null)
            {
                foreach (RoleInfo info in roleList)
                {
                    if (info.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 根据当前用户身份，获取对应的顶级机构管理节点。
        /// 如果是超级管理员，返回集团节点；如果是公司管理员，返回其公司节点
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetMyTopGroup()
        {
            List<OUInfo> list = new List<OUInfo>();
            OUInfo groupInfo = null;
            if (UserInRole(RoleInfo.SuperAdminName))
            {
                //超级管理员取集团节点
                list.AddRange(BLLFactory<OU>.Instance.GetTopGroup());
            }
            else
            {
                groupInfo = BLLFactory<OU>.Instance.FindByID(UserInfo.Company_ID);//公司管理员取公司节点
                list.Add(groupInfo);
            }
            return list;
        }

        #region 弹出提示消息窗口
        /// <summary>
        /// 弹出提示消息窗口
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="content"></param>
        public void Notify(string caption, string content)
        {
            Notify(caption, content, 400, 200, 5000);
        }

        /// <summary>
        /// 弹出提示消息窗口
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="content"></param>
        public void Notify(string caption, string content, int width, int height, int waitTime)
        {
            NotifyWindow notifyWindow = new NotifyWindow(caption, content);
            notifyWindow.TitleClicked += new System.EventHandler(notifyWindowClick);
            notifyWindow.TextClicked += new EventHandler(notifyWindowClick);
            notifyWindow.SetDimensions(width, height);
            notifyWindow.WaitTime = waitTime;
            notifyWindow.Notify();

            //保存到系统消息表
            //SystemMessageInfo messageInfo = new SystemMessageInfo();
            //messageInfo.ID = Guid.NewGuid().ToString();
            //messageInfo.Title = caption;
            //messageInfo.Content = content;
            //try
            //{
            //    BLLFactory<SystemMessage>.Instance.Insert(messageInfo);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WareHouseHelper));
            //    MessageDxUtil.ShowError(ex.Message);
            //}
        }

        private void notifyWindowClick(object sender, EventArgs e)
        {
            //SystemMessageInfo info = BLLFactory<SystemMessage>.Instance.FindLast();
            //if (info != null)
            //{
            //    //FrmEditMessage dlg = new FrmEditMessage();
            //    //dlg.ID = info.ID;
            //    //dlg.ShowDialog();
            //}
        }
        #endregion
    }
}
