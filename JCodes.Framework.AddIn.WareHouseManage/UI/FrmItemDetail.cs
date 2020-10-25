using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.AdvanceSearch;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common.Extension;
using DevExpress.Xpf.Printing;

namespace JCodes.Framework.AddIn.WareHouseManage
{
    public partial class FrmItemDetail : BaseDock
    {
        public FrmItemDetail()
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
            this.winGridViewPager1.BestFitColumnWith = false;//使用固定列宽的做法，True为自动适应宽度
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);

            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
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
            else if (columnName == "Price")
            {
                if (e.Value != null)
                {
                    e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
                }
            }
            else if (columnName == "Manufacture")
            {
                e.DisplayText = BLLFactory<Supplier>.Instance.FindById(Convert.ToInt32(e.Value)).Name;
            }
            else if (columnName == "WareHouse")
            { 
                e.DisplayText = BLLFactory<WareHouse>.Instance.FindById(Convert.ToInt32(e.Value)).Name;
            }
            else if (columnName == "ItemBigType")
            {
                e.DisplayText = BLLFactory<DictData>.Instance.GetDictName(Const.DIC_DEVICEATTR, Convert.ToInt32(e.Value));
            }
            else if (columnName == "ItemType")
            {
                e.DisplayText = BLLFactory<DictData>.Instance.GetDictName(Const.DIC_DEVICECATEGORY, Convert.ToInt32(e.Value));
            }
            else if (columnName == "Unit")
            {
                e.DisplayText = BLLFactory<DictData>.Instance.GetDictName(Const.DIC_DEVICEUNIT, Convert.ToInt32(e.Value));
            }
        }

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
                winGridViewPager1.gridView1.SetGridColumWidth("Note", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("ItemNo", 120);
                winGridViewPager1.gridView1.SetGridColumWidth("ItemBigType", 120);
                winGridViewPager1.gridView1.SetGridColumWidth("WareHouse", 120);
                winGridViewPager1.gridView1.SetGridColumWidth("ID", 80);
                winGridViewPager1.gridView1.SetGridColumWidth("StockQuantity", 80);
                winGridViewPager1.gridView1.SetGridColumWidth("Unit", 80);
                winGridViewPager1.gridView1.SetGridColumWidth("Price", 80);
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void  FormOnLoad()
        {   
            BindData();
        }

        private void InitDictItem()
        {
            txtManufacture.BindDictItems(BLLFactory<Supplier>.Instance.GetAllSupplierDic());
            txtBigType.BindDictItems(Const.DIC_DEVICEATTR);
            txtItemType.BindDictItems(Const.DIC_DEVICECATEGORY);
            this.txtWareHouse.Properties.Items.Clear();
            this.txtWareHouse.Properties.Items.AddRange(BLLFactory<WareHouse>.Instance.GetAllWareHouse().ToArray());
        }

        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
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
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                BLLFactory<ItemDetail>.Instance.DeleteByUser(ID, LoginUserInfo.Id);
            }
            BindData();
        }

        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            Int32 Id = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID").ToInt32();
            List<Int32> IdList = new List<Int32>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                Int32 intTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID").ToInt32();
                IdList.Add(intTemp);
            }

            if (Id > 0)
            {
                FrmEditItemDetail dlg = new FrmEditItemDetail();
                dlg.Id = Id;
                dlg.IdList = IdList;                
                dlg.InitFunction(this.LoginUserInfo, this.FunctionDict);//初始化权限控制信息
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }
        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            btnAddNew_Click(null, null);
        }

        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<ItemDetail>.Instance.FindToDataTable(where);
        }

        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }
                        
        /// <summary>
        /// 高级查询条件语句对象
        /// </summary>
        private SearchCondition advanceCondition;

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
            SearchCondition condition = advanceCondition;
            if (condition == null)
            {
                condition = new SearchCondition();
                condition.AddCondition("ItemName", this.txtName.Text, SqlOperator.Like)
                    .AddCondition("ItemBigType", this.txtBigType.GetComboBoxIntValue(), SqlOperator.Like)
                    .AddCondition("ItemType", this.txtItemType.GetComboBoxIntValue(), SqlOperator.Like)
                    .AddCondition("Specification", this.cmbSpecNumber.Text, SqlOperator.Like)
                    .AddCondition("MapNo", this.txtMapNo.Text, SqlOperator.Like)
                    .AddCondition("Material", this.txtMaterial.Text, SqlOperator.Like)
                    .AddCondition("Source", this.txtSource.Text, SqlOperator.Like)
                    .AddCondition("Note", this.txtNote.Text, SqlOperator.Like)
                    .AddCondition("Manufacture", this.txtManufacture.GetComboBoxStrValue(), SqlOperator.Like)
                    .AddCondition("ItemNo", this.txtItemNo.Text, SqlOperator.LikeStartAt)
                    .AddCondition("WareHouse", this.txtWareHouse.GetComboBoxStrValue(), SqlOperator.Like)
                    .AddCondition("Dept", this.txtDept.Text, SqlOperator.Like)
                    .AddCondition("UsagePos", this.txtUsagePos.Text, SqlOperator.Like)
                    .AddCondition("StoragePos", this.txtStoragePos.Text, SqlOperator.Like);
            }
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }

        /// <summary>
        /// 绑定数据到界面上的分页控件
        /// </summary>
        private void BindData()
        {
            this.winGridViewPager1.DisplayColumns = "ID,ItemNo,ItemName,Manufacture,MapNo,Specification,Material,ItemBigType,ItemType,Unit,Price,Source,StoragePos,UsagePos,StockQuantity,AlarmQuantity,Note,Dept,WareHouse";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<ItemDetail>.Instance.GetColumnNameAlias();

            string where = GetConditionSql();
            List<ItemDetailInfo> list = BLLFactory<ItemDetail>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            string tableColumns = "ID|int,ItemNo,ItemName,StockQuantity|int,Manufacture,MapNo,Specification,Material,ItemBigType,ItemType,Unit,Price|decimal,Source,StoragePos,UsagePos,Note,WareHouse,Dept";
            DataTable dt = DataTableHelper.CreateTable(tableColumns);
            DataRow dr = null;
            foreach (ItemDetailInfo info in list)
            {
                dr = dt.NewRow();
                /*dr["ID"] = info.ID;
                dr["ItemBigType"] = info.ItemBigType;
                dr["ItemName"] = info.ItemName;
                dr["ItemNo"] = info.ItemNo;
                dr["ItemType"] = info.ItemType;
                dr["Manufacture"] = info.Manufacture;
                dr["MapNo"] = info.MapNo;
                dr["Material"] = info.Material;
                dr["Note"] = info.Note;
                dr["Price"] = info.Price;
                dr["Source"] = info.Source;
                dr["Specification"] = info.Specification;
                dr["StoragePos"] = info.StoragePos;
                dr["Unit"] = info.Unit;
                dr["UsagePos"] = info.UsagePos;
                dr["WareHouse"] = info.WareHouse;
                dr["Dept"] = info.Dept;*/

                WareInfo wareInfo = BLLFactory<Stock>.Instance.FindByItemNo(info.ItemNo);
                int quantity = 0;
                if (wareInfo != null)
                {
                    quantity = wareInfo.Amount;
                }
                dr["StockQuantity"] = quantity;
                dt.Rows.Add(dr);
            }

            this.winGridViewPager1.DataSource = dt.DefaultView;//new WHC.Pager.WinControl.SortableBindingList<ItemDetailInfo>(list);
            this.winGridViewPager1.PrintTitle = this.AppInfo.AppUnit + " -- " + "备件信息报表";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            advanceCondition = null;//必须重置查询条件，否则可能会使用高级查询条件了
            BindData();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmEditItemDetail dlg = new FrmEditItemDetail();
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.InitFunction(base.LoginUserInfo, base.FunctionDict);//该步骤省略也可以，用户信息以通过基类缓存进行获取

            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private FrmAdvanceSearch dlg;
        private void btnAdvanceSearch_Click(object sender, EventArgs e)
        {
            if (dlg == null)
            {
                dlg = new FrmAdvanceSearch();
                dlg.FieldTypeTable = BLLFactory<ItemDetail>.Instance.GetFieldTypeList();
                dlg.DisplayColumns = "ID,ItemNo,ItemName,Manufacture,MapNo,Specification,Material,ItemBigType,ItemType,Unit,Price,Source,StoragePos,UsagePos,StockQuantity,AlarmQuantity,Note,Dept,WareHouse";
                dlg.ColumnNameAlias = BLLFactory<ItemDetail>.Instance.GetColumnNameAlias();//字段列显示名称转义

                #region 下拉列表数据

                dlg.AddColumnListItem("Material", BLLFactory<ItemDetail>.Instance.GetFieldList("Material"));
                dlg.AddColumnListItem("Specification", BLLFactory<ItemDetail>.Instance.GetFieldList("Specification"));
                dlg.AddColumnListItem("Unit", BLLFactory<ItemDetail>.Instance.GetFieldList("Unit"));

                List<CDicKeyValue> wareList = BLLFactory<WareHouse>.Instance.GetAllWareHouse();
                // TODO
                //dlg.AddColumnListItem("WareHouse", wareList);
                #endregion

                dlg.ConditionChanged += new FrmAdvanceSearch.ConditionChangedEventHandler(dlg_ConditionChanged);
            }
            dlg.ShowDialog();
        }

        void dlg_ConditionChanged(SearchCondition condition)
        {
            advanceCondition = condition;
            BindData();
        }

        private void menu_PrintFixColumn_Click(object sender, EventArgs e)
        {
            this.winGridViewPager1.gridView1.OptionsPrint.EnableAppearanceEvenRow = true;
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = this.winGridViewPager1.gridControl1;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A3;
            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateDocument();
            link.ShowPreview();
        }

        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            string title = this.AppInfo.AppUnit + " -- " + "备件信息报表";
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.None, title, Color.DarkBlue,
               new RectangleF(0, 0, 100, 21), BorderSide.None);

            brick.LineAlignment = BrickAlignment.Center;
            brick.Alignment = BrickAlignment.Center;
            brick.AutoWidth = true;
            brick.Font = new System.Drawing.Font("宋体", 11f, FontStyle.Bold);
        }
    }
}
