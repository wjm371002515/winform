using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.AdvanceSearch
{
    internal partial class FrmQueryDateEdit : FrmQueryBase
    {
        public FrmQueryDateEdit()
        {
            InitializeComponent();

            this.dtStart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            this.dtStart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            //判断输入的内容是否为空，来决定是否匹配日期
            if (dtStart.Text.Length > 0)
            {
                this.ReturnDisplay = string.Format("{0}", dtStart.DateTime.ToString("yyyy-MM-dd"));
                this.ReturnValue = string.Format("{0}", dtStart.DateTime.ToString("yyyy-MM-dd"));
            }
            if(dtEnd.Text.Length > 0)
            {
                this.ReturnDisplay += string.Format(" ~ {0}", dtEnd.DateTime.ToString("yyyy-MM-dd"));
                this.ReturnValue += string.Format(" ~ {0}", dtEnd.DateTime.ToString("yyyy-MM-dd"));
            }            

            ProcessDataSearch(null, null);
        }

        private void FrmQueryDateEdit_Load(object sender, EventArgs e)
        {
            this.lblFieldName.Text = this.FieldDisplayName;
            if (!string.IsNullOrEmpty(FieldDefaultValue))
            {
                string[] itemArray = FieldDefaultValue.Split('~');
                if (itemArray != null)
                {
                    DateTime value;
                    bool result = false;

                    if (itemArray.Length > 0)
                    {
                        result = DateTime.TryParse(itemArray[0].Trim(), out value);
                        if (result)
                        {
                            this.dtStart.DateTime = value;
                        }
                    }
                    if (itemArray.Length > 1)
                    {
                        result = DateTime.TryParse(itemArray[1].Trim(), out value);
                        if (result)
                        {
                            this.dtEnd.DateTime = value;
                        }
                    }
                }
            }
            this.dtStart.Focus();
        }
    }
}
