using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraSplashScreen;
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
using DevExpress.Utils;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit;
using DevExpress.Office.Internal;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Import;
using DevExpress.XtraRichEdit.Internal;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmTables : BaseDock
    {

        private XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
        private XmlHelper xmltablesinfohelper = null;

        private NavBarControl navBar = null;

        NavBarGroup selectedGroup = null;
        NavBarItemLink selectedLink = null;

        private string strBasicInfoGuid = string.Empty;
        DateEdit _txtlastupdate = null;

        public FrmTables()
        {
            InitializeComponent();
        }

        void frmMain_Load(object sender, System.EventArgs e) {
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
                selectedGroup = hi.Group;
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

            // 根据配置读取分类
            XmlNodeList xmlNodeLst = xmltableshelper.Read("datatype/tabletype");
            List<TablesTypeInfo> tablesTypeInfoList = new List<TablesTypeInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                TablesTypeInfo tablesTypeInfo = new TablesTypeInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                tablesTypeInfo.GUID = xe.GetAttribute("guid").ToString();
                tablesTypeInfo.CreateDate = xe.GetAttribute("createdate").ToString();
                tablesTypeInfo.Name = xe.GetAttribute("name").ToString();

                tablesTypeInfoList.Add(tablesTypeInfo);
            }

            XmlNodeList xmlNodeLst2 = xmltableshelper.Read("datatype/dataitem");
            List<TablesInfo> tablesInfoList = new List<TablesInfo>();
            foreach (XmlNode xn1 in xmlNodeLst2)
            {
                TablesInfo tablesInfo = new TablesInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                tablesInfo.GUID = xe.GetAttribute("guid").ToString();

                // 得到ConstantInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                tablesInfo.Name = xnl0.Item(0).InnerText;
                tablesInfo.ChineseName = xnl0.Item(1).InnerText;
                tablesInfo.FunctionId = xnl0.Item(2).InnerText;
                tablesInfo.TypeGuid = xnl0.Item(3).InnerText;
                tablesInfo.Path = xnl0.Item(4).InnerText;

                tablesInfoList.Add(tablesInfo);
            }

            if (tablesTypeInfoList.Count == 0) return;

            foreach (var tablesTypeInfo in tablesTypeInfoList)
            {
                NavBarGroup standardGroup = navBar.Groups.Add();
                standardGroup.Caption = tablesTypeInfo.Name;
                standardGroup.Tag = tablesTypeInfo.GUID;
                standardGroup.Expanded = true;

                foreach (var tablesInfo in tablesInfoList)
                {
                    if (string.Equals(tablesTypeInfo.GUID, tablesInfo.TypeGuid))
                    {
                        NavBarItem item = new NavBarItem();
                        item.Caption = string.Format("{0}-({1} {2})", tablesInfo.FunctionId, tablesInfo.ChineseName, tablesInfo.Name);
                        // 临时调整为表名
                        item.Tag = tablesInfo.GUID;
                        item.Name = tablesInfo.Name;
                        item.Hint = tablesInfo.ChineseName;
                        item.LinkClicked += Item_LinkClicked;
                        navBar.Items.Add(item);
                        standardGroup.ItemLinks.Add(item);
                    }
                }
            }
        }

        void richEditControl_InitializeDocument(object sender, EventArgs e)
        {
            Document document = (sender as DevExpress.XtraRichEdit.RichEditControl).Document;
            document.BeginUpdate();
            document.DefaultCharacterProperties.FontName = "Courier New";
            document.DefaultCharacterProperties.FontSize = 10;
            document.Sections[0].Page.Width = Units.InchesToDocumentsF(50);
            document.Sections[0].LineNumbering.CountBy = 1;
            document.Sections[0].LineNumbering.RestartType = LineNumberingRestart.Continuous;
            document.EndUpdate();
        }

        /// <summary>
        /// 新增组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAddGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmEditGroupName dlg = new FrmEditGroupName();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                NavBarGroup standardGroup = navBar.Groups.Add();
                standardGroup.Tag = dlg.ID;
                standardGroup.Caption = dlg.strGroupName;
            }
        }

        /// <summary>
        /// 修改组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiModGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedGroup == null)
            {
                MessageDxUtil.ShowError("请选择需要修改的分组");
                return;
            }

            FrmEditGroupName dlg = new FrmEditGroupName();
            dlg.ID = selectedGroup.Tag.ToString();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                selectedGroup.Caption = dlg.strGroupName;
            }

            selectedGroup = null;
        }

        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void bbiDelGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedGroup == null)
            {
                MessageDxUtil.ShowError("请选择需要修改的分组");
                return;
            }
            // 需要重新读一下xml文件不存缓存里面没有
            xmltableshelper = new XmlHelper(@"XML\tables.xml");
            // 删除大的分类
            xmltableshelper.DeleteByPathNode(string.Format("datatype/tabletype/item[@guid=\"{0}\"]", selectedGroup.Tag));
            // 删除子项
            while (true)
            {
                try
                {
                    // 再删除子节点本身
                    var objXmlDoc = xmltableshelper.GetXmlDoc();
                    XmlNode xn = objXmlDoc.SelectSingleNode(string.Format("datatype/dataitem/item[typeguid=\"{0}\"]", selectedGroup.Tag));
                    // 删除table文件
                    if (FileUtil.FileIsExist(string.Format(@"XML\{0}.table", xn.FirstChild.InnerText)))
                    {
                        FileUtil.DeleteFile(string.Format(@"XML\{0}.table", xn.FirstChild.InnerText));
                    }

                    xn.ParentNode.RemoveChild(xn);
                }
                catch (Exception ex)
                {
                    break;
                }
            }

            navBar.Groups.Remove(selectedGroup);

            xmltableshelper.Save(false);

            selectedGroup = null;
        }

        /// <summary>
        /// 新增项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedGroup == null)
            {
                MessageDxUtil.ShowError("请选择需要修改的分组");
                return;
            }

            FrmEditItemName dlg = new FrmEditItemName();

            dlg.strGuid = selectedGroup.Tag.ToString();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                NavBarItem item = new NavBarItem();
                item.Caption = string.Format("{0}-({1} {2})", dlg.strFunction, dlg.strChineseName, dlg.strItemName);
                item.Tag = dlg.ID;
                item.Name = dlg.strItemName;
                item.Hint = dlg.strChineseName;
                item.LinkClicked += Item_LinkClicked;

                navBar.Items.Add(item);
                selectedGroup.ItemLinks.Add(item);
            }

            selectedGroup = null;
        }

        /// <summary>
        /// 修改项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiModItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void bbiDelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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
                if (tabbedView.Documents[i].Tag == item.Hint)
                {
                    tabbedView.Controller.Activate(tabbedView.Documents[i]);
                    return;
                }
            }

            tabbedView.BeginUpdate();
            UserControl control = new UserControl();

            XtraTabControl xtraTabControl1 = new XtraTabControl(); ;
            XtraTabPage xtraTabPageBasic = new XtraTabPage();
            XtraTabPage xtraTabPageFields = new XtraTabPage();
            XtraTabPage xtraTabPageSQLLook = new XtraTabPage();
            XtraTabPage xtraTabPageHistoryRecord = new XtraTabPage();

            ((System.ComponentModel.ISupportInitialize)(xtraTabControl1)).BeginInit();
            xtraTabControl1.SuspendLayout();

            xtraTabControl1.Dock = DockStyle.Fill;
            xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            xtraTabControl1.Name = "xtraTabControl1";
            xtraTabControl1.SelectedTabPage = xtraTabPageBasic;
            xtraTabControl1.Size = new System.Drawing.Size(1206, 556);
            xtraTabControl1.TabIndex = 0;
            xtraTabControl1.TabPages.AddRange(new XtraTabPage[] { xtraTabPageBasic, xtraTabPageFields, xtraTabPageSQLLook, xtraTabPageHistoryRecord });

            #region 基本信息
            xtraTabPageBasic.Name = "xtraTabPageBasic";
            xtraTabPageBasic.Text = "基本信息";

            GroupControl groupControl1 = new GroupControl();
            Label lblobjectId = new Label();
            Label lblenglishName = new Label();
            Label lblchineseName = new Label();
            Label lblexistHisTable = new Label();
            Label lblDB = new Label();
            Label lblversion = new Label();
            Label lbllastupdate = new Label();
            Label lblremark = new Label();

            TextEdit txtobjectId = new TextEdit();
            TextEdit txtenglishName = new TextEdit();
            TextEdit txtchineseName = new TextEdit();
            TextEdit txtversion = new TextEdit();
            DateEdit txtlastupdate = new DateEdit();
            CheckEdit ckexistHisTable = new CheckEdit();
            ComboBoxEdit cbbDB = new ComboBoxEdit();
            MemoEdit meremark = new MemoEdit();

            ((System.ComponentModel.ISupportInitialize)(groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtobjectId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtenglishName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtchineseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtversion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtlastupdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ckexistHisTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(cbbDB.Properties)).BeginInit();
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

            lblenglishName.Location = new System.Drawing.Point(5, 55);
            lblenglishName.Name = "lblenglishName";
            lblenglishName.Size = new System.Drawing.Size(90, 22);
            lblenglishName.Text = "英文名";
            lblenglishName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtenglishName.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtenglishName.Location = new System.Drawing.Point(100, 57);
            txtenglishName.Name = "txtenglishName";
            txtenglishName.Size = new System.Drawing.Size(180, 22);

            lblchineseName.Location = new System.Drawing.Point(5, 80);
            lblchineseName.Name = "lblenglishName";
            lblchineseName.Size = new System.Drawing.Size(90, 22);
            lblchineseName.Text = "中文名";
            lblchineseName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtchineseName.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtchineseName.Location = new System.Drawing.Point(100, 82);
            txtchineseName.Name = "txtchineseName";
            txtchineseName.Size = new System.Drawing.Size(180, 22);

            lblexistHisTable.Location = new System.Drawing.Point(5, 105);
            lblexistHisTable.Name = "lblexistHisTable";
            lblexistHisTable.Size = new System.Drawing.Size(90, 22);
            lblexistHisTable.Text = "存在历史表";
            lblexistHisTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            ckexistHisTable.Location = new System.Drawing.Point(100, 107);
            ckexistHisTable.Name = "ckexistHisTable";
            ckexistHisTable.Properties.Caption = "是";

            lblDB.Location = new System.Drawing.Point(5, 130);
            lblDB.Name = "lblDB";
            lblDB.Size = new System.Drawing.Size(90, 22);
            lblDB.Text = "数据库";
            lblDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            cbbDB.Location = new System.Drawing.Point(100, 132);
            cbbDB.Name = "cbbDB";
            cbbDB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            cbbDB.Size = new System.Drawing.Size(180, 22);

            lblversion.Location = new System.Drawing.Point(5, 155);
            lblversion.Name = "lblversion";
            lblversion.Size = new System.Drawing.Size(90, 22);
            lblversion.Text = "版本号";
            lblversion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtversion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            txtversion.Location = new System.Drawing.Point(100, 157);
            txtversion.Name = "txtversion";
            txtversion.Size = new System.Drawing.Size(180, 22);

            lbllastupdate.Location = new System.Drawing.Point(5, 180);
            lbllastupdate.Name = "lbllastupdate";
            lbllastupdate.Size = new System.Drawing.Size(90, 22);
            lbllastupdate.Text = "修改日期";
            lbllastupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtlastupdate.EditValue = null;
            txtlastupdate.Location = new System.Drawing.Point(100, 182);
            txtlastupdate.Name = "txtlastupdate";
            txtlastupdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            txtlastupdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            txtlastupdate.Size = new System.Drawing.Size(180, 22);

            lblremark.Location = new System.Drawing.Point(5, 205);
            lblremark.Name = "lblremark";
            lblremark.Size = new System.Drawing.Size(90, 22);
            lblremark.Text = "说明";
            lblremark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            meremark.Location = new System.Drawing.Point(100, 207);
            meremark.Name = "meremark";
            meremark.Size = new System.Drawing.Size(180, 120);
            meremark.UseOptimizedRendering = true;

            groupControl1.Controls.Add(lblobjectId);
            groupControl1.Controls.Add(txtobjectId);
            groupControl1.Controls.Add(lblenglishName);
            groupControl1.Controls.Add(txtenglishName);
            groupControl1.Controls.Add(lblchineseName);
            groupControl1.Controls.Add(txtchineseName);
            groupControl1.Controls.Add(lblexistHisTable);
            groupControl1.Controls.Add(ckexistHisTable);
            groupControl1.Controls.Add(lblDB);
            groupControl1.Controls.Add(cbbDB);
            groupControl1.Controls.Add(lblversion);
            groupControl1.Controls.Add(txtversion);
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

            #region 绑定数据库类型
            List<CListItem> dbtypeList = new List<CListItem>();
            dbtypeList.Add(new CListItem("Oracle", "Oracle数据库"));
            dbtypeList.Add(new CListItem("Mysql", "Mysql数据库"));
            dbtypeList.Add(new CListItem("DB2", "DB2数据库"));
            dbtypeList.Add(new CListItem("SqlServer", "SqlServer数据库"));
            dbtypeList.Add(new CListItem("Sqlite", "Sqlite数据库"));
            dbtypeList.Add(new CListItem("Access", "Access数据库"));
            cbbDB.BindDictItems(dbtypeList);
            #endregion

            xmltablesinfohelper = new XmlHelper(string.Format(@"XML\{0}.table", item.Name));
            XmlNodeList xmlNodeLst = xmltablesinfohelper.Read(string.Format("datatype/basicinfo"));
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                strBasicInfoGuid = xe.GetAttribute("guid").ToString();

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                txtobjectId.Text = xnl0.Item(0).InnerText;
                txtenglishName.Text = xnl0.Item(1).InnerText;
                txtchineseName.Text = xnl0.Item(2).InnerText;
                ckexistHisTable.Checked = xnl0.Item(3).InnerText == "1" ? true : false;
                cbbDB.SetComboBoxItem(xnl0.Item(4).InnerText);
                txtversion.Text = xnl0.Item(5).InnerText;
                txtlastupdate.Text = xnl0.Item(6).InnerText;
                meremark.Text = xnl0.Item(7).InnerText;
            }
            #endregion

            #region 基本信息修改
            _txtlastupdate = txtlastupdate;
            txtobjectId.Validated += new System.EventHandler(txtValue_Validated);
            txtenglishName.Validated += new System.EventHandler(txtValue_Validated);
            txtchineseName.Validated += new System.EventHandler(txtValue_Validated);
            ckexistHisTable.Validated += new System.EventHandler(txtValue_Validated);
            cbbDB.Validated += new System.EventHandler(txtValue_Validated);
            txtversion.Validated += new System.EventHandler(txtValue_Validated);
            txtlastupdate.Validated += new System.EventHandler(txtValue_Validated);
            meremark.Validated += new System.EventHandler(txtValue_Validated);

            #endregion

            #region 字段及索引
            xtraTabPageFields.Name = "xtraTabPageFields";
            xtraTabPageFields.Text = "字段及索引";

            SplitContainer splitContainer1 = new SplitContainer();
            GroupControl groupControlFields = new GroupControl();
            GroupControl groupControlIndexs = new GroupControl();

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


            CommonControl.Pager.WinGridView gridViewFields = new CommonControl.Pager.WinGridView();
            gridViewFields.AppendedMenu = null;
            gridViewFields.Dock = DockStyle.Fill;
            gridViewFields.FixedColumns = null;
            gridViewFields.Location = new System.Drawing.Point(0, 0);
            gridViewFields.MinimumSize = new System.Drawing.Size(540, 0);
            gridViewFields.Name = "gridViewFields";
            gridViewFields.PrintTitle = "";
            gridViewFields.ShowAddMenu = false;
            gridViewFields.ShowCheckBox = false;
            gridViewFields.ShowDeleteMenu = false;
            gridViewFields.ShowEditMenu = false;
            gridViewFields.ShowExportButton = false;
            gridViewFields.Size = new System.Drawing.Size(941, 549);
            gridViewFields.TabIndex = 0;
            gridViewFields.BestFitColumnWith = false;
            gridViewFields.DisplayColumns = "FieldName,ChineseName,FieldType,FieldInfo,IsNull,Remark";
            gridViewFields.AddColumnAlias("FieldName", "字段名");
            gridViewFields.AddColumnAlias("ChineseName", "中文名");
            gridViewFields.AddColumnAlias("FieldType", "字段类型");
            gridViewFields.AddColumnAlias("FieldInfo", "字段说明");
            gridViewFields.AddColumnAlias("IsNull", "允许空");
            gridViewFields.AddColumnAlias("Remark", "备注");

            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemChkIsNull = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemChkIsNull)).BeginInit();
            gridViewFields.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemChkIsNull});
            repositoryItemChkIsNull.AutoHeight = false;
            repositoryItemChkIsNull.Caption = "Check";
            repositoryItemChkIsNull.Name = "repositoryItemChkIsNull";
            ((System.ComponentModel.ISupportInitialize)(repositoryItemChkIsNull)).EndInit();

            groupControlFields.Controls.Add(gridViewFields);

            #endregion

            #region 索引表格
            groupControlIndexs.SuspendLayout();
            groupControlIndexs.Dock = DockStyle.Fill;
            groupControlIndexs.Name = "groupControlIndexs";
            groupControlIndexs.TabIndex = 5;
            groupControlIndexs.Text = "索引";

            CommonControl.Pager.WinGridView gridViewIndexs = new CommonControl.Pager.WinGridView();
            gridViewIndexs.AppendedMenu = null;
            gridViewIndexs.Dock = DockStyle.Fill;
            gridViewIndexs.FixedColumns = null;
            gridViewIndexs.Location = new System.Drawing.Point(0, 0);
            gridViewIndexs.MinimumSize = new System.Drawing.Size(540, 0);
            gridViewIndexs.Name = "gridViewIndexs";
            gridViewIndexs.PrintTitle = "";
            gridViewIndexs.ShowAddMenu = false;
            gridViewIndexs.ShowCheckBox = false;
            gridViewIndexs.ShowDeleteMenu = false;
            gridViewIndexs.ShowEditMenu = false;
            gridViewIndexs.ShowExportButton = false;
            gridViewIndexs.Size = new System.Drawing.Size(941, 549);
            gridViewIndexs.TabIndex = 0;
            gridViewIndexs.BestFitColumnWith = false;
            gridViewIndexs.DisplayColumns = "IndexName,IndexFieldLst,Unique,Primary,Cluster";
            gridViewIndexs.AddColumnAlias("IndexName", "索引名");
            gridViewIndexs.AddColumnAlias("IndexFieldLst", "索引字段列表");
            gridViewIndexs.AddColumnAlias("Unique", "唯一");
            gridViewIndexs.AddColumnAlias("Primary", "主键");
            gridViewIndexs.AddColumnAlias("Cluster", "聚合");
            groupControlIndexs.Controls.Add(gridViewIndexs);
            #endregion

            splitContainer1.Panel1.Controls.Add(groupControlFields);
            splitContainer1.Panel2.Controls.Add(groupControlIndexs);

            xtraTabPageFields.Controls.Add(splitContainer1);

            #endregion

           

            #region 字段新增修改

            #endregion

            #region 索引初始化
            XmlNodeList xmlindexLst = xmltablesinfohelper.Read(string.Format("datatype/indexsinfo"));

            List<TableIndexsInfo> IndexsInfoLst = new List<TableIndexsInfo>();

            foreach (XmlNode xn1 in xmlindexLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                TableIndexsInfo tableindexsInfo = new TableIndexsInfo();

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                tableindexsInfo.IndexName = xnl0.Item(0).InnerText;
                tableindexsInfo.IndexFieldLst = xnl0.Item(1).InnerText;
                tableindexsInfo.Unique = xnl0.Item(2).InnerText;
                tableindexsInfo.Primary = xnl0.Item(3).InnerText;
                tableindexsInfo.Cluster = xnl0.Item(4).InnerText;
                IndexsInfoLst.Add(tableindexsInfo);
            }

            gridViewIndexs.DataSource = IndexsInfoLst;
            #endregion

            #region SQL预览
            xtraTabPageSQLLook.Name = "xtraTabPageSQLLook";
            xtraTabPageSQLLook.Text = "SQL预览";

            DevExpress.XtraRichEdit.RichEditControl richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
            richEditControl.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            richEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            richEditControl.EnableToolTips = true;
            richEditControl.Location = new System.Drawing.Point(2, 2);
            richEditControl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            richEditControl.Name = "richEditControl";
            richEditControl.Options.AutoCorrect.DetectUrls = false;
            richEditControl.Options.AutoCorrect.ReplaceTextAsYouType = false;
            richEditControl.Options.Behavior.PasteLineBreakSubstitution = DevExpress.XtraRichEdit.LineBreakSubstitute.Paragraph;
            richEditControl.Options.CopyPaste.MaintainDocumentSectionSettings = false;
            richEditControl.Options.DocumentCapabilities.Bookmarks = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.CharacterStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.HeadersFooters = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.InlinePictures = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.Numbering.Bulleted = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.Numbering.MultiLevel = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.Numbering.Simple = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            richEditControl.Options.DocumentCapabilities.ParagraphStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.Sections = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.DocumentCapabilities.TableStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
            richEditControl.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            richEditControl.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            richEditControl.Options.MailMerge.KeepLastParagraph = false;
            richEditControl.Size = new System.Drawing.Size(1111, 627);
            richEditControl.TabIndex = 1;
            richEditControl.Text = "richEditControl1";
            richEditControl.Views.SimpleView.AllowDisplayLineNumbers = true;
            richEditControl.Views.SimpleView.Padding = new System.Windows.Forms.Padding(80, 4, 0, 0);
            richEditControl.InitializeDocument += new System.EventHandler(richEditControl_InitializeDocument);
            richEditControl.AddService(typeof(ISyntaxHighlightService), new SyntaxHighlightService(richEditControl));
            IRichEditCommandFactoryService commandFactory = richEditControl.GetService<IRichEditCommandFactoryService>();
            CustomRichEditCommandFactoryService newCommandFactory = new CustomRichEditCommandFactoryService(commandFactory);
            richEditControl.RemoveService(typeof(IRichEditCommandFactoryService));
            richEditControl.AddService(typeof(IRichEditCommandFactoryService), newCommandFactory);

            IDocumentImportManagerService importManager = richEditControl.GetService<IDocumentImportManagerService>();
            importManager.UnregisterAllImporters();
            importManager.RegisterImporter(new PlainTextDocumentImporter());
            importManager.RegisterImporter(new SourcesCodeDocumentImporter());

            IDocumentExportManagerService exportManager = richEditControl.GetService<IDocumentExportManagerService>();
            exportManager.UnregisterAllExporters();
            exportManager.RegisterExporter(new PlainTextDocumentExporter());
            exportManager.RegisterExporter(new SourcesCodeDocumentExporter());

            if (!FileUtil.IsExistFile("SQL.tmp"))
            {
                FileUtil.CreateFile("SQL.tmp");
            }
            richEditControl.LoadDocument(@"SQL.tmp", DocumentFormat.PlainText);

            xtraTabPageSQLLook.Controls.Add(richEditControl);
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
            gridViewModrecord.AddColumnAlias("GUID", "GUID");
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
            ((System.ComponentModel.ISupportInitialize)(txtversion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtlastupdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ckexistHisTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(cbbDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(meremark.Properties)).EndInit();

            groupControl1.ResumeLayout(false);
            groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(xtraTabControl1)).EndInit();
            xtraTabControl1.ResumeLayout(false);

            gridViewFields.GridView1.Columns["IsNull"].ColumnEdit = repositoryItemChkIsNull;
            #region 字段初始化

            XmlNodeList xmlfieldsLst = xmltablesinfohelper.Read(string.Format("datatype/fieldsinfo"));
            List<TableFieldsInfo> FieldsInfoLst = new List<TableFieldsInfo>();

            foreach (XmlNode xn1 in xmlfieldsLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                TableFieldsInfo tablefieldInfo = new TableFieldsInfo();

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                tablefieldInfo.FieldName = xnl0.Item(0).InnerText;
                tablefieldInfo.IsNull = xnl0.Item(1).InnerText;
                tablefieldInfo.Remark = xnl0.Item(2).InnerText;
                FieldsInfoLst.Add(tablefieldInfo);
            }

            gridViewFields.DataSource = FieldsInfoLst;
            #endregion

            control.Name = item.Hint;
            control.Text = item.Hint;

            BaseDocument document = tabbedView.AddDocument(control);
            document.Footer = Directory.GetCurrentDirectory();
            document.Tag = item.Hint;

            tabbedView.EndUpdate();
            tabbedView.Controller.Activate(document);
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
                    result = "datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/functionId";
                    break;
                case "txtenglishName":
                    result = "datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/name";
                    break;
                case "txtchineseName":
                    result = "datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/chineseName";
                    break;
                case "ckexistHisTable":
                    result = "datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/existhistable";
                    break;
                case "cbbDB":
                    result = "datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/dbtype";
                    break;
                case "txtversion":
                    result = "datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/version";
                    break;
                case "txtlastupdate":
                    result = "datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/lasttime";
                    break;
                case "meremark":
                    result = "datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/remark";
                    break;
            }

            // 如果不在这些已知的控件中则停止
            if (string.IsNullOrEmpty(result))
            {
                MessageDxUtil.ShowError("修改失败，其修改的控件不在已知范围内");
                return;
            }

            string curdatetime = DateTimeHelper.GetServerDateTime();
            xmltablesinfohelper.Replace(result, c.Name == "cbbDB" ? c.Text.Split('-')[0] : c.Text);
            xmltablesinfohelper.Replace("datatype/basicinfo/item[@guid=\"" + strBasicInfoGuid + "\"]/lasttime", curdatetime);
            xmltablesinfohelper.Save(false);

            _txtlastupdate.Text = curdatetime;
        }
    }

    #region SyntaxHighlightService
    public class SyntaxHighlightService : ISyntaxHighlightService
    {
        #region Fields
        readonly DevExpress.XtraRichEdit.RichEditControl editor;
        readonly SyntaxHighlightInfo syntaxHighlightInfo;
        #endregion


        public SyntaxHighlightService(DevExpress.XtraRichEdit.RichEditControl editor)
        {
            this.editor = editor;
            this.syntaxHighlightInfo = new SyntaxHighlightInfo();
        }


        #region ISyntaxHighlightService Members
        public void ForceExecute()
        {
            Execute();
        }
        public void Execute()
        {
            DevExpress.CodeParser.TokenCollection tokens = Parse(editor.Text);
            HighlightSyntax(tokens);
        }
        #endregion
        DevExpress.CodeParser.TokenCollection Parse(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;
            DevExpress.CodeParser.ITokenCategoryHelper tokenizer = CreateTokenizer();
            if (tokenizer == null)
                return new DevExpress.CodeParser.TokenCollection();
            return tokenizer.GetTokens(code);
        }

        DevExpress.CodeParser.ITokenCategoryHelper CreateTokenizer()
        {
            string fileName = editor.Options.DocumentSaveOptions.CurrentFileName;
            if (String.IsNullOrEmpty(fileName))
                return null;
            DevExpress.CodeParser.ITokenCategoryHelper result = DevExpress.CodeParser.TokenCategoryHelperFactory.CreateHelperForFileExtensions(Path.GetExtension(fileName));
            if (result != null)
                return result;
            else
                return null;
        }

        void HighlightSyntax(DevExpress.CodeParser.TokenCollection tokens)
        {
            if (tokens == null || tokens.Count == 0)
                return;
            Document document = editor.Document;
            CharacterProperties cp = document.BeginUpdateCharacters(0, 1);

            List<SyntaxHighlightToken> syntaxTokens = new List<SyntaxHighlightToken>(tokens.Count);
            foreach (DevExpress.CodeParser.Token token in tokens)
            {
                HighlightCategorizedToken((DevExpress.CodeParser.CategorizedToken)token, syntaxTokens);
            }
            document.ApplySyntaxHighlight(syntaxTokens);
            document.EndUpdateCharacters(cp);
        }
        void HighlightCategorizedToken(DevExpress.CodeParser.CategorizedToken token, List<SyntaxHighlightToken> syntaxTokens)
        {
            Color backColor = editor.ActiveView.BackColor;
            SyntaxHighlightProperties highlightProperties = syntaxHighlightInfo.CalculateTokenCategoryHighlight(token.Category);
            SyntaxHighlightToken syntaxToken = SetTokenColor(token, highlightProperties, backColor);
            if (syntaxToken != null)
                syntaxTokens.Add(syntaxToken);
        }
        SyntaxHighlightToken SetTokenColor(DevExpress.CodeParser.Token token, SyntaxHighlightProperties foreColor, Color backColor)
        {
            if (editor.Document.Paragraphs.Count < token.Range.Start.Line)
                return null;
            int paragraphStart = DocumentHelper.GetParagraphStart(editor.Document.Paragraphs[token.Range.Start.Line - 1]);
            int tokenStart = paragraphStart + token.Range.Start.Offset - 1;
            if (token.Range.End.Line != token.Range.Start.Line)
                paragraphStart = DocumentHelper.GetParagraphStart(editor.Document.Paragraphs[token.Range.End.Line - 1]);

            int tokenEnd = paragraphStart + token.Range.End.Offset - 1;
            System.Diagnostics.Debug.Assert(tokenEnd > tokenStart);
            return new SyntaxHighlightToken(tokenStart, tokenEnd - tokenStart, foreColor);
        }
    }
    #endregion

    #region SyntaxHighlightInfo
    public class SyntaxHighlightInfo
    {
        readonly Dictionary<DevExpress.CodeParser.TokenCategory, SyntaxHighlightProperties> properties;

        public SyntaxHighlightInfo()
        {
            this.properties = new Dictionary<DevExpress.CodeParser.TokenCategory, SyntaxHighlightProperties>();
            Reset();
        }
        public void Reset()
        {
            properties.Clear();
            Add(DevExpress.CodeParser.TokenCategory.Text, DXColor.Black);
            Add(DevExpress.CodeParser.TokenCategory.Keyword, DXColor.Blue);
            Add(DevExpress.CodeParser.TokenCategory.String, DXColor.Brown);
            Add(DevExpress.CodeParser.TokenCategory.Comment, DXColor.Green);
            Add(DevExpress.CodeParser.TokenCategory.Identifier, DXColor.Black);
            Add(DevExpress.CodeParser.TokenCategory.PreprocessorKeyword, DXColor.Blue);
            Add(DevExpress.CodeParser.TokenCategory.Number, DXColor.Red);
            Add(DevExpress.CodeParser.TokenCategory.Operator, DXColor.Black);
            Add(DevExpress.CodeParser.TokenCategory.Unknown, DXColor.Black);
            Add(DevExpress.CodeParser.TokenCategory.XmlComment, DXColor.Gray);

            Add(DevExpress.CodeParser.TokenCategory.CssComment, DXColor.Green);
            Add(DevExpress.CodeParser.TokenCategory.CssKeyword, DXColor.Brown);
            Add(DevExpress.CodeParser.TokenCategory.CssPropertyName, DXColor.Red);
            Add(DevExpress.CodeParser.TokenCategory.CssPropertyValue, DXColor.Blue);
            Add(DevExpress.CodeParser.TokenCategory.CssSelector, DXColor.Blue);
            Add(DevExpress.CodeParser.TokenCategory.CssStringValue, DXColor.Blue);

            Add(DevExpress.CodeParser.TokenCategory.HtmlAttributeName, DXColor.Red);
            Add(DevExpress.CodeParser.TokenCategory.HtmlAttributeValue, DXColor.Blue);
            Add(DevExpress.CodeParser.TokenCategory.HtmlComment, DXColor.Green);
            Add(DevExpress.CodeParser.TokenCategory.HtmlElementName, DXColor.Brown);
            Add(DevExpress.CodeParser.TokenCategory.HtmlEntity, DXColor.Gray);
            Add(DevExpress.CodeParser.TokenCategory.HtmlOperator, DXColor.Black);
            Add(DevExpress.CodeParser.TokenCategory.HtmlServerSideScript, DXColor.Black);
            Add(DevExpress.CodeParser.TokenCategory.HtmlString, DXColor.Blue);
            Add(DevExpress.CodeParser.TokenCategory.HtmlTagDelimiter, DXColor.Blue);
        }
        void Add(DevExpress.CodeParser.TokenCategory category, Color foreColor)
        {
            SyntaxHighlightProperties item = new SyntaxHighlightProperties();
            item.ForeColor = foreColor;
            properties.Add(category, item);
        }

        public SyntaxHighlightProperties CalculateTokenCategoryHighlight(DevExpress.CodeParser.TokenCategory category)
        {
            SyntaxHighlightProperties result = null;
            if (properties.TryGetValue(category, out result))
                return result;
            else
                return properties[DevExpress.CodeParser.TokenCategory.Text];
        }
    }
    #endregion

    #region CustomRichEditCommandFactoryService
    public class CustomRichEditCommandFactoryService : IRichEditCommandFactoryService
    {
        readonly IRichEditCommandFactoryService service;

        public CustomRichEditCommandFactoryService(IRichEditCommandFactoryService service)
        {
            Guard.ArgumentNotNull(service, "service");
            this.service = service;
        }

        #region IRichEditCommandFactoryService Members
        RichEditCommand IRichEditCommandFactoryService.CreateCommand(RichEditCommandId id)
        {
            if (id.Equals(RichEditCommandId.InsertColumnBreak) || id.Equals(RichEditCommandId.InsertLineBreak) || id.Equals(RichEditCommandId.InsertPageBreak))
                return service.CreateCommand(RichEditCommandId.InsertParagraph);
            return service.CreateCommand(id);
        }
        #endregion
    }
    #endregion

    public static class SourceCodeDocumentFormat
    {
        public static readonly DocumentFormat Id = new DocumentFormat(1325);
    }
    public class SourcesCodeDocumentImporter : PlainTextDocumentImporter
    {
        internal static readonly FileDialogFilter filter = new FileDialogFilter("Source Files", new string[] { "cs", "vb", "html", "htm", "js", "xml", "css" });
        public override FileDialogFilter Filter { get { return filter; } }
        public override DocumentFormat Format { get { return SourceCodeDocumentFormat.Id; } }
    }
    public class SourcesCodeDocumentExporter : PlainTextDocumentExporter
    {
        public override FileDialogFilter Filter { get { return SourcesCodeDocumentImporter.filter; } }
        public override DocumentFormat Format { get { return SourceCodeDocumentFormat.Id; } }
    }
}
