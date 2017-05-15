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
    public partial class FrmImageHelper : Form
    {
        public FrmImageHelper()
        {
            InitializeComponent();
        }

        private void btnLoadBitmap_Click(object sender, EventArgs e)
        {
            //加载图片
            string file = FileDialogHelper.OpenImage();
            if (!string.IsNullOrEmpty(file))
            {
                pictureBox1.Image = Image.FromFile(file);
            }
        }

        private void btnWaterMark_Click(object sender, EventArgs e)
        {
            //图片水印效果
            string file = FileDialogHelper.OpenImage();
            if (!string.IsNullOrEmpty(file))
            {
                this.pictureBox2.Image = ImageHelper.WatermarkImage(this.pictureBox1.Image, Image.FromFile(file));
            }
        }

        private void btnTextWatermark_Click(object sender, EventArgs e)
        {
            //文字水印效果
            this.pictureBox2.Image = ImageHelper.WatermarkText(this.pictureBox1.Image, "测试水印效果");
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            //按比例放大图片
            this.pictureBox2.Image = ImageHelper.ResizeImageByPercent(this.pictureBox1.Image, 200);
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            //旋转图片
            this.pictureBox2.Image = ImageHelper.RotateImage(this.pictureBox1.Image, 90);
        }


    }
}
