namespace JCodes.Framework.AddIn.UI.Attachment
{
    partial class FrmPicturePreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kpImageViewer1 = new KaiwaProjects.KpImageViewer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpImageViewer1
            // 
            this.kpImageViewer1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.kpImageViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpImageViewer1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.kpImageViewer1.GifAnimation = false;
            this.kpImageViewer1.GifFPS = 15D;
            this.kpImageViewer1.Image = null;
            this.kpImageViewer1.Location = new System.Drawing.Point(0, 0);
            this.kpImageViewer1.MenuColor = System.Drawing.Color.LightSteelBlue;
            this.kpImageViewer1.MenuPanelColor = System.Drawing.Color.LightSteelBlue;
            this.kpImageViewer1.MinimumSize = new System.Drawing.Size(618, 197);
            this.kpImageViewer1.Name = "kpImageViewer1";
            this.kpImageViewer1.NavigationPanelColor = System.Drawing.Color.LightSteelBlue;
            this.kpImageViewer1.NavigationTextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kpImageViewer1.OpenButton = false;
            this.kpImageViewer1.PreviewButton = false;
            this.kpImageViewer1.PreviewPanelColor = System.Drawing.Color.LightSteelBlue;
            this.kpImageViewer1.PreviewText = "预览";
            this.kpImageViewer1.PreviewTextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kpImageViewer1.Rotation = 0;
            //this.kpImageViewer1.SaveButton = true;
            this.kpImageViewer1.Scrollbars = true;
            this.kpImageViewer1.ShowPreview = false;
            this.kpImageViewer1.Size = new System.Drawing.Size(1016, 734);
            this.kpImageViewer1.TabIndex = 1;
            this.kpImageViewer1.TextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kpImageViewer1.Zoom = 100D;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSaveAs});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 26);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(151, 22);
            this.menuSaveAs.Text = "图片另存为(&S)";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // FrmPicturePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.kpImageViewer1);
            this.KeyPreview = true;
            this.Name = "FrmPicturePreview";
            this.Text = "图片预览";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmPicturePreview_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPicturePreview_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KaiwaProjects.KpImageViewer kpImageViewer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
    }
}