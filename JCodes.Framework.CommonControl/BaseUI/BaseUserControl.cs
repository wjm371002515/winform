using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.Common;


namespace JCodes.Framework.CommonControl
{
    /// <summary>
    /// 自定义控件基类
    /// </summary>
    public partial class BaseUserControl : XtraUserControl, IFunction
    {
        /// <summary>
        /// 子窗体数据保存的触发
        /// </summary>
        public event EventHandler OnDataSaved;

        /// <summary>
        /// 进行数据过滤的Sql条件，默认通过 Cache.Instance["DataFilterCondition"]获取
        /// </summary>
        public string DataFilterCondition { get; set; }

        /// <summary>
        /// 选择查看的公司ID
        /// </summary>
        public string SelectedCompanyID { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseUserControl()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                //为了保证一些界面控件的权限控制和身份确认，以及简化操作，在界面初始化的时候，从缓存里面内容（如果存在的话）
                //继承的子模块，也可以通过InitFunction()进行指定用户相关信息
                this.LoginUserInfo = Cache.Instance["LoginUserInfo"] as LoginUserInfo;
                this.FunctionDict = Cache.Instance["FunctionDict"] as Dictionary<string, string>;

                // 进行数据过滤的Sql条件
                this.DataFilterCondition = Cache.Instance["DataFilterCondition"] as string;
                this.SelectedCompanyID = Cache.Instance["SelectedCompanyID"] as string;
            }
        }

        /// <summary>
        /// 处理数据保存后的事件触发
        /// </summary>
        public virtual void ProcessDataSaved(object sender, EventArgs e)
        {
            if (OnDataSaved != null)
            {
                OnDataSaved(sender, e);
            }
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="ex"></param>
        public void WriteException(Exception ex)
        {
            // 在本地记录异常
            LogTextHelper.Error(ex);
            MessageDxUtil.ShowError(ex.Message);
        }

        /// <summary>
        /// 处理异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        public void ProcessException(Exception ex)
        {
            this.WriteException(ex);

            // 显示异常页面
            //FrmException frmException = new FrmException(this.UserInfo, ex);
            //frmException.ShowDialog();

            MessageDxUtil.ShowError(ex.Message);//临时处理
        }

        /// <summary>
        /// 初始化权限控制信息
        /// </summary>
        public void InitFunction(LoginUserInfo userInfo, Dictionary<string, string> functionDict)
        {
            if (userInfo != null)
            {
                this.LoginUserInfo = userInfo;
            }
            if (functionDict != null && functionDict.Count > 0)
            {
                this.FunctionDict = functionDict;
            }
        }

        /// <summary>
        /// 是否具有访问指定控制ID的权限
        /// </summary>
        /// <param name="controlId">功能控制ID</param>
        /// <returns></returns>
        public bool HasFunction(string controlId)
        {
            bool result = false;
            if (string.IsNullOrEmpty(controlId))
            {
                result = true;
            }
            else if (FunctionDict != null && FunctionDict.ContainsKey(controlId))
            {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// 登陆用户基础信息
        /// </summary>
        public LoginUserInfo LoginUserInfo { get; set; }

        /// <summary>
        /// 登录用户具有的功能字典集合
        /// </summary>
        public Dictionary<string, string> FunctionDict { get; set; }

        private AppInfo m_AppInfo = new AppInfo();
        /// <summary>
        /// 应用程序基础信息
        /// </summary>
        public AppInfo AppInfo
        {
            get { return m_AppInfo; }
            set { this.m_AppInfo = value; }
        }
    }
}
