using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using System.IO;
using DevExpress.XtraEditors;
using JCodes.Framework.CommonControl.Other;

namespace JCodes.Framework.WinFormUI
{
	public partial class frmConnSettings : DevExpress.XtraEditors.XtraForm {
        public frmConnSettings()
        {
			InitializeComponent();
		}

        private void buttonOK_Click(object sender, EventArgs e)
        {
            MessageDxUtil.ShowTips("此功能待完善");
        }
	}
}

