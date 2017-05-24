using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace JCodes.Framework.Common.Images
{
    /// <summary>
    /// 全屏截图辅助控件
    /// </summary>
    internal partial class ScreenCaptureWindow : Form
    {
        #region 字段属性

        private Point dragStart = Point.Empty;
        private Point dragStop = Point.Empty;
        private bool mousePressed = false;
        private System.ComponentModel.IContainer components = null;
        private Bitmap bitmapCache = null;

        /// <summary>
        /// 获取或设置截图的图片对象
        /// </summary>
        public Bitmap BitmapCache
        {
            get { return bitmapCache; }
            set { this.bitmapCache = value; }
        }

        /// <summary>
        /// 图片截图结束后的事件处理
        /// </summary>
        public event EventHandler BitmapCropped;

        /// <summary>
        /// 拖动截图开始
        /// </summary>
        public Point DragStart
        {
            get { return dragStart; }
        }

        /// <summary>
        /// 拖动截图结束
        /// </summary>
        public Point DragStop
        {
            get { return dragStop; }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public ScreenCaptureWindow()
        {
            InitializeComponent();

            this.BackgroundImage = GenerateScreenBitmap();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            
            this.Cursor = Cursors.Cross;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.AutoScaleMode = AutoScaleMode.None;
            this.Size = SystemInformation.VirtualScreen.Size;
            this.Location = SystemInformation.VirtualScreen.Location;

            this.TopMost = true;
        }

        #region 覆盖相关的函数实现
        
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ResetPoints();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            HideCaptureWindow();
        }

        private void HideCaptureWindow()
        {
            this.Hide();
            if (this.DragStop != this.DragStart)
            {
                Rectangle rect = Rectangle.FromLTRB(this.DragStart.X, this.DragStart.Y, this.DragStop.X, this.DragStop.Y);
                CopyResultToClipBoard(rect);
            }
            
            //用户自定义的操作
            if (this.BitmapCropped != null)
            {
                BitmapCropped(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mousePressed = e.Button == MouseButtons.Left;
            dragStop = Control.MousePosition;
            this.Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.dragStart = Control.MousePosition;
            this.Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.dragStop = Control.MousePosition;            
            HideCaptureWindow();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Region clip = e.Graphics.Clip;
            if (mousePressed && dragStart!= Point.Empty && dragStart != dragStop)
            {
                Rectangle rect = Rectangle.FromLTRB(dragStart.X, dragStart.Y, dragStop.X, dragStop.Y);
                using (Pen pen = new Pen(Color.Black))
                {
                    e.Graphics.DrawRectangle(pen, Rectangle.Inflate(rect,-1,-1));
                }
                e.Graphics.SetClip(rect, CombineMode.Exclude);
            }

            using (Brush brush = new SolidBrush(Color.FromArgb(210,Color.WhiteSmoke)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            e.Graphics.SetClip(clip, CombineMode.Replace);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0200 /*WM_MOUSEMOVE*/)
            {
                this.Refresh();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        /// <summary>
        /// 重置截图状态
        /// </summary>
        public void ResetPoints()
        {
            this.dragStart = Point.Empty;
            this.dragStop = Point.Empty;
            this.mousePressed = false;
        }

        private void CopyResultToClipBoard(Rectangle rect)
        {
            if (rect.Width < 0)
            {
                rect.X += rect.Width;
                rect.Width *= -1;
            }
            if (rect.Height < 0)
            {
                rect.Y += rect.Height;
                rect.Height *= -1;
            }

            Bitmap result = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(result);
            g.DrawImage(this.BitmapCache, new Rectangle(Point.Empty, result.Size), rect, GraphicsUnit.Pixel);

            this.BitmapCache = result;
            Clipboard.SetImage(result);
        }

        private Bitmap GenerateScreenBitmap()
        {
            Rectangle scrBounds = new Rectangle(SystemInformation.VirtualScreen.Location, SystemInformation.VirtualScreen.Size);
            Bitmap bmp = new Bitmap(scrBounds.Width, scrBounds.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(Point.Empty, Point.Empty, scrBounds.Size, CopyPixelOperation.SourceCopy);

            this.bitmapCache = bmp;
            return bmp;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AdornerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdornerWindow";
            this.Text = "AdornerWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }
    }
}
