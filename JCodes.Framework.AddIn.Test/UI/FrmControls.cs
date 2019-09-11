using JCodes.Framework.CommonControl.BaseUI;
using System;
using JCodes.Framework.CommonControl.Other;
using System.Windows.Forms;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common;
using System.Collections.Generic;
using JCodes.Framework.CommonControl.Controls;
using System.Diagnostics;

namespace JCodes.Framework.AddIn.Test
{
    public partial class FrmControls : BaseDock
    {
        public FrmControls()
        {
            InitializeComponent();
        }
        private void FrmControls_Load(object sender, System.EventArgs e)
        {
            cbb.BindDictItems(100000);
            ccbb.BindDictItems(100000);

            gridControl1.DataSource = new JCodes.Framework.BLL.GeneralSql().GetSqlTable("select t.ID, t.User_ID, t.LoginName, t2.FullName, t.CompanyName, t2.LastLoginIP, t2.LastLoginTime, t2.LastMacAddress, t2.CurrentLoginIP, t2.CurrentLoginTime, t2.CurrentMacAddress from T_AUTH_LoginLog t, T_AUTH_User t2 where t.User_ID = t2.ID order by LastLoginTime"); 
        }
      

        /// <summary>
        /// 下拉框的值(单选)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btncbb_Click(object sender, EventArgs e)
        {
            string str = cbb.GetComboBoxIntValue().ToString();
            MessageDxUtil.ShowTips(str);
        }

        private void btnccbb_Click(object sender, EventArgs e)
        {
            MessageDxUtil.ShowTips(ccbb.GetCheckedComboBoxValue());
        }

        private void btntxtPwd_Click(object sender, EventArgs e)
        {
            MessageDxUtil.ShowTips(txtPwd.Text);
        }

        /// <summary>
        /// 日期框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnde_Click(object sender, EventArgs e)
        {
            MessageDxUtil.ShowTips(de.DateTime.ToString("yyyy-MM-dd"));
        }

        private void btnte_Click(object sender, EventArgs e)
        {
            MessageDxUtil.ShowTips(te.Text);
        }

        private void ccbb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((DevExpress.XtraEditors.CheckedComboBoxEdit)sender).SelectAll();
            }
        }

        private void ccbb_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((DevExpress.XtraEditors.CheckedComboBoxEdit)sender).SelectAll();
            }
        }
    }
}
