using JCodes.Framework.AddIn.UI.BizControl;
namespace JCodes.Framework.AddIn.Security
{
    partial class FrmEditUser
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditUser));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtCompany = new JCodes.Framework.AddIn.Basic.BizControl.CompanyControl();
            this.txtDept = new JCodes.Framework.AddIn.UI.BizControl.DeptControl();
            this.txtIsExpire = new AddIn.UI.BizControl.DictControl();
            this.txtUserCode = new DevExpress.XtraEditors.TextEdit();
            this.txtIP = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtLoginName = new DevExpress.XtraEditors.TextEdit();
            this.txtAuditStatus = new AddIn.UI.BizControl.DictControl();
            this.txtIdCard = new DevExpress.XtraEditors.TextEdit();
            this.txtMobilePhone = new DevExpress.XtraEditors.TextEdit();
            this.txtOfficePhone = new DevExpress.XtraEditors.TextEdit();
            this.txtHomePhone = new DevExpress.XtraEditors.TextEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtWorkAddr = new DevExpress.XtraEditors.TextEdit();
            this.txtBirthday = new DevExpress.XtraEditors.DateEdit();
            this.txtQq = new DevExpress.XtraEditors.TextEdit();
            this.txtSeq = new DevExpress.XtraEditors.TextEdit();
            this.txtCreator = new DevExpress.XtraEditors.TextEdit();
            this.txtCreatorTime = new DevExpress.XtraEditors.DateEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.txtSignature = new DevExpress.XtraEditors.MemoEdit();
            this.txtMac = new DevExpress.XtraEditors.TextEdit();
            this.txtGender = new JCodes.Framework.AddIn.UI.BizControl.DictControl();
            this.txtIsDelete = new AddIn.UI.BizControl.DictControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciSignature = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciRemark = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOfficePhone = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciUserCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLoginName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciMac = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCompany = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciGender = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBirthday = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIP = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciCreator = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCreatorTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIsExpire = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIsDelete = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDept = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSeq = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIdCard = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciAuditStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciEmail = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciMobilePhone = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciHomePhone = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciWorkAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.lvwRole = new System.Windows.Forms.ListBox();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.lvwOU = new System.Windows.Forms.ListBox();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabBasic = new DevExpress.XtraTab.XtraTabPage();
            this.tabFunction = new DevExpress.XtraTab.XtraTabPage();
            this.treeFunction = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdCard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobilePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOfficePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHomePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkAddr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBirthday.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBirthday.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatorTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatorTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSignature.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSignature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOfficePhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUserCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLoginName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBirthday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCreator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCreatorTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIsExpire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIsDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSeq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIdCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAuditStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMobilePhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHomePhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciWorkAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabBasic.SuspendLayout();
            this.tabFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(698, 647);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(797, 647);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(611, 647);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 642);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(205, 647);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Appearance.ControlReadOnly.BackColor = System.Drawing.Color.SeaShell;
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseBackColor = true;
            this.layoutControl1.Controls.Add(this.txtCompany);
            this.layoutControl1.Controls.Add(this.txtDept);
            this.layoutControl1.Controls.Add(this.txtIsExpire);
            this.layoutControl1.Controls.Add(this.txtUserCode);
            this.layoutControl1.Controls.Add(this.txtIP);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtLoginName);
            this.layoutControl1.Controls.Add(this.txtAuditStatus);
            this.layoutControl1.Controls.Add(this.txtIdCard);
            this.layoutControl1.Controls.Add(this.txtMobilePhone);
            this.layoutControl1.Controls.Add(this.txtOfficePhone);
            this.layoutControl1.Controls.Add(this.txtHomePhone);
            this.layoutControl1.Controls.Add(this.txtEmail);
            this.layoutControl1.Controls.Add(this.txtAddress);
            this.layoutControl1.Controls.Add(this.txtWorkAddr);
            this.layoutControl1.Controls.Add(this.txtBirthday);
            this.layoutControl1.Controls.Add(this.txtQq);
            this.layoutControl1.Controls.Add(this.txtSeq);
            this.layoutControl1.Controls.Add(this.txtCreator);
            this.layoutControl1.Controls.Add(this.txtCreatorTime);
            this.layoutControl1.Controls.Add(this.txtRemark);
            this.layoutControl1.Controls.Add(this.txtSignature);
            this.layoutControl1.Controls.Add(this.txtMac);
            this.layoutControl1.Controls.Add(this.txtGender);
            this.layoutControl1.Controls.Add(this.txtIsDelete);
            this.layoutControl1.Location = new System.Drawing.Point(12, 13);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(619, 579);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.Location = new System.Drawing.Point(154, 44);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(154, 22);
            this.txtCompany.TabIndex = 10;
            this.txtCompany.Value = "-1";
            // 
            // txtDept
            // 
            this.txtDept.Location = new System.Drawing.Point(454, 44);
            this.txtDept.Name = "txtDept";
            this.txtDept.Size = new System.Drawing.Size(153, 22);
            this.txtDept.TabIndex = 31;
            this.txtDept.Value = "-1";
            // 
            // txtIsExpire
            // 
            this.txtIsExpire.DicNo = 100001;
            this.txtIsExpire.EditValue = null;
            this.txtIsExpire.Location = new System.Drawing.Point(154, 70);
            this.txtIsExpire.Name = "txtIsExpire";
            this.txtIsExpire.Size = new System.Drawing.Size(154, 28);
            this.txtIsExpire.TabIndex = 16;
            // 
            // txtUserCode
            // 
            this.txtUserCode.Location = new System.Drawing.Point(154, 102);
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.Size = new System.Drawing.Size(153, 28);
            this.txtUserCode.StyleController = this.layoutControl1;
            this.txtUserCode.TabIndex = 2;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(453, 482);
            this.txtIP.Name = "txtIP";
            this.txtIP.Properties.ReadOnly = true;
            this.txtIP.Size = new System.Drawing.Size(154, 28);
            this.txtIP.StyleController = this.layoutControl1;
            this.txtIP.TabIndex = 7;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(154, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(153, 28);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 3;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(453, 12);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(154, 28);
            this.txtLoginName.StyleController = this.layoutControl1;
            this.txtLoginName.TabIndex = 5;
            // 
            // txtAuditStatus
            // 
            this.txtAuditStatus.DicNo = 100013;
            this.txtAuditStatus.EditValue = null;
            this.txtAuditStatus.Location = new System.Drawing.Point(154, 134);
            this.txtAuditStatus.Name = "txtAuditStatus";
            this.txtAuditStatus.Size = new System.Drawing.Size(153, 28);
            this.txtAuditStatus.TabIndex = 6;
            // 
            // txtIdCard
            // 
            this.txtIdCard.Location = new System.Drawing.Point(154, 198);
            this.txtIdCard.Name = "txtIdCard";
            this.txtIdCard.Properties.NullValuePrompt = "输入正确身份证后，回车自动填入出生日期和性别";
            this.txtIdCard.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtIdCard.Size = new System.Drawing.Size(453, 28);
            this.txtIdCard.StyleController = this.layoutControl1;
            this.txtIdCard.TabIndex = 9;
            this.txtIdCard.Validated += new System.EventHandler(this.txtIdentityCard_Validated);
            // 
            // txtMobilePhone
            // 
            this.txtMobilePhone.Location = new System.Drawing.Point(453, 166);
            this.txtMobilePhone.Name = "txtMobilePhone";
            this.txtMobilePhone.Size = new System.Drawing.Size(154, 28);
            this.txtMobilePhone.StyleController = this.layoutControl1;
            this.txtMobilePhone.TabIndex = 10;
            // 
            // txtOfficePhone
            // 
            this.txtOfficePhone.Location = new System.Drawing.Point(154, 262);
            this.txtOfficePhone.Name = "txtOfficePhone";
            this.txtOfficePhone.Size = new System.Drawing.Size(153, 28);
            this.txtOfficePhone.StyleController = this.layoutControl1;
            this.txtOfficePhone.TabIndex = 11;
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.Location = new System.Drawing.Point(453, 262);
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new System.Drawing.Size(154, 28);
            this.txtHomePhone.StyleController = this.layoutControl1;
            this.txtHomePhone.TabIndex = 12;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(154, 166);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(153, 28);
            this.txtEmail.StyleController = this.layoutControl1;
            this.txtEmail.TabIndex = 13;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(154, 326);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(453, 28);
            this.txtAddress.StyleController = this.layoutControl1;
            this.txtAddress.TabIndex = 14;
            // 
            // txtWorkAddr
            // 
            this.txtWorkAddr.Location = new System.Drawing.Point(154, 294);
            this.txtWorkAddr.Name = "txtWorkAddr";
            this.txtWorkAddr.Size = new System.Drawing.Size(453, 28);
            this.txtWorkAddr.StyleController = this.layoutControl1;
            this.txtWorkAddr.TabIndex = 15;
            // 
            // txtBirthday
            // 
            this.txtBirthday.EditValue = null;
            this.txtBirthday.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtBirthday.Location = new System.Drawing.Point(456, 230);
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBirthday.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtBirthday.Size = new System.Drawing.Size(151, 28);
            this.txtBirthday.StyleController = this.layoutControl1;
            this.txtBirthday.TabIndex = 17;
            // 
            // txtQq
            // 
            this.txtQq.Location = new System.Drawing.Point(453, 134);
            this.txtQq.Name = "txtQq";
            this.txtQq.Size = new System.Drawing.Size(154, 28);
            this.txtQq.StyleController = this.layoutControl1;
            this.txtQq.TabIndex = 18;
            // 
            // txtSeq
            // 
            this.txtSeq.Location = new System.Drawing.Point(453, 102);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Size = new System.Drawing.Size(154, 28);
            this.txtSeq.StyleController = this.layoutControl1;
            this.txtSeq.TabIndex = 27;
            // 
            // txtCreator
            // 
            this.txtCreator.Location = new System.Drawing.Point(154, 514);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Properties.ReadOnly = true;
            this.txtCreator.Size = new System.Drawing.Size(154, 28);
            this.txtCreator.StyleController = this.layoutControl1;
            this.txtCreator.TabIndex = 28;
            // 
            // txtCreatorTime
            // 
            this.txtCreatorTime.EditValue = null;
            this.txtCreatorTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreatorTime.Location = new System.Drawing.Point(454, 514);
            this.txtCreatorTime.Name = "txtCreatorTime";
            this.txtCreatorTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreatorTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCreatorTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.txtCreatorTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreatorTime.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.txtCreatorTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreatorTime.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.txtCreatorTime.Properties.ReadOnly = true;
            this.txtCreatorTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtCreatorTime.Size = new System.Drawing.Size(153, 28);
            this.txtCreatorTime.StyleController = this.layoutControl1;
            this.txtCreatorTime.TabIndex = 30;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(154, 426);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(453, 52);
            this.txtRemark.StyleController = this.layoutControl1;
            this.txtRemark.TabIndex = 22;
            this.txtRemark.UseOptimizedRendering = true;
            // 
            // txtSignature
            // 
            this.txtSignature.Location = new System.Drawing.Point(154, 358);
            this.txtSignature.Name = "txtSignature";
            this.txtSignature.Size = new System.Drawing.Size(453, 64);
            this.txtSignature.StyleController = this.layoutControl1;
            this.txtSignature.TabIndex = 19;
            this.txtSignature.UseOptimizedRendering = true;
            // 
            // txtMac
            // 
            this.txtMac.Location = new System.Drawing.Point(154, 482);
            this.txtMac.Name = "txtMac";
            this.txtMac.Properties.ReadOnly = true;
            this.txtMac.Size = new System.Drawing.Size(153, 28);
            this.txtMac.StyleController = this.layoutControl1;
            this.txtMac.TabIndex = 20;
            // 
            // txtGender
            // 
            this.txtGender.DicNo = 100014;
            this.txtGender.EditValue = null;
            this.txtGender.Location = new System.Drawing.Point(154, 230);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(156, 28);
            this.txtGender.TabIndex = 16;
            // 
            // txtIsDelete
            // 
            this.txtIsDelete.DicNo = 100001;
            this.txtIsDelete.EditValue = null;
            this.txtIsDelete.Location = new System.Drawing.Point(454, 70);
            this.txtIsDelete.Name = "txtIsDelete";
            this.txtIsDelete.Size = new System.Drawing.Size(156, 28);
            this.txtIsDelete.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciSignature,
            this.lciRemark,
            this.lciOfficePhone,
            this.lciUserCode,
            this.lblName,
            this.lblLoginName,
            this.lciMac,
            this.lblCompany,
            this.lciGender,
            this.lciBirthday,
            this.lciAddress,
            this.lciIP,
            this.emptySpaceItem1,
            this.lciCreator,
            this.lciCreatorTime,
            this.lciIsExpire,
            this.lciIsDelete,
            this.lblDept,
            this.lciSeq,
            this.lciIdCard,
            this.lciAuditStatus,
            this.layoutControlItem18,
            this.lciEmail,
            this.lciMobilePhone,
            this.lciHomePhone,
            this.lciWorkAddress});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(619, 579);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciSignature
            // 
            this.lciSignature.Control = this.txtSignature;
            this.lciSignature.CustomizationFormText = "个性签名";
            this.lciSignature.Location = new System.Drawing.Point(0, 346);
            this.lciSignature.Name = "lciSignature";
            this.lciSignature.Size = new System.Drawing.Size(599, 68);
            this.lciSignature.Text = "个性签名";
            this.lciSignature.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciRemark
            // 
            this.lciRemark.Control = this.txtRemark;
            this.lciRemark.CustomizationFormText = "备注";
            this.lciRemark.Location = new System.Drawing.Point(0, 414);
            this.lciRemark.Name = "lciRemark";
            this.lciRemark.Size = new System.Drawing.Size(599, 56);
            this.lciRemark.Text = "备注";
            this.lciRemark.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciOfficePhone
            // 
            this.lciOfficePhone.Control = this.txtOfficePhone;
            this.lciOfficePhone.CustomizationFormText = "办公电话";
            this.lciOfficePhone.Location = new System.Drawing.Point(0, 250);
            this.lciOfficePhone.Name = "lciOfficePhone";
            this.lciOfficePhone.Size = new System.Drawing.Size(299, 32);
            this.lciOfficePhone.Text = "办公电话";
            this.lciOfficePhone.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciUserCode
            // 
            this.lciUserCode.Control = this.txtUserCode;
            this.lciUserCode.CustomizationFormText = "用户编码";
            this.lciUserCode.Location = new System.Drawing.Point(0, 90);
            this.lciUserCode.Name = "lciUserCode";
            this.lciUserCode.Size = new System.Drawing.Size(299, 32);
            this.lciUserCode.Text = "用户编码";
            this.lciUserCode.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lblName
            // 
            this.lblName.Control = this.txtName;
            this.lblName.CustomizationFormText = "登陆姓名(*)";
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(299, 32);
            this.lblName.Text = "用户名(*)";
            this.lblName.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lblLoginName
            // 
            this.lblLoginName.Control = this.txtLoginName;
            this.lblLoginName.CustomizationFormText = "登陆姓名(*)";
            this.lblLoginName.Location = new System.Drawing.Point(299, 0);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(300, 32);
            this.lblLoginName.Text = "登陆姓名(*)";
            this.lblLoginName.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciMac
            // 
            this.lciMac.Control = this.txtMac;
            this.lciMac.CustomizationFormText = "Mac地址";
            this.lciMac.Location = new System.Drawing.Point(0, 470);
            this.lciMac.Name = "lciMac";
            this.lciMac.Size = new System.Drawing.Size(299, 32);
            this.lciMac.Text = "Mac地址";
            this.lciMac.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lblCompany
            // 
            this.lblCompany.Control = this.txtCompany;
            this.lblCompany.CustomizationFormText = "所属公司(*)";
            this.lblCompany.Location = new System.Drawing.Point(0, 32);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(300, 26);
            this.lblCompany.Text = "所属公司(*)";
            this.lblCompany.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciGender
            // 
            this.lciGender.Control = this.txtGender;
            this.lciGender.CustomizationFormText = "性别";
            this.lciGender.Location = new System.Drawing.Point(0, 218);
            this.lciGender.Name = "lciGender";
            this.lciGender.Size = new System.Drawing.Size(302, 32);
            this.lciGender.Text = "性别";
            this.lciGender.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciBirthday
            // 
            this.lciBirthday.Control = this.txtBirthday;
            this.lciBirthday.CustomizationFormText = "出生日期";
            this.lciBirthday.Location = new System.Drawing.Point(302, 218);
            this.lciBirthday.Name = "lciBirthday";
            this.lciBirthday.Size = new System.Drawing.Size(297, 32);
            this.lciBirthday.Text = "出生日期";
            this.lciBirthday.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciAddress
            // 
            this.lciAddress.Control = this.txtAddress;
            this.lciAddress.CustomizationFormText = "家庭住址";
            this.lciAddress.Location = new System.Drawing.Point(0, 314);
            this.lciAddress.Name = "lciAddress";
            this.lciAddress.Size = new System.Drawing.Size(599, 32);
            this.lciAddress.Text = "家庭住址";
            this.lciAddress.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciIP
            // 
            this.lciIP.Control = this.txtIP;
            this.lciIP.CustomizationFormText = "IP地址";
            this.lciIP.Location = new System.Drawing.Point(299, 470);
            this.lciIP.Name = "lciIP";
            this.lciIP.Size = new System.Drawing.Size(300, 32);
            this.lciIP.Text = "IP地址";
            this.lciIP.TextSize = new System.Drawing.Size(139, 22);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 534);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(599, 25);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciCreator
            // 
            this.lciCreator.Control = this.txtCreator;
            this.lciCreator.CustomizationFormText = "创建人";
            this.lciCreator.Location = new System.Drawing.Point(0, 502);
            this.lciCreator.Name = "lciCreator";
            this.lciCreator.Size = new System.Drawing.Size(300, 32);
            this.lciCreator.Text = "创建人";
            this.lciCreator.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciCreatorTime
            // 
            this.lciCreatorTime.Control = this.txtCreatorTime;
            this.lciCreatorTime.CustomizationFormText = "创建时间";
            this.lciCreatorTime.Location = new System.Drawing.Point(300, 502);
            this.lciCreatorTime.Name = "lciCreatorTime";
            this.lciCreatorTime.Size = new System.Drawing.Size(299, 32);
            this.lciCreatorTime.Text = "创建时间";
            this.lciCreatorTime.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciIsExpire
            // 
            this.lciIsExpire.Control = this.txtIsExpire;
            this.lciIsExpire.CustomizationFormText = "是否过期";
            this.lciIsExpire.Location = new System.Drawing.Point(0, 58);
            this.lciIsExpire.Name = "lciIsExpire";
            this.lciIsExpire.Size = new System.Drawing.Size(300, 32);
            this.lciIsExpire.Text = "是否过期";
            this.lciIsExpire.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciIsDelete
            // 
            this.lciIsDelete.Control = this.txtIsDelete;
            this.lciIsDelete.CustomizationFormText = "账号删除";
            this.lciIsDelete.Location = new System.Drawing.Point(300, 58);
            this.lciIsDelete.Name = "lciIsDelete";
            this.lciIsDelete.Size = new System.Drawing.Size(299, 32);
            this.lciIsDelete.Text = "是否删除";
            this.lciIsDelete.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lblDept
            // 
            this.lblDept.Control = this.txtDept;
            this.lblDept.CustomizationFormText = "默认机构/部门(*)";
            this.lblDept.Location = new System.Drawing.Point(300, 32);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(299, 26);
            this.lblDept.Text = "默认机构/部门(*)";
            this.lblDept.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciSeq
            // 
            this.lciSeq.Control = this.txtSeq;
            this.lciSeq.CustomizationFormText = "排序码";
            this.lciSeq.Location = new System.Drawing.Point(299, 90);
            this.lciSeq.Name = "lciSeq";
            this.lciSeq.Size = new System.Drawing.Size(300, 32);
            this.lciSeq.Text = "排序码";
            this.lciSeq.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciIdCard
            // 
            this.lciIdCard.Control = this.txtIdCard;
            this.lciIdCard.CustomizationFormText = "身份证号码";
            this.lciIdCard.Location = new System.Drawing.Point(0, 186);
            this.lciIdCard.Name = "lciIdCard";
            this.lciIdCard.Size = new System.Drawing.Size(599, 32);
            this.lciIdCard.Text = "身份证号码";
            this.lciIdCard.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciAuditStatus
            // 
            this.lciAuditStatus.Control = this.txtAuditStatus;
            this.lciAuditStatus.CustomizationFormText = "审核状态";
            this.lciAuditStatus.Location = new System.Drawing.Point(0, 122);
            this.lciAuditStatus.Name = "lciAuditStatus";
            this.lciAuditStatus.Size = new System.Drawing.Size(299, 32);
            this.lciAuditStatus.Text = "审核状态";
            this.lciAuditStatus.TextSize = new System.Drawing.Size(139, 22);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.txtQq;
            this.layoutControlItem18.CustomizationFormText = "QQ号码";
            this.layoutControlItem18.Location = new System.Drawing.Point(299, 122);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(300, 32);
            this.layoutControlItem18.Text = "QQ号码";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciEmail
            // 
            this.lciEmail.Control = this.txtEmail;
            this.lciEmail.CustomizationFormText = "邮件地址";
            this.lciEmail.Location = new System.Drawing.Point(0, 154);
            this.lciEmail.Name = "lciEmail";
            this.lciEmail.Size = new System.Drawing.Size(299, 32);
            this.lciEmail.Text = "邮件地址";
            this.lciEmail.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciMobilePhone
            // 
            this.lciMobilePhone.Control = this.txtMobilePhone;
            this.lciMobilePhone.CustomizationFormText = "移动电话";
            this.lciMobilePhone.Location = new System.Drawing.Point(299, 154);
            this.lciMobilePhone.Name = "lciMobilePhone";
            this.lciMobilePhone.Size = new System.Drawing.Size(300, 32);
            this.lciMobilePhone.Text = "移动电话";
            this.lciMobilePhone.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciHomePhone
            // 
            this.lciHomePhone.Control = this.txtHomePhone;
            this.lciHomePhone.CustomizationFormText = "家庭电话";
            this.lciHomePhone.Location = new System.Drawing.Point(299, 250);
            this.lciHomePhone.Name = "lciHomePhone";
            this.lciHomePhone.Size = new System.Drawing.Size(300, 32);
            this.lciHomePhone.Text = "家庭电话";
            this.lciHomePhone.TextSize = new System.Drawing.Size(139, 22);
            // 
            // lciWorkAddress
            // 
            this.lciWorkAddress.Control = this.txtWorkAddr;
            this.lciWorkAddress.CustomizationFormText = "办公地址";
            this.lciWorkAddress.Location = new System.Drawing.Point(0, 282);
            this.lciWorkAddress.Name = "lciWorkAddress";
            this.lciWorkAddress.Size = new System.Drawing.Size(599, 32);
            this.lciWorkAddress.Text = "办公地址";
            this.lciWorkAddress.TextSize = new System.Drawing.Size(139, 22);
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.lvwRole);
            this.groupControl4.Location = new System.Drawing.Point(649, 307);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(199, 285);
            this.groupControl4.TabIndex = 9;
            this.groupControl4.Text = "所属角色";
            // 
            // lvwRole
            // 
            this.lvwRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwRole.FormattingEnabled = true;
            this.lvwRole.ItemHeight = 22;
            this.lvwRole.Location = new System.Drawing.Point(2, 30);
            this.lvwRole.Name = "lvwRole";
            this.lvwRole.Size = new System.Drawing.Size(195, 253);
            this.lvwRole.TabIndex = 2;
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.lvwOU);
            this.groupControl3.Location = new System.Drawing.Point(649, 13);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(201, 288);
            this.groupControl3.TabIndex = 8;
            this.groupControl3.Text = "所属机构";
            // 
            // lvwOU
            // 
            this.lvwOU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwOU.FormattingEnabled = true;
            this.lvwOU.ItemHeight = 22;
            this.lvwOU.Location = new System.Drawing.Point(2, 30);
            this.lvwOU.Name = "lvwOU";
            this.lvwOU.Size = new System.Drawing.Size(197, 256);
            this.lvwOU.TabIndex = 1;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 4);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabBasic;
            this.xtraTabControl1.Size = new System.Drawing.Size(869, 632);
            this.xtraTabControl1.TabIndex = 10;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabBasic,
            this.tabFunction});
            // 
            // tabBasic
            // 
            this.tabBasic.Controls.Add(this.layoutControl1);
            this.tabBasic.Controls.Add(this.groupControl3);
            this.tabBasic.Controls.Add(this.groupControl4);
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.Size = new System.Drawing.Size(863, 595);
            this.tabBasic.Text = "用户基本信息";
            // 
            // tabFunction
            // 
            this.tabFunction.Controls.Add(this.treeFunction);
            this.tabFunction.Name = "tabFunction";
            this.tabFunction.Size = new System.Drawing.Size(863, 595);
            this.tabFunction.Text = "可操作功能";
            // 
            // treeFunction
            // 
            this.treeFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFunction.ImageIndex = 0;
            this.treeFunction.ImageList = this.imageList1;
            this.treeFunction.ItemHeight = 24;
            this.treeFunction.Location = new System.Drawing.Point(0, 0);
            this.treeFunction.Name = "treeFunction";
            treeNode1.Name = "节点0";
            treeNode1.Text = "节点0";
            treeNode2.Name = "节点2";
            treeNode2.Text = "节点2";
            treeNode3.Name = "节点1";
            treeNode3.Text = "节点1";
            this.treeFunction.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
            this.treeFunction.SelectedImageIndex = 0;
            this.treeFunction.Size = new System.Drawing.Size(863, 595);
            this.treeFunction.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "0036.ICO");
            this.imageList1.Images.SetKeyName(1, "Key16.ico");
            // 
            // FrmEditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 682);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmEditUser";
            this.Text = "系统用户信息";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            this.Controls.SetChildIndex(this.xtraTabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUserCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdCard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobilePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOfficePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHomePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkAddr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBirthday.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBirthday.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatorTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatorTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSignature.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSignature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOfficePhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUserCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLoginName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBirthday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCreator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCreatorTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIsExpire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIsDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSeq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIdCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAuditStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMobilePhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHomePhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciWorkAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabBasic.ResumeLayout(false);
            this.tabFunction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtUserCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtLoginName;
        private AddIn.UI.BizControl.DictControl txtAuditStatus;
        private DevExpress.XtraEditors.TextEdit txtIdCard;
        private DevExpress.XtraEditors.TextEdit txtMobilePhone;
        private DevExpress.XtraEditors.TextEdit txtOfficePhone;
        private DevExpress.XtraEditors.TextEdit txtHomePhone;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.TextEdit txtAddress;
        private DevExpress.XtraEditors.TextEdit txtWorkAddr;
        private DevExpress.XtraEditors.DateEdit txtBirthday;
        private DevExpress.XtraEditors.TextEdit txtQq;
        private DevExpress.XtraEditors.TextEdit txtSeq;
        private DevExpress.XtraEditors.TextEdit txtCreator;
        private DevExpress.XtraEditors.DateEdit txtCreatorTime;    
        private DevExpress.XtraLayout.LayoutControlItem lciUserCode;
        private DevExpress.XtraLayout.LayoutControlItem lblName;    
        private DevExpress.XtraLayout.LayoutControlItem lblLoginName;
        private DevExpress.XtraLayout.LayoutControlItem lciAuditStatus;    
        private DevExpress.XtraLayout.LayoutControlItem lciIsDelete;    
        private DevExpress.XtraLayout.LayoutControlItem lciIdCard;    
        private DevExpress.XtraLayout.LayoutControlItem lciMobilePhone;    
        private DevExpress.XtraLayout.LayoutControlItem lciOfficePhone;    
        private DevExpress.XtraLayout.LayoutControlItem lciHomePhone;    
        private DevExpress.XtraLayout.LayoutControlItem lciEmail;    
        private DevExpress.XtraLayout.LayoutControlItem lciAddress;    
        private DevExpress.XtraLayout.LayoutControlItem lciWorkAddress;    
        private DevExpress.XtraLayout.LayoutControlItem lciGender;    
        private DevExpress.XtraLayout.LayoutControlItem lciBirthday;    
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;    
        private DevExpress.XtraLayout.LayoutControlItem lciSignature;
        private DevExpress.XtraLayout.LayoutControlItem lciMac;
        private DevExpress.XtraLayout.LayoutControlItem lciRemark;    
        private DevExpress.XtraLayout.LayoutControlItem lciSeq;
        private DevExpress.XtraLayout.LayoutControlItem lciCreator;
        private DevExpress.XtraLayout.LayoutControlItem lciCreatorTime;
        private DevExpress.XtraEditors.TextEdit txtIP;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.MemoEdit txtSignature;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private System.Windows.Forms.ListBox lvwRole;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.ListBox lvwOU;
        private DevExpress.XtraEditors.TextEdit txtMac;
        private AddIn.UI.BizControl.DictControl txtIsExpire;
        private DevExpress.XtraLayout.LayoutControlItem lciIsExpire;
        private DeptControl txtDept;
        private DevExpress.XtraLayout.LayoutControlItem lblDept;
        private JCodes.Framework.AddIn.Basic.BizControl.CompanyControl txtCompany;
        private DevExpress.XtraLayout.LayoutControlItem lblCompany;
        private JCodes.Framework.AddIn.UI.BizControl.DictControl txtGender;
        private DevExpress.XtraLayout.LayoutControlItem lciIP;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private AddIn.UI.BizControl.DictControl txtIsDelete;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabBasic;
        private DevExpress.XtraTab.XtraTabPage tabFunction;
        private System.Windows.Forms.TreeView treeFunction;
        private System.Windows.Forms.ImageList imageList1;    
        
    }
}