using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.PlugInInterface;
using JCodes.Framework.BLL;

namespace JCodes.Framework.AddIn.WareHouseManage
{
    public partial class FrmStockSearch : BaseDock
    {
        public FrmStockSearch()
        {
            InitializeComponent();

            InitDictItem();

            this.winGridView1.BestFitColumnWith = false;
            this.winGridView1.AppendedMenu = this.contextMenuStrip1;
            this.winGridView1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            this.winGridView1.OnRefresh += new EventHandler(winGridView1_OnRefresh);
            this.winGridView1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            this.winGridView1.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
            
            this.winGridView1.gridView1.DoubleClick += new EventHandler(gridView1_DoubleClick);
            BindTreeData();

            this.menu_SetAlarm.Enabled = HasFunction("Stok/SetAlarm");
            this.menu_ModifyStock.Enabled = HasFunction("Stock/Modify");
        }

        /// <summary>
        /// 常见汇总信息
        /// </summary>
        private void CreateSummary()
        {
            GridView gridView1 = this.winGridView1.gridView1;
            if (gridView1 != null && gridView1.Columns.Count > 0)
            {
                gridView1.GroupSummary.Clear();

                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
                gridView1.OptionsView.ShowFooter = true;
                gridView1.OptionsView.ShowGroupedColumns = true;
                gridView1.OptionsView.ShowGroupPanel = false;

                DevExpress.XtraGrid.Columns.GridColumn IDColumn = gridView1.Columns["ID"];
                IDColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ID", "记录数：{0}")});

                DevExpress.XtraGrid.Columns.GridColumn StockQuantityColumn = gridView1.Columns["STOCKQUANTITY"];
                StockQuantityColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STOCKQUANTITY", "库存数量：{0}")});

                DevExpress.XtraGrid.Columns.GridColumn StockAmountColumn = gridView1.Columns["STOCKAMOUNT"];
                StockAmountColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "STOCKAMOUNT", "库存金额：{0:C2}")});
            }
        }

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column.FieldName == "OrderStatus")
            //{
            //    string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
            //    Color color = Color.White;
            //    if (status == "已审核")
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //}
        }

        /// <summary>
        /// 对显示的字段内容进行转义
        /// </summary>
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            string columnName = e.Column.FieldName;
            if (e.Column.ColumnType == typeof(DateTime))
            {
                if (e.Value != null)
                {
                    if (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                    {
                        e.DisplayText = "";
                    }
                    else
                    {
                        e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                    }
                }
            }
            else if (columnName == "PRICE")
            {
                if (e.Value != null)
                {
                    e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
                }
            }
            else if (columnName == "STOCKAMOUNT")
            {
                if (e.Value != null)
                {
                    e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
                }
            }

        }
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridView1.gridView1.Columns.Count > 0 && this.winGridView1.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridView1.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度
                winGridView1.gridView1.SetGridColumWidth("Note", 200);
                winGridView1.gridView1.SetGridColumWidth("ItemNo", 120);
                winGridView1.gridView1.SetGridColumWidth("ItemBigType", 120);
                winGridView1.gridView1.SetGridColumWidth("WareHouse", 120);

                winGridView1.gridView1.SetGridColumWidth("ID", 100);
                winGridView1.gridView1.SetGridColumWidth("StockQuantity", 120);
                winGridView1.gridView1.SetGridColumWidth("StockAmount", 160);

                //ID,StockQuantity,Unit,Price
                winGridView1.gridView1.SetGridColumWidth("Unit", 80);
                winGridView1.gridView1.SetGridColumWidth("Price", 80);
            }

            CreateSummary();
        }

        void winGridView1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        private void InitDictItem()
        {
            this.txtSearchWareHouse.Properties.Items.Clear();
            this.txtSearchWareHouse.Properties.Items.Add(new CListItem("", "所有仓库"));
            this.txtSearchWareHouse.Properties.Items.AddRange(BLLFactory<WareHouse>.Instance.GetAllWareHouse().ToArray());

            //使用扩展函数方式绑定字典
            //this.txtBigType.BindDictItems("备件属类");
            //this.txtItemType.BindDictItems("备件类别");
            //this.txtSource.BindDictItems("来源");
            //this.txtManufacture.BindDictItems("供货商");
            //this.txtUnit.BindDictItems("单位");
            this.txtUnit.BindDictItems(1000);
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void FormOnLoad()
        {
            InitTree();
            //BindData();
        }

        void gridView1_DoubleClick(object sender, EventArgs e)
        {
            menu_SetAlarm_Click(null, null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            treeConditionSql = "";
            BindData();
        }

        private void menu_InRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }

         /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("t.WareHouse", this.txtSearchWareHouse.Text.Replace("所有仓库", ""), SqlOperator.Equal)
            .AddCondition("t.StockQuantity", this.txtStockQuantity1.Text, SqlOperator.MoreThanOrEqual)
            .AddCondition("t.StockQuantity", this.txtStockQuantity2.Text, SqlOperator.LessThanOrEqual)
            .AddCondition("d.Manufacture", this.txtManufacture.Text, SqlOperator.Like)
            .AddCondition("d.Source", this.txtSource.Text, SqlOperator.Like)
            .AddCondition("d.ItemBigType", this.txtBigType.Text, SqlOperator.Like)
            .AddCondition("d.ItemType", this.txtItemType.Text, SqlOperator.Like)
            .AddCondition("d.ItemNo", this.txtItemNo.Text, SqlOperator.LikeStartAt)
            .AddCondition("d.ItemName", this.txtName.Text, SqlOperator.Like)
            .AddCondition("d.MapNo", this.txtMapNo.Text, SqlOperator.Like)
            .AddCondition("d.Specification", this.cmbSpecNumber.Text, SqlOperator.Like)
            .AddCondition("d.Material", this.txtMaterial.Text, SqlOperator.Like)
            .AddCondition("d.Unit", this.txtUnit.Text, SqlOperator.Like)
            .AddCondition("d.UsagePos", this.txtUsagePos.Text, SqlOperator.Like)
            .AddCondition("d.Price", this.txtPrice1.Text, SqlOperator.MoreThanOrEqual)
            .AddCondition("d.Price", this.txtPrice2.Text, SqlOperator.LessThanOrEqual)
            .AddCondition("d.StoragePos", this.txtStoragePos.Text, SqlOperator.Like);

            string where = condition.BuildConditionSql();

            //如果是单击节点得到的条件，则使用树列表的，否则使用查询条件的
            if (!string.IsNullOrEmpty(treeConditionSql))
            {
                where = "Where " + treeConditionSql;
            }
            return where;
        }

        private void BindData()
        {
            #region 添加别名解析
            //ID,ItemNo,ItemName,MapNo,Specification,Material,ItemBigType,ItemType,Unit,Price,
            //Source,StoragePos,UsagePos,StockQuantity,StockMoney,LowWarning,HighWarning,WareHouse,Note
            this.winGridView1.AddColumnAlias("ID", "编号");
            this.winGridView1.AddColumnAlias("ItemNo", "项目编号");
            this.winGridView1.AddColumnAlias("ItemName", "项目名称");
            this.winGridView1.AddColumnAlias("MapNo", "图号");
            this.winGridView1.AddColumnAlias("Specification", "规格型号");
            this.winGridView1.AddColumnAlias("Manufacture", "供货商");
            this.winGridView1.AddColumnAlias("Material", "材质");
            this.winGridView1.AddColumnAlias("ItemBigType", "备件属类");
            this.winGridView1.AddColumnAlias("ItemType", "备件类别");
            this.winGridView1.AddColumnAlias("Unit", "单位");
            this.winGridView1.AddColumnAlias("Price", "单价");
            this.winGridView1.AddColumnAlias("Source", "来源");
            this.winGridView1.AddColumnAlias("StoragePos", "库位");
            this.winGridView1.AddColumnAlias("UsagePos", "使用位置");
            this.winGridView1.AddColumnAlias("StockQuantity", "库存量");
            this.winGridView1.AddColumnAlias("StockAmount", "库存金额");
            this.winGridView1.AddColumnAlias("LowWarning", "低储预警");
            this.winGridView1.AddColumnAlias("HighWarning", "超储预警");                        
            this.winGridView1.AddColumnAlias("WareHouse", "仓库名称");
            this.winGridView1.AddColumnAlias("Dept", "部门名称");
            this.winGridView1.AddColumnAlias("Note", "备注");
   
            #endregion

            string where = GetConditionSql();
            DataTable dt = BLLFactory<Stock>.Instance.GetCurrentStockReport(where);

            this.winGridView1.DataSource = dt;
            this.winGridView1.PrintTitle = this.AppInfo.AppUnit + " -- " + "当前库存查询统计报表"; 
        }

        private void BindTreeData()
        {
            this.treeGoods.Nodes.Clear();
            this.treeGoods.BeginUpdate();

            int allCount = BLLFactory<Stock>.Instance.GetCurrentStockReportCount(" Where 1=1");

            this.treeGoods.Nodes.Add("按库房显示", string.Format("按库房显示({0})", allCount));
            this.treeGoods.Nodes.Add("按部门显示", "按部门显示");
            this.treeGoods.Nodes.Add("按使用位置显示", "按使用位置显示");
            this.treeGoods.Nodes.Add("按备件类别显示", "按备件类别显示");
            this.treeGoods.Nodes.Add("按备件属类显示", "按备件属类显示");

            this.treeGoods.EndUpdate();
            this.treeGoods.ExpandAll();
        }

        private void treeGoods_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            string filter = " Where 1= 1";
            if (node.Tag != null && node.Tag.GetType() == typeof(CustomTreeData))
            {
                CustomTreeData nodeData = (CustomTreeData)node.Tag;
                if (nodeData != null && nodeData.TreeType != CustomTreeType.Unkown)
                {
                    #region MyRegion
                    SearchCondition condition = new SearchCondition();
                    if (nodeData.TreeType == CustomTreeType.Ware)
                    {
                        condition.AddCondition("t.WareHouse", nodeData.TreeData, SqlOperator.Equal);
                    }
                    else if (nodeData.TreeType == CustomTreeType.Dept)
                    {
                        //condition.AddCondition("1", "2", SqlOperator.Equal);
                        condition.AddCondition("d.Dept", nodeData.TreeData, SqlOperator.Equal);
                    }
                    else if (nodeData.TreeType == CustomTreeType.UsagePos)
                    {
                        condition.AddCondition("d.UsagePos", nodeData.TreeData, SqlOperator.Equal);
                    }
                    else if (nodeData.TreeType == CustomTreeType.ItemType)
                    {
                        condition.AddCondition("d.ItemType", nodeData.TreeData, SqlOperator.Equal);
                    }
                    else if (nodeData.TreeType == CustomTreeType.ItemBigType)
                    {
                        condition.AddCondition("d.ItemBigType", nodeData.TreeData, SqlOperator.Equal);
                    }

                    filter = condition.BuildConditionSql();
                    #endregion
                }
            }

            DataTable dt = BLLFactory<Stock>.Instance.GetCurrentStockReport(filter);
            this.lvwDetail.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["ItemNo"].ToString());
                item.SubItems.Add(row["ItemName"].ToString());
                item.SubItems.Add(row["ItemBigType"].ToString());
                item.SubItems.Add(row["ItemType"].ToString());
                item.SubItems.Add(row["Unit"].ToString());
                item.SubItems.Add(Convert.ToDecimal(row["Price"]).ToString("C2"));
                item.SubItems.Add(Convert.ToInt32(row["StockQuantity"]).ToString());
                item.SubItems.Add(Convert.ToDecimal(row["StockAmount"]).ToString("C2"));
                item.SubItems.Add(row["UsagePos"].ToString());
                item.SubItems.Add(row["WareHouse"].ToString());
                item.SubItems.Add(row["Dept"].ToString());

                this.lvwDetail.Items.Add(item);
            }
        }

        private void treeGoods_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.treeGoods.SelectedNode != null)
            {
                TreeNode node = this.treeGoods.SelectedNode;//按钮默认加第一个
                if(node.Level == 0)
                {
                    node.Nodes.Clear();
                    if (node.Name == "按库房显示")
                    {
                        InitWareHouseInfo(node);
                    }
                    else if (node.Name == "按部门显示")
                    {
                        InitDeptInfo(node);
                    }
                    else if (node.Name == "按使用位置显示")
                    {
                        InitUsagePosInfo(node);
                    }
                    else if (node.Name == "按备件类别显示")
                    {
                        InitItemTypeInfo(node);
                    }
                    else if (node.Name == "按备件属类显示")
                    {
                        InitItemBigTypeInfo(node);
                    }

                    node.Expand();
                }
            }
        }

        private void InitWareHouseInfo(TreeNode node)
        {
            List<CListItem> wareHouseList = BLLFactory<WareHouse>.Instance.GetAllWareHouse();
            foreach (CListItem item in wareHouseList)
            {
                string condition = string.Format(" where t.WareHouse = '{0}' ", item.Value);
                int count = BLLFactory<Stock>.Instance.GetCurrentStockReportCount(condition);
                string displayText = string.Format("{0}({1})", item.Value, count);

                TreeNode subNode = new TreeNode(displayText, 1, 1);
                subNode.Tag = new CustomTreeData(CustomTreeType.Ware, item.Value);

                node.Nodes.Add(subNode);
            }
        }

        private void InitDeptInfo(TreeNode node)
        {
            /*CListItem[] dictList = DictItemUtil.GetDictByDictType("部门");
            foreach (CListItem item in dictList)
            {
                string condition = string.Format(" where d.Dept = '{0}' ", item.Value);
                int count = BLLFactory<Stock>.Instance.GetCurrentStockReportCount(condition);
                string displayText = string.Format("{0}({1})", item.Value, count);

                TreeNode subNode = new TreeNode(displayText, 1, 1);
                subNode.Tag = new CustomTreeData(CustomTreeType.Dept, item.Value);

                node.Nodes.Add(subNode);
            }*/
        }

        private void InitUsagePosInfo(TreeNode node)
        {
            /*CListItem[] dictList = DictItemUtil.GetDictByDictType("使用位置");
            foreach (CListItem item in dictList)
            {
                string condition = string.Format(" where d.UsagePos = '{0}' ", item.Value);
                int count = BLLFactory<Stock>.Instance.GetCurrentStockReportCount(condition);
                string displayText = string.Format("{0}({1})", item.Value, count);

                TreeNode subNode = new TreeNode(displayText, 1, 1);
                subNode.Tag = new CustomTreeData(CustomTreeType.UsagePos, item.Value);

                node.Nodes.Add(subNode); 
            }*/
        }
        private void InitItemTypeInfo(TreeNode node)
        {
            /*CListItem[] dictList = DictItemUtil.GetDictByDictType("备件类别");
            foreach (CListItem item in dictList)
            {
                string condition = string.Format(" where d.ItemType = '{0}' ", item.Value);
                int count = BLLFactory<Stock>.Instance.GetCurrentStockReportCount(condition);
                string displayText = string.Format("{0}({1})", item.Value, count);

                TreeNode subNode = new TreeNode(displayText, 1, 1);
                subNode.Tag = new CustomTreeData(CustomTreeType.ItemType, item.Value);

                node.Nodes.Add(subNode);
            }*/
        }

        private void InitItemBigTypeInfo(TreeNode node)
        {
            /*CListItem[] dictList = DictItemUtil.GetDictByDictType("备件属类");
            foreach (CListItem item in dictList)
            {
                string condition = string.Format(" where d.ItemBigType = '{0}' ", item.Value);
                int count = BLLFactory<Stock>.Instance.GetCurrentStockReportCount(condition);
                string displayText = string.Format("{0}({1})", item.Value, count);

                TreeNode subNode = new TreeNode(displayText, 1, 1);
                subNode.Tag = new CustomTreeData(CustomTreeType.ItemBigType, item.Value);

                node.Nodes.Add(subNode);
            }*/
        }

        private void txtGoodsType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
            }
        }

        private void menu_SetAlarm_Click(object sender, EventArgs e)
        {
            int[] rowSelected = this.winGridView1.GridView1.GetSelectedRows();
            if (rowSelected == null)
                return;

            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridView1.gridView1.GetRowCellDisplayText(iRow, "ID");
                if (!string.IsNullOrEmpty(ID))
                {
                    StockInfo info = BLLFactory<Stock>.Instance.FindByID(ID);
                    if (info != null)
                    {
                        FrmStockAlert dlg = new FrmStockAlert();
                        dlg.ID = ID;
                        dlg.WareHouse = info.WareHouse;
                        dlg.ItemNo = info.ItemNo;
                        dlg.ItemName = info.ItemName;
                        if (DialogResult.OK == dlg.ShowDialog())
                        {
                            BindData();
                        }
                    }
                }
                break;
            }
        }

        private void menu_ModifyStock_Click(object sender, EventArgs e)
        {
            int[] rowSelected = this.winGridView1.GridView1.GetSelectedRows();
            if (rowSelected == null)
                return;

            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridView1.gridView1.GetRowCellDisplayText(iRow, "ID");
                if (!string.IsNullOrEmpty(ID))
                {
                    StockInfo info = BLLFactory<Stock>.Instance.FindByID(ID);
                    if (info != null)
                    {
                        FrmEditStock dlg = new FrmEditStock();
                        dlg.ID = ID;
                        dlg.WareHouse = info.WareHouse;
                        dlg.InitFunction(this.LoginUserInfo, this.FunctionDict);//初始化权限控制信息
                        if (DialogResult.OK == dlg.ShowDialog())
                        {
                            BindData();
                        }
                    }
                }
                break;
            }
        }

        private void menu_ClapseAll_Click(object sender, EventArgs e)
        {
            this.treeGoods.CollapseAll();
        }

        private void menu_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeGoods.ExpandAll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtBigType.Text = "";
            this.txtItemNo.Text = "";
            this.txtItemType.Text = "";
            this.txtManufacture.Text = "";
            this.txtMapNo.Text = "";
            this.txtMaterial.Text = "";
            this.txtName.Text = "";
            this.txtPrice1.Text = "";
            this.txtPrice2.Text = "";
            this.txtSource.Text = "";
            this.txtStockQuantity1.Text = "";
            this.txtStockQuantity2.Text = "";
            this.txtUnit.Text = "";
            this.txtUsagePos.Text = "";
        }

        private void menu_Refresh_Click(object sender, EventArgs e)
        {
            BindTreeData();
        }

        private void menu_DeleteZero_Click(object sender, EventArgs e)
        {
            bool found = false;
            int[] rowSelected = this.winGridView1.GridView1.GetSelectedRows();
            if (rowSelected == null)
                return;

            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridView1.gridView1.GetRowCellDisplayText(iRow, "ID");
                string strStockQuantity = this.winGridView1.gridView1.GetRowCellDisplayText(iRow, "StockQuantity");
                int StockQuantity = 0;
                int.TryParse(strStockQuantity, out StockQuantity);

                if (StockQuantity != 0)
                {
                    found = true;
                    continue;
                }

                if (!string.IsNullOrEmpty(ID) && StockQuantity == 0)
                {
                    BLLFactory<Stock>.Instance.DeleteByUser(ID, LoginUserInfo.ID.ToString());
                }
            }

            if (found)
            {
                MessageDxUtil.ShowWarning("删除记录中存在库存大于 0 的记录，已经跳过！");
            }
            BindData();
        }

        private void InitTree()
        {
            base.LoginUserInfo = Cache.Instance["LoginUserInfo"] as LoginUserInfo;

            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();

            TreeNode WareHouseNode = new TreeNode("仓库列表", 1, 1);
            List<CListItem> warehouseList = BLLFactory<WareHouse>.Instance.GetAllWareHouse();
            foreach (CListItem item in warehouseList)
            {
                TreeNode subNode = new TreeNode(item.Text, 1, 1);
                subNode.Tag = string.Format("{0}='{1}' ", "t.WareHouse", item.Value);
                WareHouseNode.Nodes.Add(subNode);
            }
            this.treeView1.Nodes.Add(WareHouseNode);

            TreeNode BigTypeNode = new TreeNode("备件属类", 2, 2);
            this.treeView1.Nodes.Add(BigTypeNode);
            AddDictData(BigTypeNode, 0, "t.ItemBigType");

            TreeNode ItemTypeNode = new TreeNode("备件类别", 3, 3);
            this.treeView1.Nodes.Add(ItemTypeNode);
            AddDictData(ItemTypeNode, 0, "t.ItemType");

            TreeNode SourceNode = new TreeNode("来源", 4, 4);
            this.treeView1.Nodes.Add(SourceNode);
            AddDictData(SourceNode, 0, "d.Source");

            //TreeNode ManufactureNode = new TreeNode("供货商", 5, 5);
            //this.treeView1.Nodes.Add(ManufactureNode);
            //AddDictData(ManufactureNode, 0, "d.Manufacture");

            this.treeView1.ExpandAll();
            this.treeView1.EndUpdate();
        }

        /// <summary>
        /// 从数据库获取对应字典数据，并绑定到相关节点上
        /// </summary>
        private void AddDictData(TreeNode treeNode, int i, string fieldName)
        {
            /*List<DictDataInfo> dict = BLLFactory<DictData>.Instance.FindByDictType(treeNode.Text);
            foreach (DictDataInfo info in dict)
            {
                TreeNode subNode = new TreeNode(info.Name, i, i);

                subNode.Tag = string.Format("{0}='{1}' ", fieldName, info.Value);
                treeNode.Nodes.Add(subNode);
            }*/
        }

        string treeConditionSql = "";
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                treeConditionSql = e.Node.Tag.ToString();
                BindData();
            }
            else
            {
                treeConditionSql = "";
                BindData();
            }
        }

        private void menuTree_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private void menuTree_Clapase_Click(object sender, EventArgs e)
        {
            this.treeView1.CollapseAll();
        }

        private void menuTree_Refresh_Click(object sender, EventArgs e)
        {
            InitTree();
        }
    }

    internal enum CustomTreeType
    {
        Ware, Dept, UsagePos, ItemType, ItemBigType, Unkown
    }

    internal class CustomTreeData
    {
        public CustomTreeType TreeType = CustomTreeType.Unkown;
        public string TreeData = "";
        public object Tag = null;

        public CustomTreeData(CustomTreeType treeType, string treeData)
        {
            this.TreeType = treeType;
            this.TreeData = treeData;
        }
    }
}
