using JCodes.Framework.AddIn;
using JCodes.Framework.AddIn.Other;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Framework;
using JCodes.Framework.CommonControl.PlugInInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Common
{
    public partial class FrmSettings : BaseDock
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            this.firefoxDialog1.ImageList = this.imageList1;

            var loginUserInfo = Cache.Instance["LoginUserInfo"] as LoginUserInfo;

            this.firefoxDialog1.AddPage("报表设置", new PageReport());//基于本地文件的参数存储
            this.firefoxDialog1.AddPage("邮箱设置", new PageEmail(loginUserInfo.Name));//基于数据库的参数存储

            //下面是陪衬的
            this.firefoxDialog1.AddPage("短信设置", new PageEmail(loginUserInfo.Name));
            this.firefoxDialog1.AddPage("声音设置", new PageEmail(loginUserInfo.Name));
            this.firefoxDialog1.AddPage("系统设置", new PageEmail(loginUserInfo.Name));
            this.firefoxDialog1.AddPage("备份设置", new PageEmail(loginUserInfo.Name));
            this.firefoxDialog1.AddPage("其他设置", new PageEmail(loginUserInfo.Name));

            this.firefoxDialog1.Init();
        }
    }
}
