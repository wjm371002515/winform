using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.WareHouseManage
{
    public partial class FrmSetPurchaseQuantity : BaseDock
    {
        public string ID = "";
        public FrmSetPurchaseQuantity()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FrmSetPurchaseQuantity_Load(object sender, EventArgs e)
        {
            this.txtQuantity.Focus();
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }
    }
}
