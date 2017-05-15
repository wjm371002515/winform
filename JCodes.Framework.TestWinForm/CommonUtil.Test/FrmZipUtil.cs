using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JCodes.Framework.Common;

namespace TestControlUtil
{
    public partial class FrmZipUtil : Form
    {
        public FrmZipUtil()
        {
            InitializeComponent();
        }

        private void btnZipFiles_Click(object sender, EventArgs e)
        {
            string zippedFile = Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, "Package.zip");
            List<string> fileList = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory).ToList();
            ZipUtility.ZipFiles(fileList, zippedFile, "");
        }
    }
}
