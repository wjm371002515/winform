using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraNavBar;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Entity;
using System.Xml;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native;
using System;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.Services;
using System.Drawing;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Import;
using DevExpress.XtraRichEdit.Internal;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Format;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System.Text;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmClassEntity : BaseDock
    {
        #region 控件集合
        private NavBarControl navBar;
        private NavBarItemLink selectedLink;
        private NavBarGroup standardGroup;
        private UserControl control;
        private XtraTabControl xtraTabControl1;
        private XtraTabPage xtraTabPageBasic;
        private XtraTabPage xtraTabPageFields;
        private XtraTabPage xtraTabPageHistoryRecord;
        private GroupControl groupControl1;
        private Label lblobjectId;
        private Label lblenglishName;
        private Label lblchineseName;
        private Label lblDB;
        private Label lblversion;
        private Label lblfolder;
        private Label lblfieldnamespace;
        private Label lblbaseclass;
        private Label lbllastupdate;
        private Label lblremark;
        private TextEdit txtobjectId;
        private TextEdit txtenglishName;
        private TextEdit txtchineseName;
        private TextEdit txtversion;
        private TextEdit txtfolder;
        private MemoEdit mefieldnamespace;
        private TextEdit txtbaseclass;
        private DateEdit txtlastupdate;
        private MemoEdit meremark;
        private SplitContainer splitContainer1;
        private GroupControl groupControlFields;
        private GroupControl groupControlIndexs;
        private DevExpress.XtraGrid.GridControl gridControlFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFields;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditFields;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnGuid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFieldName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnChineseName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFieldType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFieldInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRemark;
        private DevExpress.XtraGrid.GridControl gridControlIndexs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewIndexs;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndexGuid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndexName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndexChineseName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndexFieldType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndexContent;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndexRemark;
        DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit;
        #endregion

        #region 读取xml配置文件
        private XmlHelper xmltableshelper = new XmlHelper(@"XML\entity.xml");
        private XmlHelper xmltablesinfohelper = null;
        
        #endregion

        #region 数据缓存
        private string xmlfieldsinfomodel = "<name>{0}</name><remark>{1}</remark>";
        private string xmldiyfieldinfomodel = "<name>{0}</name><chinesename>{1}</chinesename><fieldtype></fieldtype><content>{2}</content><remark>{3}</remark>";

        private List<DictInfo> dictTypeInfoList = null;

        private string strBasicInfoGuid = string.Empty;

        private TableFieldsInfo tmptableFieldsInfo = null;

        private DiyFieldInfo tmptableIndexsInfo = null;
        #endregion

        public FrmClassEntity()
        {
            InitializeComponent();
        }

        void frmMain_Load(object sender, System.EventArgs e) {
            LoadDicData();

            BeginInvoke(new MethodInvoker(InitDemo));
        }
       
        void InitDemo() {
            // 创建一个NarBar;
            navBar = new NavBarControl();
            ((System.ComponentModel.ISupportInitialize)(navBar)).BeginInit();
            navBar.Dock = System.Windows.Forms.DockStyle.Fill;
            navBar.DragDropFlags = ((DevExpress.XtraNavBar.NavBarDragDrop)(((DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag | DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop)
            | DevExpress.XtraNavBar.NavBarDragDrop.AllowOuterDrop)));
            navBar.Location = new System.Drawing.Point(0, 0);
            navBar.Margin = new System.Windows.Forms.Padding(2);
            navBar.Name = "navBar";
            navBar.OptionsNavPane.ExpandedWidth = 139;
            navBar.Size = new System.Drawing.Size(139, 373);
            navBar.StoreDefaultPaintStyleName = true;
            navBar.TabIndex = 3;
            navBar.Text = "navBar";

            navBar.MouseDown += (sender, e) =>
            {
                if (e.Button != MouseButtons.Right) return;
                var navBarControl1 = sender as NavBarControl;
                NavBarHitInfo hi = navBarControl1.CalcHitInfo(new Point(e.X, e.Y));
                if (hi.InLink)
                {
                    selectedLink = hi.Link;
                    pmItem.ShowPopup(navBarControl1.PointToScreen(new Point(e.X, e.Y)));
                    return;
                }
                pmGroup.ShowPopup(navBarControl1.PointToScreen(new Point(e.X, e.Y)));
            };
            ucToolbox1.Controls.Add(navBar);
            ((System.ComponentModel.ISupportInitialize)(navBar)).EndInit();

            XmlNodeList xmlNodeLst = xmltableshelper.Read("datatype/dataitem");
            List<TablesInfo> tablesInfoList = new List<TablesInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                TablesInfo tablesInfo = new TablesInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                tablesInfo.Gid = xe.GetAttribute("gid").ToString();

                // 得到ConstantInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                tablesInfo.Name = xnl0.Item(0).InnerText;
                tablesInfo.ChineseName = xnl0.Item(1).InnerText;
                tablesInfo.FunctionId = xnl0.Item(2).InnerText.ToString().ToInt32();
                tablesInfo.BasicdataPath = xnl0.Item(3).InnerText;
                tablesInfoList.Add(tablesInfo);
            }
           
            standardGroup = navBar.Groups.Add();
            standardGroup.Caption = "实体类生成类";
            standardGroup.Tag = "实体类生成类";
            standardGroup.Expanded = true;

            foreach (var tablesInfo in tablesInfoList)
            {
                NavBarItem item = new NavBarItem();
                item.Caption = string.Format("{0}-({1} {2})", tablesInfo.FunctionId, tablesInfo.ChineseName, tablesInfo.Name);
                // 临时调整为表名
                item.Tag = tablesInfo.Gid;
                item.Name = tablesInfo.Name;
                item.Hint = tablesInfo.ChineseName;
                item.LinkClicked += Item_LinkClicked;
                navBar.Items.Add(item);
                standardGroup.ItemLinks.Add(item);
            }
        }

        /// <summary>
        /// 新增项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmEditItemEntity dlg = new FrmEditItemEntity();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                NavBarItem item = new NavBarItem();
                item.Caption = string.Format("{0}-({1} {2})", dlg.intFunction, dlg.strChineseName, dlg.strItemName);
                item.Tag = dlg.strGuid;
                item.Name = dlg.strItemName;
                item.Hint = dlg.strChineseName;
                item.LinkClicked += Item_LinkClicked;

                navBar.Items.Add(item);

                standardGroup.ItemLinks.Add(item);

                if (!standardGroup.Expanded)
                    standardGroup.Expanded = true;
            }
        }

        /// <summary>
        /// 修改项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiModItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedLink == null)
            {
                MessageDxUtil.ShowError("请选择需要修改的项目");
                return;
            }

            FrmEditItemEntity dlg = new FrmEditItemEntity();
            // 构造虚拟的ID 作为修改
            dlg.Id = 1;
            dlg.strGuid = selectedLink.Item.Tag.ToString();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                NavBarItem item = selectedLink.Item;

                item.Caption = string.Format("{0}-({1} {2})", dlg.intFunction, dlg.strChineseName, dlg.strItemName);
                item.Tag = dlg.strGuid;
                item.Name = dlg.strItemName;
                item.Hint = dlg.strChineseName;
            }
            selectedLink = null;
        }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedLink == null)
            {
                MessageDxUtil.ShowError("请选择需要删除的项目");
                return;
            }

            xmltableshelper = new XmlHelper(@"XML\entity.xml");
            // 删除子项
            xmltableshelper.DeleteByPathNode(string.Format("datatype/dataitem/item[@gid=\"{0}\"]", selectedLink.Item.Tag));
            xmltableshelper.Save(false);

            // 删除table文件
            if (FileUtil.IsExistFile(string.Format(@"XML\{0}.entity", selectedLink.Item.Name)))
            {
                FileUtil.DeleteFile(string.Format(@"XML\{0}.entity", selectedLink.Item.Name));
            }
            // 界面删除元素
            standardGroup.ItemLinks.Remove(selectedLink);

            selectedLink = null;
        }

        /// <summary>
        /// 点击表信息得到的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            var item = sender as NavBarItem;

            // 判断是否已经存在了如果存在了则改为选中
            for (System.Int32 i = 0; i < tabbedView.Documents.Count; i++)
            {
                // 找到 选中
                if (string.Equals(tabbedView.Documents[i].Tag, item.Hint))
                {
                    tabbedView.Controller.Activate(tabbedView.Documents[i]);
                    dockPanel6.HideSliding();
                    return;
                }
            }

            tabbedView.BeginUpdate();
            control = new UserControl();
            xtraTabControl1 = new XtraTabControl(); ;
            xtraTabPageBasic = new XtraTabPage();
            xtraTabPageFields = new XtraTabPage();
            xtraTabPageHistoryRecord = new XtraTabPage();

            ((System.ComponentModel.ISupportInitialize)(xtraTabControl1)).BeginInit();
            xtraTabControl1.SuspendLayout();

            xtraTabControl1.Dock = DockStyle.Fill;
            xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            xtraTabControl1.Name = "xtraTabControl1";
            xtraTabControl1.SelectedTabPage = xtraTabPageBasic;
            xtraTabControl1.Size = new System.Drawing.Size(1206, 556);
            xtraTabControl1.TabIndex = 0;
            xtraTabControl1.TabPages.AddRange(new XtraTabPage[] { xtraTabPageBasic, xtraTabPageFields, xtraTabPageHistoryRecord });

            #region 基本信息
            xtraTabPageBasic.Name = "xtraTabPageBasic";
            xtraTabPageBasic.Text = "基本信息";

            groupControl1 = new GroupControl();
            lblobjectId = new Label();
            lblenglishName = new Label();
            lblchineseName = new Label();
            lblDB = new Label();
            lblversion = new Label();
            lblfolder = new Label();
            lblfieldnamespace = new Label();
            lblbaseclass = new Label();
            lbllastupdate = new Label();
            lblremark = new Label();

            txtobjectId = new TextEdit();
            txtenglishName = new TextEdit();
            txtchineseName = new TextEdit();
            txtversion = new TextEdit();
            txtfolder = new TextEdit();
            mefieldnamespace = new MemoEdit();
            txtbaseclass = new TextEdit();
            txtlastupdate = new DateEdit();
            meremark = new MemoEdit();

            ((System.ComponentModel.ISupportInitialize)(groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtobjectId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtenglishName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtchineseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtversion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtfolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(mefieldnamespace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtbaseclass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtlastupdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(meremark.Properties)).BeginInit();

            groupControl1.SuspendLayout();
            groupControl1.Dock = DockStyle.Fill;

            lblobjectId.Location = new System.Drawing.Point(5, 30);
            lblobjectId.Name = "lblobjectId";
            lblobjectId.Size = new System.Drawing.Size(90, 22);
            lblobjectId.Text = "对象号";
            lblobjectId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtobjectId.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtobjectId.Location = new System.Drawing.Point(100, 32);
            txtobjectId.Name = "txtobjectId";
            txtobjectId.Size = new System.Drawing.Size(180, 22);
            txtobjectId.Enabled = false;

            lblenglishName.Location = new System.Drawing.Point(5, 55);
            lblenglishName.Name = "lblenglishName";
            lblenglishName.Size = new System.Drawing.Size(90, 22);
            lblenglishName.Text = "英文名";
            lblenglishName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtenglishName.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtenglishName.Location = new System.Drawing.Point(100, 57);
            txtenglishName.Name = "txtenglishName";
            txtenglishName.Size = new System.Drawing.Size(180, 22);
            txtenglishName.Enabled = false;

            lblchineseName.Location = new System.Drawing.Point(5, 80);
            lblchineseName.Name = "lblenglishName";
            lblchineseName.Size = new System.Drawing.Size(90, 22);
            lblchineseName.Text = "中文名";
            lblchineseName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtchineseName.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtchineseName.Location = new System.Drawing.Point(100, 82);
            txtchineseName.Name = "txtchineseName";
            txtchineseName.Size = new System.Drawing.Size(180, 22);
            txtchineseName.Enabled = false;

            lblversion.Location = new System.Drawing.Point(5, 105);
            lblversion.Name = "lblversion";
            lblversion.Size = new System.Drawing.Size(90, 22);
            lblversion.Text = "版本号";
            lblversion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtversion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtversion.Location = new System.Drawing.Point(100, 107);
            txtversion.Name = "txtversion";
            txtversion.Size = new System.Drawing.Size(180, 22);

            lblfolder.Location = new System.Drawing.Point(5, 130);
            lblfolder.Name = "lblfolder";
            lblfolder.Size = new System.Drawing.Size(90, 22);
            lblfolder.Text = "导出文件夹";
            lblfolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtfolder.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtfolder.Location = new System.Drawing.Point(100, 132);
            txtfolder.Name = "txtfolder";
            txtfolder.Size = new System.Drawing.Size(180, 22);

            lblfieldnamespace.Location = new System.Drawing.Point(5, 155);
            lblfieldnamespace.Name = "lblfieldnamespace";
            lblfieldnamespace.Size = new System.Drawing.Size(90, 22);
            lblfieldnamespace.Text = "命名空间";
            lblfieldnamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            mefieldnamespace.Location = new System.Drawing.Point(100, 157);
            mefieldnamespace.Name = "mefieldnamespace";
            mefieldnamespace.Size = new System.Drawing.Size(180, 120);
            mefieldnamespace.UseOptimizedRendering = true;

            lblbaseclass.Location = new System.Drawing.Point(5, 280);
            lblbaseclass.Name = "lblbaseclass";
            lblbaseclass.Size = new System.Drawing.Size(90, 22);
            lblbaseclass.Text = "继承父类";
            lblbaseclass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtbaseclass.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtbaseclass.Location = new System.Drawing.Point(100, 282);
            txtbaseclass.Name = "txtbaseclass";
            txtbaseclass.Size = new System.Drawing.Size(320, 22);

            lbllastupdate.Location = new System.Drawing.Point(5, 305);
            lbllastupdate.Name = "lbllastupdate";
            lbllastupdate.Size = new System.Drawing.Size(90, 22);
            lbllastupdate.Text = "修改日期";
            lbllastupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtlastupdate.EditValue = null;
            txtlastupdate.Location = new System.Drawing.Point(100, 307);
            txtlastupdate.Name = "txtlastupdate";
            txtlastupdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            txtlastupdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            txtlastupdate.Size = new System.Drawing.Size(180, 22);

            lblremark.Location = new System.Drawing.Point(5, 330);
            lblremark.Name = "lblremark";
            lblremark.Size = new System.Drawing.Size(90, 22);
            lblremark.Text = "说明";
            lblremark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            meremark.Location = new System.Drawing.Point(100, 332);
            meremark.Name = "meremark";
            meremark.Size = new System.Drawing.Size(180, 120);
            meremark.UseOptimizedRendering = true;

            groupControl1.Controls.Add(lblobjectId);
            groupControl1.Controls.Add(txtobjectId);
            groupControl1.Controls.Add(lblenglishName);
            groupControl1.Controls.Add(txtenglishName);
            groupControl1.Controls.Add(lblchineseName);
            groupControl1.Controls.Add(txtchineseName);
            groupControl1.Controls.Add(lblDB);
            groupControl1.Controls.Add(lblversion);
            groupControl1.Controls.Add(txtversion);
            groupControl1.Controls.Add(lblfolder);
            groupControl1.Controls.Add(txtfolder);
            groupControl1.Controls.Add(lblfieldnamespace);
            groupControl1.Controls.Add(mefieldnamespace);
            groupControl1.Controls.Add(lblbaseclass);
            groupControl1.Controls.Add(txtbaseclass);
            groupControl1.Controls.Add(lbllastupdate);
            groupControl1.Controls.Add(txtlastupdate);
            groupControl1.Controls.Add(lblremark);
            groupControl1.Controls.Add(meremark);

            groupControl1.Location = new System.Drawing.Point(3, 3);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new System.Drawing.Size(xtraTabPageBasic.ClientSize.Width - 30, xtraTabPageBasic.ClientSize.Height - 30);
            groupControl1.TabIndex = 5;
            groupControl1.Text = "基本信息";

            xtraTabPageBasic.Controls.Add(groupControl1);
            #endregion

            #region 基本信息初始化
            xmltablesinfohelper = new XmlHelper(string.Format(@"XML\{0}.entity", item.Name));
            XmlNodeList xmlNodeLst = xmltablesinfohelper.Read(string.Format("datatype/basicinfo"));
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                strBasicInfoGuid = xe.GetAttribute("gid").ToString();

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                txtobjectId.Text = xnl0.Item(0).InnerText;
                txtenglishName.Text = xnl0.Item(1).InnerText;
                txtchineseName.Text = xnl0.Item(2).InnerText;
                mefieldnamespace.Text = xnl0.Item(3).InnerText;
                txtversion.Text = xnl0.Item(4).InnerText;
                txtfolder.Text = xnl0.Item(5).InnerText;
                txtbaseclass.Text = xnl0.Item(6).InnerText.Replace("&lt;", "<").Replace( "&gt;", ">");
                txtlastupdate.Text = xnl0.Item(7).InnerText;
                meremark.Text = xnl0.Item(8).InnerText;
            }
            #endregion

            #region 基本信息修改
            txtobjectId.Validated += new System.EventHandler(txtValue_Validated);
            txtenglishName.Validated += new System.EventHandler(txtValue_Validated);
            txtchineseName.Validated += new System.EventHandler(txtValue_Validated);
            mefieldnamespace.Validated += new System.EventHandler(txtValue_Validated);
            txtversion.Validated += new System.EventHandler(txtValue_Validated);
            txtfolder.Validated += new System.EventHandler(txtValue_Validated);
            txtbaseclass.Validated += new System.EventHandler(txtValue_Validated);
            txtlastupdate.Validated += new System.EventHandler(txtValue_Validated);
            meremark.Validated += new System.EventHandler(txtValue_Validated);
            #endregion

            #region 字段及索引
            xtraTabPageFields.Name = "xtraTabPageFields";
            xtraTabPageFields.Text = "字段及自定义属性";

            splitContainer1 = new SplitContainer();
            groupControlFields = new GroupControl();
            groupControlIndexs = new GroupControl();

            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(groupControlFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(groupControlIndexs)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();

            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitContainer1.SplitterDistance = 413;

            #region 字段表格
            groupControlFields.SuspendLayout();
            groupControlFields.Dock = DockStyle.Fill;
            groupControlFields.Name = "groupControlFields";
            groupControlFields.TabIndex = 5;
            groupControlFields.Text = "字段";

            gridControlFields = new DevExpress.XtraGrid.GridControl();
            gridViewFields = new DevExpress.XtraGrid.Views.Grid.GridView();
            repositoryItemLookUpEditFields = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            gridColumnGuid = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnFieldName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnChineseName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnFieldType = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnFieldInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnRemark = new DevExpress.XtraGrid.Columns.GridColumn();

            ((System.ComponentModel.ISupportInitialize)(gridControlFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(gridViewFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemLookUpEditFields)).BeginInit();

            gridControlFields.Dock = DockStyle.Fill;
            gridControlFields.Cursor = System.Windows.Forms.Cursors.Default;
            gridControlFields.MainView = gridViewFields;
            gridControlFields.Name = "gridControlFields";
            gridControlFields.Size = new System.Drawing.Size(981, 573);
            gridControlFields.TabIndex = 13;
            gridControlFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridViewFields});
            gridControlFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemLookUpEditFields});
            gridControlFields.ContextMenuStrip = contextMenuStripFields;

            gridViewFields.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightCyan;
            gridViewFields.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.LightCyan;
            gridViewFields.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewFields.Appearance.FocusedRow.Options.UseForeColor = true;
            gridViewFields.GridControl = gridControlFields;
            gridViewFields.Name = "gridViewFields";
            gridViewFields.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewFields.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewFields.OptionsCustomization.AllowFilter = false;
            gridViewFields.OptionsCustomization.AllowGroup = false;
            gridViewFields.OptionsMenu.EnableColumnMenu = false;
            gridViewFields.OptionsMenu.EnableFooterMenu = false;
            gridViewFields.OptionsMenu.EnableGroupPanelMenu = false;
            gridViewFields.OptionsView.EnableAppearanceEvenRow = true;
            gridViewFields.OptionsView.EnableAppearanceOddRow = true;
            gridViewFields.OptionsView.ShowGroupPanel = false;
            gridViewFields.OptionsBehavior.Editable = true;
            gridViewFields.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnGuid, gridColumnFieldName, gridColumnChineseName, gridColumnFieldType, gridColumnFieldInfo, gridColumnRemark });
            gridViewFields.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewFields_CellValueChanged);
            gridViewFields.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewFields_CellValueChanging);
            gridViewFields.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewFields_ValidateRow);

            gridColumnGuid.Caption = "GUID";
            gridColumnGuid.Name = "gridColumnGUID";
            gridColumnGuid.Visible = true;
            gridColumnGuid.VisibleIndex = 0;
            gridColumnGuid.FieldName = "Gid";

            gridColumnFieldName.Caption = "字段名";
            gridColumnFieldName.Name = "gridColumnFieldName";
            gridColumnFieldName.Visible = true;
            gridColumnFieldName.VisibleIndex = 0;
            gridColumnFieldName.FieldName = "FieldName";

            gridColumnChineseName.Caption = "中文名";
            gridColumnChineseName.Name = "gridColumnChineseName";
            gridColumnChineseName.Visible = true;
            gridColumnChineseName.VisibleIndex = 1;
            gridColumnChineseName.FieldName = "ChineseName";
            gridColumnChineseName.OptionsColumn.ReadOnly = true;

            gridColumnFieldType.Caption = "字段类型";
            gridColumnFieldType.Name = "gridColumnFieldType";
            gridColumnFieldType.Visible = true;
            gridColumnFieldType.VisibleIndex = 2;
            gridColumnFieldType.FieldName = "FieldType";
            gridColumnFieldType.OptionsColumn.ReadOnly = true;

            gridColumnFieldInfo.Caption = "字段说明";
            gridColumnFieldInfo.Name = "gridColumnFieldInfo";
            gridColumnFieldInfo.Visible = true;
            gridColumnFieldInfo.VisibleIndex = 3;
            gridColumnFieldInfo.FieldName = "FieldInfo";
            gridColumnFieldInfo.OptionsColumn.ReadOnly = true;

            gridColumnRemark.Caption = "备注";
            gridColumnRemark.Name = "gridColumnRemark";
            gridColumnRemark.Visible = true;
            gridColumnRemark.VisibleIndex = 5;
            gridColumnRemark.FieldName = "Remark";

            repositoryItemLookUpEditFields.PopupWidth = 400; //下拉框宽度  
            repositoryItemLookUpEditFields.NullText = "";//空时的值  
            repositoryItemLookUpEditFields.DropDownRows = 10;//下拉框行数  
            repositoryItemLookUpEditFields.ImmediatePopup = true;//输入值是否马上弹出窗体  
            repositoryItemLookUpEditFields.ValidateOnEnterKey = true;//回车确认  
            repositoryItemLookUpEditFields.SearchMode = SearchMode.OnlyInPopup;//自动过滤掉不需要显示的数据，可以根据需要变化  
            repositoryItemLookUpEditFields.TextEditStyle = TextEditStyles.Standard;//要使用户可以输入，这里须设为Standard  
            repositoryItemLookUpEditFields.AllowNullInput = DevExpress.Utils.DefaultBoolean.True; //可用Ctrl + Delete清空选择內容 
            repositoryItemLookUpEditFields.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            //添加显示列  
            repositoryItemLookUpEditFields.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {  
                 new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "字段名"),  
                 new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ChineseName", "字段名称"),
                 new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DataType", "字段类型"),
                 new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DictNo", "字典条目"),
                 new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DictNameLst", "字典条目说明"),
            });
            repositoryItemLookUpEditFields.ValueMember = "Name";
            repositoryItemLookUpEditFields.DisplayMember = "Name";
            repositoryItemLookUpEditFields.EditValueChanged += repositoryItemLookUpEditFields_EditValueChanged;

            ((System.ComponentModel.ISupportInitialize)(repositoryItemLookUpEditFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(gridControlFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(gridViewFields)).EndInit();
            groupControlFields.Controls.Add(gridControlFields);
            gridControlFields.DataSource = new List<TableFieldsInfo>();
            #endregion

            #region 字段初始化

            XmlHelper stdfieldxmlHelper = new XmlHelper(@"XML\stdfield.xml");
            XmlNodeList stdfieldxmlNodeLst = stdfieldxmlHelper.Read("datatype/dataitem");
            
            List<StdFieldComboBox> stdFieldInfoList = new List<StdFieldComboBox>();
            foreach (XmlNode xn1 in stdfieldxmlNodeLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                StdFieldComboBox listItem = new StdFieldComboBox();
                listItem.Name = xnl0.Item(0).InnerText;
                listItem.ChineseName = xnl0.Item(1).InnerText;
                listItem.DataType = xnl0.Item(2).InnerText;
                listItem.DictNo = xnl0.Item(3).InnerText.ToInt32();
                if (dictTypeInfoList != null)
                {
                    var dictType = dictTypeInfoList.Find(new Predicate<DictInfo>(dictinfo => dictinfo.Id == xnl0.Item(3).InnerText.ToInt32()));
                    if (dictType != null) listItem.DictNameLst = dictType.Remark;
                }

                stdFieldInfoList.Add(listItem);
            }

            repositoryItemLookUpEditFields.DataSource = stdFieldInfoList;

            gridViewFields.Columns["FieldName"].ColumnEdit = repositoryItemLookUpEditFields;
            gridViewFields.Columns["Gid"].Visible = false;

            XmlNodeList xmlfieldsLst = xmltablesinfohelper.Read(string.Format("datatype/fieldsinfo"));
            List<TableFieldsInfo> FieldsInfoLst = new List<TableFieldsInfo>();

            foreach (XmlNode xn1 in xmlfieldsLst)
            {
                TableFieldsInfo tablefieldInfo = new TableFieldsInfo();

                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                tablefieldInfo.Gid = xe.GetAttribute("gid").ToString();

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                
                for(Int32 i = 0; i < stdFieldInfoList.Count; i ++)
                {
                    if (string.Equals(stdFieldInfoList[i].Name, xnl0.Item(0).InnerText))
                    {
                        tablefieldInfo.FieldName = stdFieldInfoList[i].Name;
                        tablefieldInfo.ChineseName = stdFieldInfoList[i].ChineseName;
                        tablefieldInfo.DataType = stdFieldInfoList[i].DataType;
                        tablefieldInfo.FieldInfo = stdFieldInfoList[i].DictNameLst;
                        break;
                    }
                }

                tablefieldInfo.Remark = xnl0.Item(1).InnerText;
                tablefieldInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                FieldsInfoLst.Add(tablefieldInfo);
            }

            gridControlFields.DataSource = FieldsInfoLst;

            #endregion

            #region 索引表格
            groupControlIndexs.SuspendLayout();
            groupControlIndexs.Dock = DockStyle.Fill;
            groupControlIndexs.Name = "groupControlIndexs";
            groupControlIndexs.TabIndex = 5;
            groupControlIndexs.Text = "自定义属性";

            gridControlIndexs = new DevExpress.XtraGrid.GridControl();
            gridViewIndexs = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumnIndexGuid = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnIndexName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnIndexChineseName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnIndexFieldType = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnIndexContent = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnIndexRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemMemoExEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();

            ((System.ComponentModel.ISupportInitialize)(gridControlIndexs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(gridViewIndexs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemMemoExEdit)).BeginInit();

            gridControlIndexs.Dock = DockStyle.Fill;
            gridControlIndexs.Cursor = System.Windows.Forms.Cursors.Default;
            gridControlIndexs.MainView = gridViewIndexs;
            gridControlIndexs.Name = "gridControlIndexs";
            gridControlIndexs.Size = new System.Drawing.Size(981, 573);
            gridControlIndexs.TabIndex = 13;
            gridControlIndexs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridViewIndexs});
            gridControlIndexs.ContextMenuStrip = contextMenuStripIndex;
            gridControlIndexs.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemMemoExEdit });

            gridViewIndexs.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightCyan;
            gridViewIndexs.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.LightCyan;
            gridViewIndexs.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewIndexs.Appearance.FocusedRow.Options.UseForeColor = true;
            gridViewIndexs.GridControl = gridControlIndexs;
            gridViewIndexs.Name = "gridViewIndexs";
            gridViewIndexs.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewIndexs.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewIndexs.OptionsCustomization.AllowFilter = false;
            gridViewIndexs.OptionsCustomization.AllowGroup = false;
            gridViewIndexs.OptionsMenu.EnableColumnMenu = false;
            gridViewIndexs.OptionsMenu.EnableFooterMenu = false;
            gridViewIndexs.OptionsMenu.EnableGroupPanelMenu = false;
            gridViewIndexs.OptionsView.EnableAppearanceEvenRow = true;
            gridViewIndexs.OptionsView.EnableAppearanceOddRow = true;
            gridViewIndexs.OptionsView.ShowGroupPanel = false;
            gridViewIndexs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumnIndexGuid, gridColumnIndexName, gridColumnIndexChineseName, gridColumnIndexFieldType, gridColumnIndexContent, gridColumnIndexRemark });
            gridViewIndexs.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewIndexs_CellValueChanged);
            gridViewIndexs.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewIndexs_CellValueChanging);
            gridViewIndexs.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewIndexs_ValidateRow);

            gridColumnIndexGuid.Caption = "GUID";
            gridColumnIndexGuid.Name = "gridColumnIndexGuid";
            gridColumnIndexGuid.Visible = false;
            gridColumnIndexGuid.VisibleIndex = 0;
            gridColumnIndexGuid.FieldName = "Gid";

            gridColumnIndexName.Caption = "字段";
            gridColumnIndexName.Name = "gridColumnIndexName";
            gridColumnIndexName.Visible = true;
            gridColumnIndexName.VisibleIndex = 0;
            gridColumnIndexName.FieldName = "Name";

            gridColumnIndexChineseName.Caption = "中文名";
            gridColumnIndexChineseName.Name = "gridColumnIndexChineseName";
            gridColumnIndexChineseName.Visible = true;
            gridColumnIndexChineseName.VisibleIndex = 1;
            gridColumnIndexChineseName.FieldName = "ChineseName";

            gridColumnIndexFieldType.Caption = "数据类型";
            gridColumnIndexFieldType.Name = "gridColumnIndexFieldType";
            gridColumnIndexFieldType.Visible = true;
            gridColumnIndexFieldType.VisibleIndex = 1;
            gridColumnIndexFieldType.FieldName = "DataType";

            gridColumnIndexContent.Caption = "属性内容";
            gridColumnIndexContent.Name = "gridColumnIndexContent";
            gridColumnIndexContent.Visible = true;
            gridColumnIndexContent.VisibleIndex = 2;
            gridColumnIndexContent.FieldName = "AttrContent";
            gridColumnIndexContent.ColumnEdit = repositoryItemMemoExEdit;

            repositoryItemMemoExEdit.AutoHeight = false;
            repositoryItemMemoExEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemMemoExEdit.Name = "repositoryItemMemoExEdit";

            gridColumnIndexRemark.Caption = "备注";
            gridColumnIndexRemark.Name = "gridColumnIndexRemark";
            gridColumnIndexRemark.Visible = true;
            gridColumnIndexRemark.VisibleIndex = 3;
            gridColumnIndexRemark.FieldName = "Remark";

            ((System.ComponentModel.ISupportInitialize)(gridControlIndexs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(gridViewIndexs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemMemoExEdit)).EndInit();
            groupControlIndexs.Controls.Add(gridControlIndexs);
            gridControlIndexs.DataSource = new List<DiyFieldInfo>();

            #endregion

            splitContainer1.Panel1.Controls.Add(groupControlFields);
            splitContainer1.Panel2.Controls.Add(groupControlIndexs);

            xtraTabPageFields.Controls.Add(splitContainer1);

            XmlNodeList xmlindexLst = xmltablesinfohelper.Read(string.Format("datatype/diyfieldinfo"));

            List<DiyFieldInfo> IndexsInfoLst = new List<DiyFieldInfo>();

            foreach (XmlNode xn1 in xmlindexLst)
            {
                DiyFieldInfo tableindexsInfo = new DiyFieldInfo();

                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                tableindexsInfo.Gid = xe.GetAttribute("gid").ToString();

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                tableindexsInfo.Name = xnl0.Item(0).InnerText;
                tableindexsInfo.ChineseName = xnl0.Item(1).InnerText;
                tableindexsInfo.DataType = xnl0.Item(2).InnerText;
                tableindexsInfo.AttrContent = xnl0.Item(3).InnerText;
                tableindexsInfo.Remark = xnl0.Item(4).InnerText;
                tableindexsInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                IndexsInfoLst.Add(tableindexsInfo);
            }

            gridControlIndexs.DataSource = IndexsInfoLst;

            gridViewIndexs.Columns["Gid"].Visible = false;

            #endregion

            #region 修订记录
            xtraTabPageHistoryRecord.Name = "xtraTabPageHistoryRecord";
            xtraTabPageHistoryRecord.Text = "修订记录";

            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            repositoryItemDateEdit1.AutoHeight = false;
            repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            ((System.ComponentModel.ISupportInitialize)(repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemDateEdit1)).EndInit();

            CommonControl.Pager.WinGridViewPager gridViewModrecord = new CommonControl.Pager.WinGridViewPager();
            gridViewModrecord.DisplayColumns = "";
            gridViewModrecord.Dock = DockStyle.Fill;
            gridViewModrecord.FixedColumns = null;
            gridViewModrecord.Location = new System.Drawing.Point(0, 0);
            gridViewModrecord.MinimumSize = new System.Drawing.Size(540, 0);
            gridViewModrecord.Name = "gridViewModrecord";
            gridViewModrecord.PrintTitle = "";
            gridViewModrecord.ShowAddMenu = true;
            gridViewModrecord.ShowCheckBox = false;
            gridViewModrecord.ShowDeleteMenu = true;
            gridViewModrecord.ShowEditMenu = true;
            gridViewModrecord.ShowExportButton = true;
            gridViewModrecord.Size = new System.Drawing.Size(941, 549);
            gridViewModrecord.TabIndex = 0;
            gridViewModrecord.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemDateEdit1 });
            gridViewModrecord.DisplayColumns = "ModDate,ModVersion,ModOrderId,Proposer,Programmer,ModContent,ModReason,Remark";
            gridViewModrecord.AddColumnAlias("Gid", "GUID");
            gridViewModrecord.AddColumnAlias("ModDate", "修改日期");
            gridViewModrecord.AddColumnAlias("ModVersion", "修改版本");
            gridViewModrecord.AddColumnAlias("ModOrderId", "修改单号");
            gridViewModrecord.AddColumnAlias("Proposer", "申请人");
            gridViewModrecord.AddColumnAlias("Programmer", "修改人");
            gridViewModrecord.AddColumnAlias("ModContent", "修改内容");
            gridViewModrecord.AddColumnAlias("ModReason", "修改原因");
            gridViewModrecord.AddColumnAlias("Remark", "备注");
            List<ModRecordInfo> modRecordInfoLst = new List<ModRecordInfo>();
            gridViewModrecord.DataSource = modRecordInfoLst;
            xtraTabPageHistoryRecord.Controls.Add(gridViewModrecord);

            #endregion

            control.Controls.Add(xtraTabControl1);

            ((System.ComponentModel.ISupportInitialize)(groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(groupControlFields)).EndInit();
            groupControlFields.ResumeLayout(false);
            groupControlFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(groupControlIndexs)).EndInit();
            groupControlIndexs.ResumeLayout(false);
            groupControlIndexs.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(txtobjectId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtenglishName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtchineseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(mefieldnamespace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtfolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtbaseclass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtversion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtlastupdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(meremark.Properties)).EndInit();

            groupControl1.ResumeLayout(false);
            groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(xtraTabControl1)).EndInit();
            xtraTabControl1.ResumeLayout(false);

            control.Name = item.Hint;
            control.Text = item.Hint;

            BaseDocument document = tabbedView.AddDocument(control);
            document.Footer = Directory.GetCurrentDirectory();
            document.Tag = item.Hint;

            tabbedView.EndUpdate();
            tabbedView.Controller.Activate(document);

            dockPanel6.HideSliding();
        }

        /// <summary>  
        /// 实现用户自由输入  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void repositoryItemLookUpEditFields_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit edit = sender as LookUpEdit;
            if (edit.EditValue != null)
            {
                //取资料行，数据源为DataTable, 资料行是DataRowView对象。   
                StdFieldComboBox o = edit.Properties.GetDataSourceRowByKeyValue(edit.EditValue) as StdFieldComboBox;
                if (o != null)
                {
                    var tablefieldsInfo = gridViewFields.GetFocusedRow() as TableFieldsInfo;
                    tablefieldsInfo.FieldName = o.Name;
                    tablefieldsInfo.ChineseName = o.ChineseName;
                    tablefieldsInfo.DataType = o.DataType;
                    tablefieldsInfo.FieldInfo = o.DictNameLst;

                    XmlNodeList xmlNodeLst = xmltablesinfohelper.Read("datatype/fieldsinfo/item[@gid=\"" + tablefieldsInfo.Gid + "\"]");
                    xmlNodeLst.Item(0).InnerText = o.Name;
                    xmltablesinfohelper.Save(false);

                    gridViewFields.RefreshRow(gridViewFields.FocusedRowHandle);
                }
            }
        }

        /// <summary>
        /// 验证正确修改其对应的xml值 并更新其更新时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtValue_Validated(object sender, EventArgs e)
        {
            Control c = sender as Control;
            string result = string.Empty;
            string value = string.Empty;

            // 判断版本手工修改是否符合格式
            if (string.Equals(c.Name, "txtversion"))
            {
                try
                {
                    new Version(c.Text);
                }
                catch (Exception ex)
                {
                    MessageDxUtil.ShowError(ex.Message);
                    return;
                }
            }

            if (string.IsNullOrEmpty(strBasicInfoGuid))
                return;

            switch (c.Name)
            {
                case "txtobjectId":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/functionId";
                    break;
                case "txtenglishName":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/name";
                    break;
                case "txtchineseName":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/chineseName";
                    break;
                case "mefieldnamespace":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/fieldnamespace";
                    break;
                case "txtversion":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/version";
                    break;
                case "txtfolder":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/folder";
                    break;
                case "txtbaseclass":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/baseclass";
                    break;
                case "txtlastupdate":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/lasttime";
                    break;
                case "meremark":
                    result = "datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/remark";
                    break;
            }

            // 如果不在这些已知的控件中则停止
            if (string.IsNullOrEmpty(result))
            {
                MessageDxUtil.ShowError("修改失败，其修改的控件不在已知范围内");
                return;
            }

            xmltablesinfohelper.Replace(result, c.Text.Replace("<", "&lt;").Replace(">", "&gt;"));
            xmltablesinfohelper.Save(false);

            string curdatetime = DateTimeHelper.GetServerDateTime();
            xmltablesinfohelper.Replace("datatype/basicinfo/item[@gid=\"" + strBasicInfoGuid + "\"]/lasttime", curdatetime);
            xmltablesinfohelper.Save(false);

            txtlastupdate.Text = curdatetime;
        }

        /// <summary>
        /// 加载数据字典
        /// </summary>
        private void LoadDicData()
        {
            #region 加载数据字典大项
            XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
            XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/dataitem");
            dictTypeInfoList = new List<DictInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                DictInfo dictInfo = new DictInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                dictInfo.Id = xnl0.Item(0).InnerText.ToInt32();
                dictInfo.Pid = xnl0.Item(1).InnerText.ToInt32();
                dictInfo.Name = xnl0.Item(2).InnerText;

                StringBuilder sb = new StringBuilder();

                XmlNodeList xmlNodeLst2 = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic", dictInfo.Id));

                List<DictDetailInfo> dictDetailInfoList = new List<DictDetailInfo>();
                foreach (XmlNode xn12 in xmlNodeLst2)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe2 = (XmlElement)xn12;

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl02 = xe2.ChildNodes;
                    sb.Append(string.Format("{0}-{1},\r\n", xnl02.Item(0).InnerText, xnl02.Item(1).InnerText));
                }

                dictInfo.Remark = sb.ToString().TrimEnd(",\r\n".ToCharArray());

                dictTypeInfoList.Add(dictInfo);
            }
            #endregion
        }

        /// <summary>
        /// 新增字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_AddField_Click(object sender, EventArgs e)
        {
            var tableFieldsInfo = new TableFieldsInfo();
            tableFieldsInfo.Gid = System.Guid.NewGuid().ToString();
            tableFieldsInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

            xmltablesinfohelper.InsertElement("datatype/fieldsinfo", "item", "gid", tableFieldsInfo.Gid, string.Format(xmlfieldsinfomodel, string.Empty, string.Empty));
            xmltablesinfohelper.Save(false);

            (gridViewFields.DataSource as List<TableFieldsInfo>).Add(tableFieldsInfo);
            gridViewFields.RefreshData();
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_DelField_Click(object sender, EventArgs e)
        {
            // 20170824 如果是最后一行空行则不再继续操作
            if (gridViewFields.GetFocusedRow() as TableFieldsInfo == null || string.IsNullOrEmpty((gridViewFields.GetFocusedRow() as TableFieldsInfo).Gid))
                return;

            xmltablesinfohelper.DeleteByPathNode("datatype/fieldsinfo/item[@gid=\"" + gridViewFields.GetRowCellDisplayText(gridViewFields.FocusedRowHandle, "Gid") + "\"]");
            xmltablesinfohelper.Save(false);

            (gridViewFields.DataSource as List<TableFieldsInfo>).RemoveAt(gridViewFields.FocusedRowHandle);
            gridViewFields.RefreshData();
        }

        private void gridViewFields_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            XmlNodeList xmlNodeLst = xmltablesinfohelper.Read("datatype/fieldsinfo/item[@gid=\"" + tmptableFieldsInfo.Gid + "\"]");
            Int32 idx = -1;

            switch (e.Column.ToString())
            {
                case "字段名":
                    idx = 0;
                    break;
                case "备注":
                    idx = 1;
                    break;
            }

            if (idx == -1)
                return;

            xmlNodeLst.Item(idx).InnerText = e.Value.ToString();

            xmltablesinfohelper.Save(false);

            tmptableFieldsInfo = null;
        }

        private void gridViewFields_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // 20170824 如果是最后一行空行则不再继续操作
            if (string.IsNullOrEmpty((gridViewFields.GetFocusedRow() as TableFieldsInfo).Gid))
                return;

            tmptableFieldsInfo = gridViewFields.GetRow(e.RowHandle) as TableFieldsInfo;
        }

        private void gridViewFields_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            // 查询是否存在2个键值的数据
            List<TableFieldsInfo> lsttableFieldsInfo = gridViewFields.DataSource as List<TableFieldsInfo>;

            // 查找重复的Name的值 && 清楚原先的错误信息

            List<String> tmpName = new List<string>();
            List<String> lstName = new List<string>();
            foreach (TableFieldsInfo tableFieldsInfo in lsttableFieldsInfo)
            {
                if (string.IsNullOrEmpty(tableFieldsInfo.Gid))
                    continue;

                if (lstName.Contains(tableFieldsInfo.FieldName))
                {
                    tmpName.Add(tableFieldsInfo.FieldName);
                }

                lstName.Add(tableFieldsInfo.FieldName);

                tableFieldsInfo.lstInfo.Clear();
            }

            foreach (TableFieldsInfo tableFieldsInfo in lsttableFieldsInfo)
            {
                if (string.IsNullOrEmpty(tableFieldsInfo.Gid))
                    continue;

                // 判断重复的 类型名
                if (tmpName.Contains(tableFieldsInfo.FieldName))
                {
                    if (tableFieldsInfo.lstInfo.ContainsKey("FieldName"))
                    {
                        tableFieldsInfo.lstInfo["FieldName"].ErrorText = tableFieldsInfo.lstInfo["FieldName"].ErrorText + "\r\n一个表中不允许存在重复的字段名";
                        tableFieldsInfo.lstInfo["FieldName"].ErrorType = tableFieldsInfo.lstInfo["FieldName"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? tableFieldsInfo.lstInfo["FieldName"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        tableFieldsInfo.lstInfo.Add("FieldName", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("一个表中不允许存在重复的字段名", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                    }
                }

                // 判断名称是否为空
                if (string.IsNullOrEmpty(tableFieldsInfo.FieldName))
                {
                    if (tableFieldsInfo.lstInfo.ContainsKey("FieldName"))
                    {
                        tableFieldsInfo.lstInfo["FieldName"].ErrorText = tableFieldsInfo.lstInfo["FieldName"].ErrorText + "\r\n字段名不能为空";
                        tableFieldsInfo.lstInfo["FieldName"].ErrorType = tableFieldsInfo.lstInfo["FieldName"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? tableFieldsInfo.lstInfo["FieldName"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        tableFieldsInfo.lstInfo.Add("FieldName", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("字段名不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                    }
                }
            }
        }


        /// <summary>
        /// 新增索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_AddIndex_Click(object sender, EventArgs e)
        {
            var diyFieldInfo = new DiyFieldInfo();
            diyFieldInfo.Gid = System.Guid.NewGuid().ToString();
            diyFieldInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

            xmltablesinfohelper.InsertElement("datatype/diyfieldinfo", "item", "gid", diyFieldInfo.Gid, string.Format(xmldiyfieldinfomodel, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
            xmltablesinfohelper.Save(false);

            (gridViewIndexs.DataSource as List<DiyFieldInfo>).Add(diyFieldInfo);
            gridViewIndexs.RefreshData();
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_DelIndex_Click(object sender, EventArgs e)
        {
            // 20171106 wjm 修复删除没有数据报错问题
            // 20170824 如果是最后一行空行则不再继续操作
            if (gridViewIndexs.GetFocusedRow() as DiyFieldInfo == null || string.IsNullOrEmpty((gridViewIndexs.GetFocusedRow() as DiyFieldInfo).Gid))
                return;

            xmltablesinfohelper.DeleteByPathNode("datatype/diyfieldinfo/item[@gid=\"" + gridViewIndexs.GetRowCellDisplayText(gridViewIndexs.FocusedRowHandle, "Gid") + "\"]");
            xmltablesinfohelper.Save(false);

            (gridViewIndexs.DataSource as List<DiyFieldInfo>).RemoveAt(gridViewIndexs.FocusedRowHandle);
            gridViewIndexs.RefreshData();
        }
		
		private void gridViewIndexs_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            XmlNodeList xmlNodeLst = xmltablesinfohelper.Read("datatype/diyfieldinfo/item[@gid=\"" + tmptableIndexsInfo.Gid + "\"]");
            Int32 idx = -1;

            switch (e.Column.ToString())
            {
                case "字段":
                    idx = 0;
                    break;
                case "中文名":
                    idx = 1;
                    break;
                case "数据类型":
                    idx = 2;
                    break;
                case "属性内容":
                    idx = 3;
                    break;
                case "备注":
                    idx = 4;
                    break;
            }

            if (idx == -1)
                return;

            xmlNodeLst.Item(idx).InnerText = e.Value.ToString();

            xmltablesinfohelper.Save(false);

            tmptableIndexsInfo = null;
        }

        private void gridViewIndexs_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // 20170824 如果是最后一行空行则不再继续操作
            if (string.IsNullOrEmpty((gridViewIndexs.GetFocusedRow() as DiyFieldInfo).Gid))
                return;

            tmptableIndexsInfo = gridViewIndexs.GetRow(e.RowHandle) as DiyFieldInfo;
        }

        private void gridViewIndexs_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            // 查询是否存在2个键值的数据
            List<DiyFieldInfo> lsttableIndexsInfo = gridViewIndexs.DataSource as List<DiyFieldInfo>;

            // 查找重复的Name的值 && 清楚原先的错误信息

            List<String> tmpName = new List<string>();
            List<String> lstName = new List<string>();
            foreach (DiyFieldInfo tableIndexsInfo in lsttableIndexsInfo)
            {
                if (string.IsNullOrEmpty(tableIndexsInfo.Gid))
                    continue;

                if (lstName.Contains(tableIndexsInfo.Name))
                {
                    tmpName.Add(tableIndexsInfo.Name);
                }

                lstName.Add(tableIndexsInfo.Name);

                tableIndexsInfo.lstInfo.Clear();
            }

            foreach (DiyFieldInfo tableIndexsInfo in lsttableIndexsInfo)
            {
                if (string.IsNullOrEmpty(tableIndexsInfo.Gid))
                    continue;

                // 判断重复的 类型名
                if (tmpName.Contains(tableIndexsInfo.Name) && !string.IsNullOrEmpty(tableIndexsInfo.Name))
                {
                    if (tableIndexsInfo.lstInfo.ContainsKey("Name"))
                    {
                        tableIndexsInfo.lstInfo["Name"].ErrorText = tableIndexsInfo.lstInfo["Name"].ErrorText + "\r\n一个表中不允许存在重复的字段";
                        tableIndexsInfo.lstInfo["Name"].ErrorType = tableIndexsInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? tableIndexsInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        tableIndexsInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("一个表中不允许存在重复的字段", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                    }
                }
            }
        }
    }
}
