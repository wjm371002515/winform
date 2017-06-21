using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.AddIn.Other;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.LocalReports;

namespace JCodes.Framework.AddIn.UI.WareHouse
{
    public partial class FrmPurchase : BaseDock
    {
        public FrmPurchase()
        {
            InitializeComponent();
        }

        private void InitDictItem()
        {
            this.txtCreateDate.DateTime = DateTime.Now;
            //this.dtStart.DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            //this.dtEnd.DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            //this.txtManufacture.BindDictItems("供货商");
            //this.txtSearchManufacture.BindDictItems("供货商");
            //this.txtSearchDept.BindDictItems("部门");

            this.txtWareHouse.Properties.Items.Clear();
            this.txtWareHouse.Properties.Items.AddRange(WareHouseHelper.GetWareHouse(LoginUserInfo.ID, LoginUserInfo.Name).ToArray());
            this.txtWareHouse.SelectedIndex = 0;

            this.txtSearchWareHouse.Properties.Items.Clear();
            this.txtSearchWareHouse.Properties.Items.Add(new CListItem("所有仓库", ""));
            this.txtSearchWareHouse.Properties.Items.AddRange(WareHouseHelper.GetWareHouse(LoginUserInfo.ID, LoginUserInfo.Name).ToArray());            
            this.txtSearchWareHouse.SelectedIndex = 0;

            this.txtCreator.Items.Clear();
            this.txtCreator.Items.Add(this.LoginUserInfo.FullName);
            this.txtCreator.SelectedIndex = this.txtCreator.FindString(this.LoginUserInfo.FullName);

            this.txtHandNo.Text = BLLFactory<PurchaseHeader>.Instance.GetHandNumber(true);//进货单号
            this.txtCreateDate.Enabled = false;
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void FormOnLoad()
        {
            this.winGridView1.AppendedMenu = this.contextMenuStrip2;
            this.winGridView1.gridView1.FocusedRowChanged += new FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);

            this.winGridView2.BestFitColumnWith = false;
            this.winGridView2.AppendedMenu = this.contextMenuStrip3;
            this.winGridView2.gridView1.DataSourceChanged += new EventHandler(gridView2_DataSourceChanged);
            this.winGridView2.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView2_CustomColumnDisplayText);
            this.winGridView2.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView2_RowCellStyle);

            #region 添加别名解析
            //HandNo,ItemNo,ItemName,MapNo,Specification,Material,
            //ItemBigType,ItemType,Unit,Price,Quantity,Amount,Source,StoragePos,UsagePos
            this.winGridView2.AddColumnAlias("HandNo", "货单号");
            this.winGridView2.AddColumnAlias("ItemNo", "项目编号");
            this.winGridView2.AddColumnAlias("ItemName", "项目名称");
            this.winGridView2.AddColumnAlias("MapNo", "图号");
            this.winGridView2.AddColumnAlias("Specification", "规格型号");
            this.winGridView2.AddColumnAlias("Material", "材质");
            this.winGridView2.AddColumnAlias("ItemBigType", "备件属类");
            this.winGridView2.AddColumnAlias("ItemType", "备件类别");
            this.winGridView2.AddColumnAlias("Unit", "单位");
            this.winGridView2.AddColumnAlias("Price", "单价");
            this.winGridView2.AddColumnAlias("Quantity", "数量");
            this.winGridView2.AddColumnAlias("Amount", "金额");
            this.winGridView2.AddColumnAlias("Source", "来源");
            this.winGridView2.AddColumnAlias("StoragePos", "库位");
            this.winGridView2.AddColumnAlias("UsagePos", "使用位置");
            #endregion

            this.btnAdd.Enabled = HasFunction("Purchase");
            this.btnDelete.Enabled = HasFunction("Purchase");
            this.btnOK.Enabled = HasFunction("Purchase");
            this.btnExportBill.Enabled = HasFunction("Purchase/Export");

