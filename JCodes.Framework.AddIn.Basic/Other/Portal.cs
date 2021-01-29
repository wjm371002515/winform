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
using JCodes.Framework.CommonControl.Other;
using DevExpress.Utils;
using JCodes.Framework.Common.Files;
using JCodes.Framework.jCodesenum;
using System.Xml;

namespace JCodes.Framework.AddIn.Basic
{
    public class Portal
    {
        public static GlobalControl gc = new GlobalControl();
    }

    public class GlobalControl
    {
        public MainForm MainDialog;                                             // 主窗体对话框(对应的是MainForm)
        public string SYSTEMTYPEID = Const.SystemTypeId;                        // 系统类型
        public string AppUnit = string.Empty;                                   // 单位名称
        public string AppName = string.Empty;                                   // 程序名称
        public string AppWholeName = string.Empty;                              // 单位名称+程序名称
        public bool Registed { get; set;}                                       // 判断是否注册了   
        public bool EnableRegister = false;                                     // 设置一个开关，确定是否要求注册后才能使用软件
        public WaitDialogForm _waitBeforeLogin = null;                          // 登录等待窗口
        public AppConfig config = new AppConfig();                              // config 全局变量 只一次读取config文件提高效率
        public Boolean IsSuperAdmin = false;                                    // 配置超级管理员

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }

        public Dictionary<Int32, string> AllUserInfo { get; set; }

        public Dictionary<Int32, string> AllOuInfo { get; set; }

        /// <summary>
        /// 转换框架通用的用户基础信息，方便框架使用
        /// </summary>
        /// <param name="info">权限系统定义的用户信息</param>
        /// <returns></returns>
        public LoginUserInfo ConvertToLoginUser(UserInfo info)
        {
            LoginUserInfo loginInfo = new LoginUserInfo();
            loginInfo.Id = info.Id;
            loginInfo.Name = info.Name;
            loginInfo.LoginName = info.LoginName;
            loginInfo.IdCard = info.IdCard;
            loginInfo.MobilePhone = info.MobilePhone;
            loginInfo.QQ = info.QQ;
            loginInfo.Email = info.Email;
            loginInfo.Gender = info.Gender;
            loginInfo.DeptId = info.DeptId;
            loginInfo.CompanyId = info.CompanyId;
            return loginInfo;
        }

        /// <summary>
        /// 看用户是否具有某个功能
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public bool HasFunction(string functionId)
        {
            bool result = false;
            var cacheDict = Cache.Instance["FunctionDict"] as Dictionary<string, string>;
            if (string.IsNullOrEmpty(functionId))
            {
                result = true;
            }
            else if (cacheDict != null && cacheDict.ContainsKey(functionId))
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
        /// 加载缓存
        /// </summary>
        /// <returns></returns>
        public void LoadCache(UserInfo info)
        {
            Dictionary<string, string> functionDict = new Dictionary<string, string>();
            List<FunctionInfo> list = BLLFactory<Function>.Instance.GetFunctionsByUser(info.Id, Portal.gc.SYSTEMTYPEID);
            if (list != null && list.Count > 0)
            {
                functionDict.Clear();
                foreach (FunctionInfo functionInfo in list)
                {
                    // 20200303 改用功能确认是否有权限
                    if (!functionDict.ContainsKey(functionInfo.DllPath))
                    {
                        functionDict.Add(functionInfo.DllPath, functionInfo.Name);
                    }
                }
            }

            #region 获取角色对应的用户操作部门及公司范围
            List<int> companyLst = BLLFactory<RoleData>.Instance.GetBelongCompanysByUser(info.Id);
            List<int> deptLst = BLLFactory<RoleData>.Instance.GetBelongDeptsByUser(info.Id);
            StringBuilder companysb = new StringBuilder();
            StringBuilder deptsb = new StringBuilder();
            companysb.Append(" in (");
            for (int i = 0; i < companyLst.Count; i++)
            {
                companysb.Append(" '" + companyLst[i] + "', ");
            }
            companysb.Append(" '')");

            if (companyLst.Contains(-1))
            {
                companysb.Append(" or (1 = 1)");
            }

            deptsb.Append(" in (");
            for (int i = 0; i < deptLst.Count; i++)
            {
                deptsb.Append(" '" + deptsb[i] + "', ");
            }
            deptsb.Append(" '')");

            if (deptLst.Contains(-11))
            {
                deptsb.Append(" or (1 = 1)");
            }
            #endregion

            #region 获取标准字段
            XmlHelper xmlhelper = new XmlHelper(@"XML\stdfield.xml");
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/dataitem");
            Dictionary<string, string> dicStdField = new Dictionary<string, string>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;

                dicStdField.Add(xnl0.Item(0).InnerText, xnl0.Item(1).InnerText);
            }
            #endregion

            #region 获取全部用户信息
            List<UserInfo> lst = BLLFactory<User>.Instance.GetAll();

            if (AllUserInfo == null) AllUserInfo = new Dictionary<int, string>();

            foreach (var user in lst)
            {
                if (!AllUserInfo.ContainsKey(user.Id))
                    AllUserInfo.Add(user.Id, user.Name);  
            }
            #endregion

            #region 获取全部用户信息
            List<OUInfo> oulst = BLLFactory<OU>.Instance.GetAll();

            if (AllOuInfo == null) AllOuInfo = new Dictionary<int, string>();

            foreach (var ou in oulst)
            {
                if (!AllOuInfo.ContainsKey(ou.Id))
                    AllOuInfo.Add(ou.Id, ou.Name);
            }
            #endregion

                // 并保持到缓存中
            Cache.Instance["LoginUserInfo"] = ConvertToLoginUser(info);
            Cache.Instance["FunctionDict"] = functionDict;
            Cache.Instance["RoleList"] = BLLFactory<Role>.Instance.GetRolesByUser(info.Id);
            Cache.Instance["canOptCompanyId"] = companysb.ToString();
            Cache.Instance["canOptDeptId"] = deptsb.ToString();
            Cache.Instance["DictData"] = BLLFactory<DictData>.Instance.GetAllDict();
            Cache.Instance["AppConfig"] = Portal.gc.config;
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
            dlg.ID = loginUserInfo.Id;
            dlg.StartPosition = FormStartPosition.CenterScreen;
            dlg.ShowDialog();
        }*/
        #endregion

        /// <summary>
        /// 根据机构分类获取对应的图形序号
        /// </summary>
        /// <param name="category">机构分类</param>
        /// <returns></returns>
        public Int32 GetImageIndex(OuType ouType)
        {
            /*if (ouType == OuType.公司)
            {
                index = 1;
            }
            else if (ouType == OuType.部门)
            {
                index = 2;
            }
            else if (ouType == OuType.工作组)
            {
                index = 3;
            }*/
            return ((Int32)ouType - 1);
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

            if (Portal.gc.IsSuperAdmin)
            {
                //超级管理员取集团节点
                list.AddRange(BLLFactory<OU>.Instance.GetTopGroup());
            }
            else
            {
                groupInfo = BLLFactory<OU>.Instance.FindById(UserInfo.CompanyId);//公司管理员取公司节点
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
