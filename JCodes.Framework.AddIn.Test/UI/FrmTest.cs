using DevExpress.Office.Utils;
using JCodes.Framework.Common.Proj;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.Test
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();

            Init_Data();
        }

        private void Init_Data()
        {
        }

        private void 测试1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SqlOperate.printHeaderInfo();

            MessageBox.Show("hello world");
        }
    }

    public class TestGridViewEntity
    {
        public string gridColumn1
        {
            get;
            set;
        }
        public string gridColumn2
        {
            get;
            set;
        }

        public string gridColumn3
        {
            get;
            set;
        }

        public string gridColumn4
        {
            get;
            set;
        }

    }
}
