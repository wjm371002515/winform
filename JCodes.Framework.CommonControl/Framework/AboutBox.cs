using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.Framework
{
    partial class AboutBox : DevExpress.XtraEditors.XtraForm
    {
        public AboutBox()
        {
            InitializeComponent();

            //this.Text = String.Format("关于 {0} ", AssemblyTitle);
            //this.labelProductName.Text = AssemblyProduct;
            //this.labelVersion.Text = String.Format("版本 {0} ", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;

            #region 初始化系统名称
            try
            {
                AppConfig config = new AppConfig();
                string Manufacturer = config.AppConfigGet("Manufacturer");
                string CertificatedCompany = config.AppConfigGet("CertificatedCompany");
                string ApplicationName = config.AppConfigGet("ApplicationName");

                this.Text = string.Format("{0}-{1}", CertificatedCompany, ApplicationName);
                this.lblProductName.Text = ApplicationName;
                this.lblVersion.Text = String.Format("版本 {0}", AssemblyVersion);
                this.lblCopyright.Text = AssemblyCopyright;
                this.lblCertificated.Text = string.Format("授权【{0}】使用", CertificatedCompany);
                string description = config.AppConfigGet("Description");//软件介绍
                this.txtDescription.Text = description;
                this.lblContact.Text = AssemblyDescription;//联系方式
            }
            catch { }

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
                Clipboard.SetText(this.lblContact.Text);
                MessageDxUtil.ShowTips("感谢您的支持！");
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
