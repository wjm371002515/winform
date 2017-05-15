using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl
{
    internal partial class FrmQueryTextEdit : FrmQueryBase
    {
        public FrmQueryTextEdit()
        {
            InitializeComponent();

            this.txtContent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (this.txtContent.Text.Length > 0)
            {
                Clipboard.SetText(this.txtContent.Text);
            }
        }
        private void btnPaste_Click(object sender, EventArgs e)
        {
            this.txtContent.Text = Clipboard.GetText();
        }

        private void FrmQueryTextEdit_Load(object sender, EventArgs e)
        {
            this.lblFieldName.Text = this.FieldDisplayName;
            if (!string.IsNullOrEmpty(FieldDefaultValue))
            {
                this.txtContent.Text = FieldDefaultValue;
            }
            this.txtContent.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ReturnValue = this.txtContent.Text.Trim();
            this.ReturnDisplay = this.txtContent.Text.Trim();

            ProcessDataSearch(null, null);
        }

    }
}
