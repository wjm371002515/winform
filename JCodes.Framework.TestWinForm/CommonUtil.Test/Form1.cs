using JCodes.Framework.Common.Winform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestControlUtil
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAsposeCell_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmAsposeCell));
        }

        private void btnAsposeWords_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmAsposeWords));
        }

        private void btnFrmMyXls_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmMyXls));
        }

        private void btnNPOI_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmNPOI));
        }

        private void btnADSLDialer_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmADSLDialer));
        }

        private void btnZipUtil_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmZipUtil));
        }
    }
}
