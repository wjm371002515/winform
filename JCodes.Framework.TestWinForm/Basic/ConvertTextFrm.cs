using JCodes.Framework.AddIn.Proj;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace JCodes.Framework.TestWinForm.Basic
{
    public partial class ConvertTextFrm : DevExpress.XtraEditors.XtraForm
    {
        public ConvertTextFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextDirection.Text = richTextSource.Text.Replace("    ", "\\t").Replace("\n", "\\r\\n");
        }
    }
}
