using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;

using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Other.Images;
using JCodes.Framework.CommonControl.DocViewer;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.CommonControl.Device
{
    /// <summary>
    /// 保存图片的处理代理定义
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="imageBytes">图片数据</param>
    /// <param name="imageType">图片类型</param>
    /// <returns></returns>
    public delegate bool SavePortraitHandler(string id, byte[] imageBytes, string imageType);

    /// <summary>
    /// 绑定图片数据的代理定义
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="imageType">图片类型</param>
    /// <returns></returns>
    public delegate byte[] BindPortraitHandler(string id, string imageType);

    /// <summary>
    /// 图片数据显示和采集控件
    /// </summary>
    public partial class PortraitControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region 事件及属性定义
        /// <summary>
        /// 保存图片操作的事件
        /// </summary>
        public event SavePortraitHandler OnSavePortrait;

        /// <summary>
        /// 绑定图片数据的事件
        /// </summary>
        public event BindPortraitHandler OnBindPortait;

        /// <summary>
        /// 记录ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 图片是否被修改过
        /// </summary>
        public bool ImageIsDirty { get; set; }

        private string imageType = "个人肖像";
        /// <summary>
        /// 图片类型
        /// </summary>
        public string ImageType
        {
            get { return imageType; }
            set
            {
                imageType = value;
                ResetDefaultImage(imageType);
            }
        }

        private bool m_ShowEditTool = true;
        /// <summary>
        /// 是否显示编辑工具条
        /// </summary>
        public bool ShowEditTool
        {
            get
            {
                return this.m_ShowEditTool;
            }
            set
            {
                this.m_ShowEditTool = value;
                this.pnlTool.Visible = value;
                if (!value)
                {
                    this.picPortrait.Dock = DockStyle.Fill;
                }
                else
                {
                    this.picPortrait.Dock = DockStyle.None;
                }
                this.picPortrait.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            }
        }
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PortraitControl()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.picPortrait.LoadImage();

            //看是否被修改
            this.ImageIsDirty = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool sucess = SavePicture(this.ID);

            MessageDxUtil.ShowTips(sucess ? "图片保存成功！" : "保存失败！");
        }

        /// <summary>
        /// 保存图片到服务器
        /// </summary>
        public bool SavePicture(string id)
        {
            this.ID = id;//设置控件的ID值

            if (string.IsNullOrEmpty(id))
            {
                MessageDxUtil.ShowTips("记录ID未指定，无法保存，请先保存数据！");
                return false;
            }
            if (OnSavePortrait == null)
            {
                MessageDxUtil.ShowTips("控件未指定OnSavePortrait处理事件！");
                return false;
            }

            if (picPortrait.Image != null)
            {
                try
                {
                    byte[] imageBytes = ImageHelper.ImageToBytes(this.picPortrait.Image);

                    bool sucess = false;
                    if (OnSavePortrait != null)
                    {
                        sucess = OnSavePortrait(this.ID, imageBytes, ImageType);
                    }
                    return sucess;

                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(PortraitControl));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            ResetDefaultImage(ImageType);
        }

        /// <summary>
        /// 重置默认图片，可以选择“个人肖像”、“车辆图片”，其他值将显示空白
        /// </summary>
        /// <param name="imageType"></param>
        private void ResetDefaultImage(string imageType)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortraitControl));
            switch (imageType)
            {
                case "个人肖像":
                    this.picPortrait.EditValue = ((object)(resources.GetObject("picPortrait.EditValue")));
                    break;
                case "车辆照片":
                    this.picPortrait.EditValue = ((object)(resources.GetObject("CabrioletRed")));
                    break;
                default:
                    this.picPortrait.EditValue = null;
                    break;
            }
            this.ImageIsDirty = true;
        }

        /// <summary>
        /// 绑定图片的操作，触发绑定事件处理
        /// </summary>
        /// <param name="id">记录ID</param>
        public void BindPicture(string id)
        {
            try
            {
                this.ID = id; //设置控件的ID值

                #region 更新图片显示操作

                if (OnBindPortait == null)
                {
                    MessageDxUtil.ShowTips("控件未指定OnBindPortait事件处理");
                    return;
                }

                byte[] imageBytes = OnBindPortait(id, ImageType);
                if (imageBytes != null)
                {
                    this.picPortrait.Image = ImageHelper.ImageFromBytes(imageBytes);
                }
                else
                {
                    ResetDefaultImage(ImageType);
                }

                #endregion

                //this.lblPictureTips.Text = "全部图片更新成功！";
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(PortraitControl));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void PortraitControl_Load(object sender, EventArgs e)
        {
        }

        private void picPortrait_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureEdit picture = sender as PictureEdit;
            if (picture != null && picture.Image != null)
            {
                FrmImageView dlg = new FrmImageView();
                dlg.pictureEdit1.Image = picture.Image;
                dlg.ShowDialog();
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            CameraDialog dlg = new CameraDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.picPortrait.Image = dlg.Photo;
                this.ImageIsDirty = true;
            }
        }
    }
}
