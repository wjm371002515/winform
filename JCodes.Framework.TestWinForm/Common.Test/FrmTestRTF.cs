using JCodes.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestCommons
{
    public partial class FrmTestRTF : Form
    {
        public FrmTestRTF()
        {
            InitializeComponent();
        }

        private void btnSetBold_Click(object sender, EventArgs e)
        {
            int start = this.richTextBox1.SelectionStart;
            int length = this.richTextBox1.SelectionLength;
            string original = this.richTextBox1.Text.Substring(start, length);
            if (!string.IsNullOrEmpty(original))
            {
                string newValue = RTFUtility.Bold(original);
            }
        }

        private void btnSetItalic_Click(object sender, EventArgs e)
        {
            string content = RTFUtility.Bold("斜体内容");
            this.richTextBox1.AppendText(content);
        }

        private void btnConvertToHTML_Click(object sender, EventArgs e)
        {
            string content = RTFUtility.RtfToHtml(this.richTextBox1);
            MessageUtil.ShowTips(content);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ExRichTextBoxPrintHelper helper = new ExRichTextBoxPrintHelper(this.richTextBox1);
            helper.PrintRTF(true);
        }
    }
}
