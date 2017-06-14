using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.Device
{
    /// <summary>
    /// 摄像头图形显示窗体
    /// </summary>
    public partial class CameraDialog : BaseDock
    {
        private Camera _camera;
        public CameraDialog()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(CameraDialog_Disposed);
        }

        void CameraDialog_Disposed(object sender, EventArgs e)
        {
            _camera.Close();
        }

        private void CameraDialog_Load(object sender, EventArgs e)
        {
            this.Width = (this.Width - panel1.ClientSize.Width) + Const.PHOTO_WIDTH;
            this.Height = (this.Height - panel1.ClientSize.Height) + Const.PHOTO_HEIGHT;

            _camera = new Camera(panel1);
            try
            {
                _camera.Open();
            }
            catch (Exception ex)
            {
                lblError.Parent = this;
                lblError.Top = panel1.Top;
                lblError.Left = panel1.Left;
                lblError.Width = panel1.Width;
                lblError.Height = panel1.Height;
                lblError.Text = ex.Message;
                lblError.Visible = true;
                panel1.Visible = false;
                btnOK.Enabled = false;
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(CameraDialog));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Photo = _camera.GrabImage();
        }

        /// <summary>
        /// 摄像头图片对象
        /// </summary>
        public Bitmap Photo { get; private set; }
    }
}
