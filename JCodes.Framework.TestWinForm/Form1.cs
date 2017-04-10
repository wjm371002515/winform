using JCodes.Framework.Common.WEB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCodes.Framework.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnURL_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text.Trim();
            rtbLog.Text = rtbLog.Text + URL.URLDeConvert(url) + "\r\n";
        }

        private void btnDeURL_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text.Trim();
            rtbLog.Text = rtbLog.Text + URL.URLEnConvert(url) + "\r\n";
        }
    }
}
