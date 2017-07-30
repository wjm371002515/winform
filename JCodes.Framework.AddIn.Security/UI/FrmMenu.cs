using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Entity;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.Controls;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmMenu : BaseDock
    {
        public FrmMenu()
        {
            InitializeComponent();

            InitDictItem();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);

            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridViewPager1.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度
                winGridViewPager1.gridView1.SetGridColumWidth("Name", 150);
                winGridViewPager1.gridView1.SetGridColumWidth("Icon", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("Seq", 80);
                winGridViewPager1.gridView1.SetGridColumWidth("Visible", 80);
                winGridViewPager1.gridView1.SetGridColumWidth("WinformType", 400);
                winGridViewPager1.gridView1.SetGridColumWidth("WebIcon", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("Url", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("Note", 200);
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void FormOnLoad()
        {
            InitTree();
            BindData();
            Init_Function();
        }

        void Init_Function()
        {
            btnSearch.Enabled = HasFunction("Menu/search");
            btnAddNew.Enabled = HasFunction("Menu/add");
            btnExport.Enabled = HasFunction("Menu/Export");
        }

        /// <summary>
        /// 初始化字典列表内容
        /// </summary>
        private void InitDictItem()
        {
            //初始化代码
        }

        /// <summary>
        /// 分页控件刷新操作
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 分页控件删除操作
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (!HasFunction("Menu/del"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                BLLFactory<Menus>.Instance.DeleteByUser(ID, LoginUserInfo.ID.ToString());
            }

            BindData();
        }

        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("Menu/edit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
            List<string> IDList = new List<string>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                string strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID");
                IDList.Add(strTemp);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEditMenu dlg = new FrmEditMenu();
                dlg.ID = ID;
                dlg.IDList = IDList;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            InitTree();
            BindData();
        }

        /// <summary>
        /// 分页控件新增操作
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            if (!HasFunction("Menu/add"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            btnAddNew_Click(null, null);
        }

        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            if (!HasFunction("Menu/Export"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<Menus>.Instance.FindToDataTable(where);
        }

        /// <summary>
        /// 分页控件翻页的操作
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("Name", this.txtName.Text, SqlOperator.Like);
            condition.AddCondition("FunctionId", this.txtFunctionId.Text, SqlOperator.Like);
            condition.AddCondition("WinformType", this.txtWinformType.Text, SqlOperator.Like);
            condition.AddCondition("Url", this.txtUrl.Text, SqlOperator.Like);
            if (this.txtVisible.Checked)
            {
                condition.AddCondition("Visible", 1, SqlOperator.Equal);
            }
            string where = condition.BuildConditionSql().Replace("Where", "");
            //如果是单击节点得到的条件，则使用树列表的，否则使用查询条件的
            if (!string.IsNullOrEmpty(treeConditionSql))
            {
                where = treeConditionSql;
            }
            return where;
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            //entity
            this.winGridViewPager1.DisplayColumns = "Name,Icon,Seq,FunctionId,Visible,WinformType,WebIcon,Url";
            #region 添加别名解析

            this.winGridViewPager1.AddColumnAlias("ID", "");
            this.winGridViewPager1.AddColumnAlias("Name", "显示名称");
            this.winGridViewPager1.AddColumnAlias("Icon", "图标");
            this.winGridViewPager1.AddColumnAlias("Seq", "排序");
            this.winGridViewPager1.AddColumnAlias("FunctionId", "功能ID");
            this.winGridViewPager1.AddColumnAlias("Visible", "菜单可见");
            this.winGridViewPager1.AddColumnAlias("WinformType", "Winform窗体类型");
            this.winGridViewPager1.AddColumnAlias("WebIcon", "Web界面的菜单图标");
            this.winGridViewPager1.AddColumnAlias("Url", "Web界面Url地址");

            #endregion

            string where = GetConditionSql();
            List<MenuInfo> list = BLLFactory<Menus>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = new SortableBindingList<MenuInfo>(list);
            this.winGridViewPager1.PrintTitle = "功能菜单信息报表";
        }

        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!HasFunction("Menu/search"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            treeConditionSql = "";
            BindData();
        }

        /// <summary>
        /// 新增数据操作
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            string selectId = "";
            string systemType = "";
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null)
            {
                if (node.Tag != null)
                {
                    MenuNodeInfo menuInfo = node.Tag as MenuNodeInfo;
                    if (menuInfo != null)
                    {
                        selectId = menuInfo.ID;
                        systemType = menuInfo.SystemType_ID;
                    }
                }
                else
                {
                    systemType = node.Name;
                }
            }

            FrmEditMenu dlg = new FrmEditMenu();
            dlg.PID = selectId;
            dlg.SystemType_ID = systemType;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private string moduleName = "功能菜单";

        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                List<MenuInfo> list = BLLFactory<Menus>.Instance.GetAll();
                DataTable dtNew = DataTableHelper.CreateTable("序号|int,父ID,显示名称,图标,排序,功能ID,菜单可见,Winform窗体类型,Web界面的菜单图标,Web界面Url地址,系统编号");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                    dr["父ID"] = list[i].PID;
                    dr["显示名称"] = list[i].Name;
                    dr["图标"] = list[i].Icon;
                    dr["排序"] = list[i].Seq;
                    dr["功能ID"] = list[i].FunctionId;
                    dr["菜单可见"] = list[i].Visible;
                    dr["Winform窗体类型"] = list[i].WinformType;
                    dr["Web界面的菜单图标"] = list[i].WebIcon;
                    dr["Web界面Url地址"] = list[i].Url;
                    dr["系统编号"] = list[i].SystemType_ID;
                    dtNew.Rows.Add(dr);
                }

                try
                {
                    string error = "";
                    AsposeExcelTools.DataTableToExcel2(dtNew, file, out error);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageDxUtil.ShowError(string.Format("导出Excel出现错误：{0}", error));
                    }
                    else
                    {
                        if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(file);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmMenu));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        /// <summary>
        /// 绑定树形数据
        /// </summary>
        private void InitTree()
        {
            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();
            Cursor.Current = Cursors.WaitCursor;

            //先获取系统类型，然后对不同的系统类型下的菜单进行绑定显示
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                TreeNode pNode = new TreeNode();
                pNode.Text = typeInfo.Name;//系统类型节点
                pNode.Name = typeInfo.OID;
                pNode.ImageIndex = 0;
                pNode.SelectedImageIndex = 0;
                this.treeView1.Nodes.Add(pNode);

                string systemType = typeInfo.OID;//系统标识ID

                //绑定树控件
                //一般情况下，对Ribbon样式而言，一级菜单表示RibbonPage；二级菜单表示PageGroup;三级菜单才是BarButtonItem最终的菜单项。
                List<MenuNodeInfo> menuList = BLLFactory<Menus>.Instance.GetTree(systemType);
                foreach (MenuNodeInfo info in menuList)
                {
                    TreeNode item = new TreeNode();
                    item.Name = info.ID;
                    item.Text = info.Name;//一级菜单节点
                    item.Tag = info;//对菜单而言，记录其MenuNodeInfo到Tag中，作为判断依据
                    item.ImageIndex = 1;
                    item.SelectedImageIndex = 1;
                    pNode.Nodes.Add(item);

                    AddChildNode(info.Children, item);
                }
            }

            Cursor.Current = Cursors.Default;
            treeView1.EndUpdate();
            this.treeView1.ExpandAll();
        }

        private void AddChildNode(List<MenuNodeInfo> list, TreeNode fnode)
        {
            foreach (MenuNodeInfo info in list)
            {
                TreeNode item = new TreeNode();
                item.Name = info.ID;
                item.Text = info.Name;//二、三级菜单节点
                item.Tag = info;//对菜单而言，记录其MenuNodeInfo到Tag中，作为判断依据
                int index = (fnode.ImageIndex + 1 > 3) ? 3 : fnode.ImageIndex + 1;
                item.ImageIndex = index;
                item.SelectedImageIndex = index; 
                fnode.Nodes.Add(item);

                AddChildNode(info.Children, item);
            }
        }

        string treeConditionSql = "";
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Tag != null)
                {
                    string menuId = e.Node.Name;
                    treeConditionSql = string.Format("PID='{0}' ", menuId);
                    BindData();
                }
                else
                {
                    string systemTypeID = e.Node.Name;
                    treeConditionSql = string.Format("SystemType_ID='{0}' ", systemTypeID);
                    BindData();
                }
            }
        }

        private void ctxMenuTree_Refresh_Click(object sender, EventArgs e)
        {
            InitTree();
        }

        private void SelectTreeItem()
        {
            //当鼠标在指定的菜单项上移动的时候，同时调整树形菜单的位置
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
            if (!string.IsNullOrEmpty(ID))
            {
                TreeNode node = FindNode(this.treeView1.Nodes, ID);
                if (node != null)
                {
                    this.treeView1.SelectedNode = node;
                }
            }
        }

        private TreeNode FindNode(TreeNodeCollection nodes, string menuId)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag != null)
                {
                    MenuNodeInfo info = node.Tag as MenuNodeInfo;
                    if (info != null && info.ID == menuId)
                    {
                        return node;
                    }
                }

                TreeNode candidate = FindNode(node.Nodes, menuId);
                if (candidate != null)
                    return candidate;
            }

            return null;
        }

        private void txtVisible_CheckedChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }
    }
}
