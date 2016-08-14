namespace JCodes.Framework.WinFormUI
{
    partial class frmSettings
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.buttonOK = new DevExpress.XtraEditors.SimpleButton();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.listCube = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelCube = new DevExpress.XtraEditors.LabelControl();
            this.listCatalog = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelCatalog = new DevExpress.XtraEditors.LabelControl();
            this.editServer = new DevExpress.XtraEditors.ButtonEdit();
            this.labelServer = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radioFile = new DevExpress.XtraEditors.CheckEdit();
            this.radioServer = new DevExpress.XtraEditors.CheckEdit();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.buttonSample = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listCube.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listCatalog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioServer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(224, 170);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(318, 170);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.listCube);
            this.panelControl1.Controls.Add(this.labelCube);
            this.panelControl1.Controls.Add(this.listCatalog);
            this.panelControl1.Controls.Add(this.labelCatalog);
            this.panelControl1.Controls.Add(this.editServer);
            this.panelControl1.Controls.Add(this.labelServer);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.radioFile);
            this.panelControl1.Controls.Add(this.radioServer);
            this.panelControl1.Location = new System.Drawing.Point(14, 13);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(392, 144);
            this.panelControl1.TabIndex = 0;
            // 
            // listCube
            // 
            this.listCube.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listCube.Location = new System.Drawing.Point(121, 103);
            this.listCube.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.listCube.Name = "listCube";
            this.listCube.Properties.AutoComplete = false;
            this.listCube.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listCube.Properties.Sorted = true;
            this.listCube.Size = new System.Drawing.Size(250, 20);
            this.listCube.TabIndex = 4;
            // 
            // labelCube
            // 
            this.labelCube.Location = new System.Drawing.Point(19, 107);
            this.labelCube.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.labelCube.Name = "labelCube";
            this.labelCube.Size = new System.Drawing.Size(28, 14);
            this.labelCube.TabIndex = 18;
            this.labelCube.Text = "Cube";
            // 
            // listCatalog
            // 
            this.listCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listCatalog.Location = new System.Drawing.Point(121, 73);
            this.listCatalog.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.listCatalog.Name = "listCatalog";
            this.listCatalog.Properties.AutoComplete = false;
            this.listCatalog.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listCatalog.Properties.Sorted = true;
            this.listCatalog.Size = new System.Drawing.Size(250, 20);
            this.listCatalog.TabIndex = 3;
            // 
            // labelCatalog
            // 
            this.labelCatalog.Location = new System.Drawing.Point(19, 76);
            this.labelCatalog.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.labelCatalog.Name = "labelCatalog";
            this.labelCatalog.Size = new System.Drawing.Size(50, 14);
            this.labelCatalog.TabIndex = 16;
            this.labelCatalog.Text = "Database";
            // 
            // editServer
            // 
            this.editServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editServer.Location = new System.Drawing.Point(121, 43);
            this.editServer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.editServer.Name = "editServer";
            this.editServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.editServer.Size = new System.Drawing.Size(250, 20);
            this.editServer.TabIndex = 2;
            // 
            // labelServer
            // 
            this.labelServer.Location = new System.Drawing.Point(19, 46);
            this.labelServer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(35, 14);
            this.labelServer.TabIndex = 14;
            this.labelServer.Text = "Server";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Connect using";
            // 
            // radioFile
            // 
            this.radioFile.Location = new System.Drawing.Point(257, 15);
            this.radioFile.Name = "radioFile";
            this.radioFile.Properties.Caption = "Cube file";
            this.radioFile.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.radioFile.Properties.RadioGroupIndex = 0;
            this.radioFile.Size = new System.Drawing.Size(80, 19);
            this.radioFile.TabIndex = 1;
            this.radioFile.TabStop = false;
            // 
            // radioServer
            // 
            this.radioServer.EditValue = true;
            this.radioServer.Location = new System.Drawing.Point(119, 15);
            this.radioServer.Name = "radioServer";
            this.radioServer.Properties.Caption = "Analysis server";
            this.radioServer.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.radioServer.Properties.RadioGroupIndex = 0;
            this.radioServer.Size = new System.Drawing.Size(111, 19);
            this.radioServer.TabIndex = 0;
            // 
            // toolTipController1
            // 
            this.toolTipController1.Rounded = true;
            this.toolTipController1.ToolTipLocation = DevExpress.Utils.ToolTipLocation.RightCenter;
            // 
            // buttonSample
            // 
            this.buttonSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSample.Location = new System.Drawing.Point(14, 170);
            this.buttonSample.Name = "buttonSample";
            this.buttonSample.Size = new System.Drawing.Size(87, 23);
            this.buttonSample.TabIndex = 7;
            this.buttonSample.Text = "Open Sample";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 204);
            this.Controls.Add(this.buttonSample);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配置";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listCube.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listCatalog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioServer.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton buttonOK;
		private DevExpress.XtraEditors.SimpleButton buttonCancel;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.ComboBoxEdit listCube;
		private DevExpress.XtraEditors.LabelControl labelCube;
		private DevExpress.XtraEditors.ComboBoxEdit listCatalog;
		private DevExpress.XtraEditors.LabelControl labelCatalog;
		private DevExpress.XtraEditors.ButtonEdit editServer;
		private DevExpress.XtraEditors.LabelControl labelServer;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.CheckEdit radioFile;
		private DevExpress.XtraEditors.CheckEdit radioServer;
		private DevExpress.Utils.ToolTipController toolTipController1;
		private DevExpress.XtraEditors.SimpleButton buttonSample;
	}
}
