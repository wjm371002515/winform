using JCodes.Framework.CommonControl.BaseUI;
using System;
using JCodes.Framework.CommonControl.Other;
using System.Windows.Forms;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common;
using System.Collections.Generic;
using JCodes.Framework.CommonControl.Controls;

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
