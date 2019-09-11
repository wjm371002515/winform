namespace JCodes.Framework.Test
{
    partial class IPOdepthDealfrm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcData = new DevExpress.XtraGrid.GridControl();
            this.gvdata = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ddpDateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.ddpStartDateTime = new System.Windows.Forms.DateTimePicker();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.txtPwd = new DevExpress.XtraEditors.TextEdit();
            this.txtOAuserName = new DevExpress.XtraEditors.TextEdit();
            this.txtpop3ServerPort = new DevExpress.XtraEditors.TextEdit();
            this.txtpop3Server = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvdata)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOAuserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpop3ServerPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpop3Server.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gcData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(701, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 742);
            this.panel1.TabIndex = 0;
            // 
            // gcData
            // 
            this.gcData.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcData.Location = new System.Drawing.Point(0, 0);
            this.gcData.MainView = this.gvdata;
            this.gcData.Name = "gcData";
            this.gcData.Size = new System.Drawing.Size(843, 742);
            this.gcData.TabIndex = 0;
            this.gcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvdata});
            // 
            // gvdata
            // 
            this.gvdata.GridControl = this.gcData;
            this.gvdata.Name = "gvdata";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ddpDateTimeEnd);
            this.panel2.Controls.Add(this.ddpStartDateTime);
            this.panel2.Controls.Add(this.labelControl7);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.txtSubject);
            this.panel2.Controls.Add(this.txtPwd);
            this.panel2.Controls.Add(this.txtOAuserName);
            this.panel2.Controls.Add(this.txtpop3ServerPort);
            this.panel2.Controls.Add(this.txtpop3Server);
            this.panel2.Controls.Add(this.labelControl6);
            this.panel2.Controls.Add(this.txtPath);
            this.panel2.Controls.Add(this.labelControl3);
            this.panel2.Controls.Add(this.labelControl5);
            this.panel2.Controls.Add(this.labelControl4);
            this.panel2.Controls.Add(this.labelControl2);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(701, 742);
            this.panel2.TabIndex = 1;
            // 
            // ddpDateTimeEnd
            // 
            this.ddpDateTimeEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ddpDateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ddpDateTimeEnd.Location = new System.Drawing.Point(182, 287);
            this.ddpDateTimeEnd.Name = "ddpDateTimeEnd";
            this.ddpDateTimeEnd.Size = new System.Drawing.Size(479, 28);
            this.ddpDateTimeEnd.TabIndex = 22;
            // 
            // ddpStartDateTime
            // 
            this.ddpStartDateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ddpStartDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ddpStartDateTime.Location = new System.Drawing.Point(182, 236);
            this.ddpStartDateTime.Name = "ddpStartDateTime";
            this.ddpStartDateTime.Size = new System.Drawing.Size(479, 28);
            this.ddpStartDateTime.TabIndex = 21;
            this.ddpStartDateTime.Value = new System.DateTime(2017, 9, 4, 17, 0, 0, 0);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(21, 287);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(144, 22);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "搜索发送结束时间";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(21, 333);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(622, 47);
            this.btnUpdate.TabIndex = 18;
            this.btnUpdate.Text = "IPO一键处理";
            this.btnUpdate.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // txtSubject
            // 
            this.txtSubject.EditValue = "巨化EB";
            this.txtSubject.Location = new System.Drawing.Point(182, 189);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(479, 28);
            this.txtSubject.TabIndex = 15;
            // 
            // txtPwd
            // 
            this.txtPwd.EditValue = "jheb.190404";
            this.txtPwd.Location = new System.Drawing.Point(182, 148);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(479, 28);
            this.txtPwd.TabIndex = 16;
            // 
            // txtOAuserName
            // 
            this.txtOAuserName.EditValue = "jheb@stocke.com.cn";
            this.txtOAuserName.Location = new System.Drawing.Point(182, 107);
            this.txtOAuserName.Name = "txtOAuserName";
            this.txtOAuserName.Size = new System.Drawing.Size(479, 28);
            this.txtOAuserName.TabIndex = 17;
            // 
            // txtpop3ServerPort
            // 
            this.txtpop3ServerPort.EditValue = "995";
            this.txtpop3ServerPort.Location = new System.Drawing.Point(531, 66);
            this.txtpop3ServerPort.Name = "txtpop3ServerPort";
            this.txtpop3ServerPort.Size = new System.Drawing.Size(130, 28);
            this.txtpop3ServerPort.TabIndex = 11;
            // 
            // txtpop3Server
            // 
            this.txtpop3Server.EditValue = "mail.stocke.com.cn";
            this.txtpop3Server.Location = new System.Drawing.Point(182, 66);
            this.txtpop3Server.Name = "txtpop3Server";
            this.txtpop3Server.Size = new System.Drawing.Size(327, 28);
            this.txtpop3Server.TabIndex = 12;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(21, 240);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(144, 22);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "搜索发送开始时间";
            // 
            // txtPath
            // 
            this.txtPath.EditValue = "G:\\ipo_files";
            this.txtPath.Location = new System.Drawing.Point(182, 25);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(479, 28);
            this.txtPath.TabIndex = 13;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 154);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 22);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "OA邮箱密码";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(21, 196);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 22);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "发件主题";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(21, 70);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(151, 22);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "POP3服务器及端口";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 112);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(114, 22);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "OA邮箱用户名";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(138, 22);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "下载IPO文件路径";
            // 
            // IPOdepthDealfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1544, 742);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "IPOdepthDealfrm";
            this.Text = "投行IPO处理工具";
            this.Load += new System.EventHandler(this.IPOdepthDealfrm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvdata)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOAuserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpop3ServerPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpop3Server.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.TextEdit txtPwd;
        private DevExpress.XtraEditors.TextEdit txtOAuserName;
        private DevExpress.XtraEditors.TextEdit txtpop3ServerPort;
        private DevExpress.XtraEditors.TextEdit txtpop3Server;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtPath;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gcData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvdata;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.DateTimePicker ddpStartDateTime;
        private System.Windows.Forms.DateTimePicker ddpDateTimeEnd;


    }
}

