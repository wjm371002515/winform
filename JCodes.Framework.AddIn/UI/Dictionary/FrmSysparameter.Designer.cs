namespace JCodes.Framework.AddIn.UI.Dictionary
{
    partial class FrmSysparameter
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
            this.xscControls = new DevExpress.XtraEditors.XtraScrollableControl();
            this.SuspendLayout();
            // 
            // xscControls
            // 
            this.xscControls.Appearance.BackColor = System.Drawing.Color.White;
            this.xscControls.Appearance.Options.UseBackColor = true;
            this.xscControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscControls.Location = new System.Drawing.Point(0, 0);
            this.xscControls.Name = "xscControls";
            this.xscControls.Size = new System.Drawing.Size(654, 575);
            this.xscControls.TabIndex = 0;
            // 
            // FrmSysparameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xscControls);
            this.Name = "FrmSysparameter";
            this.Size = new System.Drawing.Size(654, 575);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xscControls;

    }
}