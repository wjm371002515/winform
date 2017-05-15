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
            FrmAsposeCell dlg = new FrmAsposeCell();
            dlg.ShowDialog();
        }

        private void btnAsposeWords_Click(object sender, EventArgs e)
        {
            FrmAsposeWords dlg = new FrmAsposeWords();
            dlg.ShowDialog();
        }

        private void btnFrmMyXls_Click(object sender, EventArgs e)
        {
            FrmMyXls dlg = new FrmMyXls();
            dlg.ShowDialog();
        }

        private void btnNPOI_Click(object sender, EventArgs e)
        {
            FrmNPOI dlg = new FrmNPOI();
            dlg.ShowDialog();
        }

        private void btnADSLDialer_Click(object sender, EventArgs e)
        {
            FrmADSLDialer dlg = new FrmADSLDialer();
            dlg.ShowDialog();
        }

        private void btnZipUtil_Click(object sender, EventArgs e)
        {
            FrmZipUtil dlg = new FrmZipUtil();
            dlg.ShowDialog();
        }
    }
}
