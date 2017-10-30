using System;
using System.Windows.Forms;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;
using System.Xml;
using System.Collections.Generic;
using JCodes.Framework.AddIn.Basic;
using System.Text;
using JCodes.Framework.Common.Proj;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmDictionary : BaseDock
    {
        private XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");

        private XmlHelper xmlprojectthelper = new XmlHelper(@"XML\project.xml");

        private List<DictInfo> dictTypeInfoList = null;

        private ProjectInfo projectInfo = null;

        public FrmDictionary()
        {
            InitializeComponent();
        }

        private void FrmDictionary_Load(object sender, EventArgs e)
        {
            ReadXMLData();

            InitTreeView();
            this.lblDictType.Text = "";

            BindData();

            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.BestFitColumnWith = false;
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
        }

        #region 分类数据

        /// <summary>
        /// 读取xml数据
        /// </summary>
        private void ReadXMLData()
        {
            #region 加载数据字典大项
            XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/dataitem");
            dictTypeInfoList = new List<DictInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                DictInfo dictInfo = new DictInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
             
                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                dictInfo.ID = Convert.ToInt32(xnl0.Item(0).InnerText);
                dictInfo.PID = Convert.ToInt32(xnl0.Item(1).InnerText);
                dictInfo.Name = xnl0.Item(2).InnerText;
                dictInfo.Remark = xnl0.Item(3).InnerText;

                dictTypeInfoList.Add(dictInfo);
            }
            #endregion

            #region 读取项目数据
            XmlNodeList xmlprejectNodeLst = xmlprojectthelper.Read("datatype");

            if (xmlprejectNodeLst.Count == 0)
                return;

            XmlNode xn1project = xmlprejectNodeLst[0];
            projectInfo = new ProjectInfo();

            // 将节点转换为元素，便于得到节点的属性值
            XmlElement xeproject = (XmlElement)xn1project;
            // 得到Type和ISBN两个属性的属性值
            projectInfo.GUID = xeproject.GetAttribute("guid").ToString();
            // 得到DataTypeInfo节点的所有子节点
            XmlNodeList xnl0project = xeproject.ChildNodes;
            projectInfo.Name = xnl0project.Item(0).InnerText;
            projectInfo.Version = xnl0project.Item(1).InnerText;
            projectInfo.Contract =  xnl0project.Item(2).InnerText;
            projectInfo.Remark =  xnl0project.Item(3).InnerText;
            projectInfo.DbType = xnl0project.Item(4).InnerText;
            projectInfo.DicttypeTable = xnl0project.Item(5).InnerText;
            projectInfo.DictdataTable = xnl0project.Item(6).InnerText;
            projectInfo.ErrTable = xnl0project.Item(7).InnerText;
            projectInfo.LastTime = xnl0project.Item(8).InnerText;
            projectInfo.OutputPath = xnl0project.Item(9).InnerText;

            #endregion

        }

        private void InitTreeView()
        {
            this.treeView1.Nodes.Clear();
            this.treeView1.BeginUpdate();
            // 只支持2级分级
            if (dictTypeInfoList != null)
            {
                List<DictInfo> lst = dictTypeInfoList.FindAll(new Predicate<DictInfo>(dictinfo => dictinfo.PID == -1));
                lst.Sort();
                foreach (var dictInfo in lst)
                {
                    TreeNode node = new TreeNode(string.Format("{0}-{1}", dictInfo.ID, dictInfo.Name), 1, 1);
                    node.Tag = dictInfo.ID;
                    this.treeView1.Nodes.Add(node);

                    List<DictInfo> lstNode = dictTypeInfoList.FindAll(new Predicate<DictInfo>(one => one.PID == dictInfo.ID));
                    foreach (var nodeInfo in lstNode)
                    {
                        TreeNode childnode = new TreeNode(string.Format("{0}-{1}", nodeInfo.ID, nodeInfo.Name), 1, 1);
                        childnode.Tag = nodeInfo.ID;
                        node.Nodes.Add(childnode);
                    }
                }
            }

            this.treeView1.EndUpdate();
            this.treeView1.ExpandAll();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_Refresh_Click(object sender, EventArgs e)
        {
            xmldicthelper = new XmlHelper(@"XML\dict.xml");

            ReadXMLData();

            InitTreeView();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.lblDictType.Text = e.Node.Text;
                this.lblDictType.Tag = e.Node.Tag;
                BindData();
            }
        }

        /// <summary>
        /// 绑定明细数据
        /// </summary>
        private void BindData()
        {
            #region 添加别名解析
            this.winGridViewPager1.DisplayColumns = "Value,Name,Seq,Remark,EditTime";
            this.winGridViewPager1.AddColumnAlias("ID", "编号");
            this.winGridViewPager1.AddColumnAlias("DictType_ID", "字典大类");
            this.winGridViewPager1.AddColumnAlias("Name", "项目名称");
            this.winGridViewPager1.AddColumnAlias("Value", "项目值");
            this.winGridViewPager1.AddColumnAlias("Seq", "字典排序");
            this.winGridViewPager1.AddColumnAlias("Remark", "备注");
            this.winGridViewPager1.AddColumnAlias("Editor", "修改用户");
            this.winGridViewPager1.AddColumnAlias("EditTime", "更新日期");
            #endregion

            if (this.lblDictType.Tag != null)
            {
                // xmldicthelper.Read("datatype/item[id=\"4\"]/subdic")
                XmlNodeList xmlNodeLst = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic", this.lblDictType.Tag));

                List<DictDetailInfo> dictDetailInfoList = new List<DictDetailInfo>();
                foreach (XmlNode xn1 in xmlNodeLst)
                {
                    DictDetailInfo dictDetailInfo = new DictDetailInfo();
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;
                    dictDetailInfo.Value = Convert.ToInt32(xnl0.Item(0).InnerText);
                    dictDetailInfo.Name = xnl0.Item(1).InnerText;
                    dictDetailInfo.Seq = Convert.ToInt32(xnl0.Item(2).InnerText);
                    dictDetailInfo.Remark = xnl0.Item(3).InnerText;

                    dictDetailInfoList.Add(dictDetailInfo);
                }

                dictDetailInfoList.Sort();
                winGridViewPager1.DataSource = dictDetailInfoList;
            }
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_AddType_Click(object sender, EventArgs e)
        {
            FrmEditDictType dlg = new FrmEditDictType();
            dlg.PID = GetParentNodeIndex();
            dlg.OnDataSaved += new EventHandler(dlg_OnDataTreeSaved);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                menu_Refresh_Click(null, null);
            }
        }

        void dlg_OnDataTreeSaved(object sender, EventArgs e)
        {
            menu_Refresh_Click(null, null);
        }

        private Int32 GetParentNodeIndex()
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null)
            {
                return Convert.ToInt32(node.Tag);
            }
            return -1;
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_EditType_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            Int32 tmpPID = 0;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                Int32 typeId = Convert.ToInt32(selectedNode.Tag);

                #region 加载数据字典大项
                XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/dataitem");

                for (Int32 i = 0; i < xmlNodeLst.Count; i++)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xmlNodeLst[i];

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;

                    if (typeId == Convert.ToInt32(xnl0.Item(0).InnerText))
                    {
                        tmpPID = Convert.ToInt32(xnl0.Item(1).InnerText);
                        break;
                    }
                }
                #endregion

                if (tmpPID != 0)
                {
                    FrmEditDictType dlg = new FrmEditDictType();
                    dlg.ID = typeId.ToString();
                    dlg.PID = tmpPID;
                    dlg.OnDataSaved += new EventHandler(dlg_OnDataTreeSaved);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        menu_Refresh_Click(null, null);
                    }
                }
            }
        }

        /// <summary>
        /// 删除大象节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void menu_DeleteType_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                Int32 typeId = Convert.ToInt32(selectedNode.Tag);

                string message = string.Format("您确定要删除节点：{0}，删除将子节点及其数据均一并删除，请谨慎操作。", selectedNode.Text);
                if (MessageDxUtil.ShowYesNoAndWarning(message) == DialogResult.Yes)
                {
                    try
                    {
                        // 假如说是PID=-1的父节点，则先一起删除其子节点
                        xmldicthelper = new XmlHelper(@"XML\dict.xml");
                        XmlNodeList xmlNodeLst = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]", selectedNode.Tag));
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xmlNodeLst[0];
                        Int32 Pid = Convert.ToInt32(xmlNodeLst[1].ChildNodes.Item(0).InnerText);
                        if (Pid == -1)
                        {
                            while (true)
                            {
                                try {
                                    // 再删除子节点本身
                                    xmldicthelper.DeleteByPathNode(string.Format("datatype/dataitem/item[pid=\"{0}\"]", selectedNode.Tag));
                                }
                                catch (Exception ex)
                                {
                                    break;
                                }
                            }
                        }

                        // 再删除子节点本身
                        xmldicthelper.DeleteByPathNode(string.Format("datatype/dataitem/item[id=\"{0}\"]", selectedNode.Tag));
                        xmldicthelper.Save(false);

                        menu_Refresh_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmDictionary));
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            menu_EditType_Click(null, null);
        }
        #endregion

        #region 分类数据字典数据
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            if (this.lblDictType.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请选择指定的字典大类，然后添加！");
                return;
            }

            FrmEditDictData dlg = new FrmEditDictData();
            dlg.txtDictType.Text = this.lblDictType.Text;
            dlg.txtDictType.Tag = this.lblDictType.Tag;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                winGridViewPager1_OnRefresh(null, null);
            }
        }

        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("Value");

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEditDictData dlg = new FrmEditDictData();
                dlg.txtDictType.Text = this.lblDictType.Text;
                dlg.txtDictType.Tag = this.lblDictType.Tag;
                dlg.ID = ID;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    winGridViewPager1_OnRefresh(null, null);
                }
            }
        }

        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "Value");

                // 再删除子节点本身
                xmldicthelper.DeleteByPathNode(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic/item[value=\"{1}\"]", lblDictType.Tag, ID));
                xmldicthelper.Save(false);
                
            }
            winGridViewPager1_OnRefresh(null, null);
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            winGridViewPager1_OnRefresh(null, null);
        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                this.winGridViewPager1.gridView1.Columns["Name"].Width = 200;
                this.winGridViewPager1.gridView1.Columns["Value"].Width = 200;
            }
        }

        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            xmldicthelper = new XmlHelper(@"XML\dict.xml");

            BindData();
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                this.menu_EditType.Enabled = false;
            }
            else
            {
                this.menu_EditType.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnAddNew(this.winGridViewPager1.gridView1, null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnEditSelected(this.winGridViewPager1.gridView1, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnDeleteSelected(this.winGridViewPager1.gridView1, null);
        }

        #endregion

        /// <summary>
        /// 导出到Excel表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 根据配置导出SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateSQL_Click(object sender, EventArgs e)
        {
            if (projectInfo != null)
            {
                if (string.IsNullOrEmpty(projectInfo.OutputPath))
                {
                    MessageDxUtil.ShowError("请在项目菜单中配置导出脚本路径");
                    return;
                }

                if (!DirectoryUtil.IsExistDirectory(projectInfo.OutputPath))
                {
                    MessageDxUtil.ShowError(string.Format("配置的路径{0}在系统中不存在，请在项目菜单中配置导出脚本路径", projectInfo.OutputPath));
                    return;
                }

                // 操控进度条
                var progressBar = (this.MdiParent as MainForm).progressBar;
                string fileName = "Dict.Sql";
                string sqlfile = projectInfo.OutputPath + "\\" + fileName;
                StringBuilder sb = new StringBuilder();

                switch (projectInfo.DbType)
                {
                    case "Oracle": break;
                    case "Mysql": break;
                    case "DB2": break;
                    case "SqlServer":
                        {
                            #region 对于SqlServer 脚本的处理
                            if (FileUtil.IsExistFile(sqlfile))
                                FileUtil.DeleteFile(sqlfile);
                            List<ModRecordInfo> lst = new List<ModRecordInfo>();
                            lst.Add(new ModRecordInfo() { ModDate = DateTimeHelper.GetServerDateTime2(), ModVersion = new Version("1.0.0.0").ToString(), ModOrderId = "M1234567890", Proposer = "测试递申人", Programmer = "测试修改人", ModContent = "这里是修改内容", ModReason = "修改原因", Remark = "备注信息" });

                            // 创建文件
                            FileUtil.CreateFile(sqlfile);
                            // 添加文本信息
                            FileUtil.AppendText(sqlfile, SqlGenerate.printHeaderInfo(fileName, "V1.3.2.1111", "Jimmy", "2017-09-01 13:22:11", "备注信息", "2017-07-01", lst), Encoding.Default);

                            FileUtil.AppendText(sqlfile, SqlGenerate.printInitInfo("T_Basic_DictType"), Encoding.Default);

                            FileUtil.AppendText(sqlfile, SqlGenerate.printInitInfo("T_Basic_DictData"), Encoding.Default);

                            // TODO 这里需要等表结果完成在做


                            break;
                            #endregion
                        }
                       
                    case "Sqlite": break;
                    case "Access": break;
                }
            }
        }

        /// <summary>
        /// 切换TabPage
        /// 如果切换到修改历史记录在加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            // 如果是修改历史记录则加载数据
            if (string.Equals(e.Page.Name, "tabpageModrecord"))
            {
                InitGridView();

                BindRecord();
            }
        }

        #region 绑定历史记录
        /// <summary>
        /// 绑定历史记录
        /// </summary>
        private void BindRecord() 
        {
            #region 添加别名解析
            this.gridViewModrecord.DisplayColumns = "ModDate,ModVersion,ModOrderId,Proposer,Programmer,ModContent,ModReason,Remark";
            this.gridViewModrecord.AddColumnAlias("GUID", "GUID");
            this.gridViewModrecord.AddColumnAlias("ModDate", "修改日期");
            this.gridViewModrecord.AddColumnAlias("ModVersion", "修改版本");
            this.gridViewModrecord.AddColumnAlias("ModOrderId", "修改单号");
            this.gridViewModrecord.AddColumnAlias("Proposer", "申请人");
            this.gridViewModrecord.AddColumnAlias("Programmer", "修改人");
            this.gridViewModrecord.AddColumnAlias("ModContent", "修改内容");
            this.gridViewModrecord.AddColumnAlias("ModReason", "修改原因");
            this.gridViewModrecord.AddColumnAlias("Remark", "备注");
            #endregion

            XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/histories");
            List<ModRecordInfo> modRecordInfoLst = new List<ModRecordInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                ModRecordInfo modRecordInfo = new ModRecordInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                modRecordInfo.GUID = xe.GetAttribute("guid").ToString();
                modRecordInfo.ModDate = Convert.ToDateTime(xe.GetAttribute("moddate"));
                modRecordInfo.ModVersion = new Version(xe.GetAttribute("modversion")).ToString();
                modRecordInfo.ModOrderId = xe.GetAttribute("modorderId");
                modRecordInfo.Proposer = xe.GetAttribute("proposer");
                modRecordInfo.Programmer = xe.GetAttribute("programmer");
                modRecordInfo.ModContent = xe.GetAttribute("modcontent");
                modRecordInfo.ModReason = xe.GetAttribute("modreason");
                modRecordInfo.Remark = xe.GetAttribute("remark");
                modRecordInfoLst.Add(modRecordInfo);
            }

            modRecordInfoLst.Sort();

            gridViewModrecord.DataSource = modRecordInfoLst;
            gridViewModrecord.gridView1.Columns["GUID"].Visible = false;
            gridViewModrecord.gridView1.Columns["ModDate"].ColumnEdit = repositoryItemDateEdit1;
        }

        private void InitGridView()
        {
            this.gridViewModrecord.GridView1.OptionsCustomization.AllowSort = false;
            this.gridViewModrecord.GridView1.OptionsBehavior.ReadOnly = false;
            this.gridViewModrecord.OnAddNew += new EventHandler(gridViewModrecord_OnAddNew);
            this.gridViewModrecord.OnDeleteSelected += new EventHandler(gridViewModrecord_OnDeleteSelected);
            this.gridViewModrecord.OnRefresh += new EventHandler(gridViewModrecord_OnRefresh);
            this.gridViewModrecord.GridView1.DoubleClick += new EventHandler(gridViewModrecordgridView1_DoubleClick);
            this.gridViewModrecord.GridView1.BeforeLeaveRow += gridViewModrecordgridView1_BeforeLeaveRow;
            this.gridViewModrecord.GridView1.CellValueChanged += gridViewModrecordgridView1_CellValueChanged;
            this.gridViewModrecord.GridView1.ValidatingEditor += gridViewModrecordgridView1_ValidatingEditor;
        }

        private void gridViewModrecordgridView1_DoubleClick(object sender, EventArgs e)
        {
            this.gridViewModrecord.GridView1.OptionsBehavior.Editable = true;
        }

        private void gridViewModrecordgridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            this.gridViewModrecord.GridView1.OptionsBehavior.Editable = false;
        }

        private void gridViewModrecordgridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var modrecordInfo = this.gridViewModrecord.GridView1.GetRow(e.RowHandle) as ModRecordInfo;
            string idx = string.Empty;

            this.gridViewModrecord.AddColumnAlias("GUID", "GUID");
            this.gridViewModrecord.AddColumnAlias("ModDate", "修改日期");
            this.gridViewModrecord.AddColumnAlias("ModVersion", "修改版本");
            this.gridViewModrecord.AddColumnAlias("ModOrderId", "修改单号");
            this.gridViewModrecord.AddColumnAlias("Proposer", "申请人");
            this.gridViewModrecord.AddColumnAlias("Programmer", "修改人");
            this.gridViewModrecord.AddColumnAlias("ModContent", "修改内容");
            this.gridViewModrecord.AddColumnAlias("ModReason", "修改原因");
            this.gridViewModrecord.AddColumnAlias("Remark", "备注");

            switch (e.Column.ToString())
            {
                case "GUID":
                    idx = "guid";
                    break;
                case "修改日期":
                    idx = "moddate";
                    break;
                case "修改版本":
                    idx = "modversion";
                    break;
                case "修改单号":
                    idx = "modorderId";
                    break;
                case "申请人":
                    idx = "proposer";
                    break;
                case "修改人":
                    idx = "programmer";
                    break;
                case "修改内容":
                    idx = "modcontent";
                    break;
                case "修改原因":
                    idx = "modreason";
                    break;
                case "备注":
                    idx = "remark";
                    break;
            }

            if (string.IsNullOrEmpty(idx))
                return;

            XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/histories");
            foreach(XmlElement element in xmlNodeLst)
            {
                if (string.Equals(element.Attributes["guid"].Value, modrecordInfo.GUID))
                {
                    if (string.Equals(e.Column.ColumnType.Name, "DateTime"))
                    {
                        element.Attributes[idx].Value = Convert.ToDateTime(e.Value).ToShortDateString() + " " + DateTimeHelper.GetServerDateTime2().ToLongTimeString();
                    }
                    else
                    {
                        element.Attributes[idx].Value = e.Value.ToString();
                    }
                }
            }
            xmldicthelper.Save(false);
        }

        private void gridViewModrecordgridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            #region 修改版本号
            if (this.gridViewModrecord.GridView1.FocusedColumn.FieldName == "ModVersion")
            {
                try
                {
                    new Version(e.Value.ToString().Trim());
                }
                catch (Exception ex)
                {
                    e.Valid = false;
                    e.ErrorText = ex.Message;
                }
            }
            #endregion

            #region 修改单号
            if (this.gridViewModrecord.GridView1.FocusedColumn.FieldName == "ModOrderId")
            {
                if (e.Value.ToString().Length > 11)
                {
                    e.Valid = false;
                    e.ErrorText = "修改单号最大支持长度11位，请确认后修改";
                }
            }
            #endregion

            #region 申请人
            if (this.gridViewModrecord.GridView1.FocusedColumn.FieldName == "Proposer")
            {
                if (System.Text.Encoding.Default.GetBytes(e.Value.ToString().ToCharArray()).Length > 10)
                {
                    e.Valid = false;
                    e.ErrorText = "申请人长度最大支持10位英文或者5位中文，请确认后修改";
                }
            }
            #endregion

            #region 修改人
            if (this.gridViewModrecord.GridView1.FocusedColumn.FieldName == "Programmer")
            {
                if (System.Text.Encoding.Default.GetBytes(e.Value.ToString().ToCharArray()).Length > 10)
                {
                    e.Valid = false;
                    e.ErrorText = "修改人长度最大支持10位英文或者5位中文，请确认后修改";
                }
            }
            #endregion

            #region 修改内容
            if (this.gridViewModrecord.GridView1.FocusedColumn.FieldName == "ModContent")
            {
                if (System.Text.Encoding.Default.GetBytes(e.Value.ToString().ToCharArray()).Length > 10)
                {
                    e.Valid = false;
                    e.ErrorText = "修改内容长度最大支持40位英文或者20位中文，请确认后修改";
                }
            }
            #endregion

            #region 修改原因
            if (this.gridViewModrecord.GridView1.FocusedColumn.FieldName == "ModReason")
            {
                if (System.Text.Encoding.Default.GetBytes(e.Value.ToString().ToCharArray()).Length > 10)
                {
                    e.Valid = false;
                    e.ErrorText = "修改原因长度最大支持40位英文或者20位中文，请确认后修改";
                }
            }
            #endregion

            #region 备注
            if (this.gridViewModrecord.GridView1.FocusedColumn.FieldName == "Remark")
            {
                if (System.Text.Encoding.Default.GetBytes(e.Value.ToString().ToCharArray()).Length > 10)
                {
                    e.Valid = false;
                    e.ErrorText = "备注长度最大支持40位英文或者20位中文，请确认后修改";
                }
            }
            #endregion
        }

        private void gridViewModrecord_OnRefresh(object sender, EventArgs e)
        {
            BindRecord();
        }

        private void gridViewModrecord_OnAddNew(object sender, EventArgs e)
        {
            var objXmlDoc = xmldicthelper.GetXmlDoc();
            XmlNode objNode = objXmlDoc.SelectSingleNode("datatype/histories");

            ModRecordInfo modrecoreInfo = new ModRecordInfo();
            modrecoreInfo.GUID = Guid.NewGuid().ToString();
            modrecoreInfo.ModDate = DateTimeHelper.GetServerDateTime2();
            // 没有修改版本记录则默认使用1.0.0.0 开始
            if (objNode.LastChild == null)
                modrecoreInfo.ModVersion = new Version("1.0.0.0").ToString();
            else
            {
                Version lastVersion = new Version(objNode.LastChild.Attributes["modversion"].Value);
                modrecoreInfo.ModVersion = new Version(string.Format("{0}.{1}.{2}.{3}", lastVersion.Major, lastVersion.Minor, lastVersion.Build, lastVersion.Revision + 1)).ToString();
            }
            modrecoreInfo.ModOrderId = string.Empty;
            modrecoreInfo.Proposer = string.Empty;
            modrecoreInfo.Programmer = string.Empty;
            modrecoreInfo.ModContent = string.Empty;
            modrecoreInfo.ModReason = string.Empty;
            modrecoreInfo.Remark = string.Empty;

            XmlElement objElement = objXmlDoc.CreateElement("item");
            objElement.SetAttribute("guid", modrecoreInfo.GUID);
            objElement.SetAttribute("moddate", modrecoreInfo.ModDate.ToString("yyyy-MM-dd HH:mm:ss"));
            objElement.SetAttribute("modversion", modrecoreInfo.ModVersion.ToString());
            objElement.SetAttribute("modorderId", modrecoreInfo.ModOrderId);
            objElement.SetAttribute("proposer", modrecoreInfo.Proposer);
            objElement.SetAttribute("programmer", modrecoreInfo.Programmer);
            objElement.SetAttribute("modcontent", modrecoreInfo.ModContent);
            objElement.SetAttribute("modreason", modrecoreInfo.ModReason);
            objElement.SetAttribute("remark", modrecoreInfo.Remark);
            
            objNode.AppendChild(objElement);
            xmldicthelper.Save(false);

            (gridViewModrecord.gridView1.DataSource as List<ModRecordInfo>).Insert(0, modrecoreInfo);
            gridViewModrecord.gridView1.RefreshData();
        }

        private void gridViewModrecord_OnDeleteSelected(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.gridViewModrecord.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string guid = this.gridViewModrecord.GridView1.GetRowCellDisplayText(iRow, "GUID");

                // 再删除子节点本身
                xmldicthelper.DeleteByPathNode(string.Format("datatype/histories/item[@guid=\"{0}\"]", guid));
                xmldicthelper.Save(false);
            }
            gridViewModrecord_OnRefresh(null, null);
        }
        #endregion
    }
}