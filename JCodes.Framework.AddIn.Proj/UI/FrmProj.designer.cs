namespace JCodes.Framework.AddIn.Proj
{
    partial class FrmProj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProj));
            this.basicInfo = new DevExpress.XtraEditors.GroupControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.btnRevision = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuild = new DevExpress.XtraEditors.SimpleButton();
            this.btnMinor = new DevExpress.XtraEditors.SimpleButton();
            this.btnMajor = new DevExpress.XtraEditors.SimpleButton();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtContract = new DevExpress.XtraEditors.TextEdit();
            this.lblContract = new System.Windows.Forms.Label();
            this.txtVersion = new DevExpress.XtraEditors.TextEdit();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cbbdbtype = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtoutputpath = new DevExpress.XtraEditors.TextEdit();
            this.btnoutputpath = new DevExpress.XtraEditors.SimpleButton();
            this.lbloutputpath = new System.Windows.Forms.Label();
            this.txtlasttime = new DevExpress.XtraEditors.TextEdit();
            this.lbllasttime = new System.Windows.Forms.Label();
            this.txterr = new DevExpress.XtraEditors.TextEdit();
            this.lblerr = new System.Windows.Forms.Label();
            this.txtdictdata = new DevExpress.XtraEditors.TextEdit();
            this.lbldictdata = new System.Windows.Forms.Label();
            this.txtdicttype = new DevExpress.XtraEditors.TextEdit();
            this.lbldicttype = new System.Windows.Forms.Label();
            this.lbldbtype = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.basicInfo)).BeginInit();
            this.basicInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContract.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbdbtype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtoutputpath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlasttime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txterr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdictdata.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdicttype.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // basicInfo
            // 
            this.basicInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.basicInfo.Controls.Add(this.txtRemark);
            this.basicInfo.Controls.Add(this.btnRevision);
            this.basicInfo.Controls.Add(this.btnBuild);
            this.basicInfo.Controls.Add(this.btnMinor);
            this.basicInfo.Controls.Add(this.btnMajor);
            this.basicInfo.Controls.Add(this.lblRemark);
            this.basicInfo.Controls.Add(this.txtContract);
            this.basicInfo.Controls.Add(this.lblContract);
            this.basicInfo.Controls.Add(this.txtVersion);
            this.basicInfo.Controls.Add(this.lblVersion);
            this.basicInfo.Controls.Add(this.txtName);
            this.basicInfo.Controls.Add(this.lblName);
            this.basicInfo.Location = new System.Drawing.Point(3, 3);
            this.basicInfo.Name = "basicInfo";
            this.basicInfo.Size = new System.Drawing.Size(1002, 224);
            this.basicInfo.TabIndex = 4;
            this.basicInfo.Text = "基本信息";
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Location = new System.Drawing.Point(149, 137);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(835, 80);
            this.txtRemark.TabIndex = 2;
            this.txtRemark.UseOptimizedRendering = true;
            this.txtRemark.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // btnRevision
            // 
            this.btnRevision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRevision.Location = new System.Drawing.Point(882, 68);
            this.btnRevision.Name = "btnRevision";
            this.btnRevision.Size = new System.Drawing.Size(95, 25);
            this.btnRevision.TabIndex = 1;
            this.btnRevision.Text = "编译版本号";
            this.btnRevision.Click += new System.EventHandler(this.btnVersion_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuild.Location = new System.Drawing.Point(786, 68);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(90, 25);
            this.btnBuild.TabIndex = 1;
            this.btnBuild.Text = "修正版本号";
            this.btnBuild.Click += new System.EventHandler(this.btnVersion_Click);
            // 
            // btnMinor
            // 
            this.btnMinor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinor.Location = new System.Drawing.Point(697, 68);
            this.btnMinor.Name = "btnMinor";
            this.btnMinor.Size = new System.Drawing.Size(82, 25);
            this.btnMinor.TabIndex = 1;
            this.btnMinor.Text = "子版本号";
            this.btnMinor.Click += new System.EventHandler(this.btnVersion_Click);
            // 
            // btnMajor
            // 
            this.btnMajor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMajor.Location = new System.Drawing.Point(608, 68);
            this.btnMajor.Name = "btnMajor";
            this.btnMajor.Size = new System.Drawing.Size(82, 25);
            this.btnMajor.TabIndex = 1;
            this.btnMajor.Text = "主版本号";
            this.btnMajor.Click += new System.EventHandler(this.btnVersion_Click);
            // 
            // lblRemark
            // 
            this.lblRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(97, 138);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(46, 22);
            this.lblRemark.TabIndex = 0;
            this.lblRemark.Text = "说明";
            // 
            // txtContract
            // 
            this.txtContract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContract.Location = new System.Drawing.Point(149, 101);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(829, 28);
            this.txtContract.TabIndex = 0;
            this.txtContract.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // lblContract
            // 
            this.lblContract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContract.AutoSize = true;
            this.lblContract.Location = new System.Drawing.Point(61, 105);
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(82, 22);
            this.lblContract.TabIndex = 0;
            this.lblContract.Text = "联系方式";
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(149, 67);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(443, 28);
            this.txtVersion.TabIndex = 0;
            this.txtVersion.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(97, 70);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(46, 22);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "版本";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(149, 33);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(829, 28);
            this.txtName.TabIndex = 0;
            this.txtName.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(97, 36);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(46, 22);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "名称";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.cbbdbtype);
            this.groupControl1.Controls.Add(this.txtoutputpath);
            this.groupControl1.Controls.Add(this.btnoutputpath);
            this.groupControl1.Controls.Add(this.lbloutputpath);
            this.groupControl1.Controls.Add(this.txtlasttime);
            this.groupControl1.Controls.Add(this.lbllasttime);
            this.groupControl1.Controls.Add(this.txterr);
            this.groupControl1.Controls.Add(this.lblerr);
            this.groupControl1.Controls.Add(this.txtdictdata);
            this.groupControl1.Controls.Add(this.lbldictdata);
            this.groupControl1.Controls.Add(this.txtdicttype);
            this.groupControl1.Controls.Add(this.lbldicttype);
            this.groupControl1.Controls.Add(this.lbldbtype);
            this.groupControl1.Location = new System.Drawing.Point(3, 233);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1002, 554);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "系统配置";
            // 
            // cbbdbtype
            // 
            this.cbbdbtype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbdbtype.Location = new System.Drawing.Point(149, 33);
            this.cbbdbtype.Name = "cbbdbtype";
            this.cbbdbtype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbdbtype.Size = new System.Drawing.Size(828, 28);
            this.cbbdbtype.TabIndex = 68;
            this.cbbdbtype.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // txtoutputpath
            // 
            this.txtoutputpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtoutputpath.Location = new System.Drawing.Point(149, 203);
            this.txtoutputpath.Name = "txtoutputpath";
            this.txtoutputpath.Size = new System.Drawing.Size(764, 28);
            this.txtoutputpath.TabIndex = 0;
            this.txtoutputpath.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // btnoutputpath
            // 
            this.btnoutputpath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnoutputpath.Location = new System.Drawing.Point(919, 204);
            this.btnoutputpath.Name = "btnoutputpath";
            this.btnoutputpath.Size = new System.Drawing.Size(58, 25);
            this.btnoutputpath.TabIndex = 1;
            this.btnoutputpath.Text = "浏览";
            this.btnoutputpath.Click += new System.EventHandler(this.btnoutputpath_Click);
            // 
            // lbloutputpath
            // 
            this.lbloutputpath.AutoSize = true;
            this.lbloutputpath.Location = new System.Drawing.Point(25, 205);
            this.lbloutputpath.Name = "lbloutputpath";
            this.lbloutputpath.Size = new System.Drawing.Size(118, 22);
            this.lbloutputpath.TabIndex = 0;
            this.lbloutputpath.Text = "脚本生成路径";
            // 
            // txtlasttime
            // 
            this.txtlasttime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtlasttime.Enabled = false;
            this.txtlasttime.Location = new System.Drawing.Point(149, 169);
            this.txtlasttime.Name = "txtlasttime";
            this.txtlasttime.Size = new System.Drawing.Size(829, 28);
            this.txtlasttime.TabIndex = 0;
            // 
            // lbllasttime
            // 
            this.lbllasttime.AutoSize = true;
            this.lbllasttime.Location = new System.Drawing.Point(25, 171);
            this.lbllasttime.Name = "lbllasttime";
            this.lbllasttime.Size = new System.Drawing.Size(118, 22);
            this.lbllasttime.TabIndex = 0;
            this.lbllasttime.Text = "最后更新日期";
            // 
            // txterr
            // 
            this.txterr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txterr.Location = new System.Drawing.Point(149, 135);
            this.txterr.Name = "txterr";
            this.txterr.Size = new System.Drawing.Size(829, 28);
            this.txterr.TabIndex = 0;
            this.txterr.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // lblerr
            // 
            this.lblerr.AutoSize = true;
            this.lblerr.Location = new System.Drawing.Point(25, 137);
            this.lblerr.Name = "lblerr";
            this.lblerr.Size = new System.Drawing.Size(118, 22);
            this.lblerr.TabIndex = 0;
            this.lblerr.Text = "错误号信息表";
            // 
            // txtdictdata
            // 
            this.txtdictdata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdictdata.Location = new System.Drawing.Point(149, 102);
            this.txtdictdata.Name = "txtdictdata";
            this.txtdictdata.Size = new System.Drawing.Size(829, 28);
            this.txtdictdata.TabIndex = 0;
            this.txtdictdata.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // lbldictdata
            // 
            this.lbldictdata.AutoSize = true;
            this.lbldictdata.Location = new System.Drawing.Point(25, 104);
            this.lbldictdata.Name = "lbldictdata";
            this.lbldictdata.Size = new System.Drawing.Size(118, 22);
            this.lbldictdata.TabIndex = 0;
            this.lbldictdata.Text = "字典明细信息";
            // 
            // txtdicttype
            // 
            this.txtdicttype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdicttype.Location = new System.Drawing.Point(149, 68);
            this.txtdicttype.Name = "txtdicttype";
            this.txtdicttype.Size = new System.Drawing.Size(829, 28);
            this.txtdicttype.TabIndex = 0;
            this.txtdicttype.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // lbldicttype
            // 
            this.lbldicttype.AutoSize = true;
            this.lbldicttype.Location = new System.Drawing.Point(25, 71);
            this.lbldicttype.Name = "lbldicttype";
            this.lbldicttype.Size = new System.Drawing.Size(118, 22);
            this.lbldicttype.TabIndex = 0;
            this.lbldicttype.Text = "字典大类信息";
            // 
            // lbldbtype
            // 
            this.lbldbtype.AutoSize = true;
            this.lbldbtype.Location = new System.Drawing.Point(43, 37);
            this.lbldbtype.Name = "lbldbtype";
            this.lbldbtype.Size = new System.Drawing.Size(100, 22);
            this.lbldbtype.TabIndex = 0;
            this.lbldbtype.Text = "数据库类型";
            // 
            // FrmProj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.basicInfo);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(16, 664);
            this.Name = "FrmProj";
            this.Text = "项目属性";
            this.Load += new System.EventHandler(this.FrmProj_Load);
            ((System.ComponentModel.ISupportInitialize)(this.basicInfo)).EndInit();
            this.basicInfo.ResumeLayout(false);
            this.basicInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContract.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbdbtype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtoutputpath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlasttime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txterr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdictdata.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdicttype.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl basicInfo;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.SimpleButton btnMajor;
        private System.Windows.Forms.Label lblRemark;
        private DevExpress.XtraEditors.TextEdit txtContract;
        private System.Windows.Forms.Label lblContract;
        private DevExpress.XtraEditors.TextEdit txtVersion;
        private System.Windows.Forms.Label lblVersion;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtoutputpath;
        private System.Windows.Forms.Label lbloutputpath;
        private DevExpress.XtraEditors.TextEdit txtlasttime;
        private System.Windows.Forms.Label lbllasttime;
        private DevExpress.XtraEditors.TextEdit txterr;
        private System.Windows.Forms.Label lblerr;
        private DevExpress.XtraEditors.TextEdit txtdictdata;
        private System.Windows.Forms.Label lbldictdata;
        private DevExpress.XtraEditors.TextEdit txtdicttype;
        private System.Windows.Forms.Label lbldicttype;
        private System.Windows.Forms.Label lbldbtype;
        private DevExpress.XtraEditors.SimpleButton btnRevision;
        private DevExpress.XtraEditors.SimpleButton btnBuild;
        private DevExpress.XtraEditors.SimpleButton btnMinor;
        private DevExpress.XtraEditors.SimpleButton btnoutputpath;
        private DevExpress.XtraEditors.ComboBoxEdit cbbdbtype;

    }
}