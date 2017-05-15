using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace JCodes.Framework.CommonControl
{
    public partial class NameControl : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DeleteEventHandler(string ID);
        public event DeleteEventHandler OnDeleteItem;

        public NameControl()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OnDeleteItem != null)
            {
                if (this.lblInfo.Tag != null)
                {
                    OnDeleteItem(this.lblInfo.Tag.ToString());
                }
            }
        }

        public void BindData(string ID, string Name)
        {
            this.lblInfo.Text = Name;
            this.lblInfo.Tag = ID;

            this.btnDelete.Tag = ID;
        }
    }
}
