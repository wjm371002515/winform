using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.Common;

namespace JCodes.Framework.CommonControl
{
    internal partial class FrmQueryDropdown : FrmQueryBase
    {
        public FrmQueryDropdown()
        {
            InitializeComponent();

            this.ddlContent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
        }

        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                btnOK_Click(null, null);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ProcessDataClear(FieldName);
        }

        private void FrmQueryDropdown_Load(object sender, EventArgs e)
        {
            this.lblFieldName.Text = this.FieldDisplayName;
            if (this.DropDownItems != null)
            {
                this.ddlContent.Properties.Items.Clear();
                foreach (CListItem item in this.DropDownItems)
                {
                    this.ddlContent.Properties.Items.Add(item);
                }
            }
            if (!string.IsNullOrEmpty(FieldDefaultValue))
            {
                SetComboBoxItem(this.ddlContent, FieldDefaultValue);
            }

            this.ddlContent.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CListItem item = this.ddlContent.SelectedItem as CListItem;
            if (item != null)
            {
                this.ReturnValue = item.Value;
                this.ReturnDisplay = item.Text;
            }

            ProcessDataSearch(null, null);
        }

        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        private void SetComboBoxItem(ComboBoxEdit combo, string value)
        {
            for (int i = 0; i < combo.Properties.Items.Count; i++)
            {
                CListItem item = combo.Properties.Items[i] as CListItem;
                if (item != null && item.Value == value)
                {
                    combo.SelectedIndex = i;
                }
            }
        }
    }

}
