using JCodes.Framework.AddIn;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
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

namespace JCodes.Framework.AddIn.Dictionary
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

            var lst = BLLFactory<DictData>.Instance.GetDictByTypeID(Const.DIC_PARAMETER);
            foreach (var dic in lst)
            {
                var frm1 = new FrmSysparameter(dic.DicttypeValue, dic.Name);
                this.firefoxDialog1.AddPage(dic.Name, frm1);//基于本地文件的参数存储
                frm1.MeEvent += firefoxDialog1.ChangeValue;
            }
            this.firefoxDialog1.Init();
        }
    }
}
