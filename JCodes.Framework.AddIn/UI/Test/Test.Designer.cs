using JCodes.Framework.Common;
namespace JCodes.Framework.AddIn.Test
{
    partial class Test
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "反射1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Location = new System.Drawing.Point(0, 177);
            this.txtLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(879, 326);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 10);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 30);
            this.button2.TabIndex = 0;
            this.button2.Text = "反射1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(284, 97);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 100);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "groupControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(141, 112);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(400, 200);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(548, 65);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(100, 28);
            this.textEdit1.TabIndex = 4;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 503);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Test";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit textEdit1;

    }
}