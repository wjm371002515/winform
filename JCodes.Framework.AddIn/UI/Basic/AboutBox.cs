using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Basic
{
    partial class AboutBox : DevExpress.XtraEditors.XtraForm
    {
        public AboutBox()
        {
            InitializeComponent();

            #region 初始化系统名称
            try
            {
                AppConfig config = new AppConfig();
                string Manufacturer = config.AppConfigGet("Manufacturer");
                string ApplicationName = config.AppConfigGet("ApplicationName");
                string ContactInfo = config.AppConfigGet("ContactInfo");

                this.Text = string.Format("{0}-【{1}】", Manufacturer, ApplicationName);
                this.lblProductName.Text = ApplicationName;
                this.lblVersion.Text = String.Format("版本 {0}", Const.jCodes_VERSION);
                this.lblCertificated.Text = string.Format("授权【{0}】使用", Manufacturer);
                string description = config.AppConfigGet("Description");//软件介绍
                this.txtDescription.Text = description;
                this.lblContact.Text = ContactInfo;//联系方式
            }
            catch (Exception ex){
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(AboutBox));
                MessageDxUtil.ShowError(ex.Message);
                return;
            }

            #endregion 
        }

        #region 程序集属性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void lblContact_Click(object sender, EventArgs e)
        {            
            if (this.lblContact.Text.Trim().Length > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(this.lblContact.Text);
                MessageDxUtil.ShowTips(Const.CopyOkMsg);
            }
        }

        private void AboutBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
