using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace JCodes.Framework.AddIn.UI.Basic
{
    /// <summary>
    /// 简化的登陆界面
    /// </summary>
    public partial class Frm_Login : DevExpress.XtraEditors.XtraForm 
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            //var frmMain = new frmMain();
           // frmMain.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// 关闭登陆界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbClose_Click(object sender, EventArgs e)
        {
            //alertControl1.Show(this, "报错", "测试内容");
            this.Close();
        }

        private void sbCustomization_Click(object sender, EventArgs e)
        {
            //var frmSetting = new frmSettings();
            //frmSetting.ShowDialog();
        }
    }
}
