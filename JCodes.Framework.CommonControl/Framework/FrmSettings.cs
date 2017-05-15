using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.Framework
{
    public partial class FrmSettings : BaseForm
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            this.firefoxDialog1.ImageList = this.imageList1;

            this.firefoxDialog1.AddPage("报表设置", new PageReport());//基于本地文件的参数存储
            this.firefoxDialog1.AddPage("邮箱设置", new PageEmail());//基于数据库的参数存储

            //下面是陪衬的
            this.firefoxDialog1.AddPage("短信设置", new PageEmail());
            this.firefoxDialog1.AddPage("声音设置", new PageEmail());
            this.firefoxDialog1.AddPage("系统设置", new PageEmail());
            this.firefoxDialog1.AddPage("备份设置", new PageEmail());
            this.firefoxDialog1.AddPage("其他设置", new PageEmail());

            this.firefoxDialog1.Init();
        }
    }
}
