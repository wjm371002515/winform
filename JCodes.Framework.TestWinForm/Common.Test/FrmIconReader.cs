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
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Images;
using JCodes.Framework.CommonControl.Pager.Others;

namespace TestCommons
{
    public partial class FrmIconReader : Form
    {
        public FrmIconReader()
        {
            InitializeComponent();
        }

        private void btnGetIcon_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtFilPath.Text))
            {
                //获取文件的图标
                Icon ico = IconReaderHelper.GetFileIcon(this.txtFilPath.Text, 
                    IconSize.Large, false);
                this.pictureBox1.Image = Bitmap.FromHicon(ico.Handle);

                //获取对应图标的说明名称
                string name = IconReaderHelper.GetDisplayName(this.txtFilPath.Text, false);
                this.Text = name;
            }

            //获取系统文件夹图标
            Icon ico2 = IconReaderHelper.GetFolderIcon(IconSize.Large, FolderType.Open);
            this.pictureBox2.Image = Bitmap.FromHicon(ico2.Handle);

        }

        private void btnGetExtensionIcon_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtExtension.Text))
            {
                //获取扩展名图标
                Icon ico = IconReaderHelper.ExtractIconForExtension(this.txtExtension.Text, true);
                this.pictureBox2.Image = Bitmap.FromHicon(ico.Handle);
            }
        }

        private void txtFilPath_KeyUp(object sender, KeyEventArgs e)
        {
            btnGetIcon_Click(null, null);
        }

        private void txtExtension_KeyUp(object sender, KeyEventArgs e)
        {
            btnGetExtensionIcon_Click(null, null);
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            this.txtFilPath.Text = FileDialogHelper.OpenFile();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (this.txtFilPath.Text.Length == 0)
                return;

            this.pictureBox1.Image = Image.FromFile(this.txtFilPath.Text);
        }

        private void btnPrintImage_Click(object sender, EventArgs e)
        {
            ImagePrintHelper imagePrint = new ImagePrintHelper(this.pictureBox1.Image, Path.GetFileName(this.txtFilPath.Text));
            imagePrint.AllowPrintShrink = true;
            imagePrint.AllowPrintCenter = true;
            imagePrint.AllowPrintEnlarge = true;
            imagePrint.AllowPrintRotate = true;
            imagePrint.PrintWithDialog();//弹出打印对话框，确认进行打印
        }

        private void btnPrintView_Click(object sender, EventArgs e)
        {
            ImagePrintHelper imagePrint = new ImagePrintHelper(this.pictureBox1.Image, Path.GetFileName(this.txtFilPath.Text));
            imagePrint.PrintPreview();//弹出打印预览页面
        }
    }
}
