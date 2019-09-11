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
using System.Data;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Office;
using System.Xml.Linq;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmBaseDataManage : BaseDock
    {
        #region 控件集合
        private DevExpress.XtraRichEdit.RichEditControl richEditControl;
        private NavBarControl navBar;
        private NavBarGroup selectedGroup;
        private NavBarItemLink selectedLink;
        private UserControl control;
        private XtraTabControl xtraTabControl1;
        private XtraTabPage xtraTabPageFields;
        private XtraTabPage xtraTabPageSQLLook;
        private DevExpress.XtraGrid.GridControl gridControlFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFields;
        private DataTable dt = null;
        #endregion

        #region 读取xml配置文件
        private XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
        
        #endregion

        #region 数据缓存
        private List<DictInfo> dictTypeInfoList = null;
        private Dictionary<string, string> guidGroup = new Dictionary<string, string>();
        private Dictionary<string, string> tableGroup = new Dictionary<string, string>();

        #endregion

        public FrmBaseDataManage()
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
            guidGroup.Clear();
            tableGroup.Clear();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 配置的这个节点 basicdata 为1时才加载数据
                if (string.Equals(xe.GetAttribute("basicdata").ToString(), Const.Num_One.ToString()))
                {
                    TablesTypeInfo tablesTypeInfo = new TablesTypeInfo();
                    // 得到Type和ISBN两个属性的属性值
                    tablesTypeInfo.GUID = xe.GetAttribute("guid").ToString();
                    tablesTypeInfo.CreateDate = xe.GetAttribute("createdate").ToString();
                    tablesTypeInfo.Name = xe.GetAttribute("name").ToString();

                    // 获取字符串中的英文字母 [a-zA-Z]+
                    string GroupEnglishName = CRegex.GetText(tablesTypeInfo.Name, "[a-zA-Z]+", 0);

                    guidGroup.Add(tablesTypeInfo.GUID, string.Format("{0}{1}_", Const.TablePre, GroupEnglishName));
                    tablesTypeInfoList.Add(tablesTypeInfo);
                }
            }

            XmlNodeList xmlNodeLst2 = xmltableshelper.Read("datatype/dataitem");
            List<TablesInfo> tablesInfoList = new List<TablesInfo>();
            foreach (XmlNode xn1 in xmlNodeLst2)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 得到ConstantInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;

                // 配置的这个节点 BasicdataPath 不为空时才加载数据
                if (!string.IsNullOrEmpty(xnl0.Item(5).InnerText))
                {
                    TablesInfo tablesInfo = new TablesInfo();
                    tablesInfo.GUID = xe.GetAttribute("guid").ToString();
                    tablesInfo.Name = xnl0.Item(0).InnerText;
                    tablesInfo.ChineseName = xnl0.Item(1).InnerText;
                    tablesInfo.FunctionId = xnl0.Item(2).InnerText;
                    tablesInfo.TypeGuid = xnl0.Item(3).InnerText;
                    tablesInfo.Path = xnl0.Item(4).InnerText;
                    tablesInfo.BasicdataPath = xnl0.Item(5).InnerText;

                    tableGroup.Add(tablesInfo.Name, guidGroup[tablesInfo.TypeGuid]);
                    tablesInfoList.Add(tablesInfo);
                }
               
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
            FrmEditBasicDataGroupName dlg = new FrmEditBasicDataGroupName();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                NavBarGroup standardGroup = navBar.Groups.Add();
                standardGroup.Tag = dlg.Id;
                standardGroup.Caption = dlg.strGroupName;
            }
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
                MessageDxUtil.ShowError("请选择需要删除的分组");
                return;
            }
            // 需要重新读一下xml文件不存缓存里面没有
            xmltableshelper = new XmlHelper(@"XML\tables.xml");

            var objXmlDoc = xmltableshelper.GetXmlDoc();

            // 修改大的分类basicdata
            objXmlDoc.SelectSingleNode(string.Format("datatype/tabletype/item[@guid=\"{0}\"]", selectedGroup.Tag)).Attributes["basicdata"].InnerText = "0";

            // 再删除子节点本身
            XmlNodeList nodelst = objXmlDoc.SelectNodes(string.Format("datatype/dataitem/item[typeguid=\"{0}\"]/basicdatapath", selectedGroup.Tag));

            foreach (XmlNode node in nodelst)
            {
                if (!String.IsNullOrEmpty(node.InnerText))
                {
                    // 删除table文件
                    if (FileUtil.IsExistFile(node.InnerText))
                    {
                        FileUtil.DeleteFile(node.InnerText);
                    }

                    node.InnerText = "";
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
                MessageDxUtil.ShowError("请选择需要新增的分组");
                return;
            }


            FrmEditBasicDataItemName dlg = new FrmEditBasicDataItemName();

            dlg.strGuid = selectedGroup.Tag.ToString();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                NavBarItem item = new NavBarItem();
                item.Caption = dlg.strItemName;
                item.Tag = dlg.Id;
                item.Name = dlg.strItemName.Split(' ')[1].TrimEnd(')');
                item.Hint = dlg.strItemName.Split('(')[1].Split(' ')[0];
                item.LinkClicked += Item_LinkClicked;

                navBar.Items.Add(item);
                selectedGroup.ItemLinks.Add(item);

                if (!selectedGroup.Expanded)
                    selectedGroup.Expanded = true;
            }

            selectedGroup = null;
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

            xmltableshelper = new XmlHelper(@"XML\tables.xml");

            xmltableshelper.Replace(string.Format("datatype/dataitem/item[@guid=\"{0}\"]/basicdatapath", selectedLink.Item.Tag), string.Empty);
            xmltableshelper.Save(false);
            // 删除table文件
            if (FileUtil.IsExistFile(string.Format(@"XML\{0}.basicdata", selectedLink.Item.Name)))
            {
                FileUtil.DeleteFile(string.Format(@"XML\{0}.basicdata", selectedLink.Item.Name));
            }

            // 界面删除元素
            selectedGroup.ItemLinks.Remove(selectedLink);

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

            dt = new DataTable();
            Dictionary<string, TableFieldsInfo> dic = new Dictionary<string, TableFieldsInfo>();
            dt.Columns.Add("guid");
            TableFieldsInfo f = new TableFieldsInfo();
            f.FieldName = "guid";
            f.ChineseName = "GUID";
            f.FieldType = string.Empty;
            f.DictNo = 0;
            f.FieldInfo = string.Empty;
            f.IsNull = false;
            f.Remark = string.Empty;
            dic.Add("guid", f);
            // 添加英文表头

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

            XmlHelper xmltablefieldhelper = new XmlHelper(string.Format(@"XML\{0}.table", item.Name));
            XmlNodeList xmlfieldsLst = xmltablefieldhelper.Read(string.Format("datatype/fieldsinfo"));
            foreach (XmlNode xn1 in xmlfieldsLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                //tablefieldInfo.GUID = xe.GetAttribute("guid").ToString();
                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;

                dt.Columns.Add(xnl0.Item(0).InnerText);

                TableFieldsInfo field = new TableFieldsInfo();
                field.FieldName = xnl0.Item(0).InnerText;
                for (Int32 i = 0; i < stdFieldInfoList.Count; i++)
                {
                    if (string.Equals(stdFieldInfoList[i].Name, xnl0.Item(0).InnerText))
                    {
                        field.FieldName = stdFieldInfoList[i].Name;
                        field.ChineseName = stdFieldInfoList[i].ChineseName;
                        field.FieldType = stdFieldInfoList[i].DataType;
                        field.DictNo = stdFieldInfoList[i].DictNo;
                        field.FieldInfo = stdFieldInfoList[i].DictNameLst;
                        break;
                    }
                }

                field.IsNull = xnl0.Item(1).InnerText == "0" ? false : true;
                field.Remark = xnl0.Item(2).InnerText;

                dic.Add(xnl0.Item(0).InnerText, field);
            }

            tabbedView.BeginUpdate();
            control = new UserControl();
            xtraTabControl1 = new XtraTabControl(); ;
            xtraTabPageFields = new XtraTabPage();
            xtraTabPageSQLLook = new XtraTabPage();

            ((System.ComponentModel.ISupportInitialize)(xtraTabControl1)).BeginInit();
            xtraTabControl1.SuspendLayout();

            xtraTabControl1.Dock = DockStyle.Fill;
            xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            xtraTabControl1.Name = "xtraTabControl1";
            xtraTabControl1.SelectedTabPage = xtraTabPageFields;
            xtraTabControl1.Size = new System.Drawing.Size(1206, 556);
            xtraTabControl1.TabIndex = 0;
            xtraTabControl1.TabPages.AddRange(new XtraTabPage[] { xtraTabPageFields, xtraTabPageSQLLook });
 
            #region 字段及索引
            xtraTabPageFields.Name = "xtraTabPageFields";
            xtraTabPageFields.Text = "字段及索引";

            #region 字段表格

            gridControlFields = new DevExpress.XtraGrid.GridControl();
            gridViewFields = new DevExpress.XtraGrid.Views.Grid.GridView();

            ((System.ComponentModel.ISupportInitialize)(gridControlFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(gridViewFields)).BeginInit();

            gridControlFields.Dock = DockStyle.Fill;
            gridControlFields.Cursor = System.Windows.Forms.Cursors.Default;
            gridControlFields.MainView = gridViewFields;
            gridControlFields.Name = "gridControlFields";
            gridControlFields.Size = new System.Drawing.Size(981, 573);
            gridControlFields.TabIndex = 13;
            gridControlFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridViewFields});
            gridControlFields.ContextMenuStrip = contextMenuStripFields;

            gridViewFields.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightCyan;
            gridViewFields.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.LightCyan;
            gridViewFields.Appearance.FocusedRow.Options.UseBackColor = true;
            gridViewFields.Appearance.FocusedRow.Options.UseForeColor = true;
            gridViewFields.GridControl = gridControlFields;
            gridViewFields.Name = string.Format(@"XML\{0}.basicdata", item.Name);
            gridViewFields.ViewCaption = item.Hint;
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
            //gridViewFields.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;  
            gridViewFields.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewFields_CellValueChanged);
            gridViewFields.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewFields_ValidateRow);

            #region 初始化数据字段数据
            Int32 idx = 0;
            foreach (var key in dic.Keys)
            {
                DevExpress.XtraGrid.Columns.GridColumn gc = new DevExpress.XtraGrid.Columns.GridColumn();
                gc.Caption = dic[key].ChineseName;
                gc.Name = key;
                gc.Visible = true;
                gc.VisibleIndex = idx++;
                gc.FieldName = key;
                
                if (string.Equals(dic[key].FieldName, "guid"))
                {
                    gc.Visible = false;
                }

                // 假如存在数据字典，且数据字典为整型(FieldType包含Number)或者为字符型(FieldType为Char)
                if (dic[key].DictNo > 0 && (dic[key].FieldType.Contains("Number") || string.Equals(dic[key].FieldType, "Char")))
                {
                    RepositoryItemLookUpEdit rilu = new RepositoryItemLookUpEdit();
                    ((System.ComponentModel.ISupportInitialize)(rilu)).BeginInit();
                    rilu.PopupWidth = 400; //下拉框宽度  
                    rilu.NullText = "";//空时的值  
                    rilu.DropDownRows = 10;//下拉框行数  
                    rilu.ImmediatePopup = true;//输入值是否马上弹出窗体  
                    rilu.ValidateOnEnterKey = true;//回车确认  
                    rilu.SearchMode = SearchMode.AutoFilter;//自动过滤掉不需要显示的数据，可以根据需要变化  
                    rilu.TextEditStyle = TextEditStyles.Standard;//要使用户可以输入，这里须设为Standard  
                    rilu.AllowNullInput = DevExpress.Utils.DefaultBoolean.True; //可用Ctrl + Delete清空选择內容 
                    rilu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
                    //添加显示列  
                    rilu.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] { new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "数据字典值"), new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text", "数据字典名称") });
                    ((System.ComponentModel.ISupportInitialize)(rilu)).EndInit();

                    rilu.ValueMember = "Value";
                    rilu.DisplayMember = "Text";
                    // TODO
                    //rilu.EditValueChanged += rilu_EditValueChanged;

                    string[] dicdata = dic[key].FieldInfo.Split(Const.Comma.ToCharArray());
                    List<CListItem> datasource = new List<CListItem>();
                    foreach(var d in dicdata)
                    {
                        var c = d.Split(Const.Minus.ToCharArray());
                        datasource.Add(new CListItem(c[0].TrimStart(Const.WrapText.ToCharArray()), c[1]));
                    }
                    rilu.DataSource = datasource;
                    gridControlFields.RepositoryItems.Add(rilu);

                    gc.ColumnEdit = rilu;
                }

                // 假如存在数据字典，且数据字典为字符型 
                // 取值方法gridViewFields.GetRowCellValue(0, "EditorId")
                if (dic[key].DictNo > 0 && dic[key].FieldType.Contains("Char_"))
                {

                    RepositoryItemCheckedComboBoxEdit riccbe = new RepositoryItemCheckedComboBoxEdit();

                    ((System.ComponentModel.ISupportInitialize)(riccbe)).BeginInit();
                    riccbe.AutoHeight = false;
                    riccbe.Buttons.AddRange(new EditorButton[] { new EditorButton( ButtonPredefines.Combo)});
                    riccbe.Name = "riccbe";

                    string[] dicdata = dic[key].FieldInfo.Split(Const.Comma.ToCharArray());
                    foreach (var d in dicdata)
                    {
                        var c = d.Split(Const.Minus.ToCharArray());
                        riccbe.Items.Add(c[0].TrimStart(Const.WrapText.ToCharArray()), c[1]);

                    }
                    gridControlFields.RepositoryItems.Add(riccbe);

                    gc.ColumnEdit = riccbe;
                }

                // 如果是TableField字段 则下拉全部的字段
                if (string.Equals(dic[key].FieldName, "TableField"))
                {
                    RepositoryItemLookUpEdit rilu = new RepositoryItemLookUpEdit();
                    ((System.ComponentModel.ISupportInitialize)(rilu)).BeginInit();
                    rilu.PopupWidth = 400; //下拉框宽度  
                    rilu.NullText = "";//空时的值  
                    rilu.DropDownRows = 10;//下拉框行数  
                    rilu.ImmediatePopup = true;//输入值是否马上弹出窗体  
                    rilu.ValidateOnEnterKey = true;//回车确认  
                    rilu.SearchMode = SearchMode.AutoFilter;//自动过滤掉不需要显示的数据，可以根据需要变化  
                    rilu.TextEditStyle = TextEditStyles.Standard;//要使用户可以输入，这里须设为Standard  
                    rilu.AllowNullInput = DevExpress.Utils.DefaultBoolean.True; //可用Ctrl + Delete清空选择內容 
                    rilu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
                    //添加显示列  
                    rilu.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] { new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "字段"), new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text", "字段名称") });
                    ((System.ComponentModel.ISupportInitialize)(rilu)).EndInit();

                    rilu.ValueMember = "Value";
                    rilu.DisplayMember = "Text";
                    
                    //string[] dicdata = dic[key].FieldInfo.Split(Const.Comma.ToCharArray());
                    //List<CListItem> datasource = new List<CListItem>();
                    //foreach (var d in dicdata)
                    //{
                    //    var c = d.Split(Const.Minus.ToCharArray());
                    //    datasource.Add(new CListItem(c[0].TrimStart(Const.WrapText.ToCharArray()), c[1]));
                    //}
                    // 取出标准字段加载
                    List<CListItem> datasource = new List<CListItem>();
                    XmlHelper xmlhelper = new XmlHelper(@"XML\stdfield.xml");
                    XmlNodeList xmlNodeLst3 = xmlhelper.Read("datatype/dataitem");
                    foreach (XmlNode xn1 in xmlNodeLst3)
                    {
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;
                      
                        // 得到DataTypeInfo节点的所有子节点
                        XmlNodeList xnl0 = xe.ChildNodes;
                        datasource.Add(new CListItem(xnl0.Item(0).InnerText, xnl0.Item(0).InnerText + " " + xnl0.Item(1).InnerText));
                    }

                    rilu.DataSource = datasource;
                    gridControlFields.RepositoryItems.Add(rilu);

                    gc.ColumnEdit = rilu;
                }

                gridViewFields.Columns.Add(gc);
            }

            /*XElement doc = XElement.Load(string.Format(@"XML\{0}.basicdata", item.Name));
            foreach (XElement item2 in doc.Descendants("item"))//得到每一个Person节点,得到这个节点再取他的Name的这个节点的值
            {
                Console.WriteLine("姓名：{0} 年龄：{1}", item2.Element("gid").Value, item2.Element("id").Value);//Person的节点的下得节点为Name的
            }*/

            XmlHelper xmltabledatahelper = new XmlHelper(string.Format(@"XML\{0}.basicdata", item.Name));

            XmlNodeList xmlNodeLst = xmltabledatahelper.Read("datatype/dataitem");

            foreach (XmlNode xn1 in xmlNodeLst)
            {
                DataRow dr = dt.NewRow();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (xe.Attributes[dt.Columns[i].ColumnName.ToLower()] != null)
                    {
                        dr[dt.Columns[i].ColumnName] = xe.GetAttribute(dt.Columns[i].ColumnName.ToLower()).ToString();
                    }
                    else
                    {
                        xe.SetAttribute(dt.Columns[i].ColumnName.ToLower(), string.Empty);
                    }
                }
                xmltabledatahelper.Save(false);
                dt.Rows.Add(dr);

                // 根据属性去反查是否存在不存在则删除
                List<String> tmp = new List<string>();
                foreach (XmlAttribute a in xe.Attributes)
                {
                    if (dt.Columns[a.Name] == null && !string.Equals(a.Name, "guid"))
                        tmp.Add(a.Name);
                }
                foreach(var s in tmp)
                {
                    xe.RemoveAttribute(s);
                }
                xmltabledatahelper.Save(false);
            }

            #endregion
            ((System.ComponentModel.ISupportInitialize)(gridControlFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(gridViewFields)).EndInit();
            gridControlFields.DataSource = dt;
            #endregion

            xtraTabPageFields.Controls.Add(gridControlFields);

            #endregion

            #region SQL预览
            xtraTabPageSQLLook.Name = "xtraTabPageSQLLook";
            xtraTabPageSQLLook.Text = "SQL预览";

            richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
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
            richEditControl.Views.SimpleView.Padding = new System.Windows.Forms.Padding(55, 4, 0, 0);
            richEditControl.Views.DraftView.Padding = new System.Windows.Forms.Padding(55, 4, 0, 0);
            richEditControl.Views.SimpleView.AllowDisplayLineNumbers = true;
            richEditControl.Views.DraftView.AllowDisplayLineNumbers = true;
            richEditControl.Document.Sections[0].LineNumbering.Start = 1;
            richEditControl.Document.Sections[0].LineNumbering.CountBy = 2;
            richEditControl.Document.Sections[0].LineNumbering.RestartType = DevExpress.XtraRichEdit.API.Native.LineNumberingRestart.Continuous;
            richEditControl.Document.CharacterStyles["Line Number"].FontName = "Courier";
            richEditControl.Document.CharacterStyles["Line Number"].FontSize = 10;
            richEditControl.Document.CharacterStyles["Line Number"].ForeColor = Color.DarkGray;
            richEditControl.Document.CharacterStyles["Line Number"].Bold = true;

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

            xtraTabControl1.SelectedPageChanged += (sender1, e1) => {
                if (FileUtil.IsExistFile("SQL.tmp"))
                {
                    FileUtil.DeleteFile("SQL.tmp");
                }
                FileUtil.CreateFile("SQL.tmp");

                #region 根据项目属性获取数据类型
                XmlHelper xmlprojectthelper = new XmlHelper(@"XML\project.xml");
                XmlNodeList xmlprejectNodeLst = xmlprojectthelper.Read("datatype");

                if (xmlprejectNodeLst.Count == 0)
                    return;

                XmlNode xn1project = xmlprejectNodeLst[0];

                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xeproject = (XmlElement)xn1project;

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0project = xeproject.ChildNodes;

                string dbType = xnl0project.Item(4).InnerText;
                #endregion

                #region 先读取datatype.xml 在读取defaulttype.xml 然后Linq 查询保存到数据字典dic中
                XmlHelper xmldatatypehelper = new XmlHelper(@"XML\datatype.xml");
                XmlNodeList xmldatatypeNodeLst = xmldatatypehelper.Read("datatype");
                List<DataTypeInfo> dataTypeInfoList = new List<DataTypeInfo>();
                foreach (XmlNode xn1 in xmldatatypeNodeLst)
                {
                    DataTypeInfo dataTypeInfo = new DataTypeInfo();
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;
                    // 得到Type和ISBN两个属性的属性值
                    dataTypeInfo.GUID = xe.GetAttribute("guid").ToString();

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;
                    dataTypeInfo.Name = xnl0.Item(0).InnerText;
                    dataTypeInfo.StdType = xnl0.Item(2).InnerText;
                    dataTypeInfo.Length = xnl0.Item(3).InnerText;
                    dataTypeInfo.Precision = xnl0.Item(4).InnerText;

                    dataTypeInfoList.Add(dataTypeInfo);
                }

                XmlHelper defaulttypexmlHelper = new XmlHelper(@"XML\defaulttype.xml");
                XmlNodeList defaulttypexmlNodeLst = defaulttypexmlHelper.Read("datatype");
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (var dataTypeInfo in dataTypeInfoList)
                {
                    foreach (XmlNode xn1 in defaulttypexmlNodeLst)
                    {
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;
                        // 得到DataTypeInfo节点的所有子节点
                        XmlNodeList xnl0 = xe.ChildNodes;
                        string value = string.Empty;

                        if (dbType == "Oracle")
                            value = xnl0.Item(2).InnerText;
                        else if (dbType == "Mysql")
                            value = xnl0.Item(3).InnerText;
                        else if (dbType == "DB2")
                            value = xnl0.Item(4).InnerText;
                        else if (dbType == "SqlServer")
                            value = xnl0.Item(5).InnerText;
                        else if (dbType == "Sqlite")
                            value = xnl0.Item(6).InnerText;
                        else if (dbType == "Access")
                            value = xnl0.Item(7).InnerText;

                        // 找到匹配记录
                        if (dataTypeInfo.StdType == xnl0.Item(0).InnerText)
                        {
                            if (value.Contains("$L"))
                            {
                                if (String.Empty == dataTypeInfo.Length)
                                    value = value.Replace("$L", "0");
                                else
                                    value = value.Replace("$L", dataTypeInfo.Length);

                            }
                            if (value.Contains("$P"))
                            {
                                if (String.Empty == dataTypeInfo.Precision)
                                    value = value.Replace("$P", "0");
                                else
                                    value = value.Replace("$P", dataTypeInfo.Precision);
                            }
                            dict.Add(dataTypeInfo.Name, value);
                        }
                    }
                }
                #endregion
                if (dt != null)
                {
                    string englishTable = gridViewFields.Name.Replace(@"XML\", "").Replace(@".basicdata", "");

                    // 删除不显示的字段
                    DataTable dtSql = dt.Copy();
                    List<string> delField = new List<string>();
                    foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridViewFields.Columns)
                    {
                        foreach (DataColumn dtcolumn in dtSql.Columns)
                        {
                            if (string.Equals(column.FieldName, dtcolumn.ColumnName, StringComparison.CurrentCultureIgnoreCase) && column.Visible == false)
                            {
                                delField.Add(column.FieldName);
                            }
                        }
                    }

                    foreach (string str in delField)
                    {
                        dtSql.Columns.Remove(str);
                    }

                    FileUtil.AppendText(@"SQL.tmp", JCodes.Framework.Common.Proj.SqlOperate.initTableDataInfo(dbType, tableGroup[englishTable] + englishTable, gridViewFields.ViewCaption, dtSql), Encoding.Default);
                }

                richEditControl.LoadDocument(@"SQL.tmp", DocumentFormat.PlainText);
                richEditControl.ReplaceService<ISyntaxHighlightService>(new CustomSyntaxHighlightService(richEditControl.Document));//高亮显示  
            };
            xtraTabPageSQLLook.Controls.Add(richEditControl);
            #endregion

            control.Controls.Add(xtraTabControl1);

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
                dictInfo.Id = Convert.ToInt32(xnl0.Item(0).InnerText);
                dictInfo.Pid = Convert.ToInt32(xnl0.Item(1).InnerText);
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
        /// 新增数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_AddField_Click(object sender, EventArgs e)
        {
            string guid = System.Guid.NewGuid().ToString();
            XmlHelper xmldatahelper = new XmlHelper(gridViewFields.Name);
            var objXmlDoc = xmldatahelper.GetXmlDoc();
            XmlNode objNode = objXmlDoc.SelectSingleNode("datatype/dataitem");
            XmlElement objElement = objXmlDoc.CreateElement("item");
            DataRow dr = dt.NewRow();
            
            objElement.SetAttribute("guid", guid);
            dr["guid"] = guid;

            foreach (DataColumn s in dt.Columns)
            {
                if (string.Equals(s.ColumnName.ToLower(), "guid"))
                    continue;

                objElement.SetAttribute(s.ColumnName.ToLower(), string.Empty);
            }
            objNode.AppendChild(objElement);
            xmldatahelper.Save();

            dt.Rows.Add(dr);
            gridControlFields.RefreshDataSource();
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_DelField_Click(object sender, EventArgs e)
        {
            // 20170824 如果是最后一行空行则不再继续操作
            if (gridViewFields.GetFocusedRow() == null || string.IsNullOrEmpty(gridViewFields.GetFocusedDataRow()[0].ToString()))
                return;
            XmlHelper xmldatahelper = new XmlHelper(gridViewFields.Name);
            xmldatahelper.DeleteByPathNode("datatype/dataitem/item[@guid=\"" + gridViewFields.GetFocusedDataRow()[0].ToString() + "\"]");
            xmldatahelper.Save();

            dt.Rows.RemoveAt(gridViewFields.FocusedRowHandle);
            gridControlFields.RefreshDataSource();
        }

        private void gridViewFields_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string rowGuid = gridViewFields.GetDataRow(e.RowHandle)[0].ToString();

            if (!ValidateUtil.IsGuid(rowGuid))
            {
                MessageDxUtil.ShowError("修改数据失败，此次操作不生效");
                return;
            }

            XmlHelper xmldatahelper = new XmlHelper(gridViewFields.Name);
            xmldatahelper.GetXmlDoc().SelectSingleNode("datatype/dataitem/item[@guid=\"" + rowGuid + "\"]").Attributes[e.Column.FieldName.ToLower()].Value = e.Value.ToString();
            xmldatahelper.Save();

            dt.Rows[e.RowHandle][e.Column.FieldName.ToLower()] = e.Value.ToString();
        }

        private void gridViewFields_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            // 查询是否存在2个键值的数据
            /*List<TableFieldsInfo> lsttableFieldsInfo = gridViewFields.DataSource as List<TableFieldsInfo>;

            // 查找重复的Name的值 && 清楚原先的错误信息

            List<String> tmpName = new List<string>();
            List<String> lstName = new List<string>();
            foreach (TableFieldsInfo tableFieldsInfo in lsttableFieldsInfo)
            {
                if (string.IsNullOrEmpty(tableFieldsInfo.GUID))
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
                if (string.IsNullOrEmpty(tableFieldsInfo.GUID))
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
            }*/
        }
    }
}
