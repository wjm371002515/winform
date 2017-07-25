using JCodes.Framework.Common.Winform;
namespace JCodes.Framework.AddIn.UI.WareHouseManage
{
    partial class FrmAddPurchaseItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddPurchaseItem));
            this.groupConsumeList = new System.Windows.Forms.GroupBox();
            this.lvwDetail = new JCodes.Framework.CommonControl.Pager.WinGridView();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvwGoods = new JCodes.Framework.CommonControl.Pager.WinGridView();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtItemNo = new System.Windows.Forms.TextBox();
            this.txtItemType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStockQuantity = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupConsumeList.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupConsumeList
            // 
            this.groupConsumeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupConsumeList.Controls.Add(this.lvwDetail);
            this.groupConsumeList.Controls.Add(this.btnDelete);
            this.groupConsumeList.Controls.Add(this.lblQuantity);
            this.groupConsumeList.Controls.Add(this.lblAmount);
            this.groupConsumeList.Controls.Add(this.btnOK);
            this.groupConsumeList.Location = new System.Drawing.Point(396, 12);
            this.groupConsumeList.Name = "groupConsumeList";
            this.groupConsumeList.Size = new System.Drawing.Size(596, 620);
            this.groupConsumeList.TabIndex = 0;
            this.groupConsumeList.TabStop = false;
            this.groupConsumeList.Text = "项目清单";
            // 
            // lvwDetail
            // 
            this.lvwDetail.AppendedMenu = null;
            this.lvwDetail.ColumnNameAlias = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("lvwDetail.ColumnNameAlias")));
            this.lvwDetail.DataSource = null;
            this.lvwDetail.DisplayColumns = "";
            this.lvwDetail.FixedColumns = null;
            this.lvwDetail.Location = new System.Drawing.Point(6, 28);
            this.lvwDetail.Name = "lvwDetail";
            this.lvwDetail.PrintTitle = "";
            this.lvwDetail.ShowAddMenu = true;
            this.lvwDetail.ShowCheckBox = false;
            this.lvwDetail.ShowDeleteMenu = true;
            this.lvwDetail.ShowEditMenu = true;
            this.lvwDetail.ShowExportButton = true;
            this.lvwDetail.Size = new System.Drawing.Size(584, 550);
            this.lvwDetail.TabIndex = 6;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(402, 584);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "删除选定项";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("宋体", 9F);
            this.lblQuantity.ForeColor = System.Drawing.Color.Red;
            this.lblQuantity.Location = new System.Drawing.Point(252, 1);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(143, 18);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "清单总数量：0个";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("宋体", 9F);
            this.lblAmount.ForeColor = System.Drawing.Color.Red;
            this.lblAmount.Location = new System.Drawing.Point(418, 1);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(152, 18);
            this.lblAmount.TabIndex = 4;
            this.lblAmount.Text = "清单总金额：0.00";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(512, 584);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(78, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "关闭确认";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lvwGoods);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtQuantity);
            this.groupBox2.Controls.Add(this.txtItemNo);
            this.groupBox2.Controls.Add(this.txtItemType);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblStockQuantity);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 620);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目清单";
            // 
            // lvwGoods
            // 
            this.lvwGoods.AppendedMenu = null;
            this.lvwGoods.ColumnNameAlias = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("lvwGoods.ColumnNameAlias")));
            this.lvwGoods.DataSource = null;
            this.lvwGoods.DisplayColumns = "";
            this.lvwGoods.FixedColumns = null;
            this.lvwGoods.Location = new System.Drawing.Point(6, 125);
            this.lvwGoods.Name = "lvwGoods";
            this.lvwGoods.PrintTitle = "";
            this.lvwGoods.ShowAddMenu = true;
            this.lvwGoods.ShowCheckBox = false;
            this.lvwGoods.ShowDeleteMenu = true;
            this.lvwGoods.ShowEditMenu = true;
            this.lvwGoods.ShowExportButton = true;
            this.lvwGoods.Size = new System.Drawing.Size(366, 496);
            this.lvwGoods.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(172, 52);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查  找";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(169, 91);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "入  库";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(63, 89);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(100, 29);
            this.txtQuantity.TabIndex = 4;
            this.txtQuantity.Text = "0";
            // 
            // txtItemNo
            // 
            this.txtItemNo.Location = new System.Drawing.Point(63, 22);
            this.txtItemNo.Name = "txtItemNo";
            this.txtItemNo.Size = new System.Drawing.Size(100, 29);
            this.txtItemNo.TabIndex = 0;
            // 
            // txtItemType
            // 
            this.txtItemType.Location = new System.Drawing.Point(63, 50);
            this.txtItemType.Name = "txtItemType";
            this.txtItemType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtItemType.Size = new System.Drawing.Size(100, 28);
            this.txtItemType.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(233, 22);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 29);
            this.txtName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "备件编码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "备件类型";
            // 
            // lblStockQuantity
            // 
            this.lblStockQuantity.AutoSize = true;
            this.lblStockQuantity.Location = new System.Drawing.Point(3, 94);
            this.lblStockQuantity.Name = "lblStockQuantity";
            this.lblStockQuantity.Size = new System.Drawing.Size(82, 22);
            this.lblStockQuantity.TabIndex = 0;
            this.lblStockQuantity.Text = "入库数量";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称/简拼";
            // 
            // FrmAddPurchaseItem2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 644);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupConsumeList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAddPurchaseItem2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "增加备件";
            this.Load += new System.EventHandler(this.FrmAddPurchaseItem_Load);
            this.groupConsumeList.ResumeLayout(false);
            this.groupConsumeList.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupConsumeList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtQuantity;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Label lblAmount;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.TextBox txtItemNo;
        private System.Windows.Forms.Label label4;
        public DevExpress.XtraEditors.SimpleButton btnAdd;
        private System.Windows.Forms.Label lblQuantity;
        public System.Windows.Forms.Label lblStockQuantity;
        private CommonControl.Pager.WinGridView lvwGoods;
        private CommonControl.Pager.WinGridView lvwDetail;
        private DevExpress.XtraEditors.ComboBoxEdit txtItemType;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}