namespace JCodes.Framework.TestWinForm.ZSJob
{
    partial class TouHangDiGaoFrm
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.gbCopyFile = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSourcePath = new DevExpress.XtraEditors.TextEdit();
            this.txtCopytoPath = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopyByIndex = new DevExpress.XtraEditors.SimpleButton();
            this.btnFindNotSetFile = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            this.gbCopyFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourcePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCopytoPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(40, 33);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(228, 44);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "测试WEB端生成底稿文件";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(299, 33);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(182, 44);
            this.simpleButton2.TabIndex = 0;
            this.simpleButton2.Text = "客户端底稿生成";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(462, 116);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(182, 44);
            this.simpleButton3.TabIndex = 0;
            this.simpleButton3.Text = "提取目录文件";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(40, 125);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(397, 28);
            this.txtPath.TabIndex = 1;
            // 
            // gbCopyFile
            // 
            this.gbCopyFile.Controls.Add(this.labelControl2);
            this.gbCopyFile.Controls.Add(this.labelControl1);
            this.gbCopyFile.Controls.Add(this.txtSourcePath);
            this.gbCopyFile.Controls.Add(this.txtCopytoPath);
            this.gbCopyFile.Controls.Add(this.simpleButton5);
            this.gbCopyFile.Controls.Add(this.btnCopyByIndex);
            this.gbCopyFile.Location = new System.Drawing.Point(40, 192);
            this.gbCopyFile.Name = "gbCopyFile";
            this.gbCopyFile.Size = new System.Drawing.Size(667, 225);
            this.gbCopyFile.TabIndex = 2;
            this.gbCopyFile.TabStop = false;
            this.gbCopyFile.Text = "提取拷贝文件名";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(58, 88);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(108, 22);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "拷贝源文件夹";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(144, 22);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "复制到对应文件夹";
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.EditValue = "I:\\博汇电子版底稿 - 副本";
            this.txtSourcePath.Location = new System.Drawing.Point(182, 85);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(397, 28);
            this.txtSourcePath.TabIndex = 4;
            // 
            // txtCopytoPath
            // 
            this.txtCopytoPath.EditValue = "I:\\aa";
            this.txtCopytoPath.Location = new System.Drawing.Point(182, 39);
            this.txtCopytoPath.Name = "txtCopytoPath";
            this.txtCopytoPath.Size = new System.Drawing.Size(397, 28);
            this.txtCopytoPath.TabIndex = 5;
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(335, 152);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(244, 44);
            this.simpleButton5.TabIndex = 2;
            this.simpleButton5.Text = "按照目录名和文件名匹配";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // btnCopyByIndex
            // 
            this.btnCopyByIndex.Location = new System.Drawing.Point(58, 152);
            this.btnCopyByIndex.Name = "btnCopyByIndex";
            this.btnCopyByIndex.Size = new System.Drawing.Size(197, 44);
            this.btnCopyByIndex.TabIndex = 3;
            this.btnCopyByIndex.Text = "按照索引名提取文件";
            this.btnCopyByIndex.Click += new System.EventHandler(this.btnCopyByIndex_Click);
            // 
            // btnFindNotSetFile
            // 
            this.btnFindNotSetFile.Location = new System.Drawing.Point(40, 450);
            this.btnFindNotSetFile.Name = "btnFindNotSetFile";
            this.btnFindNotSetFile.Size = new System.Drawing.Size(182, 44);
            this.btnFindNotSetFile.TabIndex = 0;
            this.btnFindNotSetFile.Text = "查找不适用文件";
            this.btnFindNotSetFile.Click += new System.EventHandler(this.btnFindNotSetFile_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(275, 450);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(182, 44);
            this.simpleButton4.TabIndex = 0;
            this.simpleButton4.Text = "查找未上传文件夹";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // TouHangDiGaoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 657);
            this.Controls.Add(this.gbCopyFile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.btnFindNotSetFile);
            this.Controls.Add(this.simpleButton1);
            this.Name = "TouHangDiGaoFrm";
            this.Text = "投行底稿自定义小工具";
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            this.gbCopyFile.ResumeLayout(false);
            this.gbCopyFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourcePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCopytoPath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.TextEdit txtPath;
        private System.Windows.Forms.GroupBox gbCopyFile;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSourcePath;
        private DevExpress.XtraEditors.TextEdit txtCopytoPath;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton btnCopyByIndex;
        private DevExpress.XtraEditors.SimpleButton btnFindNotSetFile;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
    }
}