            InitDictItem();
        }

        void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
        void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
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
            else if (columnName == "AMOUNT")
            {
                if (e.Value != null)
                {
                    e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
                }
            }
        }
        void gridView2_DataSourceChanged(object sender, EventArgs e)
        {
            GridView gridView1 = this.winGridView2.gridView1;
            if (gridView1.Columns.Count > 0 && gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns)
                {
                    column.Width = 100;
                }

                SetGridColumWidth(gridView1, "QUANTITY", 150);
                SetGridColumWidth(gridView1, "AMOUNT", 200);
            }
        }

        private void SetGridColumWidth(GridView gridView1, string columnName, int width)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = gridView1.Columns.ColumnByFieldName(columnName);
            if (column != null)
            {
                column.Width = width;
            }
            else
            {
                //如果是数据库获取的Datatable可能字段为大写
                column = gridView1.Columns.ColumnByFieldName(columnName.ToUpper());
                if (column != null)
                {
                    column.Width = width;
                }
            }
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BindDetail();
        }

        private void BindDetail()
        {
            string headID = winGridView1.gridView1.GetFocusedRowCellDisplayText("ID");
            if (!string.IsNullOrEmpty(headID))
            {
                this.winGridView2.PrintTitle = this.AppInfo.AppUnit + " -- " + string.Format("入库单[{0}]的备件列表", headID);
                this.winGridView2.DisplayColumns = "HandNo,ItemNo,ItemName,MapNo,Specification,Material,ItemBigType,ItemType,Unit,Price,Quantity,Amount,Source,StoragePos,UsagePos";

                DataTable dt = BLLFactory<PurchaseDetail>.Instance.GetPurchaseDetailReportByID(Convert.ToInt32(headID));
                this.winGridView2.DataSource = dt.DefaultView;

                CreateSummary();// 明细增加汇总信息
            }
        }

        /// <summary>
        /// 明细增加汇总信息
        /// </summary>
        private void CreateSummary()
        {
            GridView gridView1 = this.winGridView2.gridView1;
            if (gridView1 != null && gridView1.Columns.Count > 0)
            {
                gridView1.GroupSummary.Clear();

                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
                gridView1.OptionsView.ShowFooter = true;
                gridView1.OptionsView.ShowGroupedColumns = true;
                gridView1.OptionsView.ShowGroupPanel = false;

                DevExpress.XtraGrid.Columns.GridColumn IDColumn = gridView1.Columns["HANDNO"];
                IDColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "HANDNO", "记录数：{0}")});

                DevExpress.XtraGrid.Columns.GridColumn StockQuantityColumn = gridView1.Columns["QUANTITY"];
                StockQuantityColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QUANTITY", "总数量：{0}")});

                DevExpress.XtraGrid.Columns.GridColumn StockAmountColumn = gridView1.Columns["AMOUNT"];
                StockAmountColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "总金额：{0:C2}")});
            }
        }

        private void BindData()
        {
            this.winGridView1.DisplayColumns = "ID,HandNo,OperationType,Manufacture,WareHouse,Dept,Note,CreateDate,Creator";

            #region 添加别名解析
            this.winGridView1.AddColumnAlias("ID", "编号");
            this.winGridView1.AddColumnAlias("HandNo", "货单编号");
            this.winGridView1.AddColumnAlias("OperationType", "入库/出库");
            this.winGridView1.AddColumnAlias("Manufacture", "供货商");
            this.winGridView1.AddColumnAlias("WareHouse", "仓库名称");
            this.winGridView1.AddColumnAlias("Dept", "入库部门");
            this.winGridView1.AddColumnAlias("Note", "备注信息");
            this.winGridView1.AddColumnAlias("CreateDate", "入库日期");
            this.winGridView1.AddColumnAlias("Creator", "操作员");
            #endregion

            SearchCondition condition = new SearchCondition();
            condition.AddCondition("h.Manufacture", this.txtSearchManufacture.Text, SqlOperator.Like)
                .AddCondition("h.Creator", this.txtSearchOperator.Text, SqlOperator.Like)
                .AddCondition("h.WareHouse", this.txtSearchWareHouse.Text.Replace("所有仓库", ""), SqlOperator.Like)
                .AddCondition("h.OperationType", "入库", SqlOperator.Equal)
                .AddCondition("d.Dept", this.txtSearchDept.Text, SqlOperator.Like)
                .AddCondition("d.ItemName", this.txtName.Text, SqlOperator.Like)
                .AddCondition("d.ItemNo", this.txtItemNo.Text, SqlOperator.LikeStartAt);
            if (this.dtStart.Text.Length > 0)
            {
                condition.AddCondition("h.CreateDate", this.dtStart.DateTime, SqlOperator.MoreThanOrEqual);
            }
            if (this.dtEnd.Text.Length > 0)
            {
                condition.AddCondition("h.CreateDate", this.dtEnd.DateTime, SqlOperator.LessThanOrEqual);
            }
            string filter = condition.BuildConditionSql();

            DataTable dt = BLLFactory<PurchaseHeader>.Instance.GetPurchaseReport(filter);
            DataTable dtNew = new DataTable();
            foreach (DataColumn col in dt.Columns)
            {
                dtNew.Columns.Add(col.ColumnName, col.DataType);
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                if (!dict.ContainsKey(row["ID"].ToString()))
                {
                    dtNew.ImportRow(row);
                    dict.Add(row["ID"].ToString(), row["ID"].ToString());
                }
            }

            this.winGridView1.DataSource = dtNew.DefaultView;
            this.winGridView1.PrintTitle = this.AppInfo.AppUnit + " -- " + "入库单查询报表";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtWareHouse.Text == "")
            {
                MessageDxUtil.ShowTips("请选择仓库");
                this.txtWareHouse.Focus();
                return;
            }

            FrmAddPurchaseItem dlg = new FrmAddPurchaseItem();
            dlg.WareHourse = this.txtWareHouse.Text;
            dlg.HandNumber = this.txtHandNo.Text;
            dlg.Text = "备件入库";
            dlg.lblStockQuantity.Text = "入库数量";
            dlg.IsPurchase = true;
            dlg.InitFunction(this.LoginUserInfo, this.FunctionDict);//初始化权限控制信息

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int i = 1;
                foreach (PurchaseDetailInfo info in dlg.detailDict.Values)
                {
                    //ItemNo,ItemName,MapNo,Specification,Material,
                    //ItemBigType,ItemType,Unit,Price,Quantity,Amount,Source,StoragePos,UsagePos
                    ListViewItem item = new ListViewItem(info.ItemNo);
                    item.SubItems.Add(info.ItemName);
                    item.SubItems.Add(Math.Abs(info.Quantity).ToString());
                    item.SubItems.Add(info.ItemBigType);
                    item.SubItems.Add(info.ItemType);
                    item.SubItems.Add(info.MapNo);
                    item.SubItems.Add(info.Specification);
                    item.SubItems.Add(info.Unit);
                    item.SubItems.Add(Math.Abs(info.Price).ToString("C2"));
                    item.SubItems.Add(Math.Abs(info.Amount).ToString("C2"));
                    item.SubItems.Add(info.Material);
                    item.SubItems.Add(info.Source);
                    item.SubItems.Add(info.StoragePos);
                    item.SubItems.Add(info.UsagePos);
                    item.SubItems.Add(info.WareHouse);
                    item.SubItems.Add(info.Dept);

                    item.Tag = info;

                    this.lvwDetail.Items.Add(item);
                    i++;
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lvwDetail.SelectedItems.Count == 0) return;
            for (int i = this.lvwDetail.SelectedItems.Count - 1; i >= 0; i--)
            {
                this.lvwDetail.Items.RemoveAt(i);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 验证输入

            //检查是否可以入库出库
            for (int i = 0; i < this.lvwDetail.Items.Count; i++)
            {
                PurchaseDetailInfo detailInfo = this.lvwDetail.Items[i].Tag as PurchaseDetailInfo;
                if (detailInfo != null)
                {
                    bool isInit = BLLFactory<Stock>.Instance.CheckIsInitedWareHouse(this.txtWareHouse.Text, detailInfo.ItemNo);
                    if (!isInit)
                    {
                        if (MessageDxUtil.ShowYesNoAndTips(string.Format("备件项目【{0}】在库房【{1}】还没有期初建账，您是否要进行期初建账，初始化库存为0？\r\n按【是】初始化库房并继续，按【否】退出。",
                            detailInfo.ItemNo, this.txtWareHouse.Text)) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            ItemDetailInfo itemInfo = new ItemDetailInfo();
                            itemInfo.ItemNo = detailInfo.ItemNo;
                            itemInfo.ItemName = detailInfo.ItemName;
                            itemInfo.ItemBigType = detailInfo.ItemBigType;
                            itemInfo.ItemType = detailInfo.ItemType;

                            BLLFactory<Stock>.Instance.InitStockQuantity(itemInfo, 0, this.txtWareHouse.Text);
                        }
                    }
                }
            }
 
            if (this.txtHandNo.Text.Trim() == "")
            {
                MessageDxUtil.ShowTips("货单编号不能为空");
                this.txtHandNo.Focus();
                return;
            }
            else if (this.txtManufacture.Text.Trim() == "")
            {
                MessageDxUtil.ShowTips("请选择供应商");
                this.txtManufacture.Focus();
                return;
            }
            else if (this.txtWareHouse.Text.Trim() == "")
            {
                MessageDxUtil.ShowTips("请选择仓库");
                this.txtWareHouse.Focus();
                return;
            }
            else if (this.lvwDetail.Items.Count == 0)
            {
                MessageDxUtil.ShowTips("请添加商品");
                return;
            }
            else if (this.txtCreator.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请选择经手人");
                this.txtCreator.Focus();
                return;
            }
            #endregion

            try
            {
                PurchaseHeaderInfo headInfo = new PurchaseHeaderInfo();
                headInfo.CreateDate = txtCreateDate.DateTime;
                headInfo.Creator = this.txtCreator.Text;
                headInfo.HandNo = this.txtHandNo.Text;
                headInfo.Manufacture = this.txtManufacture.Text;
                headInfo.Note = this.txtNote.Text;
                headInfo.OperationType = "入库";
                headInfo.WareHouse = this.txtWareHouse.Text;
                headInfo.CreateYear = DateTime.Now.Year;
                headInfo.CreateMonth = DateTime.Now.Month;

                int headId = BLLFactory<PurchaseHeader>.Instance.Insert2(headInfo);
                if (headId > 0)
                {
                    for (int i = 0; i < this.lvwDetail.Items.Count; i++)
                    {
                        PurchaseDetailInfo detailInfo = this.lvwDetail.Items[i].Tag as PurchaseDetailInfo;
                        if (detailInfo != null)
                        {
                            detailInfo.PurchaseHead_ID = headId;
                            BLLFactory<PurchaseDetail>.Instance.Insert(detailInfo);
                            AddStockQuantity(detailInfo);//增加库存
                        }
                    }

                    MessageDxUtil.ShowTips("保存成功");
                    ClearContent();

                    //超库存预警检查
                    bool highWarning = BLLFactory<Stock>.Instance.CheckStockHighWarning(this.txtWareHouse.Text);
                    if (highWarning)
                    {
                        string message = string.Format("{0} 库存量已经高过超预警库存量\r\n请注意减少库存积压", this.txtWareHouse.Text);
                        WareHouseHelper.Notify(string.Format("{0} 超库存预警", this.txtWareHouse.Text), message);
                    }
                }
                else
                {
                    MessageDxUtil.ShowError("保存失败");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPurchase));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void AddStockQuantity(PurchaseDetailInfo detailInfo)
        {
            //先更新库存的价格为加权价格
            StockInfo stockInfo = BLLFactory<Stock>.Instance.FindByItemNo(detailInfo.ItemNo, this.txtWareHouse.Text);
            if (stockInfo != null)
            {
                int oldQuantity = stockInfo.StockQuantity;
                decimal oldPrice = 0M;
                ItemDetailInfo info = BLLFactory<ItemDetail>.Instance.FindByItemNo(detailInfo.ItemNo);
                if (info != null)
                {
                    oldPrice = info.Price;
                    decimal newPrice = ((Convert.ToInt32(detailInfo.Quantity) * detailInfo.Price) + (oldQuantity * oldPrice)) / (Convert.ToInt32(detailInfo.Quantity) + oldQuantity);
                    
                    info.Price = newPrice;
                    BLLFactory<ItemDetail>.Instance.Update(info, info.ID.ToString());
                }
            }

            BLLFactory<Stock>.Instance.AddStockQuantiy(detailInfo.ItemNo, detailInfo.ItemName, 
                Convert.ToInt32(detailInfo.Quantity), this.txtWareHouse.Text);
        }

        private void ClearContent()
        {
            this.txtHandNo.Text = BLLFactory<PurchaseHeader>.Instance.GetHandNumber(true);//进货单号
            this.lvwDetail.Items.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void txtGoodsType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
            }
        }

        private void menu_InRefresh_Click(object sender, EventArgs e)
        {
        }

        private void menu_Delete_Click(object sender, EventArgs e)
        {
            btnDelete_Click(null, null);
        }

        private void btnModifyExpired_Click(object sender, EventArgs e)
        {
            if (this.lvwDetail.SelectedItems.Count == 0) return;

            PurchaseDetailInfo info = this.lvwDetail.SelectedItems[0].Tag as PurchaseDetailInfo;
            if (info != null)
            {
            }
            
        }

        private void txtSearchWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void menu_DetailRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnExportBill_Click(object sender, EventArgs e)
        {
            #region 准备数据
            string columns = @"流水号,备注,供货商,操作员,库房名称,备件编号（pm码）,备件名称,图号,规格型号,材质,备件属类,备件类别,单位,最新单价（元）,入库数量,总价,入库日期,来源,库位,部门,使用位置";
            DataTable dt = DataTableHelper.CreateTable(columns);
            DataRow row = null;
            for (int i = 0; i < this.lvwDetail.Items.Count; i++)
            {
                PurchaseDetailInfo info = this.lvwDetail.Items[i].Tag as PurchaseDetailInfo;
                if (info != null)
                {
                    row = dt.NewRow();
                    row["流水号"] = this.txtHandNo.Text;
                    row["备注"] = this.txtNote.Text;
                    row["供货商"] = this.txtManufacture.Text;
                    row["操作员"] = this.txtCreator.Text;
                    row["入库日期"] = this.txtCreateDate.DateTime.ToString();
                    row["库房名称"] = info.WareHouse;
                    row["备件编号（pm码）"] = info.ItemNo;
                    row["备件名称"] = info.ItemName;
                    row["图号"] = info.MapNo;
                    row["规格型号"] = info.Specification;
                    row["材质"] = info.Material;
                    row["备件属类"] = info.ItemBigType;
                    row["备件类别"] = info.ItemType;
                    row["单位"] = info.Unit;
                    row["最新单价（元）"] = info.Price.ToString("C2");
                    row["入库数量"] = info.Quantity.ToString();
                    row["总价"] = info.Amount.ToString("C2");
                    row["来源"] = info.Source;
                    row["库位"] = info.StoragePos;
                    row["部门"] = info.Dept;
                    row["使用位置"] = info.UsagePos;
                    dt.Rows.Add(row);
                }
            } 
            #endregion

            #region 导出数据
            try
            {
                SpecialDirectories sp = new SpecialDirectories();
                string fileName = FileDialogHelper.SaveExcel(string.Format("入库单({0})", DateTime.Now.ToString("yyyy-MM-dd")), sp.Desktop);
                if (string.IsNullOrEmpty(fileName))
                {
                    return;
                }
                string outError = "";
                AsposeExcelTools.DataTableToExcel2(dt, fileName, out outError);
                if (!string.IsNullOrEmpty(outError))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, outError, typeof(FrmPurchase));
                    MessageDxUtil.ShowError(outError);
                }
                else
                {
                    Process.Start(fileName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPurchase));
                MessageDxUtil.ShowError(ex.Message);
            } 
            #endregion
        }

        private void menu_ModifyStockPos_Click(object sender, EventArgs e)
        {
            if (this.lvwDetail.SelectedItems.Count == 0) return;

            FrmSetStoragePos dlg = new FrmSetStoragePos();
            dlg.txtNewStoragePos.Text = "";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < this.lvwDetail.SelectedItems.Count; i++)
                {
                    PurchaseDetailInfo info = this.lvwDetail.Items[i].Tag as PurchaseDetailInfo;
                    if (info != null)
                    {
                        this.lvwDetail.SelectedItems[i].SubItems[12].Text = dlg.txtNewStoragePos.Text;
                        info.StoragePos = dlg.txtNewStoragePos.Text;
                        this.lvwDetail.Items[i].Tag = info;
                    }
                }
            }
        }

        private void btnExportDetail_Click(object sender, EventArgs e)
        {
            if (this.winGridView1.gridView1.RowCount == 0)
                return;

            #region 构造数据列表
            //HandNo,ItemNo,ItemName,MapNo,Specification,Material,ItemBigType,ItemType,Unit,Price,Quantity,Amount,Source,StoragePos,UsagePos,d.WareHouse,d.Dept
            string columns = "货单号,项目编号,项目名称,图号,规格型号,材质,备件属类,备件类别,单位,单价|decimal,数量|int,金额|decimal,来源,库位,使用位置,库房,部门";
            DataTable dtDetail = DataTableHelper.CreateTable(columns);

            DataGridViewRow row;
            for (int i = 0; i < winGridView1.gridView1.RowCount; i++)
            {
                string ID = this.winGridView1.gridView1.GetRowCellDisplayText(i, "ID");
                if (!string.IsNullOrEmpty(ID))
                {
                    DataTable dt = BLLFactory<PurchaseDetail>.Instance.GetPurchaseDetailReportByID(Convert.ToInt32(ID));
                    dt.Rows.Add(dt.NewRow());

                    //复制到中文列的表中
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        DataRow r = dtDetail.NewRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            r[j] = dt.Rows[k][j];
                        }
                        dtDetail.Rows.Add(r);
                    }
                }
            }
            #endregion

            #region 导出数据操作
            SpecialDirectories sp = new SpecialDirectories();
            string fileToSave = FileDialogHelper.SaveExcel(string.Format("入库单明细({0})", DateTime.Now.ToString("yyyy-MM-dd")), sp.Desktop);
            if (string.IsNullOrEmpty(fileToSave))
            {
                return;
            }

            try
            {
                string fileName = fileToSave;
                string outError = "";
                AsposeExcelTools.DataTableToExcel2(dtDetail, fileName, out outError);
                if (!string.IsNullOrEmpty(outError))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, outError, typeof(FrmPurchase));
                    MessageDxUtil.ShowError(outError);
                }
                else
                {
                    Process.Start(fileName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPurchase));
                MessageDxUtil.ShowError(ex.Message);
            }
            #endregion
        }

        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            PurchaseHeaderInfo headInfo = new PurchaseHeaderInfo();
            headInfo.CreateDate = txtCreateDate.DateTime;
            headInfo.Creator = this.txtCreator.Text;
            headInfo.HandNo = this.txtHandNo.Text;
            headInfo.Manufacture = this.txtManufacture.Text;
            headInfo.Note = this.txtNote.Text;
            headInfo.OperationType = "入库";
            headInfo.WareHouse = this.txtWareHouse.Text;
            headInfo.CreateYear = DateTime.Now.Year;
            headInfo.CreateMonth = DateTime.Now.Month;

            List<PurchaseDetailInfo> detailList = new List<PurchaseDetailInfo>();
            for (int i = 0; i < this.lvwDetail.Items.Count; i++)
            {
                PurchaseDetailInfo detailInfo = this.lvwDetail.Items[i].Tag as PurchaseDetailInfo;
                if (detailInfo != null)
                {
                    StockInfo stockInfo = BLLFactory<Stock>.Instance.FindByItemNo(detailInfo.ItemNo, this.txtWareHouse.Text);
                    if (stockInfo != null)
                    {
                        int oldQuantity = stockInfo.StockQuantity;
                        decimal oldPrice = 0M;
                        ItemDetailInfo info = BLLFactory<ItemDetail>.Instance.FindByItemNo(detailInfo.ItemNo);
                        if (info != null)
                        {
                            oldPrice = info.Price;
                            decimal newPrice = ((Convert.ToInt32(detailInfo.Quantity) * detailInfo.Price) + (oldQuantity * oldPrice)) / (Convert.ToInt32(detailInfo.Quantity) + oldQuantity);

                            detailInfo.Price = newPrice;
                        }
                    }

                    detailList.Add(detailInfo);
                }
            }

            ReportViewerDialog dlg = new ReportViewerDialog();
            dlg.DataSourceDict.Add("PurchaseHeaderInfo", new List<PurchaseHeaderInfo>() { headInfo });
            dlg.DataSourceDict.Add("PurchaseDetailInfo", detailList);
            dlg.ReportFilePath = "Report/WHC.WareHouseMis.PurchaseReport.rdlc";

            dlg.Parameters.Add("CompanyName", this.AppInfo.AppUnit);
            dlg.ShowDialog();
        }

    }
}
