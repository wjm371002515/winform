using JCodes.Framework.CommonControl.BaseUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.DocViewer
{
    /// <summary>
    /// 图片显示窗体
    /// </summary>
    public partial class FrmImageView : BaseForm
    {
        public FrmImageView()
        {
            InitializeComponent();
        }

        private void pictureEdit1_Properties_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }  
    }
}
