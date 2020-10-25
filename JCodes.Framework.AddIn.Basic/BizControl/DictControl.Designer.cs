namespace JCodes.Framework.AddIn.UI.BizControl
{
    partial class DictControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictControl));
            this.imageList2 = new System.Windows.Forms.ImageList();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.lueDic = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDic.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "star.ico");
            this.imageList2.Images.SetKeyName(1, "Organ.ico");
            this.imageList2.Images.SetKeyName(2, "usergroup6.ico");
            this.imageList2.Images.SetKeyName(3, "usergroup5.ico");
            this.imageList2.Images.SetKeyName(4, "user005.ico");
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRefresh.Location = new System.Drawing.Point(164, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 20);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lueDic
            // 
            this.lueDic.Location = new System.Drawing.Point(0, 0);
            this.lueDic.Name = "lueDic";
            this.lueDic.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDic.Properties.NullText = "";
            this.lueDic.Size = new System.Drawing.Size(160, 28);
            this.lueDic.TabIndex = 11;
            // 
            // DictControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.lueDic);
            this.Controls.Add(this.btnRefresh);
            this.Name = "DictControl";
            this.Size = new System.Drawing.Size(196, 28);
            this.Load += new System.EventHandler(this.DictControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lueDic.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.LookUpEdit lueDic;
    }
}
